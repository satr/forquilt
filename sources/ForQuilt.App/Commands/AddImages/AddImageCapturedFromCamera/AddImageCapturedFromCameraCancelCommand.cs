//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using ForQuilt.App.ViewModels;

namespace ForQuilt.App.Commands.AddImages.AddImageCapturedFromCamera
{
    class AddImageCapturedFromCameraCancelCommand : AddImageCapturedFromCameraCommandBase
    {
        public AddImageCapturedFromCameraCancelCommand(AddImageCapturedFromCameraViewModel viewModel) : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            ViewModel.CloseView();
        }
    }
}