//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using ForQuilt.App.ViewModels.Controls;
using ForQuilt.App.ViewModels;

namespace ForQuilt.App.Views.Controls
{
    /// <summary>
    /// Interaction logic for VerticalToolBoxControlView.xaml
    /// </summary>
    public partial class VerticalToolBoxControlView
    {
        public VerticalToolBoxControlView()
        {
            InitializeComponent();
            DataContext = new VerticalToolBoxControlViewModel();
        }
    }
}
