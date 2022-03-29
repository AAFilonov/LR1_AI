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

        int _koeffFreeCells; //  количество пустых клеток
        
        //после разменовых ходов и ходов в несколько "атак" подряд количествоо занятых клеток резко сокращается
        //поэтому увеличение числа пустых клеток при выишгрышной ситуации по шашкам и дамкам может(?) служить свидетельсвтом выгодного хода 


        Random rand = new Random();

        internal MyCheckersAI( int maxDeep, int koeffKings, int koeffPawns, int koeffMoves, int koeffFreeCells)
        {
            _maxDeep = maxDeep;

            _koeffKings = koeffKings;
            _koeffPawns = koeffPawns;
            _koeffMoves = koeffMoves;
            _koeffFreeCells = koeffFreeCells;
        }

        public Motion FindMotion(Board board, bool isWhite)
        {
            IsWhiteTurn = isWhite;


            int alpha = int.MinValue;
            int beta = int.MaxValue;
            //получили все возможные ходы
            var motions = RulesWrapper.FindAllMotions(board, IsWhiteTurn);

            if (motions.Count == 1)
                return motions.First();
            List<Tuple<int, Motion>> results = new List<Tuple<int, Motion>>();

            foreach (var mnt in motions.ToList())
            {
                int v = MaxValue(Rules.ApplyMotion(board, mnt, IsWhiteTurn), alpha, beta, 1);
                results.Add(new Tuple<int, Motion>(v, mnt));
            }

            if (results.Count == 0)
                return null;

            int max = results.Select(tt => tt.Item1).Max();

            var bestMotions = results.Where(t => t.Item1 == max).Select(r => r.Item2).ToList();

            var mtn = bestMotions[rand.Next(bestMotions.Count)];


            return mtn;
        }


        protected override int Eval(Board board)
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
                        default:
                            info.freeCells++;
                            break;
                    }
                }
            }

            var moves = Rules.GetMotionsCount(board);
            int wC = moves.Item1;
            int bC = moves.Item2;

            int f = 0;

            PlayersCheckers a, b;

            a = IsWhiteTurn ? info.White : info.Black;
            b = IsWhiteTurn ? info.Black : info.White;

            int aCount = IsWhiteTurn ? wC : bC;
            int bCount = IsWhiteTurn ? bC : wC;

            int advantageInMoves = aCount - bCount;
            int advantageInKings = a.Kings - b.Kings;
            int advantageInPawns = a.Pawns - b.Pawns;


            f = _koeffKings * advantageInKings + _koeffPawns * advantageInPawns + _koeffMoves * advantageInMoves +
                _koeffFreeCells * info.freeCells;

            return f;
        }

        protected override bool IsMaximumDeep(int D)
        {
            return D > _maxDeep;
        }
    }
}