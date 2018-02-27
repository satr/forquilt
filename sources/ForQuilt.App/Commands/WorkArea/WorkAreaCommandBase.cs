//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using System.Windows.Controls;
using ForQuilt.App.Models;

namespace ForQuilt.App.Commands.WorkArea
{
    internal abstract class WorkAreaCommandBase : WorkAreaEditingCommandBase
    {
        protected override void ExecuteEditing(object parameter)
        {
            ModelStorage.WorkAreaModel.CurrentInkCanvas.EditingMode = EditingMode;
        }

        protected abstract InkCanvasEditingMode EditingMode { get; }
    }
}