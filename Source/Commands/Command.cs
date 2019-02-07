using System;
using System.Threading;
using UniRx;
using UniRx.Async;
using UnityEngine;

namespace Variables.Source.Commands
{
    /// Basically just ReactiveCommand wrapped into a ScriptableObject
    public class Command<T>: ScriptableObject, IReactiveCommand<T>
    {
        private ReactiveCommand<T> _command = new ReactiveCommand<T>();

        /// Returns false if CanExecute of the command is false
        public bool Execute(T value)
        {
            return _command.Execute(value);
        }

        public UniTask<T> WaitUntilExecuteAsync(CancellationToken cancellationToken)
        {
            return _command.WaitUntilExecuteAsync(cancellationToken);
        }

        public IReadOnlyReactiveProperty<bool> CanExecute => _command.CanExecute;

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return _command.Subscribe(observer);
        }
    }

    [CreateAssetMenu(menuName = "Commands/No Param")]
    public class Command : Command<Unit>
    {
        
    }
}