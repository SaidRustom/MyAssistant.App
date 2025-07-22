using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyAssistant.Core.Contracts.Persistence.Service;
using MyAssistant.Domain.Base;
using MyAssistant.Domain.Interfaces;
using MyAssistant.Domain.Lookups;

namespace MyAssistant.Core.Services
{
    public class RecurringShoppingListItemActivationService : BackgroundService, IMyAssistantService
    {
        public int ServiceTypeCode => MyAssistantServiceType.RecurringShoppingListItemActivationService;
        private readonly IServiceProvider _serviceProvider;

        public RecurringShoppingListItemActivationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await ProcessRecurringItemsAsync(stoppingToken);

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }

        private async Task ProcessRecurringItemsAsync(CancellationToken cancellationToken)
        {
            try
            {
                using (var scope = _serviceProvider.CreateAsyncScope())
                {
                    var repo = scope.ServiceProvider.GetRequiredService<IRecurringShoppingListItemActivationRepository>();
                    //var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    MyAssistantServiceLog serviceLog = new MyAssistantServiceLog(MyAssistantServiceTypeList.Get(ServiceTypeCode));
                
                    await repo.LogServiceStartedAsync(serviceLog);
                
                    var items = await repo.GetItemsAsync();
                
                    if (items.Count > 0)
                    {
                        foreach (var item in items)
                        {
                            item.IsActive = true;
                            item.NextOccurrenceDate = null;
                            //await mediator.Send(new HandleNotificationsCommand<ShoppingList>(item.ShoppingListId));
                        }

                        await repo.UpdateItems(items);
                    }

                    serviceLog.ResultDescription = $"Activated {items.Count} Items";
                    await repo.LogServiceEndedAsync(serviceLog);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //TODO: Log the exception
            }
            
        }
    }
}
