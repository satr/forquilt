//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using ForQuilt.App.Helpers;
using ForQuilt.App.Models;
using ForQuilt.App.Properties;

namespace ForQuilt.App.Commands.WorkArea.Editing
{
    internal class WorkAreaClearCommand : WorkAreaEditingCommandBase
    {
        protected override void ExecuteEditing(object parameter)
        {
            if (MessageDialogHelper.ShowWarningYesNo(Resources.Message_Do_you_want_to_clear_all_from_this_work_table))
            {
                ModelStorage.WorkAreaModel.ClearCurrentInkCanvas();
            }
        }
    }
}