//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using System.Windows;

namespace ForQuilt.App.Commands.Application
{
    class ShowViewCommand<T> : RegisteredCommandBase
        where T : Window, new()
    {
        public ShowViewCommand()
        {
        }

        public ShowViewCommand(CommandBroker.CommandType commandType): base(commandType)
        {
        }

        public override void Execute(object parameter)
        {
            Window view = new T();
            view.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            view.ShowDialog();
        }
    }
}