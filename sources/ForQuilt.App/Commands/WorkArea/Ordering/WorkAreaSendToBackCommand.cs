//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;
using ForQuilt.App.Helpers;

namespace ForQuilt.App.Commands.WorkArea.Ordering
{
    class WorkAreaSendToBackCommand : WorkAreaOrderingCommandBase
    {
        protected override IOrderedEnumerable<Tuple<int, T>> OrderTuples<T>(List<Tuple<int, T>> tuples)
        {
            return tuples.OrderByDescending(tuple => tuple.Obj1);
        }

        protected override void PutInto<TCollection, TItem>(TCollection list, TItem item)
        {
            list.Insert(0, item);
        }
    }
}