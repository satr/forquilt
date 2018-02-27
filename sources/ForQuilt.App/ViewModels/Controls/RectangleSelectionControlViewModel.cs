//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using ForQuilt.App.Views.Controls;

namespace ForQuilt.App.ViewModels.Controls
{
    public class RectangleSelectionControlViewModel
    {
        private readonly RectangleSelectionControlView _view;
        private readonly Rectangle _selectionBoxControl;
        private readonly Grid _gridControl;
        private bool _mouseIsDown;
        private Point _startMousePos;
        private Rect _selectedRect;

        public RectangleSelectionControlViewModel(RectangleSelectionControlView view, Grid gridControl, Rectangle selectionBoxControl)
        {
            _view = view;
            _selectionBoxControl = selectionBoxControl;
            _gridControl = gridControl;
            gridControl.MouseDown += Grid_MouseDown;
            gridControl.MouseUp += Grid_MouseUp;
            gridControl.MouseMove += Grid_MouseMove;
        }

        private Rectangle SelectionBoxControl
        {
            get { return _selectionBoxControl; }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!SelectionInProgress)
            {
                return;
            }
            _mouseIsDown = true;
            _startMousePos = e.GetPosition(_gridControl);
            _gridControl.CaptureMouse();

            Canvas.SetLeft(SelectionBoxControl, _startMousePos.X);
            Canvas.SetTop(SelectionBoxControl, _startMousePos.Y);
            SelectionBoxControl.Width = 0;
            SelectionBoxControl.Height = 0;

            SelectionBoxControl.Visibility = Visibility.Visible;
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!SelectionInProgress)
            {
                return;
            }
            _mouseIsDown = false;
            _gridControl.ReleaseMouseCapture();

            var endMousePos = e.GetPosition(_gridControl);

            var startX = _startMousePos.X < 0 ? 0 : _startMousePos.X;
            var endX = endMousePos.X < 0 ? 0 : endMousePos.X;
            var x = startX < endX? startX: endX;
            var startY = _startMousePos.Y < 0 ? 0 : _startMousePos.Y;
            var endY = endMousePos.Y < 0 ? 0 : endMousePos.Y;
            var y = startY < endY? startY: endY;
            _selectedRect = new Rect(x, y, Math.Abs(startX - endX), Math.Abs(startY - endY));
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_mouseIsDown || !SelectionInProgress)
            {
                return;
            }

            var mousePos = e.GetPosition(_gridControl);
            if (mousePos.X < 0 || mousePos.Y < 0)
            {
                return;
            }

            if (_startMousePos.X < mousePos.X)
            {
                Canvas.SetLeft(SelectionBoxControl, _startMousePos.X);
                SelectionBoxControl.Width = mousePos.X - _startMousePos.X;
            }
            else
            {
                Canvas.SetLeft(SelectionBoxControl, mousePos.X);
                SelectionBoxControl.Width = _startMousePos.X - mousePos.X;
            }

            if (_startMousePos.Y < mousePos.Y)
            {
                Canvas.SetTop(SelectionBoxControl, _startMousePos.Y);
                SelectionBoxControl.Height = mousePos.Y - _startMousePos.Y;
            }
            else
            {
                Canvas.SetTop(SelectionBoxControl, mousePos.Y);
                SelectionBoxControl.Height = _startMousePos.Y - mousePos.Y;
            }
        }

        public void BeginSelection()
        {
            _view.Visibility = Visibility.Visible;
            SelectionInProgress = true;
        }

        public void EndSelection()
        {
            SelectionBoxControl.Visibility = Visibility.Collapsed;
            _view.Visibility = Visibility.Collapsed;
            _mouseIsDown = SelectionInProgress = false;
        }

        public bool SelectionInProgress { get; private set; }

        public Rect SelectedArea()
        {
            return _selectedRect;
        }
    }
}
