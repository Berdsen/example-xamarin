using CoreBluetooth;
using Foundation;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Prism;
using Prism.Ioc;
using UIKit;

namespace SharpBrick.PoweredUp.Mobile.Examples.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            
            LoadApplication(ApplicationWrapper.Application);

            return base.FinishedLaunching(app, options);
        }
    }

    public static class ApplicationWrapper
    {
        private static App instance;

        public static App Application
        {
            get
            {
                return instance ??= new App(new IosInitializer());
            }
        }
    }

    public class IosInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IPermissions>(CrossPermissions.Current);
            containerRegistry.RegisterInstance<INativeDeviceInfoProvider>(NativeDeviceInfoProvider.Current);
        }
    }

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
            // don't know for know
            
            if (deviceInfoObject is CBPeripheral peripheral)
            {
                return new NativeDeviceInfo()
                {
                    DeviceIdentifier = peripheral.Identifier.ToString(),
                    MacAddress = new byte[6] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 },
                    MacAddressString = "00:00:00:00:00:00",
                    MacAddressNumeric = counter++
                };
            }
            

            if (deviceInfoObject is ulong deviceAddress)
            {
                string address = $"000000000000{deviceAddress:X}";
                address = address.Substring(address.Length - 12);
                return new NativeDeviceInfo()
                {
                    // MacAddress = string.Join(":", Enumerable.Range(0, 6).Select(i => address.Substring(i * 2, 2))),
                    MacAddress = new byte[6] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 },
                    MacAddressString = "00:00:00:00:00:00",
                    MacAddressNumeric = deviceAddress
                };

            }

            return null;
        }
    }
}
