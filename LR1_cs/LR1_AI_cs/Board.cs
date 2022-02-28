using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LR1_AI_cs.Properties
{
    public class Board
    {
        
        private Game _game { get; } = new Game();

        public State targetState { get; set; } = new State();
        public State currentState { get; set; } = new State();



        public void rotateClockwise(int pos)
        {
          
            State updatedState = _game.rotateClockwise(currentState, pos);
            setCurrent(updatedState);
            
        }

      


        public Boolean isWin()
        {
            return currentState.Equals(targetState);
        }

        public void resetAll()
        {
            currentState._cells = State.deepCopy(State.initialState_cells);
            targetState._cells = State.deepCopy(State.initialState_cells);
        }

        public void setCurrent(State newState)
        {
            currentState = newState;
        }
    }
}