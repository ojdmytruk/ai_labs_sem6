using System;
using System.Collections.Generic;

namespace ai_lab2
{    
    public interface IUpdate<T> where T : AStarState, IComparable<T>
    {
        //оновлення пріорітету в черзі з пріорітетами, якщо знаходимо "дешевший/вигідніший" розв'язок
        void UpdatePriorityQueue(T state);
    }

    public abstract class AStarState : IComparable<AStarState>
    {
        public AStarState parent; //батьківська вершина

        // список станів, у які можна "піти"
        public List<KeyValuePair<AStarState, int>> successors = new List<KeyValuePair<AStarState, int>>();
        public readonly string solutionStep;

        public int f;               // g + h
        public int g;               // реальна вартість переходу до наступної вершини
        private readonly int h;     // "оцінка" вартості переходу до наступної вершини

        //конструктор нових вершин графу станів, кожна з яких містить оцінки переходів
        public AStarState(AStarState previousState, string newStep, int g_, int h_)
        {
            parent = previousState;
            solutionStep = newStep;
            g = g_;
            h = h_;
            f = g + h;
        }

        public int CompareTo(AStarState otherState)
        {
            return otherState.f - f;    
        }

        public void OptimizeCosts<T>(AStarState possibleParent, int costFromPossibleParent,
            IUpdate<T> refresher) where T : AStarState, IComparable<T>
        {
            var gPosiible = possibleParent.g + costFromPossibleParent;

            //перевіряємо, чи можлива реальна вартість більша теперішньої
            if (gPosiible >= g)
            {
                return;
            }

            //присвоюємо батьківській вершині можливу вершинну (вище переконались, що у неї вигідніше піти)
            parent = possibleParent;
            g = gPosiible;//присвоюємо нову оцінку
            f = g + h; //перераховуємо сумарну оцінку

            //оновлюємо пріорітети вершин
            if (refresher != null)
            {
                refresher.UpdatePriorityQueue((T)this);
            }

            //обираємо найоптимальнішу з вершин, у які можемо піти для кожної вершини
            foreach (var successorData in successors)
            {
                successorData.Key.OptimizeCosts(this, successorData.Value, refresher);
            }
        }


        public void AddStep(List<string> solutionSteps)
        {

            if (parent != null)
            {
                parent.AddStep(solutionSteps);
            }

            if (!string.IsNullOrEmpty(solutionStep))
            {
                solutionSteps.Add(solutionStep);
            }
        }

        public List<string> Solution()
        {
            var solution = new List<string>();

            AddStep(solution);
            return solution;
        }

        public void PrintSolution()
        {
            foreach (string step in Solution())
            {
                Console.WriteLine(step);
            }
        }
    }
}
