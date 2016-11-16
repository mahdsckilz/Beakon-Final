using Android.App;
using Android.Content;
using Android.Net.Wifi;
using BeakonMvvm.Core.Interfaces;

namespace BeakonMvvm.Droid.Services
{
    public class Network : INetwork
    {
        public string SSID()
        {
            var wifiManager = (WifiManager)Application.Context.GetSystemService(Context.WifiService);

            string ssId = wifiManager.ConnectionInfo.SSID;

            return ssId;
        }
    }
}