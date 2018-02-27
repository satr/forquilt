//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System.Windows.Controls;

namespace ForQuilt.App.Commands.WorkArea.Drawing
{
    internal class WorkAreaEraseCommand : WorkAreaCommandBase
    {
        protected override InkCanvasEditingMode EditingMode
        {
            get { return InkCanvasEditingMode.EraseByPoint; }
        }
    }
}