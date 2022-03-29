using System;
using System.Collections.Generic;
using System.Linq;
using CheckersBase;
using CheckersBase.BrainBase;
using CheckersRules;

namespace AgentMessiah
{
    public abstract class BaseCheckersAI
    {
        protected bool IsWhiteTurn;
        protected abstract int Eval(Board board);
        protected abstract bool IsMaximumDeep(int D);

        protected int MaxValue(Board board, int alpha, int beta, int deep)
        {
            if (IsMaximumDeep(deep))
                return Eval(board);

            int v = int.MinValue;

            var motions = RulesWrapper.FindAllMotions(board, !IsWhiteTurn);

            if (motions.Count == 0)
                return Eval(board);

            foreach (var mnt in motions.ToList())
            {
                //Найти максимальное из минимальных значений оценок хода
                v = Math.Max(v, MinValue(Rules.ApplyMotion(board, mnt, !IsWhiteTurn), alpha, beta, deep + 1));
                //разница
                if (v >= beta)
                    return v;
                alpha = Math.Max(alpha, v);
            }

            return v;
        }

        protected int MinValue(Board board, int alpha, int beta, int deep)
        {
            if (IsMaximumDeep(deep))
                return Eval(board);

            int v = int.MaxValue;

            var motions = RulesWrapper.FindAllMotions(board, IsWhiteTurn);

            if (motions.Count == 0) return Eval(board);

            foreach (var mnt in motions.ToList())
            {
                //Найти минимальное из максимальных значений оценок хода
                v = Math.Min(
                    v,
                    MaxValue(Rules.ApplyMotion(board, mnt, IsWhiteTurn), alpha, beta, deep + 1));
                //разница
                if (v <= alpha)
                    return v;
                beta = Math.Min(beta, v);
            }

            return v;
        }

        protected static bool NoValidMotions(Board board, bool isWhite)
        {
            var validator = Rules.FindValidMotions(board, isWhite);
            return validator.NoValidMotions();
        }

        public class PlayersCheckers
        {
            public int Kings { get; set; }
            public int Pawns { get; set; }
        }

        public class CheckersInfo
        {
            public PlayersCheckers Black { get; set; }
            public PlayersCheckers White { get; set; }

            public int freeCells { get; set; } = 0;

            public CheckersInfo()
            {
                Black = new PlayersCheckers();
                White = new PlayersCheckers();
            }
        }
    }
}