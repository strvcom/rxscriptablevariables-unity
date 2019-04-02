using System;
using UnityEngine;
using STRV.Variables.Utils;
#if REACTIVE_VARIABLE_RX_ENABLED
using UniRx;
#endif
using System.Collections.Generic;
using STRV.Variables.Persistance;
using UnityEngine.Assertions;
using Variables.Source.Persistance;

// ReSharper disable once CheckNamespace
namespace STRV.Variables
{
    // Non-generic type to allow it to be exposed in Unity Inspector
    public abstract class Variable : PersistableScriptableObject
    {
        public override string GetKey()
        {
            return GetInstanceID().ToString();
        }

        public override string GetStringValue()
        {
            throw new NotImplementedException("Please implement this on Variable subsclass to support it's persisting: " + GetType().Name);
        }

        public override void SetStringValue(string value)
        {
            throw new NotImplementedException("Please implement this on Variable subsclass to support it's persisting: " + GetType().Name);
        }
    }
    
    /// Generic base variable that all other variables inherit
    public abstract class Variable<T> : Variable, IObservable<T>
    {
        [Header("Remote Settings:")]
        public bool RemoteSettingsVariable;
        public string RemoteSettingsId;
        
        [Header("Developer:")]
        [Multiline]
        public string DeveloperDescription = "";
        
        /// Default value of the variable, exposed in editor if T is serializable, should not be changed from game code without a good reason
        [Header("Values:")]
        public T DefaultValue;

        private T _currentValue;
        /// Current value of the variable
        [ExposeProperty]
        public T CurrentValue {
            get {
                return _currentValue;
            }
            set {
                if (RemoteSettingsVariable)
                {
                    Debug.LogError("Trying to set value for remote settings variable");
                }
                HandleValueChange(value);
            }
        }
        
#if REACTIVE_VARIABLE_RX_ENABLED
        [NonSerialized]
        private BehaviorSubject<T> _publisher;

        public IObservable<T> AsObservable() {
            if (_publisher == null) {
                _publisher = new BehaviorSubject<T>(_currentValue);
            }
            return _publisher;
        }
#else
        public Action<T> OnValueChanged;
#endif

        public void SetValue(Variable<T> value) {
            CurrentValue = value.CurrentValue;
        }

        protected void HandleValueChange(T value, bool forced = false)
        {
            if (!EqualityComparer<T>.Default.Equals(_currentValue, value) || forced) {
                _currentValue = value;
#if REACTIVE_VARIABLE_RX_ENABLED
                if (_publisher != null) {
                    _publisher.OnNext(value);
                }
#else
                if (OnValueChanged != null) {
                    OnValueChanged(value);
                }
#endif
            }
        }

        private void OnEnable() {
            if (!SkipDefaultValueReset)
            {
                HandleValueChange(DefaultValue);
            }

            Assert.IsTrue((RemoteSettingsVariable && !string.IsNullOrEmpty(RemoteSettingsId) || !RemoteSettingsVariable), string.Format("Remote settings variable without remote settings id: {0}", name));

            if (RemoteSettingsVariable)
            {
                SyncValue();
                VariablesUpdatedHandler.Updated += SyncValue;
            }
        }

        public static implicit operator T(Variable<T> variable) {
            if (variable == null) {
                return default(T);
            }
            return variable.CurrentValue;
        }

        /// If this value is syncable via remote settings, implement this 
        protected virtual void SyncValue()
        {
            // Not implemented for all values
        }
        
        /// Does this variable type supports remote settings sync
        public virtual bool SupportsRemoteSettings()
        {
            return false;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return AsObservable().Subscribe(observer);
        }
    }
}
