//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using ForQuilt.App.Commands.AddImages.AddImageCapturedFromCamera;
using ForQuilt.App.Helpers;
using ForQuilt.App.Models;
using ForQuilt.App.Views;
using SnapShot;
using Timer = System.Threading.Timer;

namespace ForQuilt.App.ViewModels
{
    class AddImageCapturedFromCameraViewModel: IDisposable
    {
        private readonly AddImageCapturedFromCameraView _view;
        private readonly PictureBox _capturePreviewArea;
        private readonly PictureBox _captureArea;
        private readonly bool _isInDesignMode;
        private Timer _timer;
        private bool _disposed;
        private Capture _capture;
        IntPtr _capturedBitmapPtr = IntPtr.Zero;

        public AddImageCapturedFromCameraViewModel(AddImageCapturedFromCameraView view, PictureBox capturePreviewArea, PictureBox captureArea, bool isInDesignMode)
        {
            _view = view;
            _capturePreviewArea = capturePreviewArea;
            _captureArea = captureArea;
            _isInDesignMode = isInDesignMode;
            PauseCommand = new AddImageCapturedFromCameraPauseCommand(this);
            RepeatCommand = new AddImageCapturedFromCameraRepeatCommand(this);
            ImportCommand = new AddImageCapturedFromCameraAddToWorkTableCommand(this);
            CancelCommand = new AddImageCapturedFromCameraCancelCommand(this);
            view.Loaded += view_Loaded;
            EventBroker.Instance.OnImportImageFromCameraViewExit += (sender, args) => Dispose();
            view.Closing += (sender, args) => Dispose();
            _captureArea = captureArea;
        }

        public ICommand CancelCommand { get; set; }
        public ICommand ImportCommand { get; set; }
        public ICommand RepeatCommand { get; set; }
        public ICommand PauseCommand { get; set; }

        private void ReleasePreviousBuffer()
        {
            if (_capturedBitmapPtr != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(_capturedBitmapPtr);
                _capturedBitmapPtr = IntPtr.Zero;
            }
        }

        private void view_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isInDesignMode)
            {
                return;
            }
            const int deviceIndex = 0; // zero based index of video capture device to use
            const int videoWidth = 640; // Depends on video device caps
            const int videoHeight = 480; // Depends on video device caps
            const int videoBitsPerPixel = 24; // BitsPerPixel values determined by device
            try
            {
                _capture = new Capture(deviceIndex, videoWidth, videoHeight, videoBitsPerPixel, _capturePreviewArea);
            }
            catch (NullReferenceException ex)
            {
                //TODO
            }
            catch (Exception ex)
            {
                //TODO
            }
            CaptureFrame(null);
        }

        private void StartCameraCapturingTimer()
        {
            DisposeCameraCapturingTimer();
            _timer = new Timer(CaptureFrame, null, 0, 100);
        }

        private void CaptureFrame(object state)
        {
            if (_captureArea == null || _capturePreviewArea == null || _capture == null)
            {
                return;//TODO
            }
            CopyFrame();
        }

        private void CopyFrame()
        {
            if (_captureArea.InvokeRequired)
            {
                _captureArea.Invoke(new Action(InvokeCopyPasteFrame));
                return;
            }
            InvokeCopyPasteFrame();
        }

        private void InvokeCopyPasteFrame()
        {
            ReleasePreviousBuffer();
            if (_disposed || _capture == null)
            {
                return;
            }
            _capturedBitmapPtr = _capture.Click();
            _captureArea.Image = new Bitmap(_capture.Width, _capture.Height, _capture.Stride, PixelFormat.Format24bppRgb, _capturedBitmapPtr);
            FlipImageVertically(_captureArea.Image);
        }

        private static void FlipImageVertically(Image image)
        {
            image.RotateFlip(RotateFlipType.RotateNoneFlipY);
        }

        public bool CanBeCaptured
        {
            get { return _captureArea != null && _capture != null && _timer != null; }
        }

        public bool CanBeImported
        {
            get { return _captureArea != null && _captureArea.Image != null; }
        }

        public void PauseCapture()
        {
            DisposeCameraCapturingTimer();
            CaptureFrame(null);
        }

        public void RepeatCapture()
        {
            StartCameraCapturingTimer();
        }

        public void ImportAndClose()
        {
            if (CanBeImported)
            {
                ImageHelper.AddImageTo(ModelStorage.WorkAreaModel.CurrentInkCanvas, new Bitmap(_captureArea.Image));
            }
            CloseView();
        }

        public void CloseView()
        {
            _view.Close();
        }

        private void DisposeCameraCapturingTimer()
        {
            if (_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            DisposeCameraCapturingTimer();
            if (_capture != null)
            {
                _capture.Dispose();
            }
            ReleasePreviousBuffer(); 
            _disposed = true;
        }
    }
}
