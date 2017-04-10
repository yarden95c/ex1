using System;
using System.Collections.Generic;
using System.Text;


namespace SearchAlgorithmsLib
{
    public interface ISearchable<T>
    {
        State<T> getInitialState();
        State<T> getGoalState();
        List<State<T>> getAllPossibleStates(State<T> s);
        float betterDiraction(State<T> state, State<T> state2);
    }
}
