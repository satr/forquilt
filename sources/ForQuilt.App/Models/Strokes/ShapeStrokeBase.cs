//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using ForQuilt.App.Annotations;

namespace ForQuilt.App.Models.Strokes
{
    internal abstract class ShapeStrokeBase : Stroke
    {
        private bool _drawingIsInProcess;
        private readonly object _syncRootDraw = new Object();

        protected ShapeStrokeBase([NotNull] StylusPointCollection stylusPoints) : base(stylusPoints)
        {
            Pen = new Pen();
            InitPen();
        }

        protected ShapeStrokeBase([NotNull] StylusPointCollection stylusPoints, [NotNull] DrawingAttributes drawingAttributes) : base(stylusPoints, drawingAttributes)
        {
            Pen = new Pen();
            InitPen();
        }

        protected Pen Pen { get; set; }

        protected override void OnDrawingAttributesChanged(PropertyDataChangedEventArgs e)
        {
            base.OnDrawingAttributesChanged(e);
            InitPen();
        }

        protected override void OnDrawingAttributesReplaced(DrawingAttributesReplacedEventArgs e)
        {
            base.OnDrawingAttributesReplaced(e);
            InitPen();
        }

        internal void InitPen()
        {
            Pen = new Pen(new SolidColorBrush(DrawingAttributes.Color), DrawingAttributes.Width);
        }

        protected override void DrawCore(DrawingContext drawingContext, DrawingAttributes drawingAttributes)
        {
            lock (_syncRootDraw)//possible performance issues?
            {
                if (_drawingIsInProcess)
                {
                    return;
                }
                _drawingIsInProcess = true;
            }
            try
            {
                DrawShapeCore(drawingContext);
            }
            finally
            {
                _drawingIsInProcess = false;
            }
        }

        protected abstract void DrawShapeCore(DrawingContext drawingContext);

        protected void LeaveOnlyFirstAndLastStylusPoints()
        {
            while (StylusPoints.Count > 2)
            {
                StylusPoints.RemoveAt(1);
            }
        }
    }
}