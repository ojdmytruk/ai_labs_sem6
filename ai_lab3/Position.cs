namespace ai_lab3
{
    //Клас для збереження координат порожньої частини кімнати
    class Position
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Update(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
