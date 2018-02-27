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
    class EllipseStroke : ShapeStrokeBase
    {
        private readonly object _syncRootRect = new Object();

        public EllipseStroke([NotNull] StylusPointCollection stylusPoints) : base(stylusPoints)
        {
        }

        public EllipseStroke([NotNull] StylusPointCollection stylusPoints, [NotNull] DrawingAttributes drawingAttributes) : base(stylusPoints, drawingAttributes)
        {
        }

        protected override void DrawShapeCore(DrawingContext drawingContext)
        {
            var points = GetPoints();
            drawingContext.DrawEllipse(null, Pen, points.Obj1, points.Obj2.X, points.Obj2.Y);
        }

        public override Rect GetBounds()
        {
            var points = GetPoints();
            return new EllipseGeometry(points.Obj1, points.Obj2.X, points.Obj2.Y).Bounds;
        }

        private Tuple<Point, Point> GetPoints()
        {
            lock (_syncRootRect)
            {
                var centerPoint = new Point(StylusPoints[0].X, StylusPoints[0].Y);
                LeaveOnlyFirstAndLastStylusPoints();
                var radiusPoint = (StylusPoints.Count == 1) ? new Point() : new Point(StylusPoints[1].X - StylusPoints[0].X, StylusPoints[1].Y - StylusPoints[0].Y);
                return new Tuple<Point, Point>(centerPoint, radiusPoint);
            }
        }
    }
}