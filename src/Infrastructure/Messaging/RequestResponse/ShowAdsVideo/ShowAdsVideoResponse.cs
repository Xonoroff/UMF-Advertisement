using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdsModule.src.Infrastructure.Messaging.RequestResponse.ShowVideoRequest
{
    public class ShowAdsVideoResponse
    {
        public string Data { get; set; }
        public AdsVideoResult VideoState { get; set; }
    }

    public enum AdsVideoResult
    {    
        WasNotReady,
        Shown,
        Skipped,
        Failed,
        Error,
    }
}