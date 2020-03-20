using System;
using System.Collections.Generic;

namespace ai_lab2
{

    public abstract class AStarSearch<T> where T : AStarState, IComparable<T>
    {
        protected readonly PriorityQueue<T> openQueue = new PriorityQueue<T>();
        protected readonly HashSet<T> closedSet = new HashSet<T>(); //сет для збереження поточного стану

        //генерація можливих наступних станів
        protected abstract void GenerateSuccessorStates(T CurState, T StartState, T GoalState);

        protected T Search(T StartState, T GoalState)
        {
            openQueue.Clear();
            closedSet.Clear();
            openQueue.Enqueue(StartState);

            while (!openQueue.IsEmpty)
            {
                T CurState = openQueue.Dequeue(); //переходимо в поточний стан з черги

                //повертаємо поточний стан як рішення, якщо він є цільовим станом
                if (CurState.Equals(GoalState))
                {
                    return CurState;
                }

                //додаємо поточний стан у сет 
                closedSet.Add(CurState);
                //генеруємо стани, у які можемо здійснити перехід
                GenerateSuccessorStates(CurState, StartState, GoalState);
            }

            return null;
        }

        public bool Report(T solutionState)
        {

            if (solutionState == null)
            {
                Console.WriteLine("Задача не має рішення");
                return false;
            }

            solutionState.PrintSolution();
            return true;
        }
    }
}
