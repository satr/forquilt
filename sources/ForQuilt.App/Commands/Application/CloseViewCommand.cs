//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using System.Windows;

namespace ForQuilt.App.Commands.Application
{
    class CloseViewCommand : RegisteredCommandBase
    {
        private readonly Window _view;

        public CloseViewCommand(Window view) : base(CommandBroker.CommandType.AlwaysEnabled)
        {
            _view = view;
        }

        public override void Execute(object parameter)
        {
            if (_view != null)
            {
                _view.Close();
            }
        }
    }
}