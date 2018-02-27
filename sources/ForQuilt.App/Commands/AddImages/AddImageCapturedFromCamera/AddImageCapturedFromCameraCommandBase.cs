//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System;
using System.Windows.Input;
using ForQuilt.App.ViewModels;

namespace ForQuilt.App.Commands.AddImages.AddImageCapturedFromCamera
{
    internal abstract class AddImageCapturedFromCameraCommandBase : RegisteredCommandBase
    {
        protected AddImageCapturedFromCameraViewModel ViewModel { get; private set; }

        protected AddImageCapturedFromCameraCommandBase(AddImageCapturedFromCameraViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}