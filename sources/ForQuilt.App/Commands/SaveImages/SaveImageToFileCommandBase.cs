//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using ForQuilt.App.Models;

namespace ForQuilt.App.Commands.SaveImages
{
    internal abstract class SaveImageToFileCommandBase : ImageFileOperationCommandBase
    {
        public override void Execute(object parameter)
        {
            var saveFileDialog = new SaveFileDialog {Filter = Filter};
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel || string.IsNullOrEmpty(saveFileDialog.FileName))
            {
                return;
            }

            var inkCanvas = ModelStorage.WorkAreaModel.CurrentInkCanvas;
            var originalMargin = inkCanvas.Margin;
            inkCanvas.Margin = new Thickness(0, 0, 0, 0);

            using (var fs = File.Open(saveFileDialog.FileName, FileMode.Create))
            {
                SaveImage(inkCanvas, fs, new FileInfo(saveFileDialog.FileName));
            }
            inkCanvas.Margin = originalMargin;
        }

        protected abstract void SaveImage(InkCanvas inkCanvas, FileStream fs, FileInfo fileInfo);
    }
}