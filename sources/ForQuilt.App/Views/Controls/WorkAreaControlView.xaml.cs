//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using ForQuilt.App.Helpers;
using ForQuilt.App.ViewModels.Controls;
using ForQuilt.App.ViewModels;

namespace ForQuilt.App.Views
{
    /// <summary>
    /// Interaction logic for WorkAreaControlView.xaml
    /// </summary>
    public partial class WorkAreaControlView
    {
        private readonly Dictionary<int, Tuple<InkCanvas, RectangleSelectionControlViewModel>> _inkCanvasList = new Dictionary<int, Tuple<InkCanvas, RectangleSelectionControlViewModel>>();
        private readonly WorkAreaControlViewModel _workAreaControlViewModel = new WorkAreaControlViewModel();

        public WorkAreaControlView()
        {
            InitializeComponent();
            DataContext = _workAreaControlViewModel;
            Register(MainWorkTableCanvas, MainWorkTableCanvasSelector.ViewModel);
            Register(SketchWorkTableCanvas, SketchWorkTableCanvasSelector.ViewModel);
            _workAreaControlViewModel.ClipboardCanvas = ClipboardCanvas;
            SetCurrentWorkAreaCanvas(WorkAreas);
        }

        private void Register(InkCanvas inkCanvas, RectangleSelectionControlViewModel viewModel)
        {
            _inkCanvasList.Add(_inkCanvasList.Count, new Tuple<InkCanvas, RectangleSelectionControlViewModel>(inkCanvas, viewModel));
        }

        private void WorkAreaChanged(object sender, SelectionChangedEventArgs e)
        {
            SetCurrentWorkAreaCanvas(((TabControl) sender));
        }

        private void SetCurrentWorkAreaCanvas(Selector tabControl)
        {
            var selectedItemIndex = tabControl.SelectedIndex;
            if (selectedItemIndex < 0 
                || !_inkCanvasList.ContainsKey(selectedItemIndex))
            {
                return;
            }
            _workAreaControlViewModel.SetCurrentWorkAreaCanvas(_inkCanvasList[selectedItemIndex].Obj1, _inkCanvasList[selectedItemIndex].Obj2);
        }        
    }
}
