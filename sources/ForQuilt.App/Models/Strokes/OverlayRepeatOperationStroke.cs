//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using System.Globalization;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ForQuilt.App.Models.Strokes
{
    class OverlayRepeatOperationStroke : LineStroke
    {
        public OverlayRepeatOperationStroke(StylusPointCollection stylusPoints) : base(stylusPoints)
        {
            Pen = new Pen(new SolidColorBrush(){Color = Colors.Red}, 2);
        }

        public OverlayRepeatOperationStroke(StylusPointCollection stylusPoints, DrawingAttributes drawingAttributes) : base(stylusPoints, drawingAttributes)
        {
        }

        public RepeateSelectionModel.RepeateSelectionState ProcessRepeateSelectionState { get; set; }

        protected override void DrawShapeCore(DrawingContext drawingContext)
        {
            var points = GetPoints();
            if (ProcessRepeateSelectionState == RepeateSelectionModel.RepeateSelectionState.WaitingEndPoint)
            {
                drawingContext.DrawLine(Pen, points.Obj1, points.Obj2);
            }
        }
    }
}