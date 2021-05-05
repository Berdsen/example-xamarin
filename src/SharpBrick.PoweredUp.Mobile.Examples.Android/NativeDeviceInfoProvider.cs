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
                    DeviceIdentifier = btDevice.Address
                };
            }

            return null;
        }
    }
}