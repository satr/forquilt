//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Ink;

namespace ForQuilt.App.Helpers
{
    static class SelectionHelper
    {
        static readonly ICollection<InkCanvas> SelectionChangedInProgress = new Collection<InkCanvas>();

        public static void SelectionChanged(InkCanvas inkCanvas, ICollection<StrokeCollection> groupedStrokesCollections)
        {

            lock (SelectionChangedInProgress)
            {
                if (SelectionChangedInProgress.Contains(inkCanvas))
                {
                    return;
                }
                SelectionChangedInProgress.Add(inkCanvas);
            }
            try
            {
                var selectedStrokes = inkCanvas.GetSelectedStrokes();
                var uniqueStrokesCollection = new HashSet<Stroke>(selectedStrokes);
                foreach (var groupedStrokesCollection in groupedStrokesCollections)
                {
                    if (!groupedStrokesCollection.Any(uniqueStrokesCollection.Contains))
                    {
                        continue;
                    }
                    foreach (var stroke in groupedStrokesCollection)
                    {
                        uniqueStrokesCollection.Add(stroke);
                    }
                }
                inkCanvas.Select(new StrokeCollection(uniqueStrokesCollection), inkCanvas.GetSelectedElements());
            }
            finally
            {
                lock (SelectionChangedInProgress)
                {
                    if (SelectionChangedInProgress.Contains(inkCanvas))
                    {
                        SelectionChangedInProgress.Remove(inkCanvas);
                    }
                }
            }
        }

        public static void GroupSelectedDrawings(InkCanvas inkCanvas, ICollection<StrokeCollection> groupedStrokesCollections)
        {
            var groupedStrokeCollection = new StrokeCollection(inkCanvas.GetSelectedStrokes());
            UnGroupSelectedDrawings(inkCanvas, groupedStrokesCollections);
            groupedStrokesCollections.Add(groupedStrokeCollection);
        }

        public static void UnGroupSelectedDrawings(InkCanvas inkCanvas, ICollection<StrokeCollection> groupedStrokesCollections)
        {
            foreach (var selectedStroke in inkCanvas.GetSelectedStrokes())
            {
                var stroke = selectedStroke;
                groupedStrokesCollections.Where(strokeCollection => strokeCollection.Contains(stroke))
                                          .ToList()
                                          .ForEach(strokeCollection => strokeCollection.Remove(stroke));
            }
            groupedStrokesCollections.Where(strokeCollection => strokeCollection.Count == 0)
                                      .ToList()
                                      .ForEach(strokeCollection => groupedStrokesCollections.Remove(strokeCollection));
        }

        public static void ClearSelection(InkCanvas inkCanvas)
        {
            inkCanvas.Select(new StrokeCollection());
        }
    }
}
