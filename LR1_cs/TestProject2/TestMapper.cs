using System.Collections.Generic;
using LR1_AI_cs;
using LR1_AI_cs.ai.heurisitc.dbHeuristic;
using LR1_AI_cs.Properties;
using NUnit.Framework;

namespace TestProject2
{
    public class TestMapper
    {
        private List<State> _possibleTargets = new List<State>();

        [OneTimeSetUp]
        public void GlobalSetup()
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
        }


        [Test]
        public void test2PosMapping()
        {
            DB db_for_test = new DB("AI_DB_TEST.db");
            DbStateMapper mapper = new DbStateMapper(db_for_test);

           mapper.map(TargetEstimator.prepareLowerPart(_possibleTargets[0]), 20);
           mapper.map(TargetEstimator.prepareUpperPart(_possibleTargets[0]), 20);

           mapper.map(TargetEstimator.prepareLowerPart(_possibleTargets[1]), 20);
           mapper.map(TargetEstimator.prepareUpperPart(_possibleTargets[1]), 20);

            mapper.map(TargetEstimator.prepareLowerPart(_possibleTargets[2]), 20);
            mapper.map(TargetEstimator.prepareUpperPart(_possibleTargets[2]), 20);
        }
    }
}