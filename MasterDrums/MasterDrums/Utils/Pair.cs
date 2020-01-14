using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDrums.Utils
{
    class Pair<T1, T2>
    {
        public Pair(T1 i1, T2 i2)
        {
            this.Item1 = i1;
            this.Item2 = i2;
        }

        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
    }
}
