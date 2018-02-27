//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using ForQuilt.App.Models;

namespace ForQuilt.App.Commands.WorkArea.Drawing
{
    class WorkAreaChangeThicknessCommand : RegisteredCommandBase
    {
        public override void Execute(object parameter)
        {
            ModelStorage.WorkAreaModel.ChangeThicknessOfSelectedDrawing();
        }
    }
}
