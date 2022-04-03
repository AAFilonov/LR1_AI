using System;
using System.Text;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs.ai.heurisitc.dbHeuristic
{
    public class Parser
    {
        public static string[] toArray(State state)
        {
            string[] arr = new string[19];

            for (int i = 0; i < 19; i++)
            {
                arr[i] =
                    CellParser.colorToChar(state._cells[i].color);
            }

            return arr;
        }

        public static State fromArray(string[] arr)
        {
            State state = new State();

            for (int i = 0; i < 19; i++)
            {
                var color = CellParser.parseColorByChar(arr[i]);
                state._cells[i].color = color;
            }

            return state;
        }

        public static String toString(string[] arr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (var val in arr)
            {
                sb.Append(val + ",");
            }

            sb.Append("]");
            return sb.ToString();
        }

        public static string[] arrayFromString(string val)
        {
            //проверить регексом валидность строки
            val = val.Remove(val.IndexOf('['), 1);
            val = val.Remove(val.IndexOf(']'), 1);
            val = val.Remove(val.LastIndexOf(','), 1);
            var vals = val.Split(',');
            return vals;
        }

        public static State fromString(string val)
        {
            var arr = arrayFromString(val);
            return fromArray(arr);
        }

        public static String toString(State val)
        {
            string[] arr = toArray(val);
            return toString(arr);
        }
    }
}