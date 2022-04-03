using LR1_AI_cs;
using LR1_AI_cs.ai.heurisitc.dbHeuristic;
using LR1_AI_cs.Properties;
using NUnit.Framework;

namespace TestProject2
{
    public class TestDB
    {
        [Test]
        public void tesSave()
        {
            DB db_for_test = new DB("AI_DB_TEST.db");
            State state = prepareState();
            State state2 = prepareState();
            db_for_test.save(state, state2, 0);
        }

        [Test]
        public void testGet()
        {
            DB db_for_test = new DB("AI_DB_TEST.db");
            State templateState = prepareState();
            State targetState = prepareState();
            db_for_test.save(templateState, targetState, 0);
            db_for_test.findByTargetAndTemplate(Parser.toString(targetState), Parser.toString(templateState));
        }
        private State prepareState()
        {
            State st = new State();
            st._cells[0].color = Cell.Color.RED;
            st._cells[1].color = Cell.Color.BLUE;
            st._cells[2].color = Cell.Color.GRAY;
            st._cells[3].color = Cell.Color.UNDEF;
            st._cells[4].color = Cell.Color.ORANGE;
            return st;
        }
    }
}