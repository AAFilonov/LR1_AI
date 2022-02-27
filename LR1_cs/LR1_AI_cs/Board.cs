using System;
using System.Collections;
using System.Collections.Generic;

namespace LR1_AI_cs.Properties
{
    public class Board
    {
        private const int INDEX_OF_LAST_ADJACENT_CELL =5;
        public State targetState {get;set;}= new State();
        public State currentState{get;set;}= new State();

        public void rotateAround(int pos)
        {
            var adjacentCells = currentState.getAdjacentCells(pos);

            for (int i = 0; i <= INDEX_OF_LAST_ADJACENT_CELL; i++)
            {
               var a = adjacentCells[(i + 1) % INDEX_OF_LAST_ADJACENT_CELL].color; 
               var b=  adjacentCells[i].color;
               (a, b) = (b, a);

            }
            updateState(adjacentCells);
        }

        private void updateState(IList<Cell> cellsToUpdate)
        {
            foreach (var cell in cellsToUpdate)
            {
               
            }
        }

       
        public Boolean isWin()
        {
            return currentState == targetState;
        }
    }
    
}