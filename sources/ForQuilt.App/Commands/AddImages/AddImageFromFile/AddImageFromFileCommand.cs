//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using System.IO;
using System.Windows.Forms;
using ForQuilt.App.Helpers;
using ForQuilt.App.Models;

namespace ForQuilt.App.Commands.AddImages.AddImageFromFile
{
    class AddImageFromFileCommand : ImageFileOperationCommandBase
    {
        public override void Execute(object parameter)
        {
            var openFileDialog = new OpenFileDialog {Filter = Filter};
            if (openFileDialog.ShowDialog() == DialogResult.Cancel || string.IsNullOrEmpty(openFileDialog.FileName))
            {
                return;
            }
            using (Stream stream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
            {
                ImageHelper.AddImageTo(ModelStorage.WorkAreaModel.CurrentInkCanvas, stream);
            }
        }
    }
}