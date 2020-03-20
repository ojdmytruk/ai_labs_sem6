namespace ai_lab2
{
    public class CannibalsMissionariesState : AStarState
    {
        public readonly bool boatOnRight; //змінна для контролю сторони, на якій знаходиться човен
        public readonly int numCannibalsRight;//кількість канібалів на правому березі
        public readonly int numMissionariesRight;//кількість місіонерів на правому березі
        public readonly int numCannibalsLeft;//кількість канібалів на лівому березі
        public readonly int numMissionariesLeft;//кількість місіонерів на лівому березі

        public CannibalsMissionariesState(bool boatRight, int cannibalsRight, int missionariesRight, 
                                                          int cannibalsLeft, int missionariesLeft,
                                          CannibalsMissionariesState previousState, string newStep, 
                                          int g, int h)
            : base(previousState, newStep, g, h)
        {
            boatOnRight = boatRight;
            numCannibalsRight = cannibalsRight;
            numMissionariesRight = missionariesRight;
            numCannibalsLeft = cannibalsLeft;
            numMissionariesLeft = missionariesLeft;
        }

        public CannibalsMissionariesState(bool boatRight, int cannibalsRight, int missionariesRight, 
                                                          int cannibalsLeft, int missionariesLeft)
            : this(boatRight, cannibalsRight, missionariesRight, cannibalsLeft, missionariesLeft, null, null, 0, 0)
        {
        }

        public bool IsValid()
        {
            //якщо на правому березі є місіонери, їх менше ніж канібалів і човен з кимось всередині на 
            //протилежному боці - їх з'їдять, стан не прийнятний
            if (numMissionariesRight > 0 && numCannibalsRight > numMissionariesRight && boatOnRight == false)
            {
                return false;
            }
            //якщо на лівому березі є місіонери, їх менше ніж канібалів і човен з кимось всередині на 
            //протилежному боці - їх з'їдять, стан не прийнятний
            if (numMissionariesLeft > 0 && numCannibalsLeft > numMissionariesLeft && boatOnRight == true)
            {
                return false;
            }
            //якщо на правому березі є канібали, їх менше ніж місіонерів і човен з кимось всередині на 
            //протилежному боці - їх обернуть в віру місіонерів, стан не прийнятний
            if (numCannibalsRight > 0 && numCannibalsRight < numMissionariesRight && boatOnRight == false)
            {
                return false;
            }
            //якщо на лівому березі є канібали, їх менше ніж місіонерів і човен з кимось всередині на 
            //протилежному боці - їх обернуть в віру місіонерів, стан не прийнятний
            if (numCannibalsLeft > 0 && numCannibalsLeft < numMissionariesLeft && boatOnRight == true)
            {
                return false;
            }

            //решта станів задовільняють умові
            return true;
        }

        public override bool Equals(object obj)
        {

            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            CannibalsMissionariesState otherState = obj as CannibalsMissionariesState;

            return otherState != null && boatOnRight == otherState.boatOnRight &&
                numCannibalsRight == otherState.numCannibalsRight &&
                numMissionariesRight == otherState.numMissionariesRight &&
                numCannibalsLeft == otherState.numCannibalsLeft &&
                numMissionariesLeft == otherState.numMissionariesLeft;
        }
    }
}
