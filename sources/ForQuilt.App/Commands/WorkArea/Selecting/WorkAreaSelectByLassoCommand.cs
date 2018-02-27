//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System.Windows.Controls;

namespace ForQuilt.App.Commands.WorkArea.Selecting
{
    internal class WorkAreaSelectByLassoCommand : WorkAreaCommandBase
    {
        protected override InkCanvasEditingMode EditingMode
        {
            get { return InkCanvasEditingMode.Select; }
        }
    }
}