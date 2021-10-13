#if UNITY_ADS_ENABLED
using AdsModule.src.Infrastructure.Messaging.RequestResponse.CheckVideo;
using AdsModule.src.Infrastructure.Messaging.RequestResponse.ShowVideoRequest;
using Core.src.Utils;
using Zenject;

namespace AdsModule.src.Feature.Installers
{
    public class UnityAdsModuleGlobalInstaller : GlobalInstallerBase<UnityAdsModuleGlobalInstaller, UnityAdsModuleInstaller>
    {
        [Inject]
        private SignalBus signalBus;

        protected override string SubContainerName { get; } = "UnityAdsModule";

        public override void InstallBindings()
        {
            signalBus.DeclareSignal<ShowAdsVideoRequest>();
            signalBus.DeclareSignal<CheckAdsVideoRequest>();
            
            base.InstallBindings();
        }
    }
}
#endif