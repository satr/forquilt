using System.Windows;
using System.Windows.Ink;

namespace ForQuilt.App.Models
{
    class DrawRectangleModel : DrawShapeModel<Stroke>
    {
        protected override void MoveEndPoint(Point endPoint)
        {
            var startPoint = FirstLinePoint;
            RemovePointsAfterFirst();
            AddLinePoint(startPoint.X, endPoint.Y);
            AddLinePoint(endPoint.X, endPoint.Y);
            AddLinePoint(endPoint.X, startPoint.Y);
            PointCollection.Add(startPoint);
        }
    }
}