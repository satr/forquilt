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
using ForQuilt.App.Helpers;

namespace ForQuilt.App.Models.Strokes
{
    class LineStroke : ShapeStrokeBase
    {
        private readonly object _syncRootRect = new Object();

        public LineStroke([NotNull] StylusPointCollection stylusPoints) : base(stylusPoints)
        {
        }

        public LineStroke([NotNull] StylusPointCollection stylusPoints, [NotNull] DrawingAttributes drawingAttributes) : base(stylusPoints, drawingAttributes)
        {
        }

        protected override void DrawShapeCore(DrawingContext drawingContext)
        {
            var points = GetPoints();
            drawingContext.DrawLine(Pen, points.Obj1, points.Obj2); 
        }

        protected Tuple<Point, Point> GetPoints()
        {
            lock (_syncRootRect)
            {
                var startPoint = new Point(StylusPoints[0].X, StylusPoints[0].Y);
                LeaveOnlyFirstAndLastStylusPoints();
                var endPoint = (StylusPoints.Count == 1) ? startPoint : new Point(StylusPoints[1].X, StylusPoints[1].Y);
                return new Tuple<Point, Point>(startPoint, endPoint);
            }
        }
    }
}