//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ForQuilt.App.Views;

namespace ForQuilt.App.ViewModels
{
    class MessageDialogViewModel
    {
        public enum Buttons
        {
            Cancel,
            Clear,
            Delete,
            No,
            Ok,
            Save,
            Yes
        }
        private readonly TextBlock _messageBlock;
        readonly IDictionary<Buttons, Button> _buttons = new Dictionary<Buttons, Button>();

        public MessageDialogViewModel(MessageDialogView view, Button buttonCancel, Button buttonClear, Button buttonDelete, Button buttonNo, Button buttonOk, Button buttonSave, Button buttonYes, TextBlock messageBlock)
        {
            _messageBlock = messageBlock;
            _buttons.Add(Buttons.Cancel, buttonCancel);
            _buttons.Add(Buttons.Clear, buttonClear);
            _buttons.Add(Buttons.Delete, buttonDelete);
            _buttons.Add(Buttons.No, buttonNo);
            _buttons.Add(Buttons.Ok, buttonOk);
            _buttons.Add(Buttons.Save, buttonSave);
            _buttons.Add(Buttons.Yes, buttonYes);
            InitButtons(view);
        }

        private void InitButtons(MessageDialogView view)
        {
            foreach (var button in _buttons.Values)
            {
                button.Visibility = Visibility.Collapsed;
                if (button.IsDefault)
                {
                    button.Click += (sender, args) =>
                        {
                            view.DialogResult = true;
                            view.Close();
                        };
                }
            }
        }

        public void HideAllButtons()
        {
            foreach (var button in _buttons.Values)
            {
                button.Visibility = Visibility.Collapsed;
            }
        }

        public void ShowButton(Buttons button)
        {
            _buttons[button].Visibility = Visibility.Visible;
        }

        public void SetMessage(string message)
        {
            _messageBlock.Text = message;
        }
    }
}
