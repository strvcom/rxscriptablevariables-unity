using UnityEngine;
#if REACTIVE_VARIABLE_RX_ENABLED
using UniRx;
#endif

namespace STRV.Variables.Views
{
    public class GameObjectToggle : MonoBehaviour
    {
        public BoolReference IsActive;

        /// Should this be hidden when toggle is on
        public bool HideOnToggle;

        private void Awake() {
            
#if REACTIVE_VARIABLE_RX_ENABLED
            IsActive.AsObservable()
                .Subscribe(HandleVariableChange)
                .AddTo(this);
#else
            IsActive.OnValueChanged += HandleVariableChange;
            HandleVariableChange(IsActive.Value);
#endif
        }

#if !REACTIVE_VARIABLE_RX_ENABLED
        private void OnDestroy()
        {
            IsActive.OnValueChanged -= HandleVariableChange;
        }
#endif        

        private void HandleVariableChange(bool value)
        {
            gameObject.SetActive(!HideOnToggle && value || HideOnToggle && !value);
        }
    }
}