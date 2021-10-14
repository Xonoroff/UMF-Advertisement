using System;
using Core.src.Infrastructure;
using MF.Advertisement.src.Infrastructure.ViewManagers;
using Zenject;

namespace MF.Advertisement.src.IronSource.Commands
{
    public class IronSourceInitializeCommand : ICommand
    {
        private IAdsViewManager adsViewManager;

        public IronSourceInitializeCommand(IAdsViewManager adsViewManager)
        {
            this.adsViewManager = adsViewManager;
        }
        
        public void Execute()
        {
            if (!adsViewManager.IsInitialized)
            {
                adsViewManager.Initialize();
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
