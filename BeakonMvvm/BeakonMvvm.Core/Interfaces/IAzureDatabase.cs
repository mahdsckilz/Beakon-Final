using Microsoft.WindowsAzure.MobileServices;
namespace BeakonMvvm.Core.Interfaces
{
    public interface IAzureDatabase
    {
        MobileServiceClient GetMobileServiceClient();
    }
}
