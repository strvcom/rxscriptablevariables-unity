using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.RemoteConfig;
using STRV.Variables;
using UniRx;
using UnityEngine;

namespace Variables.Source.Utils
{
    public static class FirebaseInit
    {
        public static bool IsInitialized;
        public static FirebaseApp App;
        
        private static bool _remoteConfigFetched;
        private static bool _eventsSubscribed;
        private static bool _environmentSet;
        private static IDisposable _remoteConfigFetch;

        public static void Initialize()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available) 
                {
                    // Create and hold a reference to your FirebaseApp, i.e.
                    //   app = Firebase.FirebaseApp.DefaultInstance;
                    // where app is a Firebase.FirebaseApp property of your application class.

                    // Set a flag here indicating that Firebase is ready to use by your
                    // application.
                    #if UNITY_EDITOR
                        var ops = new AppOptions();
                        App = FirebaseApp.Create(ops, "EditorOnly");
                    #else
                        App = FirebaseApp.DefaultInstance;
                    #endif
                    
                    IsInitialized = true;
                    
                    Debug.Log("Firebase initialized");
                } else {
                    Debug.LogErrorFormat("Could not resolve all Firebase dependencies: {0}", dependencyStatus);
                    // Firebase Unity SDK is not safe to use here.
                    IsInitialized = false;
                }
            });

            if (!_eventsSubscribed)
            {
                _eventsSubscribed = true;
                
                _remoteConfigFetch = 
                    Observable.Interval(TimeSpan.FromSeconds(10f))
                        .Merge(Observable.Timer(TimeSpan.FromSeconds(2f)))
                        .StartWith(0)
                        .Subscribe(_ => { InitializeRemoteSettings(); });
            
#if !UNITY_EDITOR
                EventDispatcher.Receive<ApplicationFocusEvent>()
                    .Where(e => e.HasFocus)
                    .Subscribe(_ => { UpdateRemoteSettings(); });
#endif
            }
        }

        private static void InitializeRemoteSettings()
        {
            if (_remoteConfigFetched)
            {
                return;
            }

            if (Debug.isDebugBuild)
            {
                SetUserProperties();
                Observable.Timer(TimeSpan.FromSeconds(1.5f))
                    .Take(1)
                    .Subscribe(_ => UpdateRemoteSettings());
            }
            else
            {
                SetUserProperties();
                UpdateRemoteSettings();    
            }
        }

        private static void SetUserProperties()
        {
            if (IsInitialized && !_environmentSet)
            {
                _environmentSet = true;
                var environment = Debug.isDebugBuild ? "development" : "release";
                Firebase.Analytics.FirebaseAnalytics.SetUserProperty("build", environment);
            }
        }
        
        private static void UpdateRemoteSettings()
        {
            SetUserProperties();
            if (!IsInitialized)
            {
                return;
            }
            
            Task fetchTask;
            if (Debug.isDebugBuild)
            {
                // Default interval is 12 hours, let's make it zero for the purpose of testing
                fetchTask = FirebaseRemoteConfig.FetchAsync(TimeSpan.Zero);
                
                var settings = FirebaseRemoteConfig.Settings;
                settings.IsDeveloperMode = true;
                FirebaseRemoteConfig.Settings = settings;
            }
            else
            {
                fetchTask = FirebaseRemoteConfig.FetchAsync();
            }

            PerformRemoteSettingsFetch(fetchTask);
        }

        private static void PerformRemoteSettingsFetch(Task fetchTask)
        {
            Debug.Log("<color=#ffa500ff>Remote Config</color> - Request fetch");
            
            fetchTask
                .ContinueWith(task =>
                {
                    if (!task.IsCompleted)
                    {
                        Debug.LogError("<color=#ffa500ff>Remote Config</color> - Fetch task cancelled or failed");
                        return;
                    }
                    
                    var info = FirebaseRemoteConfig.Info;
                    if (info.LastFetchStatus != LastFetchStatus.Success)
                    {
                        Debug.LogError("<color=#ffa500ff>Remote Config</color> - Fetch failed or is still pending");
                        return;
                    }
                    
                    Debug.Log("<color=#ffa500ff>Remote Config</color> - Fetched");
                    FirebaseRemoteConfig.ActivateFetched();
                    Debug.Log("<color=#ffa500ff>Remote Config</color> - Fetched activated");
                })
                .ContinueWith(task =>
                {
                    if (!task.IsCompleted)
                    {
                        Debug.LogError("<color=#ffa500ff>Remote Config</color> - Fetch task cancelled or failed");
                        return;
                    }
                    VariablesUpdatedHandler.VariablesUpdated();

                    Debug.Log("<color=#ffa500ff>Remote Config</color> - Variables updated");
                    _remoteConfigFetched = true;
                    _remoteConfigFetch.Dispose();   
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}