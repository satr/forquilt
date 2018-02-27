//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using ForQuilt.App.ViewModels.Controls;

namespace ForQuilt.App.Commands.WorkArea.RotateImage
{
    internal class WorkAreaRotateLeftCommand : WorkAreaRotationCommandBase
    {
        private readonly int _angle;

        public WorkAreaRotateLeftCommand(RotationControlViewModel viewModel, int angle)
            : base(viewModel)
        {
            _angle = angle;
        }

        protected override void SetNewValueTo(RotationControlViewModel viewModel)
        {
            var value = (viewModel.RotationAngle - _angle)%360;
            viewModel.RotationAngle = value <= -180 ? value + 360 : value;
        }
    }
}