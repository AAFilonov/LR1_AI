using System;
using System.Collections.Generic;
using System.Linq;
using CheckersBase;
using CheckersBase.BrainBase;
using CheckersRules;

namespace AgentMessiah
{
    
    #region Utils

    static class Utils
    {
        public static void LogMessage(string msg)
        {
#if DEBUG_OUTPUT
            Debug.WriteLine(msg);
#endif
        }

        public static void LogMotion(int value, Motion mtn, int deep)
        {
#if DEBUG_OUTPUT

            string format = "{0," + deep * 3 + "}  ";
            Debug.Write(String.Format(format, value));

            LogMotion(mtn);

#endif
        }

        public static void LogMotion(Motion mtn)
        {
#if DEBUG_OUTPUT

            for (int i = 0; i < mtn.Moves.Count; i++)
            {
                var m = mtn.Moves[i];
                Debug.Write(String.Format("[ {0} x {1} ]", m.X, m.Y));
            }

            Debug.Write("\n");

#endif
        }

        public static void LogBoard(Board board)
        {
#if DEBUG_OUTPUT

            Debug.WriteLine("Board: ");

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    string color = "/";

                    if (board.IsWhite(j, i)) color = "W";
                    if (board.IsBlack(j, i)) color = "B";

                    Debug.Write(FormatWithSpace(color + (board.IsKing(j, i) ? "K" : "")));
                }
                Debug.Write("\n");
            }

#endif
        }

        private static string FormatWithSpace(string text)
        {
            return String.Format("{0,3}", text);
        }
    }

    #endregion
     static class RulesWrapper
    {
        public static List<Motion> FindAllMotions(Board board, bool isWhite)
        {
            return Rules.FindValidMotions(board, isWhite).GetAllMotions();
        }
    }

     
    
    [BrainInfo(BrainName = "Мессия", Student = "Студенческий совет", StudentGroup = "18-ПРИ")]
    public class Messia : BrainBase
    {
        MyCheckersAI intell = new MyCheckersAI(6, 1, 3, 2);
        public override Motion FindMotion(Board board, bool isWhite)
        {
            return intell.FindMotion(board, isWhite);
        }
    }
 
}