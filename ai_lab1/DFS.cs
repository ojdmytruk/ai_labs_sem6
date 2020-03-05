using System.Collections.Generic;
namespace ai_lab1
{
    public class DFS 
    {
        private List<KnightSpearmanState> closed = null;

        public LinkedList<KnightSpearmanState> Solve(KnightSpearmanState initial)
        {
            closed = new List<KnightSpearmanState>();
            AddState(initial);

            while (HasElements())
            {
                KnightSpearmanState state = NextState();

                if (state.IsSolution())
                    return FindPath(state);

                closed.Add(state);

                LinkedList<KnightSpearmanState> moves = state.GetPossibleMoves();

                foreach (KnightSpearmanState move in moves)
                    if (!closed.Contains(move))
                        AddState(move);
            }

            return null;
        }

        public int GetVistedStateCount()
        {
            return closed.Count;
        }

        private LinkedList<KnightSpearmanState> FindPath(KnightSpearmanState solution)
        {
            LinkedList<KnightSpearmanState> path = new LinkedList<KnightSpearmanState>();

            while (solution != null)
            {
                path.AddFirst(solution);
                solution = solution.Parent;
            }

            return path;
        }

        private Stack<KnightSpearmanState> stack = new Stack<KnightSpearmanState>();

        void AddState(KnightSpearmanState state)
        {
            if (!stack.Contains(state))
                stack.Push(state);
        }

        bool HasElements()
        {
            return stack.Count != 0;
        }

        KnightSpearmanState NextState()
        {
            return stack.Pop();
        }

    }
}
