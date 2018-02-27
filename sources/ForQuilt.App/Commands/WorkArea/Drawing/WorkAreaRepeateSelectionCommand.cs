//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ForQuilt.App.Models;

namespace ForQuilt.App.Commands.WorkArea.Drawing
{
    class WorkAreaRepeateSelectionCommand: WorkAreaEditingCommandBase
    {
        private readonly RepeateSelectionModel _model;

        public WorkAreaRepeateSelectionCommand(RepeateSelectionModel model)
        {
            _model = model;
        }

        protected override void ExecuteEditing(object parameter)
        {
            var inkCanvas = ModelStorage.WorkAreaModel.CurrentInkCanvas;
            if (inkCanvas.GetSelectedStrokes().Count == 0
                && inkCanvas.GetSelectedElements().Count == 0)
            {
                return;
            }
             Unbind(inkCanvas);
            Bind(inkCanvas);
            _model.Start(inkCanvas);
        }

        private void Bind(UIElement inkCanvas)
        {
            inkCanvas.PreviewMouseDown += InkCanvasOnPreviewMouseDown;
            inkCanvas.PreviewMouseUp += InkCanvasOnPreviewMouseUp;
            inkCanvas.PreviewMouseMove += InkCanvasOnPreviewMouseMove;
            inkCanvas.MouseWheel += InkCanvasOnMouseWheel;
        }

        private void Unbind(UIElement inkCanvas)
        {
            inkCanvas.PreviewMouseDown -= InkCanvasOnPreviewMouseDown;
            inkCanvas.PreviewMouseUp -= InkCanvasOnPreviewMouseUp;
            inkCanvas.PreviewMouseMove -= InkCanvasOnPreviewMouseMove;
            inkCanvas.MouseWheel -= InkCanvasOnMouseWheel;
        }

        private void InkCanvasOnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            _model.MouseWheel((InkCanvas)sender, e);
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