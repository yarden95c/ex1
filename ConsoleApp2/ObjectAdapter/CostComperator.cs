using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
namespace Ass1
{
    public class CostComperator : Comperator<double>
    {
        public bool Compare(State<double> state1, State<double> state2)
        {
            return state1.Cost > state2.Cost;
        }
    }
}
