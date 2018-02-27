//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using ForQuilt.App.Helpers;

namespace ForQuilt.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            try
            {
                var args = Environment.GetCommandLineArgs() as string[];
                if (args.Length > 1 && !string.IsNullOrEmpty(args[1])) 
                {
                    var culture = args[1].ToLower();
                    switch (culture)
                    {
                        case "en-us":
                        case "ru-ru":
                            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
                            break;
                    }
                }
            }
            catch
            {
            }
        }

        private void App_OnExit(object sender, ExitEventArgs e)
        {
            ShutdownApplicationResources();
        }

        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            ShutdownApplicationResources();
            MessageBox.Show(string.Format("Critical error occurred - the program was terminated.\nPlease try to run the program again.\n\nError description:\n{0}",
                                          e.Exception.Message));
        }

        private static void ShutdownApplicationResources()
        {
            EventBroker.Instance.ImportImageFromCameraViewExit();
            EventBroker.Instance.ApplicationExit();
        }

    }
}
