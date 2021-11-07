using Core.src.Infrastructure;
using MF.Advertisement.src.Infrastructure.ViewManagers;
using MF.Advertisement.src.IronSource.Commands;
using MF.Advertisement.src.IronSource.Managers;
using MF.Advertisement.src.IronSource.ViewManagers;
using Zenject;

namespace MF.Advertisement.src.IronSource.Installers
{
    public class IronSourceModuleInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<IAdsViewManager>().To<IronSourceViewManager>().AsCached();
            Container.Bind<IInterstitialViewManager>().To<IronSourceInterstitialAdsViewManager>().AsCached();

            Container.Bind<IIronSourceManager>().To<IronSourceManager>().AsTransient();

            Container.Bind<IronSourceInitializeCommand>().AsTransient();
            
            IronSourceSignalsInstaller.Install(Container);
        }
    }
}