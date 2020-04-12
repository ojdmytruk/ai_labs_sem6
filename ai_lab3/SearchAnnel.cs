using System;

namespace ai_lab3
{
    class SearchAnneal
    {
        //теперішній стан
        State CurrentState;
        //стан, в який можемо здійснити перехід, якщо його "енергія" менша
        State CandidateState = new State();

        public SearchAnneal(State currentState)
        {
            CurrentState = currentState;
        }

        //функція зменшення температури (можна змінити, впливає на кількість ітерацій)
        public double DecreaseTemperature(double initTemp, double i)
        {
            return initTemp * (0.1 / i);
        }

        //перевіряємо, чи будемо здійснювати перехід з ймовірністю 
        public bool IsTransaction(double probability)
        {
            bool result;
            Random random = new Random();
            int value = random.Next(1);
            if (value <= probability)
                result = true;
            else result = false;
            return result;
        }

        //пошук з імітацією відпалу
        public void SimulatedAnnealing()
        {
            double i = 0;
            //обраховуємо енергію для початкового стану
            int energy1 = CurrentState.calculateEnergy();
            int energy2;
            //задаємо початкову температуру
            double temperature = 10000000;
            //поки температура більше мінімальної (у цьому випадку 0) та задача не вирішена
            while (temperature > 0 && !CurrentState.Solved())
            {
                i += 1;

                Random random = new Random();
                int direction = 0;
                //генеруємо рандомне значення для переміщення (переміщуємо пусту частину кімнати, тобто, ставимо на 
                //пусту частину предмет мебелі і звільняємо таким чином верхню, нижню, ліву чи праву частини кімнати)
                direction = random.Next(4) + 1;
                switch (direction)
                {
                    case 1:
                        CandidateState.Move(Room.Direction.Up);
                        break;
                    case 2:
                        CandidateState.Move(Room.Direction.Down);
                        break;
                    case 3:
                        CandidateState.Move(Room.Direction.Right);
                        break;
                    case 4:
                        CandidateState.Move(Room.Direction.Left);
                        break;
                }
                //обчислюємо енергію кандидата - стана, який отримаємо рандомним переміщенням
                energy2 = CandidateState.calculateEnergy();
                //різниця енергій
                int delta = energy1 - energy2;
                //якщо кандидат має меншу енергією
                if (delta > 0)
                {
                    //переходимо в стан-кандидат
                    energy1 = energy2;
                    CurrentState = CandidateState;
                }
                else
                {
                    //інакше обчислюємо ймовірність
                    //перевіряємо, чи будемо здійснювати перехід з ймовірністю
                    //здійснюємо або не здійснюємо перехід
                    double probability = Math.Exp(-(double)delta / (double)temperature);
                    if (IsTransaction(probability))
                    {
                        energy1 = energy2;
                        CurrentState = CandidateState;
                    }
                }

                //знижуємо температуру
                temperature = DecreaseTemperature(temperature, i);

                CurrentState.DrawRoom();
                Console.WriteLine();

            }
        }
    }
}
