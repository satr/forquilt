//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using System.Windows;
using System.Windows.Input;
using ForQuilt.App.ViewModels.Controls;
using ForQuilt.App.ViewModels;

namespace ForQuilt.App.Views
{
    /// <summary>
    /// Interaction logic for HorizontalToolBoxControlView.xaml
    /// </summary>
    public partial class HorizontalToolBoxControlView
    {
        private readonly HorizontalToolBoxControlViewModel _viewModel;

        public HorizontalToolBoxControlView()
        {
            InitializeComponent();
            DataContext = _viewModel = new HorizontalToolBoxControlViewModel();
            ShowClipboardButton.AddHandler(MouseDownEvent, (RoutedEventHandler)ShowClipboard_OnMouseDown, true);
            ShowClipboardButton.AddHandler(MouseUpEvent, (RoutedEventHandler)ShowClipboard_OnMouseUp, true);
            ShowClipboardButton.AddHandler(MouseLeaveEvent, (RoutedEventHandler)ShowClipboard_OnMouseLeave, true);
            ShowClipboardButton.LostFocus += ShowClipboard_OnLostFocus;
        }

        private void ShowClipboard_OnMouseDown(object sender, RoutedEventArgs routedEventArgs)
        {
            _viewModel.ShowClipboard();
        }

        private void ShowClipboard_OnMouseUp(object sender, RoutedEventArgs routedEventArgs)
        {
            _viewModel.HideClipboard();
        }

        private void ShowClipboard_OnMouseLeave(object sender, RoutedEventArgs routedEventArgs)
        {
            _viewModel.HideClipboard();
        }

        private void ShowClipboard_OnLostFocus(object sender, RoutedEventArgs e)
        {
            _viewModel.HideClipboard();
        }
    }
}
