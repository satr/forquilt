//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ForQuilt.App.Helpers;
using ForQuilt.App.ViewModels.Controls;

namespace ForQuilt.App.Views.Controls
{
    /// <summary>
    /// Interaction logic for TimerControlView.xaml
    /// </summary>
    public partial class TimerControlView : UserControl
    {
        private static readonly DependencyProperty StartCommandProperty =
            WPFHelper.RegisterCommandProperty<TimerControlView>("StartCommand");

        private static readonly DependencyProperty FinishCommandProperty =
            WPFHelper.RegisterCommandProperty<TimerControlView>("FinishCommand");

        private readonly TimerControlViewModel _viewModel;

        public TimerControlView()
        {
            InitializeComponent();
            // ViewModel is not assigned to DataContext due to 
            // StartCommand and FinishCommand will not use parent's assigned command
            // but instead they will use local ViewModel's commands
            _viewModel = new TimerControlViewModel(this, TimerSlider);
        }

        public ICommand StartCommand
        {
            get{ return (ICommand) GetValue(StartCommandProperty); }
            set { SetValue(StartCommandProperty, value); }
        }

        public ICommand FinishCommand
        {
            get { return (ICommand)GetValue(FinishCommandProperty); }
            set { SetValue(FinishCommandProperty, value); }
        }
    }
}
