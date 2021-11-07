using System;
using Cysharp.Threading.Tasks;
using MF.Advertisement.src.Infrastructure.Entities;
using MF.Advertisement.src.Infrastructure.ViewManagers;
using UnityEngine;

namespace MF.Advertisement.src.IronSource.ViewManagers
{
    public class AdsEditorMockViewManager : MonoBehaviour, IAdsViewManager
    {
        public bool IsInitialized { get; private set; }

        private Action onGuiWrapper;
        
        void OnGUI()
        {
            if (onGuiWrapper != null)
            {
                onGuiWrapper();
            }
        }

        void DrawDefaultWindow(string title, GUI.WindowFunction func)
        {
            var screenWidth = Screen.width;
            var screenHeight = Screen.height;
            
            Rect windowRect = new Rect(0, 0, screenWidth, screenHeight);

            var windowStyle = new GUIStyle("window")
            {
                fontSize = 64,
            };
                
            GUILayout.Window(0, windowRect, func, title, windowStyle);
            
            GUILayout.Space(100);
        }
        
        public UniTask<bool> Initialize()
        {
            var result = new UniTaskCompletionSource<bool>();
            
            onGuiWrapper = DrawInitializeWindow;
            void DrawInitializeWindow()
            {
                DrawDefaultWindow("Initialize ads mock popup", DrawInitializeContent);
                void DrawInitializeContent(int windowId)
                {
                    GUIStyle customButton = new GUIStyle("button")
                    {
                        fontSize = 64,
                    };
                    
                    GUILayout.Space(100);
                    if (GUILayout.Button("Initialize success", customButton))
                    {
                        onGuiWrapper = null;
                        IsInitialized = true;
                        result.TrySetResult(true);
                    }

                    if (GUILayout.Button("Initialize fail", customButton))
                    {
                        onGuiWrapper = null;
                        IsInitialized = false;
                        result.TrySetResult(false);
                    }
                }
            }

            return result.Task;
        }
        

        public UniTask<AdsResultEntity> ShowAds(AdsContextEntity context)
        {
            if (!IsInitialized)
            {
                Debug.LogError("Can't preload ads before initializing");
                return UniTask.FromResult(new AdsResultEntity()
                {
                    ResultState = AdsResultState.Error,
                    PlacementId = context.PlacementId,
                });
            }

            var result = new UniTaskCompletionSource<AdsResultEntity>();
            
            onGuiWrapper = DrawInitializeWindow;
            void DrawInitializeWindow()
            {
                DrawDefaultWindow("View ads", DrawInitializeContent);
                void DrawInitializeContent(int windowId)
                {
                    GUIStyle customButton = new GUIStyle("button")
                    {
                        fontSize = 64,
                    };
                    
                    GUILayout.Space(100);
                    if (GUILayout.Button("View success", customButton))
                    {
                        onGuiWrapper = null;
                        result.TrySetResult(new AdsResultEntity()
                        {
                            PlacementId = context.PlacementId,
                            ResultState = AdsResultState.Shown,
                        });
                    }

                    if (GUILayout.Button("View fail", customButton))
                    {
                        onGuiWrapper = null;
                        
                        result.TrySetResult(new AdsResultEntity()
                        {
                            PlacementId = context.PlacementId,
                            ResultState = AdsResultState.Failed,
                        });
                    }
                }
            }

            return result.Task;
        }

        public UniTask<bool> PreloadAds(AdsContextEntity context)
        {
            if (!IsInitialized)
            {
                Debug.LogError("Can't preload ads before initializing");
                return UniTask.FromResult(false);
            }

            var result = new UniTaskCompletionSource<bool>();

            onGuiWrapper = DrawInitializeWindow;
            void DrawInitializeWindow()
            {
                DrawDefaultWindow("Preload ads", DrawInitializeContent);
                void DrawInitializeContent(int windowId)
                {
                    GUIStyle customButton = new GUIStyle("button")
                    {
                        fontSize = 64,
                    };
                    
                    GUILayout.Space(100);
                    if (GUILayout.Button("Preload success", customButton))
                    {
                        onGuiWrapper = null;
                        IsInitialized = true;
                        result.TrySetResult(true);
                    }

                    if (GUILayout.Button("Preload fail", customButton))
                    {
                        onGuiWrapper = null;
                        IsInitialized = false;
                        result.TrySetResult(false);
                    }
                }
            }

            return result.Task;
        }
    }
}