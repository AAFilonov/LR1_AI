using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LR1_AI_cs.Properties
{
    public class Board
    {
        

        public State targetState { get; set; } = new State();
        public State currentState { get; set; } = new State();



        public void moveClockwise(int pos)
        {
          
            State updatedState = Game.rotateClockwise(currentState, pos);
            setCurrent(updatedState);
            
        }
        public void moveCounterclockwise(int pos)
        {
          
            State updatedState = Game.rotateCounterclockwise(currentState, pos);
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
            currentState = new State(newState);
        }
    }
}