using System;
using System.Collections;
using System.Collections.Generic;

namespace LR1_AI_cs.Properties
{
    public class Board
    {
        private Form1 _form { get; }
        private const int INDEX_OF_LAST_ADJACENT_CELL = 5;
        public State targetState { get; set; } = new State();
        public State currentState { get; set; } = new State();
        public List<State> history { get; set; } = new List<State>();

        public Board(Form1 form)
        {
            _form = form;
        }

        public void rotateAround(int pos)
        {
            history.Add(currentState);
            var adjacentCells = currentState.getAdjacentCells(pos);
            var temp = adjacentCells[0].color;

            for (int i = 1; i <= INDEX_OF_LAST_ADJACENT_CELL; i++)
            {
                var tempColor = adjacentCells[i].color;
                adjacentCells[i].color = temp;
                temp = tempColor;
            }

            adjacentCells[0].color = temp;
            updateState(adjacentCells);
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
            return currentState == targetState;
        }
    }
}