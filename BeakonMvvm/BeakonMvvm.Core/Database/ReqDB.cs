using BeakonMvvm.Core.Interfaces;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using MvvmCross.Platform;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

// Author Gurpreet Dhaliwal
namespace BeakonMvvm.Core.Database
{

    public class ReqDB : IReqDB
    {
        private MobileServiceClient azureDatabase;
        private IMobileServiceSyncTable<Req> azureSyncTable;
        public ReqDB()
        {
            azureDatabase = Mvx.Resolve<IAzureDatabase>().GetMobileServiceClient();
            azureSyncTable = azureDatabase.GetSyncTable<Req>();
        }

        public async Task<bool> CheckIfExists(Req location)
        {
            await SyncAsync(true);
            var locations = await azureSyncTable.Where(x => x.Id == location.Id).ToListAsync();
            return locations.Any();
        }

        public async Task<int> DeleteReq(object id)
        {
            await SyncAsync(true);

            var location = await azureSyncTable.Where(x => x.Id == (string)id).ToListAsync();
            if (location.Any())
            {
                await azureSyncTable.DeleteAsync(location.FirstOrDefault());
                await SyncAsync(false);
                return 1;
            }
            else
            {
                return 0;

            }
        }

        public async Task<List<Req>> GetReq(string name)
        {
            await SyncAsync(true);
            var location = await azureSyncTable.Where(x => x.ReqTo == (string)name).ToListAsync();
            return location;

        }
 
        public async Task<int> InsertReq(Req p)
        {
            await SyncAsync(true);
            await azureSyncTable.InsertAsync(p);
            await SyncAsync(false);
            return 1;


        }
        private async Task SyncAsync(bool pullData)
        {

            try
            {
               
                await azureDatabase.SyncContext.PushAsync();
                if (pullData == true)
                {
                   await azureSyncTable.PullAsync("allReqs", azureSyncTable.CreateQuery()); // query ID is used for incremental sync
                }
            }

            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

    }
}

