//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System;
using System.Timers;
using System.Windows.Controls;
using ForQuilt.App.Views.Controls;

namespace ForQuilt.App.ViewModels.Controls
{
    class TimerControlViewModel: IDisposable
    {
        private TimerControlView View { get; set; }
        private readonly Slider _timerSlider;
        private readonly Timer _timer;
        private bool _disposed;

        public TimerControlViewModel(TimerControlView view, Slider timerSlider)
        {
            View = view;
            _timerSlider = timerSlider;
            _timer = new Timer(1000);
            _timer.Elapsed += timer_Elapsed;
            timerSlider.ValueChanged += timerSlider_ValueChanged;
        }

        void timerSlider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            _timer.Stop();
            if (Math.Round(_timerSlider.Value, 0).Equals(0) || _timer.Enabled)
            {
                return;
            }
            _timer.Start();
            if (View.StartCommand != null)
            {
                View.StartCommand.Execute(null);
            }
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timerSlider.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(TimeElapsedAction));
        }

        private void TimeElapsedAction()
        {
            var newValue = Math.Round(_timerSlider.Value - 1, 0);
            if (newValue > 0)
            {
                _timerSlider.Value = newValue;
                return;
            }
            if (View.FinishCommand != null)
            {
                View.FinishCommand.Execute(null);
            }
            _timerSlider.Value = newValue;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            if (_timer != null)
            {
                _timer.Dispose();
            }
            _disposed = true;
        }
    }
}

