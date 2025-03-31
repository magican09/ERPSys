namespace DictationaryDataService.gRPC.Infrastructure.Services;

public interface IIdentityService
{
    
    string? GetUserIdentity();
    string GetUserName();
}