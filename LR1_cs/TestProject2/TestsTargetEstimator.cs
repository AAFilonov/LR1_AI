using System;
using System.Linq;
using LR1_AI_cs;
using LR1_AI_cs.ai.heurisitc.dbHeuristic;
using LR1_AI_cs.Properties;
using NUnit.Framework;

namespace TestProject2
{
    [TestFixture]
    public class TargetEstimatorTests
    {
        private TargetEstimator _targetEstimator;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            State targetState = new State();
            targetState._cells[0].color = Cell.Color.BLUE;
            targetState._cells[18].color = Cell.Color.BLUE;

            _targetEstimator = new TargetEstimator(targetState, new DB("TEST_DB.db"));
        }
        [OneTimeTearDown] public void Cleanup()
        { /* ... */ }

        [Test]
        public void testPartialTargetStates2()
        {
            State stateToTest = new State();
            stateToTest._cells[1].color = Cell.Color.BLUE; //to 0
            stateToTest._cells[17].color = Cell.Color.BLUE; // to 18
            int expectedScore = 2;
            int actualScore = _targetEstimator.estimate(stateToTest);
            Assert.AreEqual(expectedScore, actualScore);
        }

        [Test]
        public void testPartialTargetStates4()
        {
            State stateToTest = new State();
            stateToTest._cells[2].color = Cell.Color.BLUE; //to 0
            stateToTest._cells[16].color = Cell.Color.BLUE; // to 18
            int expectedScore = 4;
            int actualScore = _targetEstimator.estimate(stateToTest);
            Assert.AreEqual(expectedScore, actualScore);
        }


        [Test]
        public void testSplitingUpper()
        {
            State stateToTest = new State();
            stateToTest._cells[0].color = Cell.Color.BLUE;
            stateToTest._cells[18].color = Cell.Color.BLUE;

            State expectedUpper = new State();
            expectedUpper._cells.ToList().ForEach(cell => cell.color = Cell.Color.UNDEF);
            expectedUpper._cells[0].color = Cell.Color.BLUE;

            var actualUpper = TargetEstimator.prepareUpperPart(stateToTest);

            Assert.AreEqual(expectedUpper.Equals( actualUpper),true);
        }

        [Test]
        public void testSplitingLower()
        {
            State stateToTest = new State();
            stateToTest._cells[0].color = Cell.Color.BLUE;
            stateToTest._cells[18].color = Cell.Color.BLUE;

            State expectedLower = new State();
            expectedLower._cells.ToList().ForEach(cell => cell.color = Cell.Color.UNDEF);
            expectedLower._cells[18].color = Cell.Color.BLUE;

            var actualLower = TargetEstimator.prepareLowerPart(stateToTest);

            Assert.AreEqual(expectedLower.Equals( actualLower),true);
        }
    }
}