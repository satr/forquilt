//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ForQuilt.App.Commands;
using ForQuilt.App.Helpers;
using ForQuilt.App.Properties;
using Image = System.Drawing.Image;

namespace ForQuilt.App.Models
{
    class ClipboardModel
    {
        private const string ClipboardIdFormat = "ForQuiltClipboardId";
        private Guid _clipboardStateId = Guid.Empty;
        private const int PasteOffset = 10;
        private int _currentPasteOffset;

        private static bool ContainsImage()
        {
            return System.Windows.Forms.Clipboard.ContainsImage();
        }

        private bool ClipboardWasChangedOutside()
        {
            if (_clipboardStateId == Guid.Empty)
            {
                return true;
            }
            var guidObject = Clipboard.GetData(ClipboardIdFormat);
            return !(guidObject is Guid) || _clipboardStateId != (Guid)guidObject;
        }

        private static Image GetImage()
        {
            return System.Windows.Forms.Clipboard.GetImage();
        }

        public void Paste(InkCanvas inkCanvas, InkCanvas clipboardCanvas)
        {
            if (!ContainsImage())
            {
                EventBroker.Instance.ActionResult(Resources.Title_Paste, Resources.Message_Clipboard_does_not_contain_any_image_as_expected);
                return;
            }

            if (ClipboardWasChangedOutside())
            {
                var image = ImageHelper.AddImageTo(inkCanvas, new Bitmap(GetImage()));
                ActivateSelectionMode(inkCanvas);
                inkCanvas.Select(new List<UIElement> { image });
                return;
            }
            _currentPasteOffset += PasteOffset;
            ClearSelection(inkCanvas);
            var elements = new List<UIElement>();
            var strokes = new StrokeCollection();
            WPFHelper.CloneElementsTo(elements, clipboardCanvas.Children.GetEnumerator(), inkCanvas, _currentPasteOffset);
            WPFHelper.CloneStrokesTo(strokes, clipboardCanvas.Strokes, _currentPasteOffset);
            elements.ForEach(element => inkCanvas.Children.Add(element));
            inkCanvas.Strokes.Add(strokes);
            ActivateSelectionMode(inkCanvas);
            inkCanvas.Select(strokes, elements);
        }

        private static void ActivateSelectionMode(InkCanvas inkCanvas)
        {
            CommandBroker.BreakAllEditingCommands();
            inkCanvas.EditingMode = InkCanvasEditingMode.Select;
        }

        public void CopyInkCanvasObjects(InkCanvas inkCanvas, InkCanvas clipboardCanvas)
        {
            var selectionBounds = inkCanvas.GetSelectionBounds();
            if (selectionBounds.IsEmpty)
            {
                return;
            }
            Clear(clipboardCanvas);

            var selectedElements = inkCanvas.GetSelectedElements().ToList();
            var selectedStrokes = new StrokeCollection(inkCanvas.GetSelectedStrokes());
            ClearSelection(inkCanvas);

            var elements = new List<UIElement>();
            WPFHelper.CloneElementsTo(elements, selectedElements.GetEnumerator(), clipboardCanvas);
            WPFHelper.CloneStrokesTo(clipboardCanvas.Strokes, selectedStrokes);
            elements.ForEach(element => clipboardCanvas.Children.Add(element));
            inkCanvas.Select(selectedStrokes, selectedElements);

            var dataObject = new DataObject();
            dataObject.SetData(ClipboardIdFormat, _clipboardStateId = Guid.NewGuid());
            dataObject.SetData(DataFormats.Bitmap, ImageHelper.GetCroppedImageFromInkCanvas(selectionBounds, inkCanvas));
            Clipboard.SetDataObject(dataObject);
        }

        private static void ClearSelection(InkCanvas inkCanvas)
        {
            inkCanvas.Select(new StrokeCollection());
        }

        public void CopyImage(InkCanvas inkCanvas, InkCanvas clipboardCanvas, Rect rectangularSelection)
        {
            var croppedImage = ImageHelper.GetCroppedImageFromInkCanvas(rectangularSelection, inkCanvas);
            Clipboard.SetImage(croppedImage);
            Clear(clipboardCanvas);
            clipboardCanvas.Children.Add(ImageHelper.GetImageFrom(croppedImage));
        }

        private void Clear(InkCanvas clipboardCanvas)
        {
            _currentPasteOffset = 0;
            clipboardCanvas.Children.Clear();
            clipboardCanvas.Strokes.Clear();
        }
    }
}
