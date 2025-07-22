using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyAssistant.Core.Contracts.Persistence.Service;

namespace MyAssistant.Core.Services
{
    public class RecurringShoppingListItemActivationService : BackgroundService
    {
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
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IRecurringShoppingListItemActivationRepository>();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

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
            }
        }
    }
}
