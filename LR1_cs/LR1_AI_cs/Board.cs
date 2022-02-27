using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LR1_AI_cs.Properties
{
    public class Board
    {
        private Form1 _form { get; }
        private const int INDEX_OF_LAST_ADJACENT_CELL = 5;
        public State targetState { get; set; } = new State();
        public State currentState { get; set; } = new State();
      

        public Board(Form1 form)
        {
            _form = form;
        }

        public void rotateClockwise(int pos)
        {
         
            IList<Cell> updatedCells = rotateCells(pos);
            updateState(updatedCells);
          
            
        }
           
        private IList<Cell> rotateCells(int pos)
        {
            var adjacentCells = currentState.getAdjacentCells(pos);
            var temp = adjacentCells[0].color;

            for (int i = 1; i <= INDEX_OF_LAST_ADJACENT_CELL; i++)
            {
                var tempColor = adjacentCells[i].color;
                adjacentCells[i].color = temp;
                temp = tempColor;
            }

            adjacentCells[0].color = temp;
            return adjacentCells;
        }

        private void updateState(IList<Cell> cellsToUpdate)
        {
            foreach (var cell in cellsToUpdate)
            {
                _form.sync(cell);
            }
        }


        public Boolean isWin()
        {
            bool isEqual = true;
            for (int i = 0; i < 19; i++)
            {
                if (currentState._cells[i].color != targetState._cells[i].color)
                    return false;
            }
            return true;
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