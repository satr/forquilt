//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System.Windows;
using System.Windows.Controls;
using ForQuilt.App.ViewModels.Controls;

namespace ForQuilt.App.Views.Controls
{
    /// <summary>
    /// Interaction logic for RotationControlView.xaml
    /// </summary>
    public partial class RotationControlView : UserControl
    {
        private readonly RotationControlViewModel _rotationControlViewModel;

        public RotationControlView()
        {
            InitializeComponent();
            DataContext = _rotationControlViewModel = new RotationControlViewModel();
        }

        private void RangeBase_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _rotationControlViewModel.Update();
        }
    }
}
