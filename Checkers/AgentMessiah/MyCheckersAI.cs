using System;
using System.Collections.Generic;
using System.Linq;
using CheckersBase;
using CheckersRules;

namespace AgentMessiah
{
    public class MyCheckersAI : BaseCheckersAI
    {
        int _maxDeep; // количество полуходов для поиска решения

        int _koeffKings;
        int _koeffPawns;
        int _koeffMoves;

    

        Random rand = new Random();

        internal MyCheckersAI(int koeffKings, int koeffPawns, int koeffMoves, int maxDeep)
        {
            _maxDeep = maxDeep; // количество полуходов для поиска решения

            _koeffKings = koeffKings;
            _koeffPawns = koeffPawns;
            _koeffMoves = koeffMoves;
        }

        public Motion FindMotion(Board board, bool isWhite)
        {
            _isWhiteTurn = isWhite;
            Utils.LogBoard(board);

            int alpha = int.MinValue;
            int beta = int.MaxValue;

            var motions = RulesWrapper.FindAllMotions(board, _isWhiteTurn);

            if (motions.Count == 1)
                return motions.First();


            List<Tuple<int, Motion>> results = new List<Tuple<int, Motion>>();

            foreach (var mnt in motions.ToList())
            {
                Utils.LogMotion(0, mnt, 0);
                int v = MaxValue(Rules.ApplyMotion(board, mnt, _isWhiteTurn), alpha, beta, 1);
                results.Add(new Tuple<int, Motion>(v, mnt));
            }

            if (results.Count == 0)
                return null;

            Utils.LogMessage("After alpha-beta:");

            foreach (var tuple in results)
            {
                Utils.LogMotion(tuple.Item1, tuple.Item2, 0);
            }

            int max = results.Select(tt => tt.Item1).Max();

            Utils.LogMessage(String.Format("Max value is {0}", max));

            var bestMotions = results.Where(t => t.Item1 == max).Select(r => r.Item2).ToList();

            var mtn = bestMotions[rand.Next(bestMotions.Count)];

            Utils.LogMessage("Result:");
            Utils.LogMotion(mtn);

            return mtn;
        }

       

        class PlayersCheckers
        {
            public int Kings { get; set; }
            public int Pawns { get; set; }
        }

        class CheckersInfo
        {
            public PlayersCheckers Black { get; set; }
            public PlayersCheckers White { get; set; }

            public CheckersInfo()
            {
                Black = new PlayersCheckers();
                White = new PlayersCheckers();
            }
        }

        public  override int Eval(Board board)
        {
            CheckersInfo info = new CheckersInfo();

            for (int i = 0; i < Board.BOARD_SIZE; i++)
            {
                for (int j = 0; j < Board.BOARD_SIZE; j++)
                {
                    switch (board[i, j].Type)
                    {
                        case PieceTypes.BlackPawn:
                            info.Black.Pawns++;
                            break;
                        case PieceTypes.BlackKing:
                            info.Black.Kings++;
                            break;
                        case PieceTypes.WhitePawn:
                            info.White.Pawns++;
                            break;
                        case PieceTypes.WhiteKing:
                            info.White.Kings++;
                            break;
                    }
                }
            }

            var moves = Rules.GetMotionsCount(board);
            int wC = moves.Item1;
            int bC = moves.Item2;

            int f = 0;

            PlayersCheckers a, b;

            a = _isWhiteTurn ? info.White : info.Black;
            b = _isWhiteTurn ? info.Black : info.White;

            int aCount = _isWhiteTurn ? wC : bC;
            int bCount = _isWhiteTurn ? bC : wC;

            int advKings = a.Kings - b.Kings;
            int advPawns = a.Pawns - b.Pawns;
            int advMoves = aCount - bCount;

            f = _koeffKings * advKings + _koeffPawns * advPawns + _koeffMoves * advMoves;

            return f;
        }

        public  override bool IsMaximumDeep(int D)
        {
            return D > _maxDeep;
        }

     
        

       
    }
}