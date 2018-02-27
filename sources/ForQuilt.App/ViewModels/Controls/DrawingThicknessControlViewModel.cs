//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using ForQuilt.App.Models;

namespace ForQuilt.App.ViewModels.Controls
{
    class DrawingThicknessControlViewModel
    {
        public decimal Value
        {
            get { return (decimal) ModelStorage.WorkAreaModel.DrawingThickness; }
            set
            {
                ModelStorage.WorkAreaModel.DrawingThickness = (double) value;
            }
        }
    }
}
