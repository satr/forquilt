//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System.Windows;

namespace ForQuilt.App.Helpers
{
    class GeometryHelper
    {
        public static Int32Rect ConvertToInt32Rect(Rect rect)
        {
            return new Int32Rect((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
        }
    }
}
