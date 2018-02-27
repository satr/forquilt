//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using ForQuilt.App.Helpers;
using ForQuilt.App.Models.Strokes;

namespace ForQuilt.App.Models
{
    class RepeateSelectionModel
    {
        private Point _startPoint;
        private List<Tuple<ReadOnlyCollection<UIElement>, StrokeCollection>> _repetitions = new List<Tuple<ReadOnlyCollection<UIElement>, StrokeCollection>>();
        private List<UIElement> _selectedElements = new List<UIElement>();
        private StrokeCollection _selectedStrokes = new StrokeCollection();
        private OverlayRepeatOperationStroke _overlayStroke;
        private double _dX;
        private double _dY;

        internal enum RepeateSelectionState
        {
            Inactive,
            WaitingStartPoint,
            WaitingEndPoint
        }

        private RepeateSelectionState ProcessRepeateSelectionState { get; set; }

        public void Start(InkCanvas inkCanvas)
        {
            ClearRepetitionCollections();
            _selectedElements = new List<UIElement>(inkCanvas.GetSelectedElements());
            _selectedStrokes = new StrokeCollection(inkCanvas.GetSelectedStrokes());
            inkCanvas.EditingMode = InkCanvasEditingMode.None;
            _startPoint = new Point(-100,-100);
            ProcessRepeateSelectionState = RepeateSelectionState.WaitingStartPoint;
        }

        private void ClearRepetitionCollections()
        {
            _repetitions = new List<Tuple<ReadOnlyCollection<UIElement>, StrokeCollection>>();
        }

        private void AddCopy(InkCanvas inkCanvas)
        {
            var strokeCollection = new StrokeCollection();
            foreach (var selectedStroke in _selectedStrokes)
            {
                var stroke = selectedStroke.Clone();
                strokeCollection.Add(stroke);
                inkCanvas.Strokes.Add(stroke);
            }
            var uiElementCollection = new List<UIElement>();
            foreach (UIElement selectedElement in _selectedElements)
            {
                var element = WPFHelper.Clone(selectedElement, inkCanvas);
                if (element == null)
                {
                    continue;
                }
                uiElementCollection.Add(element);
                inkCanvas.Children.Add(element);
                InkCanvas.SetLeft(element, InkCanvas.GetLeft(selectedElement));
                InkCanvas.SetTop(element, InkCanvas.GetTop(selectedElement));
            }
            _repetitions.Add(new Tuple<ReadOnlyCollection<UIElement>, StrokeCollection>(new ReadOnlyCollection<UIElement>(uiElementCollection), strokeCollection));
        }

        private void RemoveCopy(InkCanvas inkCanvas)
        {
            if (_repetitions.Count < 1)
            {
                return;
            }
            var tuple = _repetitions[_repetitions.Count - 1];
            foreach (var stroke in tuple.Obj2.Where(stroke => inkCanvas.Strokes.Contains(stroke)))
            {
                inkCanvas.Strokes.Remove(stroke);
            }
            foreach (var element in tuple.Obj1.Where(uiElement => inkCanvas.Children.Contains(uiElement)))
            {
                inkCanvas.Children.Remove(element);
            }
            _repetitions.Remove(tuple);
        }

        public void Break(InkCanvas inkCanvas)
        {
            if (ProcessRepeateSelectionState != RepeateSelectionState.Inactive && _repetitions.Count > 0)
            {
                foreach (var repetition in _repetitions)
                {
                    foreach (var element in repetition.Obj1.Where(uiElement => inkCanvas.Children.Contains(uiElement)))
                    {
                        inkCanvas.Children.Remove(element);
                    }
                    foreach (var stroke in repetition.Obj2.Where(stroke => inkCanvas.Strokes.Contains(stroke)))
                    {
                        inkCanvas.Strokes.Remove(stroke);
                    }
                }
                _repetitions.Clear();
            }
            Stop(inkCanvas);
        }

        private void Stop(InkCanvas inkCanvas)
        {
            if (_overlayStroke != null && inkCanvas.Strokes.Contains(_overlayStroke))
            {
                inkCanvas.Strokes.Remove(_overlayStroke);
            }
            foreach (var repetition in _repetitions)
            {
                _selectedElements.AddRange(repetition.Obj1);
                _selectedStrokes.Add(repetition.Obj2);
            }
            inkCanvas.Select(_selectedStrokes, _selectedElements);
            if (_repetitions.Count > 0)
            {
                ModelStorage.WorkAreaModel.GroupSelectedDrawings();
            }
            ProcessRepeateSelectionState = RepeateSelectionState.Inactive;
            ClearRepetitionCollections();
        }

        public void MouseDown(InkCanvas inkCanvas, MouseButtonEventArgs e)
        {
            if (ProcessRepeateSelectionState == RepeateSelectionState.WaitingEndPoint)
            {
                Stop(inkCanvas);
            }
        }

        public void MouseMove(InkCanvas inkCanvas, MouseEventArgs e)
        {
            if (ProcessRepeateSelectionState != RepeateSelectionState.WaitingEndPoint
                || _repetitions.Count < 1)
            {
                return;
            }
            var point = e.GetPosition(inkCanvas);
            _dX = point.X - _startPoint.X;
            _dY = point.Y - _startPoint.Y;
            _overlayStroke.StylusPoints.Add(new StylusPoint(point.X, point.Y));
            Refresh();
        }

        private void Refresh()
        {
            for (int repIndex = 0; repIndex < _repetitions.Count; repIndex++)
            {
                var dX = _dX*(repIndex + 1);
                var dY = _dY*(repIndex + 1);
                RefreshStrokes(dX, dY, repIndex);
                RefreshUIElements(dX, dY, repIndex);
            }
        }

        private void RefreshUIElements(double dX, double dY, int repIndex)
        {
            for (int i = 0; i < _selectedElements.Count; i++)
            {
                var uiElement = _repetitions[repIndex].Obj1[i];
                var originalElement = _selectedElements[i];
                InkCanvas.SetLeft(uiElement, InkCanvas.GetLeft(originalElement) + dX);
                InkCanvas.SetTop(uiElement, InkCanvas.GetTop(originalElement) + dY);
            }
        }

        private void RefreshStrokes(double dX, double dY, int repIndex)
        {
            for (int i = 0; i < _selectedStrokes.Count; i++)
            {
                StylusPointCollection stylusPointCollection = null;
                foreach (var origStylusPoint in _selectedStrokes[i].StylusPoints.ToList())
                {
                    if (stylusPointCollection == null)
                    {
                        stylusPointCollection = new StylusPointCollection { CreateMovedStylesPoint(origStylusPoint, dX, dY) };
                    }
                    else
                    {
                        stylusPointCollection.Add(CreateMovedStylesPoint(origStylusPoint, dX, dY));
                    }
                }
                _repetitions[repIndex].Obj2[i].StylusPoints = stylusPointCollection;
            }
        }

        private static StylusPoint CreateMovedStylesPoint(StylusPoint stylusPoint, double dX, double dY)
        {
            return new StylusPoint(stylusPoint.X + dX, stylusPoint.Y + dY);
        }

        public void MouseUp(InkCanvas inkCanvas, MouseButtonEventArgs e)
        {
            if (ProcessRepeateSelectionState == RepeateSelectionState.WaitingStartPoint)
            {
                _startPoint = e.GetPosition(inkCanvas);
                AddCopy(inkCanvas);
                _overlayStroke = new OverlayRepeatOperationStroke(new StylusPointCollection
                                                                        {
                                                                            new StylusPoint(_startPoint.X, _startPoint.Y),
                                                                            new StylusPoint(_startPoint.X, _startPoint.Y)
                                                                        });
                inkCanvas.Strokes.Add(_overlayStroke);
                _overlayStroke.ProcessRepeateSelectionState = ProcessRepeateSelectionState = RepeateSelectionState.WaitingEndPoint;
            }
        }

        public void MouseWheel(InkCanvas inkCanvas, MouseWheelEventArgs e)
        {
            if (ProcessRepeateSelectionState != RepeateSelectionState.WaitingEndPoint)
            {
                return;
            }
            if (e.Delta < 0 && _repetitions.Count > 1)
            {
                RemoveCopy(inkCanvas);
                Refresh();
            }
            else if (e.Delta > 0 && _repetitions.Count < 100)
            {
                AddCopy(inkCanvas);
                Refresh();
            }
        }
    }
}
