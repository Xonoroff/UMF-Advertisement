#if UNITY_ADS_ENABLED
using AdsModule.src.Feature.Managers;
using Zenject;

namespace AdsModule.src.Feature.Installers
{
    public class UnityAdsModuleInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<IUnityAdsViewManager>().To<UnityAdsViewManager>().AsCached().NonLazy();
            Container.Bind<IUnityAdsManager>().To<UnityAdsManager>().AsTransient();
            
            UnityAdsModuleSignalsInstaller.Install(Container);
        }
    }
}
#endif