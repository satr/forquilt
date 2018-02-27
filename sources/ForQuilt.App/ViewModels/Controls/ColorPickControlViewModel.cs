//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ForQuilt.App.Models;

namespace ForQuilt.App.ViewModels.Controls
{
    class ColorPickControlViewModel
    {
        private readonly Rectangle _pickedColor;
        private readonly Slider _rColorSlider;
        private readonly Slider _gColorSlider;
        private readonly Slider _bColorSlider;
        private bool _isColorSetBySlider = true;

        public ColorPickControlViewModel(StackPanel container, Rectangle pickedColor, Slider rColorSlider, Slider gColorSlider, Slider bColorSlider)
        {
            _pickedColor = pickedColor;
            _rColorSlider = rColorSlider;
            _gColorSlider = gColorSlider;
            _bColorSlider = bColorSlider;
            _rColorSlider.ValueChanged += ColorSliderOnValueChanged;
            _gColorSlider.ValueChanged += ColorSliderOnValueChanged;
            _bColorSlider.ValueChanged += ColorSliderOnValueChanged;
            foreach (var stackPanel in container.Children.OfType<StackPanel>())
            {
                foreach (var button in stackPanel.Children.OfType<Button>())
                {
                    button.Click += OnClickColorButton;
                }
            }
        }

        private void OnClickColorButton(object sender, RoutedEventArgs e)
        {
            var button = ((Button) sender);
            try
            {
                _isColorSetBySlider = false;
                _pickedColor.Fill = button.Background;
                var brush = _pickedColor.Fill as SolidColorBrush;
                if (brush != null)
                {
                    ModelStorage.WorkAreaModel.CurrentColor = brush.Color;
                } 
            }
            finally
            {
                _isColorSetBySlider = true;
            }
        }

        private void ColorSliderOnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> routedPropertyChangedEventArgs)
        {
            if (!_isColorSetBySlider)
            {
                return;
            }
            var color = new Color();
            color.A = 0xFF;
            color.R = (byte) _rColorSlider.Value;
            color.G = (byte) _gColorSlider.Value;
            color.B = (byte) _bColorSlider.Value;
            _pickedColor.Fill = new SolidColorBrush(color);
        }
    }
}
