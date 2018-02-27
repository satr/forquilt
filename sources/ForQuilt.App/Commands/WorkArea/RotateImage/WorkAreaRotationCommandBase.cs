//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System;
using System.Windows.Input;
using ForQuilt.App.ViewModels.Controls;

namespace ForQuilt.App.Commands.WorkArea.RotateImage
{
    internal abstract class WorkAreaRotationCommandBase : RegisteredCommandBase
    {
        private RotationControlViewModel ViewModel { get; set; }

        protected WorkAreaRotationCommandBase(RotationControlViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            SetNewValueTo(ViewModel);
            ViewModel.Update();
        }

        protected abstract void SetNewValueTo(RotationControlViewModel viewModel);
    }
}