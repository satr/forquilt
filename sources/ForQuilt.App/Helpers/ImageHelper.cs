//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ForQuilt.App.Properties;
using Image = System.Windows.Controls.Image;
using Size = System.Windows.Size;

namespace ForQuilt.App.Helpers
{
    class ImageHelper
    {

        private static readonly Func<BitmapEncoder> BmpEncoderActivator = () => new BmpBitmapEncoder();
        private static readonly IDictionary<string, Func<BitmapEncoder>> Encoders = new Dictionary<string, Func<BitmapEncoder>>
            {
                {".jpg", () => new JpegBitmapEncoder()},
                {".png", () => new PngBitmapEncoder()},
                {".bmp", BmpEncoderActivator}
            };

        public static Image AddImageTo(InkCanvas inkCanvas, Bitmap bitmap)
        {
            using (Stream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Bmp);
                return AddImageTo(inkCanvas, stream);
            }
        }

        public static Image AddImageTo(InkCanvas inkCanvas, Stream stream)
        {
            var image = GetImageFrom(GetBitmapImageFrom(stream));
            inkCanvas.Children.Add(image);
            return image;
        }

        public static Image GetImageFrom(BitmapImage bitmapImage)
        {
            return new Image {Source = bitmapImage};
        }

        public static BitmapEncoder EncodeToBitmap(FileInfo fileInfo, Rect rect, UIElement uiElement)
        {
            return EncodeToBitmap(rect, uiElement, GetEncoderActivator(fileInfo));
        }

        private static Func<BitmapEncoder> GetEncoderActivator(FileInfo fileInfo)
        {
            string fileExtension = fileInfo.Extension.ToLower();
            if (!Encoders.ContainsKey(fileExtension))
            {
                throw new ArgumentException(String.Format("{0}: \"{1}\"", Resources.Message_File_extension_is_not_supported, fileExtension));
            }
            var encoderActivator = Encoders[fileExtension];
            return encoderActivator;
        }

        public static BitmapEncoder EncodeToBitmap(Rect rect, UIElement uiElement, Func<BitmapEncoder> encoderActivator)
        {
            var size = new Size(rect.Width, rect.Height);
            uiElement.Measure(size);
            uiElement.Arrange(rect);
            var renderTargetBitmap = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, PixelFormats.Default);
            renderTargetBitmap.Render(uiElement);
            var encoder = encoderActivator();
            encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
            return encoder;
        }        

        public static BitmapImage GetBitmapImageFrom(Stream stream)
        {
            var bitmapImage = new BitmapImage();
            if (stream == null)
            {
                return bitmapImage;
            }
            stream.Seek(0, SeekOrigin.Begin);
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = stream;
            bitmapImage.EndInit();
            return bitmapImage;
        }

        public static BitmapImage GetCroppedImageFromInkCanvas(Rect rect, InkCanvas inkCanvas)
        {
            if (inkCanvas == null)
            {
                throw new ArgumentException("InkCanvas is not initialised");
            }
            var originalMargin = inkCanvas.Margin;
            inkCanvas.Margin = new Thickness(0, 0, 0, 0);
            var croppedImage = GetCroppedImageFromUIElement(rect, inkCanvas);
            inkCanvas.Margin = originalMargin;
            return croppedImage;
        }

        public static BitmapImage GetCroppedImageFromUIElement(Rect rect, UIElement uiElement)
        {
            using (var stream = new MemoryStream())
            {
                var rectFromLeftTop = new Rect(0, 0, uiElement.RenderSize.Width, uiElement.RenderSize.Height);
                EncodeToBitmap(rectFromLeftTop, uiElement, BmpEncoderActivator).Save(stream);
                return GetCroppedImageFromStream(rect, stream);
            }
        }

        public static BitmapImage GetCroppedImageFromStream(Rect rect, MemoryStream stream)
        {
            var bitmapImage = GetBitmapImageFrom(stream);
            if (rect.Width > bitmapImage.Width)
            {
                rect.Width = bitmapImage.Width;
            }
            if (rect.Height > bitmapImage.Height)
            {
                rect.Height = bitmapImage.Height;
            } 
            var sourceRect = GeometryHelper.ConvertToInt32Rect(rect);
            var croppedBitmap = new CroppedBitmap(bitmapImage, sourceRect);
            var encoder = BmpEncoderActivator();
            encoder.Frames.Add(BitmapFrame.Create(croppedBitmap));
            using (var outStream = new MemoryStream())
            {
                encoder.Save(outStream);
                return GetBitmapImageFrom(outStream);
            }
        }

        public static void SaveBitmapImage(FileInfo fileInfo, BitmapImage image)
        {
            using (var fs = fileInfo.Create())
            {
                SaveBitmapImage(fileInfo, image, fs);
            }
        } 

        public static void SaveBitmapImage(FileInfo fileInfo, BitmapImage image, FileStream fs)
        {
            var encoder = GetEncoderActivator(fileInfo)();
            encoder.Frames.Add(BitmapFrame.Create(image));
            encoder.Save(fs);
        }
    }
}
