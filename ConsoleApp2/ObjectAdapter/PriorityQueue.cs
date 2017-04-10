using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ass1
{
    public class PriorityQueue<T>
    {
        private Comperator<T> comperator;
        public PriorityQueue(Comperator<T> comperator)
        {
            this.comperator = comperator;
        }
       // public void add()
    }
}
