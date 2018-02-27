//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using ForQuilt.App.Models;

namespace ForQuilt.App.Commands.WorkArea.Editing
{
    internal class WorkAreaDeleteCommand : RegisteredCommandBase
    {
        public override void Execute(object parameter)
        {
            ModelStorage.WorkAreaModel.DeleteSelectedInCurrentInkCanvas();
        }
    }
}