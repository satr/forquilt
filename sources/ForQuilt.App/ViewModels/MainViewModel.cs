//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System;
using ForQuilt.App.Helpers;
using ForQuilt.App.Views;
using ForQuilt.App.Models;

namespace ForQuilt.App.ViewModels
{
    class MainViewModel
    {
        private MainView View { get; set; }

        public class TabTitlesSet
        {
            public string Main { get { return "Main"; } }
            public string Test { get { return "Test"; } }
        }

        public MainViewModel(MainView view)
        {
            View = view;
            TabTitles = new TabTitlesSet();
            //Infinite loop for next lines is solved in an EventBroker
            view.Closed += (sender, args) => EventBroker.Instance.ApplicationExit();
            EventBroker.Instance.OnApplicationExit += (sender, args) => View.Close();
        }

        private void InstanceOnApplicationExit(object sender, EventArgs eventArgs)
        {
            View.Close();
        }

        public TabTitlesSet TabTitles { get; private set; }
    }
}
