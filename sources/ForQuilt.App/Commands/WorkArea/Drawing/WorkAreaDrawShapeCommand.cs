//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using ForQuilt.App.Models;

namespace ForQuilt.App.Commands.WorkArea.Drawing
{
    internal class WorkAreaDrawShapeCommand<T> : WorkAreaEditingCommandBase
        where T: Stroke
    {
        private readonly DrawShapeModel<T> _model;

        public WorkAreaDrawShapeCommand(DrawShapeModel<T> model)
        {
            _model = model;
        }

        protected override void ExecuteEditing(object parameter)
        {
            var inkCanvas = ModelStorage.WorkAreaModel.CurrentInkCanvas;
            inkCanvas.EditingMode = InkCanvasEditingMode.None;
            Unbind(inkCanvas);
            Bind(inkCanvas);
            _model.Start(inkCanvas);
        }

        private void Bind(InkCanvas inkCanvas)
        {
            inkCanvas.PreviewMouseDown += InkCanvasOnPreviewMouseDown;
            inkCanvas.PreviewMouseUp += InkCanvasOnPreviewMouseUp;
            inkCanvas.PreviewMouseMove += InkCanvasOnPreviewMouseMove;
        }

        private void Unbind(UIElement inkCanvas)
        {
            inkCanvas.PreviewMouseUp -= InkCanvasOnPreviewMouseUp;
            inkCanvas.PreviewMouseMove -= InkCanvasOnPreviewMouseMove;
            inkCanvas.PreviewMouseDown -= InkCanvasOnPreviewMouseDown;
        }

        private void InkCanvasOnPreviewMouseDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            _model.MouseDown((InkCanvas) sender, mouseButtonEventArgs);
        }

        private void InkCanvasOnPreviewMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            _model.MouseMove((InkCanvas) sender, mouseEventArgs);
        }

        private void InkCanvasOnPreviewMouseUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            _model.MouseUp((InkCanvas)sender, mouseButtonEventArgs);
        }

        public override void Break()
        {
            var inkCanvas = ModelStorage.WorkAreaModel.CurrentInkCanvas;
            _model.Break(inkCanvas);
            Unbind(inkCanvas);
        }
    }
}