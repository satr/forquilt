//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System.Windows.Controls;
using ForQuilt.App.ViewModels.Controls;

namespace ForQuilt.App.Views.Controls
{
    /// <summary>
    /// Interaction logic for DrawingThicknessControlView.xaml
    /// </summary>
    public partial class DrawingThicknessControlView : UserControl
    {
        public DrawingThicknessControlView()
        {
            InitializeComponent();
            DataContext = new DrawingThicknessControlViewModel();
        }
    }
}
