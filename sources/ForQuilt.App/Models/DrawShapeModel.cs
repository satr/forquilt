//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;

namespace ForQuilt.App.Models
{
    internal class DrawShapeModel<T> : DrawShapeModelBase where T: Stroke
    {
        private DrawingAttributes _drawingAttributes;
        private Stroke _shapeStroke;

        private enum DrawShapeState
        {
            Inactive,
            WaitingStartPoint,
            WaitingEndPoint,
        }

        public DrawShapeModel()
        {
            ProcessDrawShapeState = DrawShapeState.Inactive;
        }

        private DrawShapeState ProcessDrawShapeState { get; set; }
        
        public void Start(InkCanvas inkCanvas)
        {
            Break(inkCanvas);
            inkCanvas.DefaultDrawingAttributes.AttributeChanged += DrawingAttributesOnAttributeChanged;
            _drawingAttributes = inkCanvas.DefaultDrawingAttributes.Clone();
            ProcessDrawShapeState = DrawShapeState.WaitingStartPoint;
        }

        private void DrawingAttributesOnAttributeChanged(object sender, PropertyDataChangedEventArgs e)
        {
            if (_drawingAttributes == null)
            {
                return;
            }
            _drawingAttributes.AddPropertyData(e.PropertyGuid, e.NewValue);
        }

        public void Break(InkCanvas inkCanvas)
        {
            if (ProcessDrawShapeState != DrawShapeState.Inactive && !PointCollectionIsEmpty && PointCollection.Count == 1)
            {
                RemoveStrokesWithoutPoints(inkCanvas, _shapeStroke);
            }
            ProcessDrawShapeState = DrawShapeState.Inactive;
            inkCanvas.DefaultDrawingAttributes.AttributeChanged -= DrawingAttributesOnAttributeChanged;
        }

        public void MouseDown(InkCanvas inkCanvas, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (ProcessDrawShapeState != DrawShapeState.WaitingStartPoint)
            {
                return;
            }
            InitShape(inkCanvas, _drawingAttributes, mouseButtonEventArgs.GetPosition(inkCanvas));
            ProcessDrawShapeState = DrawShapeState.WaitingEndPoint;
        }

        public void MouseUp(InkCanvas inkCanvas, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (ProcessDrawShapeState != DrawShapeState.WaitingEndPoint)
            {
                return;
            }
            MoveEndPoint(mouseButtonEventArgs.GetPosition(inkCanvas));
            Start(inkCanvas);
        }

        public void MouseMove(InkCanvas inkCanvas, MouseEventArgs mouseEventArgs)
        {
            if (ProcessDrawShapeState != DrawShapeState.WaitingEndPoint)
            {
                return;
            }
            var point = mouseEventArgs.GetPosition(inkCanvas);
            MoveEndPoint(point);
        }

        protected virtual void MoveEndPoint(Point endPoint)
        {
            AddLinePoint(endPoint);
        }

        private static StylusPoint CreateLinePoint(Point point)
        {
            return new StylusPoint(point.X, point.Y);
        }

        private void InitShape(InkCanvas inkCanvas, DrawingAttributes drawingAttributes, Point point)
        {
            InitPointCollection(CreateLinePoint(point));
            _shapeStroke = (Stroke) Activator.CreateInstance(typeof (T), PointCollection);
            _shapeStroke.DrawingAttributes = drawingAttributes;
            inkCanvas.Strokes.Add(_shapeStroke);
        }

        protected StylusPoint AddLinePoint(double x, double y)
        {
            return AddLinePoint(new Point(x, y));
        }

        private StylusPoint AddLinePoint(Point point)
        {
            var stylusPoint = CreateLinePoint(point);
            PointCollection.Add(stylusPoint);
            return stylusPoint;
        }

        private static void RemoveStrokesWithoutPoints(InkCanvas inkCanvas, Stroke stroke)
        {
            if (stroke == null || !inkCanvas.Strokes.Contains(stroke))
            {
                return;
            }
            inkCanvas.Strokes.Remove(stroke);
        }

        protected void RemovePointBeforeLastPoint()
        {
            PointCollection.RemoveAt(PointCollection.Count - 2);
        }

        protected double Thickness
        {
            get{ return _drawingAttributes.Width;}
        }

        protected StylusPoint FirstLinePoint
        {
            get
            {
                return PointCollection[0];
            }
        }

        protected void RemoveFirstPoint()
        {
            if (PointCollection.Count <= 0)
            {
                return;
            }
            PointCollection.RemoveAt(0);
        }

        protected void RemovePointsAfterFirst()
        {
            while (PointCollection.Count > 1)
            {
                PointCollection.RemoveAt(1);
            }
        }
    }

    internal class DrawShapeModelBase
    {
        private StylusPointCollection _pointCollection;

        protected void InitPointCollection(StylusPoint stylusPoint)
        {
            _pointCollection = new StylusPointCollection { stylusPoint };
        }

        protected StylusPointCollection PointCollection
        {
            get
            {
                if (PointCollectionIsEmpty)
                {
                    throw new InvalidOperationException("PointCollection is empty");
                }
                return _pointCollection;
            }
        }

        protected bool PointCollectionIsEmpty
        {
            get { return _pointCollection == null || _pointCollection.Count <= 0; }
        }
    }
}