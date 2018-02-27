//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using System;

namespace ForQuilt.App.Helpers
{
    [Serializable]
    public class Tuple<T1, T2>
    {
        public T1 Obj1 { get; set; }
        public T2 Obj2 { get; set; }

        public Tuple()
        {
        }

        public Tuple(T1 obj1, T2 obj2)
        {
            Obj1 = obj1;
            Obj2 = obj2;
        }
    }
}