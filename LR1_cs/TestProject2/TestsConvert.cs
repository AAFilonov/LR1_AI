using System;
using LR1_AI_cs;
using LR1_AI_cs.ai.heurisitc.dbHeuristic;
using LR1_AI_cs.Properties;
using NUnit.Framework;

namespace TestProject2
{
    [TestFixture]
    public class Test_Db_convert
    {
        [Test]
        public void testConvertStateToArray()
        {
            State st = prepareState();
            var arr = Parser.toArray(st);
            DB.print(arr, 0);
        }

        [Test]
        public void testConvertArrayToString()
        {
            State st = prepareState();
            var arr = Parser.toArray(st);
            var str = Parser.toString(arr);
            Console.WriteLine(str);
            
            Assert.AreEqual("[r,b,g,*,o,g,g,g,g,g,g,g,g,g,g,g,g,g,g,]", str);
        }

        [Test]
        public void testConvertStringToArray()
        {
            State st = prepareState();
            var arr = Parser.toArray(st);
            var str = Parser.toString(arr);
            var arr2 = Parser.arrayFromString(str);
            Console.WriteLine(str);
            Assert.AreEqual(arr, arr2);
        }

        [Test]
        public void testConvertArrayToState()
        {
            State state = prepareState();
            var arr = Parser.toArray(state);
            var state2 = Parser.fromArray(arr);
          
            Assert.AreEqual(true, state.Equals(state2));
        }

        [Test]
        public void testConvertStateToStringToState()
        {
            State state = prepareState();
            var str = Parser.toString(state);
            var state2 = Parser.fromString(str);
            Assert.AreEqual(true, state.Equals(state2));
        }
        [Test]
        public void testConvertStateToArrayToState()
        {
            State state = prepareState();
            var arr = Parser.toArray(state);
            Console.WriteLine( Parser.toString(arr));
            var state2 = Parser.fromArray(arr);
            Assert.AreEqual(true, state.Equals(state2));
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