namespace ai_lab3
{
    class State : Room
    {
        public State() : base() { }

        public int energy;

        //функція для підрахунку енергії стану
        public int calculateEnergy()
        {
            energy = 0;

            RoomPart[,] sboard = new RoomPart[2, 3];
            FillRoom(sboard);
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (RoomParts[i, j].Value == sboard[i, j].Value)
                        energy++;
                }
            }
            return energy;
        }

        //перевірка, чи є стан кінцевим
        public bool Solved()
        {
            bool isSolved = false;
            int x = 1, i = 0, j = 0;
            RoomPart[,] solvedBoard = new RoomPart[2, 3];
            FillRoom(solvedBoard);
            //стан є кінцевим, якщо шафа та крісло помінялись місцями
            if (RoomParts[0, 2].Value == "armchair" && RoomParts[1, 2].Value == "cupboard")
                isSolved = true;

            return isSolved;
        }
    }
}
