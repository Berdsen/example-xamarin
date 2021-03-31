using System;
using System.Linq;

namespace SharpBrick.PoweredUp.Mobile.Examples.Droid
{
    public class NativeDeviceInfoProvider : INativeDeviceInfoProvider
    {
        private static NativeDeviceInfoProvider _instance;
        
        public static NativeDeviceInfoProvider Current
        {
            get { return _instance ??= new NativeDeviceInfoProvider(); }
        }

        public NativeDeviceInfo GetNativeDeviceInfo(object deviceInfoObject)
        {
            if (deviceInfoObject is Android.Bluetooth.BluetoothDevice btDevice && btDevice.Address != null)
            {
                return new NativeDeviceInfo()
                {
                    MacAddress = btDevice.Address,
                    MacAddressNumeric = Convert.ToUInt64(btDevice.Address.Replace(":", ""), 16)
                };
            }

            if (deviceInfoObject is ulong deviceAddress)
            {
                string address = $"000000000000{deviceAddress:X}";
                address = address.Substring(address.Length - 12);
                return new NativeDeviceInfo()
                {
                    MacAddress = string.Join(":", Enumerable.Range(0, 6).Select(i => address.Substring(i * 2, 2))),
                    MacAddressNumeric = deviceAddress
                };

            }

            return null;
        }
    }
}