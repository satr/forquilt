//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using ForQuilt.App.ViewModels.Controls;

namespace ForQuilt.App.Commands.WorkArea.RotateImage
{
    internal class WorkAreaResetRotationCommand : WorkAreaRotationCommandBase
    {
        public WorkAreaResetRotationCommand(RotationControlViewModel viewModel) : base(viewModel)
        {
        }

        protected override void SetNewValueTo(RotationControlViewModel viewModel)
        {
            viewModel.RotationAngle = 0;
        }
    }
}