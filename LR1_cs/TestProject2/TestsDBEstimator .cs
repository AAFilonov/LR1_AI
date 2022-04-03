using System;
using System.Collections.Generic;
using System.Linq;
using LR1_AI_cs;
using LR1_AI_cs.ai;
using LR1_AI_cs.ai.heurisitc.dbHeuristic;
using LR1_AI_cs.Properties;
using NUnit.Framework;

namespace TestProject2
{
    [TestFixture]
    public class DbEstimatorTests
    {
        private DbEstimator estimator;
        private readonly List<State> _possibleTargets = new List<State>();

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            estimator = new DbEstimator();


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

        [OneTimeTearDown]
        public void Cleanup()
        {
            /* ... */
        }

        [Test]
        public void testPartialTargetStates2Score2()
        {
            State stateToTest = new State();
            stateToTest._cells[1].color = Cell.Color.BLUE; //to 0
            stateToTest._cells[17].color = Cell.Color.BLUE; // to 18
            int expectedScore = 2;
            int actualScore = estimator.estimate(stateToTest, _possibleTargets[0]);
            Assert.AreEqual(expectedScore, actualScore);
        }

        [Test]
        public void testPartialTargetStates2Score4()
        {
            State stateToTest = new State();
            stateToTest._cells[2].color = Cell.Color.BLUE; //to 0
            stateToTest._cells[16].color = Cell.Color.BLUE; // to 18
            int expectedScore = 4;
            int actualScore = estimator.estimate(stateToTest, _possibleTargets[0]);
            Assert.AreEqual(expectedScore, actualScore);
        }

        [Test]
        public void testPartialTargetStates3Score3()
        {
            State stateToTest = new State();
            stateToTest._cells[0].color = Cell.Color.BLUE; //to 3
            stateToTest._cells[2].color = Cell.Color.BLUE; //to 6
            stateToTest._cells[18].color = Cell.Color.BLUE; //to 17
            int expectedScore = 3;
            int actualScore = estimator.estimate(stateToTest, _possibleTargets[1]);
            Assert.AreEqual(expectedScore, actualScore);
        }

        [Test]
        public void testPartialTargetStates3Score6()
        {
            State stateToTest = new State();
            stateToTest._cells[1].color = Cell.Color.BLUE; //to 3
            stateToTest._cells[12].color = Cell.Color.BLUE; //to 6
            stateToTest._cells[15].color = Cell.Color.BLUE; //to 17
            int expectedScore = 6;
            int actualScore = estimator.estimate(stateToTest, _possibleTargets[2]);
            Assert.AreEqual(expectedScore, actualScore);
        }
    }
}