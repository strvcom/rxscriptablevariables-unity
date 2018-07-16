using UnityEngine;
using UnityEngine.UI;
#if REACTIVE_VARIABLE_RX_ENABLED
using UniRx;
#endif

// ReSharper disable once CheckNamespace
namespace STRV.Variables.Views
{
    [RequireComponent(typeof(Toggle))]
    public class VariableToggle : MonoBehaviour
    {
        public BoolVariable IsOn;

        private Toggle _toggle;

        void Start() {
            _toggle = GetComponent<Toggle>();
            
#if REACTIVE_VARIABLE_RX_ENABLED
            IsOn.AsObservable()
                .Subscribe(HandleValueChanged)
                .AddTo(this);
            
            _toggle.OnValueChangedAsObservable()
                .Subscribe(HandleToggleValueChanged)
                .AddTo(this);
#else
            HandleValueChanged(IsOn.CurrentValue);        
            _toggle.onValueChanged.AddListener(HandleToggleValueChanged);
            IsOn.OnValueChanged += HandleValueChanged;
#endif
        }
        
#if !REACTIVE_VARIABLE_RX_ENABLED

        private void OnDestroy()
        {
            _toggle.onValueChanged.RemoveListener(HandleToggleValueChanged);
            IsOn.OnValueChanged -= HandleValueChanged;
        }

#endif

        private void HandleToggleValueChanged(bool value)
        {
            IsOn.CurrentValue = value;
        }
        
        private void HandleValueChanged(bool value)
        {
            _toggle.isOn = value;
        }
    }
}