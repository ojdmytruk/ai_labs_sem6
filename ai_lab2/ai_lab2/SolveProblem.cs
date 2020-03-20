namespace ai_lab2
{
    //для покрокового отримання розв'язку, з застасуванням алгоритму
    public class SolveProblem : AStarSearch<CannibalsMissionariesState>
    {

        public SolveProblem()
        {
        }

        protected override void GenerateSuccessorStates(CannibalsMissionariesState CurState, 
                                                        CannibalsMissionariesState StartState,
                                                        CannibalsMissionariesState GoalState)
        {

            for (int i = 0; i < 5; ++i)
            {
                int numCannibalsOnSide = (CurState.boatOnRight) ? CurState.numCannibalsRight : CurState.numCannibalsLeft;
                int numMissionariesOnSide = (CurState.boatOnRight) ? CurState.numMissionariesRight : CurState.numMissionariesLeft;
                
                //до переправи у човні 0 канібалів і 0 місіонерів
                int numCannibalsInBoat = 0;
                int numMissionariesInBoat = 0;

                //з якого берега переправляються 
                string startSide = CurState.boatOnRight ? "правого берега" : "лiвого берега";
                //на який берез переправляються
                string destinationSide = CurState.boatOnRight ? "лiвий берег" : "правий берег";
                
                string newStepStr = string.Format(" з {0} на {1}.", startSide, destinationSide);

                string newStep = null;

                switch (i)
                {
                    case 0: // у човні 1 канібал

                        if (numCannibalsOnSide >= 1)
                        {
                            numCannibalsInBoat = 1;
                            newStep = "1 канiбал перепливає " + newStepStr;
                        }

                        break;

                    case 1: // у човні 1 місіонер

                        if (numMissionariesOnSide >= 1)
                        {
                            numMissionariesInBoat = 1;
                            newStep = "1 мiсiонер перепливає" + newStepStr;
                        }

                        break;

                    case 2: // у човні 2 канібали

                        if (numCannibalsOnSide >= 2)
                        {
                            numCannibalsInBoat = 2;
                            newStep = "2 канiбали перепливають " + newStepStr;
                        }

                        break;

                    case 3: // у човні 2 міссіонери

                        if (numMissionariesOnSide >= 2)
                        {
                            numMissionariesInBoat = 2;
                            newStep = "2 мiсiонери перепливають " + newStepStr;
                        }

                        break;

                    case 4: // у човні 1 канібал та 1 місіонер

                        if (numCannibalsOnSide >= 1 && numMissionariesOnSide >= 1)
                        {
                            numCannibalsInBoat = 1;
                            numMissionariesInBoat = 1;
                            newStep = "1 канiбал та 1 мiсiонер перепливають" + newStepStr;
                        }

                        break;

                    default:
                        break;
                }

                if (newStep == null)
                {
                    continue;
                }

                //визначаємо нові кількості канібалів та місіонерів на правому та лівому берегах
                int new_cannibalsRight;
                int new_missionariesRight;
                int new_cannibalsLeft;
                int new_missionariesLeft;

                if (CurState.boatOnRight)
                {
                    new_cannibalsRight = CurState.numCannibalsRight - numCannibalsInBoat;
                    new_missionariesRight = CurState.numMissionariesRight - numMissionariesInBoat;
                    new_cannibalsLeft = CurState.numCannibalsLeft + numCannibalsInBoat;
                    new_missionariesLeft = CurState.numMissionariesLeft + numMissionariesInBoat;
                }
                else
                {
                    new_cannibalsRight = CurState.numCannibalsRight + numCannibalsInBoat;
                    new_missionariesRight = CurState.numMissionariesRight + numMissionariesInBoat;
                    new_cannibalsLeft = CurState.numCannibalsLeft - numCannibalsInBoat;
                    new_missionariesLeft = CurState.numMissionariesLeft - numMissionariesInBoat;
                }

                CannibalsMissionariesState newState = new CannibalsMissionariesState(!CurState.boatOnRight, 
                                                                                      new_cannibalsRight, new_missionariesRight,
                                                                                      new_cannibalsLeft, new_missionariesLeft,
                    CurState, newStep, CurState.g + 1, new_cannibalsRight + new_missionariesRight);

                if (newState.IsValid() && !openQueue.Contains(newState) && !closedSet.Contains(newState))
                {
                    openQueue.Enqueue(newState);
                }
            }
        }

        public CannibalsMissionariesState Run()
        {
            int cannibals = 3;
            int missionaries = 3;

            CannibalsMissionariesState StartState = new CannibalsMissionariesState(true, cannibals, missionaries, 0, 0);
            CannibalsMissionariesState GoalState = new CannibalsMissionariesState(false, 0, 0, cannibals, missionaries);

            return Search(StartState, GoalState);
        }

        public bool RunAndReport()
        {
            return Report(Run());
        }
    }
      
}
