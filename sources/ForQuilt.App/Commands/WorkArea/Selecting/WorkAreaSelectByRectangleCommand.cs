//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System.Windows.Controls;
using ForQuilt.App.Models;

namespace ForQuilt.App.Commands.WorkArea.Selecting
{
    internal class WorkAreaSelectByRectangleCommand : WorkAreaCommandBase
    {
        protected override InkCanvasEditingMode EditingMode
        {
            get { return InkCanvasEditingMode.None; }
        }

        protected override void ExecuteEditing(object parameter)
        {
            base.ExecuteEditing(parameter);
            ModelStorage.WorkAreaModel.BeginRectanglarSelection();
        }

        public override void Break()
        {
            ModelStorage.WorkAreaModel.EndRectanglarSelection();
        }
    }
}