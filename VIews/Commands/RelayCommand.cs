using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PhotoSort.VIews.Commands
{
    public class RelayCommand : ICommand
    {
        private Action<object> executeHandler = null;
        private Func<object, bool> canExecuteHandler = null;
        
        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.executeHandler = execute;
            this.canExecuteHandler = canExecute;
        }

        public void RaiseCanExecute()
        {
            var handler = this.CanExecuteChanged;
            if (handler != null)
                handler(this, new EventArgs());
        }

        public bool CanExecute(object parameter)
        {
            if (this.canExecuteHandler != null)
                return this.canExecuteHandler(parameter);

            return true;
        }

        public void Execute(object parameter)
        {
            if (this.executeHandler != null)
                this.executeHandler(parameter);
        }
    }
}
