//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;

namespace ForQuilt.App.Helpers
{
    class WPFHelper
    {
        public static DependencyProperty RegisterCommandProperty<T>(string commandName)
        {
            return RegisterCommandProperty<T>(commandName, null);
        }

        public static DependencyProperty RegisterCommandProperty<T>(string commandName, PropertyChangedCallback defaultValue)
        {
            return DependencyProperty.Register(commandName, typeof(ICommand), typeof(T), new UIPropertyMetadata(defaultValue));
        }

        public static UIElement Clone(UIElement uiElement, InkCanvas inkCanvas)
        {
            var image = uiElement as Image;
            if (image != null)
            {
                return new Image { Source = image.Source.Clone() };
            }
            return null;
        }

        public static void CloneStrokesTo(StrokeCollection targetCollection, IEnumerable<Stroke> sourceCollection)
        {
            CloneStrokesTo(targetCollection, sourceCollection, 0);
        }

        public static void CloneStrokesTo(StrokeCollection targetCollection, IEnumerable<Stroke> sourceCollection, int offset)
        {
            var strokes = new StrokeCollection();
            foreach (var stroke in sourceCollection)
            {
                strokes.Add(stroke.Clone());
            }
            if (offset != 0)
            {
                MoveStrokesBy(offset, strokes);
            }
            targetCollection.Add(strokes);
        }

        private static void MoveStrokesBy(int offset, StrokeCollection strokes)
        {
            var transformMatrix = new Matrix();
            transformMatrix.Translate(offset, offset);
            strokes.Transform(transformMatrix, false);
        }

        public static void CloneElementsTo(IList<UIElement> targetCollection, IEnumerator sourceCollection,
                                           InkCanvas inkCanvas)
        {
            CloneElementsTo(targetCollection, sourceCollection, inkCanvas, 0);
        }

        public static void CloneElementsTo(IList<UIElement> targetCollection, IEnumerator sourceCollection, InkCanvas inkCanvas, int offset)
        {
            while (sourceCollection.MoveNext())
            {
                if (!(sourceCollection.Current is UIElement))
                {
                    return;
                }
                var element = (UIElement) sourceCollection.Current;
                var clone = Clone(element, inkCanvas);
                if (clone == null)
                {
                    continue;
                }
                targetCollection.Add(clone);
                InkCanvas.SetLeft(clone, InkCanvas.GetLeft(element) + offset);
                InkCanvas.SetTop(clone, InkCanvas.GetTop(element) + offset);

            }
        }
    }
}
