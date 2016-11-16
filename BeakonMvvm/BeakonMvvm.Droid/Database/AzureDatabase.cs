using Microsoft.WindowsAzure.MobileServices;
using System.IO;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using BeakonMvvm.Core;
using BeakonMvvm.Core.Interfaces;

namespace BeakonMvvm.Droid.Database
{
    public class AzureDatabase : IAzureDatabase
    {
        MobileServiceClient azureDatabase;
        public MobileServiceClient GetMobileServiceClient()
        {
            CurrentPlatform.Init();

            azureDatabase = new MobileServiceClient("https://beakon.azurewebsites.net/");

            InitializeLocal();
            return azureDatabase;
        }

        private void InitializeLocal()
        {
            var sqliteFilename = "SQLite.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }
            var store = new MobileServiceSQLiteStore(path);
            store.DefineTable<Perso>();
            store.DefineTable<Answ>();
            store.DefineTable<Req>();
            azureDatabase.SyncContext.InitializeAsync(store);
        }
    }

}