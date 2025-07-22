using MyAssistant.Domain.Base;

namespace MyAssistant.Core.Contracts.Persistence.Service;

public interface IMyAssistantServiceReporsitory
{
    Task<MyAssistantServiceLog> LogServiceStartedAsync(MyAssistantServiceLog entity);

    Task LogServiceEndedAsync(MyAssistantServiceLog entity);
}
