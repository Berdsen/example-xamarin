using CoreBluetooth;

namespace SharpBrick.PoweredUp.Mobile.Examples.iOS
{
    public class NativeDeviceInfoProvider : INativeDeviceInfoProvider
    {
        private static NativeDeviceInfoProvider _instance;

        private ulong counter = 0;

        public static NativeDeviceInfoProvider Current
        {
            get { return _instance ??= new NativeDeviceInfoProvider(); }
        }

        public NativeDeviceInfo GetNativeDeviceInfo(object deviceInfoObject)
        {
            if (deviceInfoObject is CBPeripheral peripheral)
            {
                return new NativeDeviceInfo()
                {
                    DeviceIdentifier = peripheral.Identifier.ToString(),
                };
            }
            
            return null;
        }
    }
}