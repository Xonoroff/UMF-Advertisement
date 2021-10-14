using UnityEngine;
using IronSourceRaw = global::IronSource.Scripts.IronSource;
namespace MF.Advertisement.src.IronSource.View
{
    public class IronSourceContainer : MonoBehaviour
    {
        private void OnApplicationPause(bool isPaused)
        {
            IronSourceRaw.Agent.onApplicationPause(isPaused);
        }
    }
}
