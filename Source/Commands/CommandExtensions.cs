using System;
using UniRx;
using UnityEngine.UI;

namespace Variables.Source.Commands
{
    public static class CommandExtensions
    {
        public static IDisposable BindToCommand(this Button button, Command<Unit> command)
        {
            return button.OnClickAsObservable().Subscribe(_ => { command.Execute(Unit.Default); });
        }
        
    }
}