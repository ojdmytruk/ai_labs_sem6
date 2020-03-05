using System.Collections.Generic;

namespace ai_lab1
{
    public class KnightSpearmanState
    {
        private KnightSpearmanState parent = null;

        public KnightSpearmanState Parent
        {
            get
            {
                return parent;
            }

            set
            {
                parent = value;
            }
        }

        public KnightSpearmanState(KnightSpearmanState parent)
        {
            this.parent = parent;
        }

        private Side knight1 = new Side(Side.LeftRight.left);
        private Side spearman1 = new Side(Side.LeftRight.left);
        private Side knight2 = new Side(Side.LeftRight.left);
        private Side spearman2 = new Side(Side.LeftRight.left);
        private Side knight3 = new Side(Side.LeftRight.left);
        private Side spearman3 = new Side(Side.LeftRight.left);

        public KnightSpearmanState() { }

        public KnightSpearmanState( KnightSpearmanState parent,
                                    Side knight1, Side spearman1,
                                    Side knight2, Side spearman2,
                                    Side knight3, Side spearman3)
        {
            this.Parent = parent;
            this.knight1 = knight1;
            this.spearman1 = spearman1;
            this.knight2 = knight2;
            this.spearman2 = spearman2;
            this.knight3 = knight3;
            this.spearman3 = spearman3;
        }

       public bool IsSolution()
        {
            return
                knight1.KeepSide == Side.LeftRight.right && spearman1.KeepSide == Side.LeftRight.right &&
                knight2.KeepSide == Side.LeftRight.right && spearman2.KeepSide == Side.LeftRight.right &&
                knight3.KeepSide == Side.LeftRight.right && spearman3.KeepSide == Side.LeftRight.right;
        }

        public LinkedList<KnightSpearmanState> GetPossibleMoves()
        {
            LinkedList<KnightSpearmanState> moves = new LinkedList<KnightSpearmanState>();

            if (knight1.KeepSide == spearman1.KeepSide)
            {
                (new KnightSpearmanState(
                    this,
                    new Side(knight1.GetOpposite()), new Side(spearman1.GetOpposite()),
                    knight2, spearman2, 
                    knight3, spearman3)).AddIfRequest(moves);
            }
            
            if (knight2.KeepSide == spearman2.KeepSide)
            {
                (new KnightSpearmanState(
                    this,
                    knight1, spearman1,
                    new Side(knight2.GetOpposite()), new Side(spearman2.GetOpposite()),
                    knight3, spearman3)).AddIfRequest(moves);
            }

            if (knight3.KeepSide == spearman3.KeepSide)
            {
                (new KnightSpearmanState(
                    this,
                    knight1, spearman1,
                    knight2, spearman2,
                    new Side(knight1.GetOpposite()), new Side(spearman1.GetOpposite()))).AddIfRequest(moves);
            }

            if (knight1.KeepSide == knight2.KeepSide)
            {
                (new KnightSpearmanState(
                    this,
                    new Side(knight1.GetOpposite()), spearman1, 
                    new Side(knight2.GetOpposite()), spearman2,
                    knight3, spearman3)).AddIfRequest(moves);
            }


            if (knight1.KeepSide == knight3.KeepSide)
            {
                (new KnightSpearmanState(
                    this,
                    new Side(knight1.GetOpposite()), spearman1,
                    knight2, spearman2,
                    new Side(knight3.GetOpposite()), spearman3)).AddIfRequest(moves);
            }


            if (knight2.KeepSide == knight3.KeepSide)
            {
                (new KnightSpearmanState(
                    this,
                    knight1, spearman1,
                    new Side(knight2.GetOpposite()), spearman2, 
                    new Side(knight3.GetOpposite()), spearman3)).AddIfRequest(moves);
            }
            
            if (spearman1.KeepSide == spearman2.KeepSide)
            {
                (new KnightSpearmanState(
                    this,
                    knight1, new Side(spearman1.GetOpposite()), 
                    knight2, new Side(spearman2.GetOpposite()),
                    knight3, spearman3)).AddIfRequest(moves);
            }

            if (spearman1.KeepSide == spearman3.KeepSide)
            {
                (new KnightSpearmanState(
                    this,
                    knight1, new Side(spearman1.GetOpposite()),
                    knight2, spearman2,
                    knight3, new Side(spearman3.GetOpposite()))).AddIfRequest(moves);
            }

            if (spearman2.KeepSide == spearman3.KeepSide)
            {
                (new KnightSpearmanState(
                    this,
                    knight1, new Side(spearman1.GetOpposite()), 
                    knight2, spearman2, 
                    knight3, new Side(spearman3.GetOpposite()))).AddIfRequest(moves);
            }

            (new KnightSpearmanState( this, new Side(knight1.GetOpposite()), spearman1,
                                                     knight2, spearman2,
                                                     knight3, spearman3)).AddIfRequest(moves);

            (new KnightSpearmanState(this, knight1, spearman1, 
                                           new Side(knight2.GetOpposite()), spearman2,
                                           knight3, spearman3)).AddIfRequest(moves);

            (new KnightSpearmanState(this, knight1, spearman1,
                                           knight2, spearman2,
                                           new Side(knight3.GetOpposite()), spearman3)).AddIfRequest(moves);

            (new KnightSpearmanState(this, knight1, new Side(spearman1.GetOpposite()),
                                           knight2, spearman2,
                                           knight3, spearman3)).AddIfRequest(moves);

            (new KnightSpearmanState(this, knight1, spearman1,
                                           knight2, new Side(spearman2.GetOpposite()), 
                                           knight3, spearman3)).AddIfRequest(moves);

            (new KnightSpearmanState(this, knight1, spearman1,
                                           knight2, spearman2,
                                           knight3, new Side(spearman3.GetOpposite()))).AddIfRequest(moves);

            return moves;
        }

        
        private void AddIfRequest(LinkedList<KnightSpearmanState> moves)
        {
            bool nogood = (knight1.KeepSide != spearman2.KeepSide && knight1.KeepSide != spearman3.KeepSide) &&
                          (knight2.KeepSide != spearman1.KeepSide && knight2.KeepSide != spearman3.KeepSide) &&
                          (knight3.KeepSide != spearman1.KeepSide && knight3.KeepSide != spearman2.KeepSide);

            if (!nogood)
                moves.AddLast(this);
        }

        private string FormatSide(Side side)
        {
            return side.KeepSide == Side.LeftRight.left ? "Left" : "Right";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is KnightSpearmanState))
                return false;

            KnightSpearmanState knightsSpearman = (KnightSpearmanState)obj;

            return
                knight1.KeepSide == knightsSpearman.knight1.KeepSide && 
                spearman1.KeepSide == knightsSpearman.spearman1.KeepSide &&
                knight2.KeepSide == knightsSpearman.knight2.KeepSide && 
                spearman2.KeepSide == knightsSpearman.spearman2.KeepSide &&
                knight3.KeepSide == knightsSpearman.knight3.KeepSide && 
                spearman3.KeepSide == knightsSpearman.spearman3.KeepSide;
        }


        public override string ToString()
        {
            string solution = string.Empty;

            solution += "K1: " + FormatSide(knight1) + " ; ";
            solution += "S_men 1: " + FormatSide(spearman1) + " ; ";
            solution += "K2: " + FormatSide(knight2) + " ; ";
            solution += "S_men 2: " + FormatSide(spearman2) + " ; ";
            solution += "K3: " + FormatSide(knight3) + " ; ";
            solution += "S_men 3: " + FormatSide(spearman3) + "\r\n";

            return solution;
        }
    }
}
 