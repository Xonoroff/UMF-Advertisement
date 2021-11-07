using AdsModule.src.Infrastructure.Messaging.RequestResponse.ShowVideoRequest;
using Core.src.Infrastructure;
using Core.src.Messaging;
using Core.src.Utils;
using MF.Advertisement.src.AdsEditorMock.Commands;
using MF.Advertisement.src.Infrastructure.Messaging.RequestResponse.PreloadAds;
using MF.Advertisement.src.IronSource.ViewManagers;
using Zenject;

namespace MF.Advertisement.src.AdsEditorMock.Installers
{
    public class AdsEditorMockGlobalInstaller : GlobalInstallerBase<AdsEditorMockGlobalInstaller, AdsEditorMockInstaller>
    {
        [Inject]
        private IEventBus eventBus;
        
        protected override string SubContainerName { get; } = "AdsEditorMockSubContainer";

        public override void InstallBindings()
        {
            eventBus.DeclareSignal<ShowAdsRequest>();
            eventBus.DeclareSignal<PreloadAdsRequest>();

            Container.Bind<ICommand>()
                .WithId("preloading_command")
                .To<AdsEditorMockInitializeCommand>()
                .FromSubContainerResolve()
                .ByInstanceGetter(SubContainerInstanceGetter)
                .AsTransient();
        
            base.InstallBindings();
        }
    }
}