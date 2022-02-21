using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LR1_AI_cs.Properties
{
    

    public class State
    {
        public static Dictionary<int, int[]> adjacentCells = new Dictionary<int, int[]>()
        {
            {5,  new int[] {1, 2, 6,10,9,4}},
            {6,  new int[] { 2, 3,7,11,10,5}},
            {9,  new int[] {4,5,10,14,13,8}},
            {10, new int[] {5,6,11,15,14,9}},
            {11, new int[] {6,7,12,16,15,10}},
            {14, new int[] {9,10,15,18,17,13}},
            {15, new int[] {10,11,16,19,18,14}},
        };
        public static Cell[] initialState = new[]
        {
                                                                           new Cell(1,Cell.Color.GRAY),new Cell(2,Cell.Color.GRAY),new Cell(3,Cell.Color.GRAY),
                                            new Cell(4,Cell.Color.GRAY),new Cell(5,Cell.Color.GRAY),new Cell(6,Cell.Color.GRAY),new Cell(7,Cell.Color.GRAY),
            new Cell(8,Cell.Color.GRAY),new Cell(9,Cell.Color.GRAY),new Cell(10,Cell.Color.GRAY),new Cell(11,Cell.Color.GRAY),new Cell(12,Cell.Color.GRAY),
                                            new Cell(13,Cell.Color.GRAY),new Cell(14,Cell.Color.GRAY),new Cell(15,Cell.Color.GRAY),new Cell(16,Cell.Color.GRAY),
                                                                            new Cell(17,Cell.Color.GRAY),new Cell(18,Cell.Color.GRAY),new Cell(19,Cell.Color.GRAY),
        };

        public Cell[] _cells { get; set; } = initialState.Clone() as Cell[];

        public IList<Cell> getAdjacentCells(int pos)
        {
            if (adjacentCells.ContainsKey(pos))
            {
                var adjacentPostions = adjacentCells[pos];
                return initialState.Where(cell => adjacentPostions.Contains(cell.position)).ToList();
            }
            else return new List<Cell>();
        }
    }
}