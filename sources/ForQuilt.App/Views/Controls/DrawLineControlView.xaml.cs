//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ForQuilt.App.Helpers;

namespace ForQuilt.App.Views.Controls
{
    /// <summary>
    /// Interaction logic for DrawLineControlView.xaml
    /// </summary>
    public partial class DrawLineControlView : UserControl
    {
        private static readonly DependencyProperty CommandProperty = WPFHelper.RegisterCommandProperty<DrawLineControlView>("Command");
        
        public DrawLineControlView()
        {
            InitializeComponent();
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (Command != null)
            {
                Command.Execute(null);
            }
            
        }
    }
}
