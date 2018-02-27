//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

namespace ForQuilt.App.Commands.WorkArea
{
    internal abstract class WorkAreaEditingCommandBase : RegisteredCommandBase
    {
        protected WorkAreaEditingCommandBase()
            : base(CommandBroker.CommandType.Editing)
        {
        }

        public override void Execute(object parameter)
        {
            CommandBroker.BreakCurrentEditingCommand(this);
            ExecuteEditing(parameter);
        }

        protected abstract void ExecuteEditing(object parameter);

        public virtual void Break()
        {
        }
    }
}