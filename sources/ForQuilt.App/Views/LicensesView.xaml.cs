//----------------------------------------------------------------------------
//  Copyright © 2013
//  All rights reserved.       
//----------------------------------------------------------------------------
using System.Windows;
using ForQuilt.App.ViewModels;

namespace ForQuilt.App.Views
{
    /// <summary>
    /// Interaction logic for LicensesView.xaml
    /// </summary>
    public partial class LicensesView : Window
    {
        public LicensesView()
        {
            InitializeComponent();
            DataContext = new LicensesViewModel(this, LicenseReaderForQuilt, LicenseReaderDirectShowNet);
        }
    }
}
