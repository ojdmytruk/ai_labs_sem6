using System;

namespace ai_lab3
{
    class Room
    {
        //оголошуємо матрицю частин кімнати
        protected RoomPart[,] RoomParts;
        //позиція пустої частини кімнати
        Position pos, oldPos;

        //перераховуємо можливі напрямки переміщення пустої частини кімнати (ставимо на пусту частину
        //предмет мебелі і звільняємо таким чином верхню, нижню, ліву чи праву частини кімнати)
        public enum Direction { Up, Down, Right, Left }

        //конструктор кімнати, задаємо позицію пустої кімнати 
        public Room()
        {
            RoomParts = new RoomPart[2, 3];
            FillRoom(RoomParts);

            pos = new Position(1, 1);
            oldPos = new Position(1, 1);


        }

        //виведення станів частин кімнати на консоль
        public void DrawRoom()
        {
            var formatString = string.Format("{{0, -{0}}}|", 9);
            for (int i = 0; i < 2; i++)
            {

                for (int j = 0; j < 3; j++)
                {
                    Console.Write(formatString, RoomParts[i, j].Value);

                    if (j == 2)
                    {
                        Console.Write("|");
                    }
                }
                Console.WriteLine();
            }
        }

        //Заповнюємо кімнату за заданою умовою
        protected void FillRoom(RoomPart[,] boardToFill)
        {
            boardToFill[0, 0] = new RoomPart("table");
            boardToFill[0, 1] = new RoomPart("chair1");
            boardToFill[0, 2] = new RoomPart("cupboard");
            boardToFill[1, 0] = new RoomPart("chair2");
            boardToFill[1, 1] = new RoomPart("_______");
            boardToFill[1, 2] = new RoomPart("armchair");
        }

        //функція переміщення
        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    //якщо пуста частина не біля лівого краю
                    if (pos.Y > 0)
                        //переміщуємо порожню частину ліворуч
                        pos.Update(pos.X, pos.Y - 1);
                    break;
                case Direction.Right:
                    //якщо пуста частина не біля правого краю
                    if (pos.Y < 3 - 1)
                        //переміщуємо порожню частину праворуч
                        pos.Update(pos.X, pos.Y + 1);
                    break;
                case Direction.Up:
                    //якщо пуста частина не біля верхнього краю
                    if (pos.X > 0)
                        //переміщуємо порожню частину вгору
                        pos.Update(pos.X - 1, pos.Y);
                    break;
                case Direction.Down:
                    //якщо пуста частина не біля нижнього краю
                    if (pos.X < 2 - 1)
                        //переміщуємо порожню частину вниз
                        pos.Update(pos.X + 1, pos.Y);
                    break;
            }
            //змінюємо координати порожньої частини
            Swap(oldPos, pos);
            oldPos.Update(pos.X, pos.Y);
        }

        //міняємо місцями порожню частину з якоюсь (за координатами) непорожньою
        public void Swap(Position oldPosition, Position newPos)
        {
            RoomPart temp = RoomParts[oldPosition.X, oldPosition.Y];
            RoomParts[oldPosition.X, oldPosition.Y] = RoomParts[newPos.X, newPos.Y];
            RoomParts[newPos.X, newPos.Y] = temp;
        }
    }
}
