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

    public class AnsDB : IAnsDB
    {
        private MobileServiceClient azureDatabase;
        private IMobileServiceSyncTable<Answ> azureSyncTable;
        public AnsDB()
        {
            azureDatabase = Mvx.Resolve<IAzureDatabase>().GetMobileServiceClient();
            azureSyncTable = azureDatabase.GetSyncTable<Answ>();
        }

        public async Task<bool> CheckIfExists(Answ location)
        {
            await SyncAsync(true);
            var locations = await azureSyncTable.Where(x => x.Id == location.Id).ToListAsync();
            return locations.Any();
        }

        public async Task<int> DeleteAns(object id)
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

        public async Task<List<Answ>> GetAns(string name)
        {
            await SyncAsync(true);
            var location = await azureSyncTable.Where(x => x.AnsTo == (string)name).ToListAsync();
            return location;

        }

        public async Task<int> InsertAns(string from, string to, string cal, string loc, string extra)
        {
            Answ answer = new Answ
            {
                AnsFrom = from,
                AnsTo = to,
                AnsCal = cal,
                AnsLoc = loc,
                AnsExtra = extra
            };
            await SyncAsync(true);
            await azureSyncTable.InsertAsync(answer);
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
                    await azureSyncTable.PullAsync("allAnsws", azureSyncTable.CreateQuery()); // query ID is used for incremental sync
                }
              
            }

            catch(Exception e)
            {
                Debug.WriteLine(e);
            }
        }

    }

}

