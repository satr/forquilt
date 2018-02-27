//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Windows.Ink;
using ForQuilt.App.Models.Strokes;

namespace ForQuilt.App.Models
{
    static class ModelStorage
    {
        private static readonly Dictionary<Type, object> Models = new Dictionary<Type, object>();

        private static T GetModel<T>() 
            where T:new()
        {
            if (!Models.ContainsKey(typeof (T)))
            {
                Models.Add(typeof(T), new T());
            }
            return (T) Models[typeof (T)];
        }

        public static WorkAreaModel WorkAreaModel
        {
            get
            {
                return GetModel<WorkAreaModel>();
            }
        }

        public static DrawShapeModel<LineStroke> DrawLineModel
        {
            get
            {
                return GetModel<DrawShapeModel<LineStroke>>();
            }
        }
        public static DrawRectangleModel DrawRectangleModel
        {
            get
            {
                return GetModel<DrawRectangleModel>();
            }
        }

        public static DrawShapeModel<EllipseStroke> DrawEllipseModel
        {
            get
            {
                return GetModel<DrawShapeModel<EllipseStroke>>();
            }
        }

        public static RepeateSelectionModel RepeateSelectionModel
        {
            get
            {
                return GetModel<RepeateSelectionModel>();
            }
        }

        public static ClipboardModel ClipboardModel
        {
            get
            {
                return GetModel<ClipboardModel>();
            }
        }
    }
}
