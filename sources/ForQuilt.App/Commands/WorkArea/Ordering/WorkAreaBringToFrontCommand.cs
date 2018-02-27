//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;
using ForQuilt.App.Helpers;

namespace ForQuilt.App.Commands.WorkArea.Ordering
{
    class WorkAreaBringToFrontCommand : WorkAreaOrderingCommandBase
    {
        protected override IOrderedEnumerable<Tuple<int, T>> OrderTuples<T>(List<Tuple<int, T>> tuples)
        {
            return tuples.OrderBy(tuple => tuple.Obj1);
        }

        protected override void PutInto<TList, TItem>(TList list, TItem item)
        {
            list.Add(item);
        }
    }
}
