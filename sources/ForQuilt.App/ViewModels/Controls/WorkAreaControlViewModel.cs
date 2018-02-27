//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using System.Windows.Controls;
using ForQuilt.App.Models;

namespace ForQuilt.App.ViewModels.Controls
{
    class WorkAreaControlViewModel
    {    
        public void SetCurrentWorkAreaCanvas(InkCanvas inkCanvas, RectangleSelectionControlViewModel selectorViewModel)
        {
            ModelStorage.WorkAreaModel.SetCurrentCanvas(inkCanvas, selectorViewModel);
        }

        public InkCanvas ClipboardCanvas
        {
            set { ModelStorage.WorkAreaModel.ClipboardCanvas = value; }
        }
    }
}
