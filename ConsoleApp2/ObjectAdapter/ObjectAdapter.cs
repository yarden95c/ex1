using System;
using System.Collections.Generic;
using MazeLib;
using SearchAlgorithmsLib;

namespace Ass1
{
    public class ObjectAdapter : ISearchable<Position>
    {
        private Maze maze;
        public ObjectAdapter(Maze maze)
        {
            this.maze = maze;
        }
        public State<Position> getGoalState()
        {
            return new State<Position>(maze.GoalPos);
        }
        public State<Position> getInitialState()
        {
            return new State<Position>(maze.InitialPos);
        }
        public List<State<Position>> getAllPossibleStates(State<Position> s)
        {
            List<State<Position>> neighbours = new List<State<Position>>();
            int i = s.getPosition().Row;
            int j = s.getPosition().Col;
            Position position;
            if (i + 1 < maze.Rows && maze[i + 1, j] == CellType.Free)
            {
                position = new Position(i + 1, j);
                addToNeighbours(s, position, neighbours);
            }
            if (i > 0 && maze[i - 1, j] == CellType.Free)
            {
                position = new Position(i - 1, j);
                addToNeighbours(s, position, neighbours);
            }
            if (j + 1 < maze.Cols && maze[i, j + 1] == CellType.Free)
            {
                position = new Position(i, j + 1);
                addToNeighbours(s, position, neighbours);
            }
            if (j > 0 && maze[i, j - 1] == CellType.Free)
            {
                position = new Position(i, j - 1);
                addToNeighbours(s, position, neighbours);
            }
            return neighbours;
        }
        private void addToNeighbours(State<Position> stateOriginal, Position position, List<State<Position>> neighbours)
        {
            State<Position> state = new State<Position>(position);
            state.Cost = stateOriginal.Cost + 1;
            state.Parent = stateOriginal;
            neighbours.Add(state);
        }
        public float betterDiraction(State<Position> state, State<Position> state2)
        {
            return state.Cost + 1;
        }
    }
}
