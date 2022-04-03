using System;
using System.Collections.Generic;
using LR1_AI_cs.ai.heurisitc.dbHeuristic;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs.ai
{
    public class DbEstimator : IHeuristicEstimator
    {
        private const string DB_NAME = "AI_DB_TEST.db";
        private DB _db = new DB(DB_NAME);
        private List<TargetEstimator> _estimators = new List<TargetEstimator>();
        private readonly List<State> _possibleTargets = new List<State>();

        public DbEstimator()
        {
            State state2Pos = new State();
            state2Pos._cells[0].color = Cell.Color.BLUE;
            state2Pos._cells[18].color = Cell.Color.BLUE;
            _possibleTargets.Add(state2Pos);

            State state3Pos = new State();
            state3Pos._cells[3].color = Cell.Color.BLUE;
            state3Pos._cells[6].color = Cell.Color.BLUE;
            state3Pos._cells[17].color = Cell.Color.BLUE;
            _possibleTargets.Add(state3Pos);

            State state4Pos = new State();
            state4Pos._cells[3].color = Cell.Color.BLUE;
            state4Pos._cells[6].color = Cell.Color.BLUE;
            state4Pos._cells[12].color = Cell.Color.BLUE;
            state4Pos._cells[15].color = Cell.Color.BLUE;
            _possibleTargets.Add(state4Pos);

            _possibleTargets.ForEach(state => _estimators.Add(new TargetEstimator(state, _db)));
        }

        public int estimate(State initialState, State targetState)
        {
            foreach (var estimator in _estimators)
            {
                if (estimator.targetState.Equals(targetState))
                {
                    return estimator.estimate(initialState);
                }
            }

            throw new Exception("Evaluator not found for target" + targetState);
        }
    }
}