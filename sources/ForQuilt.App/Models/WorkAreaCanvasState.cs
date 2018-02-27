//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using ForQuilt.App.ViewModels.Controls;

namespace ForQuilt.App.Models
{
    internal class WorkAreaCanvasState
    {
        private readonly InkCanvas _inkCanvas;
        private readonly WorkAreaModel _workAreaModel;
        private readonly RectangleSelectionControlViewModel _rectangularSelectorViewModel;
        private ICollection<UIElement> _selectedElements = new Collection<UIElement>();
        private StrokeCollection _selectedStrokes = new StrokeCollection();
        private readonly IDictionary<UIElement, Point> _originPoints = new Dictionary<UIElement, Point>();
        private Point _rotatePoint;

        public WorkAreaCanvasState(InkCanvas inkCanvas, WorkAreaModel workAreaModel, RectangleSelectionControlViewModel rectangularSelectorViewModel)
        {
            _inkCanvas = inkCanvas;
            _workAreaModel = workAreaModel;
            _rectangularSelectorViewModel = rectangularSelectorViewModel;
            inkCanvas.SelectionChanged += InkCanvasOnSelectionChanged;
            inkCanvas.SelectionMoving += InkCanvasOnSelectionMoving;
        }

        private void InkCanvasOnSelectionMoving(object sender, InkCanvasSelectionEditingEventArgs e)
        {
            var shiftX = e.OldRectangle.X - e.NewRectangle.X;
            var shiftY = e.OldRectangle.Y - e.NewRectangle.Y;
            _rotatePoint = new Point(_rotatePoint.X - shiftX, _rotatePoint.Y - shiftY);
        }

        private void InkCanvasOnSelectionChanged(object sender, EventArgs eventArgs)
        {
            _selectedElements = new Collection<UIElement>();
            _selectedStrokes = new StrokeCollection();
            _originPoints.Clear();
            _rotatePoint = new Point();
            LastAngle = 0;
            _workAreaModel.SelectionChanged();
            _selectedElements = _inkCanvas.GetSelectedElements();
            _selectedStrokes = _inkCanvas.GetSelectedStrokes();
            if (!IsSelected)
            {
                return;
            }
            var selectionBounds = ModelStorage.WorkAreaModel.CurrentInkCanvas.GetSelectionBounds();
            if (selectionBounds.IsEmpty)
            {
                return;
            }
            _rotatePoint = new Point(selectionBounds.X + (selectionBounds.Width / 2),
                                     selectionBounds.Y + (selectionBounds.Height / 2));
            foreach (var element in _selectedElements)
            {
                var translatePoint = element.TranslatePoint(new Point(0, 0), _inkCanvas);
                var x = element.DesiredSize.Width.Equals(0)
                            ? 0
                            : (_rotatePoint.X - translatePoint.X) / element.DesiredSize.Width;
                var y = element.DesiredSize.Height.Equals(0)
                            ? 0
                            : (_rotatePoint.Y - translatePoint.Y) / element.DesiredSize.Height;
                _originPoints.Add(element, new Point(x, y));
            }
            LastAngle = 0;
        }

        public bool IsSelected
        {
            get { return _selectedElements.Count > 0 || _selectedStrokes.Count > 0; }
        }

        public ICollection<UIElement> SelectedElements
        {
            get { return _selectedElements; }
        }

        public StrokeCollection SelectedStrokes
        {
            get { return _selectedStrokes; }
        }

        public IDictionary<UIElement, Point> OriginPoints
        {
            get { return _originPoints; }
        }

        public Point RotatePoint
        {
            get { return _rotatePoint; }
        }

        public double LastAngle { get; set; }

        public bool SelectionInProgress
        {
            get
            {
                return _rectangularSelectorViewModel != null && _rectangularSelectorViewModel.SelectionInProgress;
            }
        }

        public void BeginSelection()
        {
            if (_rectangularSelectorViewModel == null)
            {
                return;
            }
            _rectangularSelectorViewModel.BeginSelection();
        }

        public void EndSelection()
        {
            if (_rectangularSelectorViewModel == null)
            {
                return;
            }
            _rectangularSelectorViewModel.EndSelection();
        }

        public Rect SelectedArea()
        {
            return _rectangularSelectorViewModel != null ? _rectangularSelectorViewModel.SelectedArea() : new Rect();
        }
    }
}