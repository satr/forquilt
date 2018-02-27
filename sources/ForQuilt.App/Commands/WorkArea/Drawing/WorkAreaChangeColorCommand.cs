//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using ForQuilt.App.Models;

namespace ForQuilt.App.Commands.WorkArea.Drawing
{
    class WorkAreaChangeColorCommand : RegisteredCommandBase
    {
        public override void Execute(object parameter)
        {
            ModelStorage.WorkAreaModel.ChangeColorOfSelectedDrawing();
        }
    }
}
