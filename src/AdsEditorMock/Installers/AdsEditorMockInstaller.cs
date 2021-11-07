using System.Collections;
using System.Collections.Generic;
using Core.src.Infrastructure;
using MF.Advertisement.src.AdsEditorMock.Commands;
using MF.Advertisement.src.AdsEditorMock.Installers;
using MF.Advertisement.src.Infrastructure.ViewManagers;
using MF.Advertisement.src.IronSource.ViewManagers;
using UnityEngine;
using Zenject;

public class AdsEditorMockInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.Bind<AdsEditorMockInitializeCommand>().AsTransient();
        
        Container.Bind<IAdsViewManager>().To<AdsEditorMockViewManager>().FromNewComponentOnNewGameObject().AsCached();
        
        AdsEditorMockSignalsInstaller.Install(Container);
    }
}
