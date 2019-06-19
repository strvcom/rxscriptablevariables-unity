using System;
using UnityEngine;

#if REACTIVE_VARIABLE_RX_ENABLED
using UniRx;
#endif

// ReSharper disable once CheckNamespace
namespace STRV.Variables.Persistance
{
    /// Purpose of this class is to watch certain events / timers and save data from time to time
    /// Slam this on some game object in the scene to get this funcionality
    public class SaveDataWatcher: MonoBehaviour
    {
        private static SaveDataWatcher _instance;
        
        [SerializeField] private VariablePersistor[] _persistors;
        [SerializeField] private int _autosaveInterval = 30;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
#if REACTIVE_VARIABLE_RX_ENABLED
            Observable.Interval(TimeSpan.FromSeconds(_autosaveInterval)).Subscribe(_ =>
            {
                Save();
            }).AddTo(this);
#endif
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            #if !UNITY_EDITOR
            if (!hasFocus)
            {
                Save();
            }
            #endif
        }

        private void OnApplicationQuit()
        {
            Save();
        }

        private void Save()
        {
            foreach (var persistor in _persistors)
            {
                persistor.Save();
            }
        }
    }
}