using System;
using System.Linq;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs.ai.heurisitc.dbHeuristic
{
    public class TargetEstimator
    {
        private DB _db;
        private const int STATE_SPLIT_POSITION = 10;
        public State targetState { get; }

        public TargetEstimator(State state, DB db)
        {
            this.targetState = new State(state);
            _db = db;
        }

        public int estimate(State initialState)
        {
            int lowerPartScore = get(prepareLowerPart(initialState), prepareLowerPart( targetState));
            int upperPartScore = get(prepareUpperPart(initialState), prepareUpperPart(targetState));
            return lowerPartScore + upperPartScore;
        }
        public int get(State templateState, State targetState)
        {
            var results = _db.findByTargetAndTemplate(Parser.toString(targetState), Parser.toString(templateState));
            if (results.Count == 0)
                throw new Exception("Target state " + Parser.toString(targetState) + " was not mapped!");
            return results.Select(tuple => tuple.Item3).Max();
        }

        public static State prepareUpperPart(State state)
        {
            State upperPart = new State(state);
            upperPart._cells.ToList().Take(STATE_SPLIT_POSITION).ToList().ForEach(cell =>
            {
                if (cell.color == Cell.Color.GRAY)
                {
                    cell.color = Cell.Color.UNDEF;
                }
            });
            upperPart._cells.ToList().Skip(STATE_SPLIT_POSITION).ToList()
                .ForEach(cell => cell.color = Cell.Color.UNDEF);
            return upperPart;
        }

        public static State prepareLowerPart(State state)
        {
            State lowerPart = new State(state);

            lowerPart._cells.ToList().Take(STATE_SPLIT_POSITION).ToList()
                .ForEach(cell => cell.color = Cell.Color.UNDEF);
            lowerPart._cells.ToList().Skip(STATE_SPLIT_POSITION).ToList().ForEach(cell =>
            {
                if (cell.color == Cell.Color.GRAY)
                {
                    cell.color = Cell.Color.UNDEF;
                }
            });
            return lowerPart;
        }
    }
}