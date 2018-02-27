//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using System.Windows.Controls;
using ForQuilt.App.Helpers;
using ForQuilt.App.Models;

namespace ForQuilt.App.Commands.WorkArea.Editing
{
    internal class WorkAreaCropCommand : RegisteredCommandBase
    {
        public override void Execute(object parameter)
        {
            var inkCanvas = ModelStorage.WorkAreaModel.CurrentInkCanvas;
            if (!ModelStorage.WorkAreaModel.RectangularSelectionInProgress)
            {
                return;
            }
            var selectedRectangle = ModelStorage.WorkAreaModel.RectangularSelection;
            var croppedImage = ImageHelper.GetCroppedImageFromInkCanvas(selectedRectangle, inkCanvas);
            ModelStorage.WorkAreaModel.ClearCurrentInkCanvas();
            var image = new Image();
            image.BeginInit();
            image.Source = croppedImage;
            image.EndInit();
            ModelStorage.WorkAreaModel.CurrentInkCanvas.Children.Add(image);
            ModelStorage.WorkAreaModel.EndRectanglarSelection();
        }
    }
}