//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System;
using ForQuilt.App.ViewModels;
using ForQuilt.App.Views;

namespace ForQuilt.App.Helpers
{
    class MessageDialogHelper
    {
        public static bool ShowWarningYesNo(string format, params object[] args)
        {
            return ShowDialog(MessageDialogView.CreateWarningDialog, 
                string.Format(format, args), 
                MessageDialogViewModel.Buttons.Yes, 
                MessageDialogViewModel.Buttons.No);
        }

        public static bool ShowOkCancel(string format,params object[] args)
        {
            return ShowDialog(MessageDialogView.CreateWarningDialog, 
                string.Format(format, args), MessageDialogViewModel.Buttons.Ok, 
                MessageDialogViewModel.Buttons.Cancel);
        }

        private static bool ShowDialog(Func<MessageDialogView> createDialogAction, string message, params MessageDialogViewModel.Buttons[] buttons)
        {
            var view = createDialogAction();
            var viewModel = (MessageDialogViewModel)view.DataContext;
            viewModel.HideAllButtons();
            viewModel.SetMessage(message);
            foreach (var button in buttons)
            {
                viewModel.ShowButton(button);
            }
            return view.ShowDialog() ?? false;
        }
    }
}
