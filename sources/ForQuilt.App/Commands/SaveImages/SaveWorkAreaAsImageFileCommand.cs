//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using System.IO;
using System.Windows;
using System.Windows.Controls;
using ForQuilt.App.Helpers;

namespace ForQuilt.App.Commands.SaveImages
{
    class SaveWorkAreaAsImageFileCommand : SaveImageToFileCommandBase
    {
        protected override void SaveImage(InkCanvas inkCanvas, FileStream fs, FileInfo fileInfo)
        {
            var rect = new Rect(0.0, 0.0, inkCanvas.DesiredSize.Width, inkCanvas.DesiredSize.Height);
            ImageHelper.EncodeToBitmap(fileInfo, rect, inkCanvas).Save(fs);
        }
    }
}