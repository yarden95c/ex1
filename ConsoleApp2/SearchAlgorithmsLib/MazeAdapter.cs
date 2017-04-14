using System;
using System.Collections.Generic;
using MazeLib;

namespace Server
{
    /// <summary>
    /// make maze as ISearchable.
    /// </summary>
    /// <seealso cref="Ass1.ISearchable{MazeLib.Position}" />
    public class MazeAdapter : ISearchable<Position>
    {
        private Maze maze;
        /// <summary>
        /// Initializes a new instance of the <see cref="MazeAdapter"/> class.
        /// </summary>
        /// <param name="maze">The maze.</param>
        public MazeAdapter(Maze maze)
        {
            this.maze = maze;
        }
        /// <summary>
        /// Gets the state of the goal.
        /// </summary>
        /// <returns></returns>
        public State<Position> GetGoalState()
        {
            return State<Position>.StatePool.GetState(maze.GoalPos);

            //  return new State<Position>(maze.GoalPos);
        }
        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns></returns>
        public State<Position> GetInitialState()
        {
            State<Position> state = State<Position>.StatePool.GetState(maze.InitialPos);
            state.Cost = 0;
            return state;
            //return new State<Position>(maze.InitialPos);
        }
        /// <summary>
        /// Gets all possible states.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public List<State<Position>> GetAllPossibleStates(State<Position> s)
        {
            List<State<Position>> neighbours = new List<State<Position>>();
            int i = s.GetPosition().Row;
            int j = s.GetPosition().Col;
            Position position;
            if (i + 1 < maze.Rows && maze[i + 1, j] == CellType.Free)
            {
                position = new Position(i + 1, j);
                AddToNeighbours(s, position, neighbours);
            }
            if (i > 0 && maze[i - 1, j] == CellType.Free)
            {
                position = new Position(i - 1, j);
                AddToNeighbours(s, position, neighbours);
            }
            if (j + 1 < maze.Cols && maze[i, j + 1] == CellType.Free)
            {
                position = new Position(i, j + 1);
                AddToNeighbours(s, position, neighbours);
            }
            if (j > 0 && maze[i, j - 1] == CellType.Free)
            {
                position = new Position(i, j - 1);
                AddToNeighbours(s, position, neighbours);
            }
            return neighbours;
        }

        /// <summary>
        /// Adds to neighbours.
        /// </summary>
        /// <param name="stateOriginal">The state original.</param>
        /// <param name="position">The position.</param>
        /// <param name="neighbours">The neighbours.</param>
        private void AddToNeighbours(State<Position> stateOriginal, Position position, List<State<Position>> neighbours)
        {
            State<Position> state = State<Position>.StatePool.GetState(position);
            neighbours.Add(state);
        }
        /// <summary>
        /// Betters the diraction.
        /// </summary>
        /// <param name="Previous">The previous.</param>
        /// <param name="New">The new.</param>
        /// <returns></returns>
        public bool BetterDiraction(State<Position> Previous, State<Position> New)
        {
            return Previous.Cost > (New.Cost + 1);
        }
        /// <summary>
        /// Updates the cost.
        /// </summary>
        /// <param name="Previous">The previous.</param>
        /// <param name="New">The new.</param>
        public void UpdateCost(State<Position> Previous, State<Position> New)
        {
            Previous.Cost = New.Cost + 1;
        }
        /// <summary>
        /// Updates the came from.
        /// </summary>
        /// <param name="child">The child.</param>
        /// <param name="father">The father.</param>
        public void UpdateCameFrom(State<Position> child, State<Position> father)
        {
            child.Parent = father;
        }
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return this.maze.Name;
        }

        /// <summary>
        /// Prints the solution.
        /// </summary>
        /// <param name="solution">The solution.</param>
        public static string PrintSolution(Solution<Position> solution)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            List<State<Position>> list = solution.GetSolution();
            List<int> list2 = new List<int>();
            foreach (State<Position> state in list)
            {
                if (state.Parent != null)
                {
                    if (state.GetPosition().Col < state.Parent.GetPosition().Col)
                    {
                        //sb.Append(MazeLib.Direction.Left);
                        list2.Add((int)MazeLib.Direction.Left);
                    }
                    if (state.GetPosition().Col > state.Parent.GetPosition().Col)
                    {
                        //  sb.Append(MazeLib.Direction.Right);
                        list2.Add((int)MazeLib.Direction.Right);
                    }
                    if (state.GetPosition().Row < state.Parent.GetPosition().Row)
                    {
                        //  sb.Append(MazeLib.Direction.Up);
                        list2.Add((int)MazeLib.Direction.Up);
                    }
                    if (state.GetPosition().Row > state.Parent.GetPosition().Row)
                    {
                        //  sb.Append(MazeLib.Direction.Down);
                        list2.Add((int)MazeLib.Direction.Down);
                    }
                }

            }
            
            foreach (int i in list2)
            {
                sb.Append(i.ToString());
                //Console.Write(i);
            }
            return sb.ToString();
        }
    }
}
