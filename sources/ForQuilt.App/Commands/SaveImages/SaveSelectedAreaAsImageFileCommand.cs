//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using System.IO;
using System.Windows.Controls;
using ForQuilt.App.Helpers;
using ForQuilt.App.Models;

namespace ForQuilt.App.Commands.SaveImages
{
    class SaveSelectedAreaAsImageFileCommand : SaveImageToFileCommandBase
    {
        public override void Execute(object parameter)
        {
            if (ModelStorage.WorkAreaModel.RectangularSelectionInProgress)
            {
                base.Execute(parameter);
            }
        }

        protected override void SaveImage(InkCanvas inkCanvas, FileStream fs, FileInfo fileInfo)
        {
            var selectedRectangle = ModelStorage.WorkAreaModel.RectangularSelection;
            var croppedImage = ImageHelper.GetCroppedImageFromInkCanvas(selectedRectangle, inkCanvas);
            ImageHelper.SaveBitmapImage(fileInfo, croppedImage, fs);
        }
    }
}