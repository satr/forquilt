//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using ForQuilt.App.Annotations;

namespace ForQuilt.App.Models.Strokes
{
    class RectangleStroke: ShapeStrokeBase
    {
        private readonly object _syncRootRect = new Object();

        public RectangleStroke([NotNull] StylusPointCollection stylusPoints)
            : base(stylusPoints)
        {
        }

        public RectangleStroke([NotNull] StylusPointCollection stylusPoints, [NotNull] DrawingAttributes drawingAttributes) : base(stylusPoints, drawingAttributes)
        {
        }

        protected override void DrawShapeCore(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(null, Pen, GetRect());
        }

        public override Rect GetBounds()
        {
            return GetRect();
        }

        protected Rect GetRect()
        {
            lock (_syncRootRect)
            {
                var startPoint = this.StylusPoints[0];
                LeaveOnlyFirstAndLastStylusPoints();
                var endPoint = (StylusPoints.Count == 1) ? startPoint : this.StylusPoints[1];
                var x = (startPoint.X <= endPoint.X) ? startPoint.X : endPoint.X;
                var y = (startPoint.Y <= endPoint.Y) ? startPoint.Y : endPoint.Y;
                return new Rect(x, y, Math.Abs(endPoint.X - startPoint.X), Math.Abs(endPoint.Y - startPoint.Y));
            }
        }
    }
}
