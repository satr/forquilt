//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Windows;
using ForQuilt.App.Helpers;
using ForQuilt.App.ViewModels;

namespace ForQuilt.App.Views
{
    /// <summary>
    /// Interaction logic for AddImageCapturedFromCameraView.xaml
    /// </summary>
    public partial class AddImageCapturedFromCameraView : Window
    {
        public AddImageCapturedFromCameraView()
        {
            InitializeComponent();
            Closed += (sender, args) => EventBroker.Instance.ImportImageFromCameraViewExit();
            EventBroker.Instance.OnImportImageFromCameraViewExit += Instance_OnImportImageFromCameraViewExit;
            DataContext = new AddImageCapturedFromCameraViewModel(this,CapturePreviewArea, CaptureArea, DesignerProperties.GetIsInDesignMode(this));
        }

        void Instance_OnImportImageFromCameraViewExit(object sender, System.EventArgs e)
        {
            var disposable = TimerControl.DataContext as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
