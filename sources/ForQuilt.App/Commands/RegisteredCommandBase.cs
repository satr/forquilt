using System;
using System.Windows.Input;

namespace ForQuilt.App.Commands
{
    internal abstract class RegisteredCommandBase : ICommand
    {
        protected RegisteredCommandBase() : this(CommandBroker.CommandType.Unspecified)
        {
        }

        protected RegisteredCommandBase(CommandBroker.CommandType commandType, params CommandBroker.CommandType[] alterCommandTypes)
        {
            CommandBroker.Register(this, commandType);
            foreach (var type in alterCommandTypes)
            {
                CommandBroker.Register(this, type);
            }
        }

        public abstract void Execute(object parameter);

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}