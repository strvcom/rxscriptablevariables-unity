using System;
using System.Threading;
using UniRx;
using UniRx.Async;
using UnityEngine;

namespace Variables.Source.Commands
{
    /// Basically just ReactiveCommand wrapped into a ScriptableObject
    public class Command<T>: ScriptableObject, IObservable<T>
    {
        private Subject<T> _subject = new Subject<T>();

        /// Returns false if CanExecute of the command is false
        public bool Execute(T value)
        {
            _subject.OnNext(value);
            return true;
        }
        
        public IDisposable Subscribe(IObserver<T> observer)
        {
            return _subject.Subscribe(observer);
        }
    }

    [CreateAssetMenu(menuName = "Commands/No Param")]
    public class Command : Command<Unit>
    {
        public bool Execute()
        {
            return base.Execute(Unit.Default);
        }
    }
}