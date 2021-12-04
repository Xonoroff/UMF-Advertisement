using UnityEngine;
namespace MF.Advertisement.src.IronSource.View
{
    public class IronSourceContainer : MonoBehaviour
    {
        private void OnApplicationPause(bool isPaused)
        {
            global::IronSource.Agent.onApplicationPause(isPaused);
        }
    }
}
