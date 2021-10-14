using AdsModule.src.Infrastructure.Messaging.RequestResponse.ShowVideoRequest;
using Core.src.Infrastructure;
using Core.src.Messaging;
using Core.src.Utils;
using MF.Advertisement.src.Infrastructure.Messaging.RequestResponse.PreloadAds;
using MF.Advertisement.src.IronSource.Commands;
using MF.Advertisement.src.IronSource.Installers;
using Zenject;

public class IronSourceModuleGlobalInstaller : GlobalInstallerBase<IronSourceModuleGlobalInstaller, IronSourceModuleInstaller>
{
    [Inject]
    private IEventBus eventBus;
    
    protected override string SubContainerName { get; } = "IronSourceAdsContainer";

    public override void InstallBindings()
    {
        eventBus.DeclareSignal<ShowAdsRequest>();
        eventBus.DeclareSignal<PreloadAdsRequest>();

        Container.Bind<ICommand>()
            .WithId("preloading_command")
            .To<IronSourceInitializeCommand>()
            .FromSubContainerResolve()
            .ByInstanceGetter(SubContainerInstanceGetter)
            .AsTransient();
        
        base.InstallBindings();
    }
}
