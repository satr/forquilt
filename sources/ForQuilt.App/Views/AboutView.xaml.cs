//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using System.Diagnostics;
using System.Windows.Navigation;
using ForQuilt.App.ViewModels;

namespace ForQuilt.App.Views
{
    /// <summary>
    /// Interaction logic for AboutView.xaml
    /// </summary>
    public partial class AboutView
    {
        public AboutView()
        {
            InitializeComponent();
            DataContext = new AboutViewModel(this);
        }

        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
