//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using ForQuilt.App.Helpers;

namespace ForQuilt.App.Commands.Application
{
    internal class ExitFromAppCommand : RegisteredCommandBase
    {
        public ExitFromAppCommand() : base(CommandBroker.CommandType.AlwaysEnabled)
        {
        }

        public override void Execute(object parameter)
        {
            EventBroker.Instance.ApplicationExit();
        }
    }
}