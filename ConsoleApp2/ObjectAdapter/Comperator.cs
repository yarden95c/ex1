using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;

namespace Ass1
{
    public interface Comperator<T>
    {
        bool Compare(State<T> state1, State<T> state2);
    }
}
