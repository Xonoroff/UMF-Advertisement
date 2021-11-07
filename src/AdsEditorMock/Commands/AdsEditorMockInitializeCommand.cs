using System;
using Core.src.Infrastructure;
using Cysharp.Threading.Tasks;
using MF.Advertisement.src.Infrastructure.ViewManagers;

namespace MF.Advertisement.src.AdsEditorMock.Commands
{
    public class AdsEditorMockInitializeCommand : ICommand
    {
        private IAdsViewManager adsViewManager;

        public AdsEditorMockInitializeCommand(IAdsViewManager adsViewManager)
        {
            this.adsViewManager = adsViewManager;
        }
        
        public void Execute()
        {
            AsyncWrapper().Forget();
        }

        public async UniTaskVoid AsyncWrapper()
        {
            if (!adsViewManager.IsInitialized)
            {
                await adsViewManager.Initialize();
            }
            
            OnSuccess?.Invoke();
        }

        public void Undo()
        {
        }

        public bool IsAvailable()
        {
            return true;
        }

        public string Description { get; set; } = "Initializing ADS";

        public int Priority { get; } = 800;
        
        public Action OnSuccess { get; set; }
        
        public Action<float> OnProgressChanged { get; set; }
        
        public Action<Exception> OnFail { get; set; }
    }
}