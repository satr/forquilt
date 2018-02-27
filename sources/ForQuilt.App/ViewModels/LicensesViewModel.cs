//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using ForQuilt.App.Commands.Application;
using ForQuilt.App.Properties;

namespace ForQuilt.App.ViewModels
{
    public class LicensesViewModel
    {
        private readonly FlowDocumentScrollViewer _licenseReaderForQuilt;
        private readonly FlowDocumentScrollViewer _licenseReaderDirectShowNet;

        public LicensesViewModel(Window view, FlowDocumentScrollViewer licenseReaderForQuilt, FlowDocumentScrollViewer licenseReaderDirectShowNet)
        {
            _licenseReaderForQuilt = licenseReaderForQuilt;
            _licenseReaderDirectShowNet = licenseReaderDirectShowNet;
            CloseViewCommand = new CloseViewCommand(view);
            view.Loaded += ViewOnLoaded;
        }

        private void ViewOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            try
            {
                LoadLicenseText("License.txt", _licenseReaderForQuilt);
                LoadLicenseText("DirectShow.NET.License.txt", _licenseReaderDirectShowNet);
            }
            catch (Exception e)
            {
                //TODO
            }
        }

        private static void LoadLicenseText(string licenseFileName, FlowDocumentScrollViewer documentControl)
        {
            var fileInfo = new FileInfo(licenseFileName);
            if (!fileInfo.Exists)
            {
                AddParagraph(
                    string.Format("{0}: {1}", Resources.Message_File_with_license_does_not_exist, licenseFileName),
                    documentControl);
                return;
            }
            using (var stream = File.OpenText(licenseFileName))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    AddParagraph(line, documentControl);
                }
            }
        }

        private static void AddParagraph(string line, FlowDocumentScrollViewer viewer)
        {
            viewer.Document.Blocks.Add(new Paragraph(new Run(line)));
        }

        public ICommand CloseViewCommand { get; set; }
    }
}