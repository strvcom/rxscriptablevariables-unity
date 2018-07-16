using UnityEngine;
using UnityEngine.Assertions;
#if REACTIVE_VARIABLE_RX_ENABLED
using UniRx;
#endif

namespace STRV.Variables.Operations
{
    /// Purpose of this class is to control
    [CreateAssetMenu(menuName = "Variables/Operations/Bool/Logic Operation", order = 800)]
    public class BoolLogicOperation: ScriptableObject
    {
        private enum Operation
        {
            And,
            Or,
            Xor
        }
        
        [Header("Variables")]
        [SerializeField] private BoolReference _left;
        [SerializeField] private BoolReference _right;
        [SerializeField] private BoolVariable _result;

        [Header("Operation")] 
        [SerializeField] private Operation _operation;

#if REACTIVE_VARIABLE_RX_ENABLED
        private CompositeDisposable _disposables = new CompositeDisposable();
#endif
        
        private void OnEnable()
        {
            if (_left == null)
            {
                Assert.IsTrue(false, "Missing left operand");
                return;
            }
            if (_right == null)
            {
                Assert.IsTrue(false, "Missing right operand");
                return;
            }
            
#if REACTIVE_VARIABLE_RX_ENABLED
            _left.AsObservable()
                .Subscribe(HandleValueChanged)
                .AddTo(_disposables);
            _right.AsObservable()
                .Subscribe(HandleValueChanged)
                .AddTo(_disposables);
#else
            _left.OnValueChanged += HandleValueChanged;
            _right.OnValueChanged += HandleValueChanged;
#endif
        }

        private void OnDestroy()
        {
#if REACTIVE_VARIABLE_RX_ENABLED
            _disposables.Dispose();
            _disposables = null;
#else
            _left.OnValueChanged -= HandleValueChanged;
            _right.OnValueChanged -= HandleValueChanged;
#endif
        }

        private void HandleValueChanged(bool value)
        {
            Compare();
        }

        private void Compare()
        {
            bool result = true;
            switch (_operation)
            {
                case Operation.Or:
                    result = _left.Value || _right.Value;
                    break;
                case Operation.Xor:
                    result = _left.Value ^ _right.Value;
                    break;
                case Operation.And:
                    result = _left.Value && _right.Value;
                    break;
            }

            // ReSharper disable once RedundantCheckBeforeAssignment
            if (result != _result.CurrentValue)
            {
                _result.CurrentValue = result;
            }
        }
    }
}