//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Ink;
using ForQuilt.App.Helpers;
using ForQuilt.App.Models;

namespace ForQuilt.App.Commands.WorkArea.Ordering
{
    internal abstract class WorkAreaOrderingCommandBase : RegisteredCommandBase
    {
        public override void Execute(object parameter)
        {
            var inkCanvas = ModelStorage.WorkAreaModel.CurrentInkCanvas;
            var selectedStrokes = inkCanvas.GetSelectedStrokes();
            ReorderItems<IList, Stroke>(selectedStrokes, inkCanvas.Strokes);
            var selectedElements = inkCanvas.GetSelectedElements().ToList();//copying because collection will be changed during reordering
            var uiElementCollection = inkCanvas.Children;
            ReorderItems<IList, UIElement>(selectedElements, uiElementCollection);
            inkCanvas.Select(selectedStrokes, selectedElements);
        }

        private void ReorderItems<TList, TItem>(TList selectedItemList, TList itemList)
            where TList: IList
        {
            if (selectedItemList.Count >= itemList.Count)
            {
                return;
            }
            var tuples = new List<Tuple<int, TItem>>();
            foreach (var item in selectedItemList)
            {
                tuples.Add(new Tuple<int, TItem>(itemList.IndexOf(item), (TItem)item));
                itemList.Remove(item);
            }
            foreach (var tuple in OrderTuples(tuples))
            {
                PutInto(itemList, tuple.Obj2);
            }
        }

        protected abstract IOrderedEnumerable<Tuple<int, T>> OrderTuples<T>(List<Tuple<int, T>> tuples);

        protected abstract void PutInto<TList, TItem>(TList list, TItem item) 
            where TList: IList;
    }
}