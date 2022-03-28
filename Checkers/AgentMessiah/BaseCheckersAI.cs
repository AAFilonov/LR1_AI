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
        protected bool _isWhiteTurn;
        public abstract int Eval(Board board);
        public abstract bool IsMaximumDeep(int D);
        protected int MaxValue(Board board, int alpha, int beta, int deep)
        {
            if (IsMaximumDeep(deep))
                return Eval(board);

            int v = int.MinValue;

            var motions = RulesWrapper.FindAllMotions(board, !_isWhiteTurn);

            if (motions.Count == 0)
                return Eval(board);

            foreach (var mnt in motions.ToList())
            {
                Utils.LogMotion(0, mnt, deep + 1);
                v = Math.Max(v, MinValue(Rules.ApplyMotion(board, mnt, !_isWhiteTurn), alpha, beta, deep + 1));
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

            var motions = RulesWrapper.FindAllMotions(board, _isWhiteTurn);

            if (motions.Count == 0) return Eval(board);

            foreach (var mnt in motions.ToList())
            {
                Utils.LogMotion(0, mnt, deep + 1);
                v = Math.Min(v, MaxValue(Rules.ApplyMotion(board, mnt, _isWhiteTurn), alpha, beta, deep + 1));
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
    }
}