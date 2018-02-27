//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System.Windows.Controls;
using ForQuilt.App.ViewModels.Controls;

namespace ForQuilt.App.Views.Controls
{
    /// <summary>
    /// Interaction logic for RectangleSelectionControlViewModel.xaml
    /// </summary>
    public partial class RectangleSelectionControlView : UserControl
    {
        public RectangleSelectionControlView()
        {
            InitializeComponent();
            ViewModel = new RectangleSelectionControlViewModel(this, GridControl, SelectionBoxControl);
            DataContext = ViewModel;
        }

        public RectangleSelectionControlViewModel ViewModel { get; private set; }

    }
}
