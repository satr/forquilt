//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using ForQuilt.App.Commands.Application;
using ForQuilt.App.Properties;
using ForQuilt.App.Views;

namespace ForQuilt.App.ViewModels
{
    class AboutViewModel
    {
        public AboutViewModel(Window view)
        {
            CloseViewCommand = new CloseViewCommand(view);
            ShowLicensesCommand = new ShowViewCommand<LicensesView>();
        }

        public ICommand CloseViewCommand{ get; private set; }
        public ICommand ShowLicensesCommand { get; private set; }
        public string Version
        {
            get
            {
                Version version = Assembly.GetExecutingAssembly().GetName().Version;
                return string.Format("{0}: {1}.{2}.{3}", Resources.Title_Version, version.Major, version.Minor, version.Build);
            }
        }
    }
}
