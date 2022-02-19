using System;

namespace LR1_AI_cs.Properties
{
    public class Board
    {
        private State targetState;
        private State currentState;

        public void rotateAround(int pos)
        {
            
        }
        public Boolean isWin()
        {
            return currentState == targetState;
        }
    }
    
}