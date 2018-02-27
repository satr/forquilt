//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System.Windows.Controls;
using ForQuilt.App.ViewModels.Controls;

namespace ForQuilt.App.Views.Controls
{
    /// <summary>
    /// Interaction logic for ColorPickControlView.xaml
    /// </summary>
    public partial class ColorPickControlView : UserControl
    {
        public ColorPickControlView()
        {
            InitializeComponent();
            DataContext = new ColorPickControlViewModel(Container, PickedColor, RColorSlider, GColorSlider, BColorSlider);
        }
    }
}
