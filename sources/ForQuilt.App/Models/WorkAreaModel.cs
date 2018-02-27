//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using ForQuilt.App.Helpers;
using ForQuilt.App.ViewModels.Controls;
using Color = System.Windows.Media.Color;

namespace ForQuilt.App.Models
{
    class WorkAreaModel
    {
        public event EventHandler OnSelectionChanged;

        private readonly Dictionary<InkCanvas, WorkAreaCanvasState> _canvasStates = new Dictionary<InkCanvas, WorkAreaCanvasState>();
        private Color _currentColor;
        private double _drawingThickness;
        private DrawingAttributes _sharedDrawingAttributes = new DrawingAttributes { Color = Colors.Black, Width = 1, Height = 1 };
        private readonly ICollection<StrokeCollection> _groupedStrokesCollections = new Collection<StrokeCollection>();

        public WorkAreaModel()
        {
            CurrentColor = Colors.Black;
            DrawingThickness = 1;
        }

        public void SelectionChanged()
        {
            if (OnSelectionChanged != null)
            {
                OnSelectionChanged(this, EventArgs.Empty);
            }
        }

        public void SetCurrentCanvas(InkCanvas inkCanvas, RectangleSelectionControlViewModel selectorViewModel)
        {
            if (!_canvasStates.ContainsKey(inkCanvas))
            {
                InitSharedDrawingAttributes(inkCanvas);
                _canvasStates.Add(inkCanvas, new WorkAreaCanvasState(inkCanvas, this, selectorViewModel));
                inkCanvas.SelectionChanged += inkCanvas_SelectionChanged;
            }
            CurrentInkCanvas = inkCanvas;
            ChangeDrawingColor();
        }

        private void inkCanvas_SelectionChanged(object sender, EventArgs e)
        {
            SelectionHelper.SelectionChanged(CurrentInkCanvas, _groupedStrokesCollections);
        }

        private void InitSharedDrawingAttributes(InkCanvas inkCanvas)
        {
            if (_canvasStates.Count == 0)
            {
                _sharedDrawingAttributes = inkCanvas.DefaultDrawingAttributes;
            }
            else
            {
                inkCanvas.DefaultDrawingAttributes = _sharedDrawingAttributes;
            }
            inkCanvas.DefaultDrawingAttributesReplaced -= inkCanvas_DefaultDrawingAttributesReplaced;
            inkCanvas.DefaultDrawingAttributesReplaced += inkCanvas_DefaultDrawingAttributesReplaced;
        }

        //Prevent DrawAttributes object be replaced in InkCanvas
        private void inkCanvas_DefaultDrawingAttributesReplaced(object sender, DrawingAttributesReplacedEventArgs e)
        {
            var inkCanvas = ((InkCanvas)sender);
            inkCanvas.DefaultDrawingAttributesReplaced -= inkCanvas_DefaultDrawingAttributesReplaced;
            inkCanvas.DefaultDrawingAttributes = _sharedDrawingAttributes;
            inkCanvas.DefaultDrawingAttributesReplaced += inkCanvas_DefaultDrawingAttributesReplaced;
        }

        private void ChangeDrawingColor()
        {
            _sharedDrawingAttributes.Color = CurrentColor;
        }

        public InkCanvas CurrentInkCanvas { get; private set; }

        private WorkAreaCanvasState CurrentCanvasState
        {
            get { return _canvasStates[CurrentInkCanvas]; }
        }

        private bool CurrentCanasStateIsInitialised
        {
            get { return _canvasStates.ContainsKey(CurrentInkCanvas); }
        }

        public Color CurrentColor
        {
            get { return _currentColor; }
            set
            {
                _currentColor = value;
                ChangeDrawingColor();
            }
        }

        public double DrawingThickness
        {
            get { return _drawingThickness; }
            set
            {
                _drawingThickness = value;
                _sharedDrawingAttributes.Height = _sharedDrawingAttributes.Width = _drawingThickness;
            }
        }

        public bool RectangularSelectionInProgress
        {
            get { return CurrentCanvasState.SelectionInProgress; }
        }

        public Rect RectangularSelection
        {
            get { return CurrentCanvasState.SelectedArea(); }
        }

        public InkCanvas ClipboardCanvas { get; set; }

        public void RotateSelected(decimal value)
        {
            if (!CurrentCanasStateIsInitialised || !CurrentCanvasState.IsSelected)
            {
                return;
            }
            var angle = (double)value;
            RotateSelectedElements(angle);
            RotateSelectedStrokes(angle);
            CurrentCanvasState.LastAngle = angle;
        }

        private void RotateSelectedStrokes(double angle)
        {
            foreach (var stroke in CurrentCanvasState.SelectedStrokes)
            {
                var rotatingMatrix = new Matrix();
                rotatingMatrix.RotateAt(angle - CurrentCanvasState.LastAngle, CurrentCanvasState.RotatePoint.X, CurrentCanvasState.RotatePoint.Y);
                stroke.Transform(rotatingMatrix, false);
            }
        }

        private void RotateSelectedElements(double angle)
        {
            foreach (var element in CurrentCanvasState.SelectedElements)
            {
                element.RenderTransformOrigin = CurrentCanvasState.OriginPoints[element];
                element.RenderTransform = new RotateTransform(angle);
            }
        }

        public void BeginRectanglarSelection()
        {
            CurrentCanvasState.BeginSelection();
        }

        public void EndRectanglarSelection()
        {
            CurrentCanvasState.EndSelection();
        }

        public void ClearCurrentInkCanvas()
        {
            CurrentInkCanvas.Strokes.Clear();
            CurrentInkCanvas.Children.Clear();
        }

        public void DeleteSelectedInCurrentInkCanvas()
        {
            foreach (var element in CurrentInkCanvas.GetSelectedElements().ToList())
            {
                CurrentInkCanvas.Children.Remove(element);
            }
            foreach (var stroke in CurrentInkCanvas.GetSelectedStrokes().ToList())
            {
                CurrentInkCanvas.Strokes.Remove(stroke);
            }
        }

        public void ChangeColorOfSelectedDrawing()
        {
            foreach (var stroke in CurrentInkCanvas.GetSelectedStrokes())
            {
                stroke.DrawingAttributes.Color = _sharedDrawingAttributes.Color;
            }
        }

        public void ChangeThicknessOfSelectedDrawing()
        {
            foreach (var stroke in CurrentInkCanvas.GetSelectedStrokes())
            {
                stroke.DrawingAttributes.Width = _sharedDrawingAttributes.Width;
                stroke.DrawingAttributes.Height = _sharedDrawingAttributes.Height;
            }
        }

        public void GroupSelectedDrawings()
        {
            SelectionHelper.GroupSelectedDrawings(CurrentInkCanvas, _groupedStrokesCollections);
        }

        public void UnGroupSelectedDrawings()
        {
            SelectionHelper.UnGroupSelectedDrawings(CurrentInkCanvas, _groupedStrokesCollections);
            SelectionHelper.ClearSelection(CurrentInkCanvas);
        }

        private void CopyInkCanvaSelection()
        {
            ModelStorage.ClipboardModel.CopyInkCanvasObjects(CurrentInkCanvas, ClipboardCanvas);
        }

        private void CopyRectangularSelection()
        {
            ModelStorage.ClipboardModel.CopyImage(CurrentInkCanvas, ClipboardCanvas, RectangularSelection);
        }

        public void PasteCopiedObjects()
        {
            ModelStorage.ClipboardModel.Paste(CurrentInkCanvas, ClipboardCanvas);
        }

        public void CopySelection()
        {
            if (RectangularSelectionInProgress)
            {
                CopyRectangularSelection();
                return;
            }
            CopyInkCanvaSelection();
        }

        public void ShowClipboard()
        {
            ChangeClipboardUIIndex(1);
        }

        public void HideClipboard()
        {
            ChangeClipboardUIIndex(0);
        }

        private void ChangeClipboardUIIndex(int xIndex)
        {
            Panel.SetZIndex(ClipboardCanvas, xIndex);
        }
    }
}
