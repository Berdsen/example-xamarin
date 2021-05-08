using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Prism;
using Prism.Ioc;

namespace SharpBrick.PoweredUp.Mobile.Examples.iOS
{
    public class IosInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IPermissions>(CrossPermissions.Current);
            containerRegistry.RegisterInstance<INativeDeviceInfoProvider>(NativeDeviceInfoProvider.Current);
        }
    }
}