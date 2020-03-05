namespace ai_lab1
{
    public class Side
    {
        public enum LeftRight { left, right };
        private LeftRight side;

        public LeftRight KeepSide
        {
            get
            {
                return side;
            }

            set
            {
                side = value;
            }
        }

        public Side(LeftRight side)
        {
            this.side = side;
        }

        public LeftRight GetOpposite()
        {
            if (side == LeftRight.left)
                return LeftRight.right;
            else
                return LeftRight.left;
        }
    }
}
