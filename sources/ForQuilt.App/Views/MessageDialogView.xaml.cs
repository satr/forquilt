//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using ForQuilt.App.ViewModels;

namespace ForQuilt.App.Views
{
    /// <summary>
    /// Interaction logic for MessageDialogView.xaml
    /// </summary>
    public partial class MessageDialogView
    {
        public MessageDialogView()
        {
            InitializeComponent();
            DataContext = new MessageDialogViewModel(this, ButtonCancel, ButtonClear, ButtonDelete, ButtonNo, ButtonOk, ButtonSave, ButtonYes, Message);
        }

        public static MessageDialogView CreateWarningDialog()
        {
            return CreatePredefinedDialog(Properties.Resources.Title_Warning);
        }

        public static MessageDialogView CreateErrorDialog()
        {
            return CreatePredefinedDialog(Properties.Resources.Title_Error);
        }

        public static MessageDialogView CreateInformationDialog()
        {
            return CreatePredefinedDialog(Properties.Resources.Title_Information);
        }

        private static MessageDialogView CreatePredefinedDialog(string dialogTitle)
        {
            return new MessageDialogView {Title = dialogTitle};
        }
    }
}
