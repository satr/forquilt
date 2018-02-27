//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using System.Windows.Input;
using ForQuilt.App.Commands.AddImages.AddImageCapturedFromCamera;
using ForQuilt.App.Commands.AddImages.AddImageFromFile;
using ForQuilt.App.Commands.Application;
using ForQuilt.App.Commands.SaveImages;
using ForQuilt.App.Commands.WorkArea.Drawing;
using ForQuilt.App.Commands.WorkArea.Editing;
using ForQuilt.App.Commands.WorkArea.Ordering;
using ForQuilt.App.Commands.WorkArea.Selecting;
using ForQuilt.App.Models;

namespace ForQuilt.App.ViewModels.Controls
{
    class HorizontalToolBoxControlViewModel
    {
        public HorizontalToolBoxControlViewModel()
        {
            DrawCommand = new WorkAreaDrawCommand();
            SelectCommand = new WorkAreaSelectByLassoCommand();
            EraseCommand = new WorkAreaEraseCommand();
            CutCommand = new WorkAreaCutCommand();
            CopyCommand = new WorkAreaCopyCommand();
            PasteCommand = new WorkAreaPasteCommand();
            CropCommand = new WorkAreaCropCommand();
            DeleteCommand = new WorkAreaDeleteCommand();
            ClearWorkAreaCommand = new WorkAreaClearCommand();
            BringToFrontCommand = new WorkAreaBringToFrontCommand();
            SendToBackCommand = new WorkAreaSendToBackCommand();
            ImportImageFromFileCommand = new AddImageFromFileCommand();
            ImportImageFromCameraCommand = new AddImageCapturedFromCameraCommand();
            ExportWorkAreaAsImageToFileCommand = new SaveWorkAreaAsImageFileCommand();
            ExportSelectedAreaAsImageToFileCommand = new SaveSelectedAreaAsImageFileCommand();
            ShowAboutCommand = new ShowAboutCommand();
            ExitFromAppCommand = new ExitFromAppCommand();
        }
        public ICommand DrawCommand { get; set; }
        public ICommand SelectCommand { get; set; }
        public ICommand EraseCommand { get; set; }
        public ICommand CutCommand { get; set; }
        public ICommand CopyCommand { get; set; }
        public ICommand PasteCommand { get; set; }
        public ICommand CropCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ClearWorkAreaCommand { get; set; }
        public ICommand BringToFrontCommand { get; set; }
        public ICommand SendToBackCommand { get; set; }
        public ICommand ImportImageFromFileCommand { get; set; }
        public ICommand ImportImageFromCameraCommand { get; set; }
        public ICommand ExportWorkAreaAsImageToFileCommand { get; set; }
        public ICommand ExportSelectedAreaAsImageToFileCommand { get; set; }
        public ICommand ShowAboutCommand { get; set; }
        public ICommand ExitFromAppCommand { get; set; }

        public void ShowClipboard()
        {
            ModelStorage.WorkAreaModel.ShowClipboard();
        }

        public void HideClipboard()
        {
            ModelStorage.WorkAreaModel.HideClipboard();
        }
    }
}
