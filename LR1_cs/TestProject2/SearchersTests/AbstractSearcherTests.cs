using System;
using System.Collections.Generic;
using System.Diagnostics;
using LR1_AI_cs;
using LR1_AI_cs.ai;
using LR1_AI_cs.Properties;
using NUnit.Framework;
using NUnit.Framework.Api;

namespace TestProject2.SearchersTests
{
    public abstract class AbstractSearcherTests
    {
        protected ISolutionFinder _searcher;

        public abstract ISolutionFinder getSearcher();
        private long memoryPerNode = memoryForNode();

        [OneTimeSetUp]
        public void Setup()
        {
            //назанчает различную реализацию в каждом алгоритме
            this._searcher = getSearcher();
        }

        public delegate List<State> MyTest();

        public void runTest(MyTest test)
        {
            Stopwatch clock = Stopwatch.StartNew();
            var result = test();
            clock.Stop();

            AbstractSolutionSearcher stat = _searcher as AbstractSolutionSearcher;

            Console.WriteLine("Proc: " + Process.GetCurrentProcess().Id);
            Console.WriteLine("Длина пути: " + result.Count);
            Console.WriteLine("Итераций: " + stat.iterationsCount);
            Console.WriteLine("Открытых узлов: " + stat.countOpen);
            Console.WriteLine("Закрытых узлов: " + stat.countClosed);
            Console.WriteLine("Всего узлов: " + (stat.countOpen + stat.countClosed));
            Console.WriteLine("");
            Console.WriteLine("Время: " + clock.Elapsed.Milliseconds + " ms");
            Console.WriteLine("Память: " + (stat.countOpen + stat.countClosed) * memoryPerNode / 1000 + " KB");
        }


        public static long memoryForNode()
        {
            var before = GC.GetTotalMemory(true);
            var node = new Tuple<int, State>(0, new State());
            // var MyByteArray = new byte[20000];
            var after = GC.GetTotalMemory(true);
            long totalBytesOfMemoryUsedKB = (after - before);
            return totalBytesOfMemoryUsedKB;
        }

        [Test]
        public void _1test_depth1()
        {
            runTest(() =>
            {
                State initialState = new State();
                initialState._cells[0].color = Cell.Color.RED;

                State targetState = new State();
                targetState._cells[1].color = Cell.Color.RED;
                var result = _searcher.findMoves(initialState, targetState);
                Assert.NotZero(result.Count);
                return result;
            });
        }


        [Test]
        public void _1test_depth1Multiple()
        {
            runTest(() =>
            {
                State initialState = new State();
                initialState._cells[0].color = Cell.Color.RED;

                State targetState = new State();
                targetState._cells[1].color = Cell.Color.RED;
                _searcher.findMoves(initialState, targetState);
                var result = _searcher.findMoves(initialState, targetState);
                Assert.NotZero(result.Count);
                return result;
            });
        }

        [Test]
        public void _2test_depth3()
        {
            runTest(() =>
            {
                State initialState = new State();
                initialState._cells[10].color = Cell.Color.RED;

                State targetState = new State();
                targetState._cells[17].color = Cell.Color.RED;
                var result = _searcher.findMoves(initialState, targetState);
                Assert.NotZero(result.Count);
                return result;
            });
        }

        [Test]
        public void _3test_2balls_depth2_rnd1()
        {
            runTest(() =>
            {
                State targetState = new State();
                targetState._cells[0].color = Cell.Color.RED;
                targetState._cells[18].color = Cell.Color.RED;

                State initialState = new State();
                initialState._cells[3].color = Cell.Color.RED;
                initialState._cells[17].color = Cell.Color.RED;


                var result = _searcher.findMoves(initialState, targetState);
                Assert.NotZero(result.Count);
                return result;
            });
        }

        [Test]
        public void _4test_2balls_depth4_rnd2()
        {
            runTest(() =>
            {
                State targetState = new State();
                targetState._cells[0].color = Cell.Color.RED;
                targetState._cells[18].color = Cell.Color.RED;

                State initialState = new State();
                initialState._cells[1].color = Cell.Color.RED;
                initialState._cells[16].color = Cell.Color.RED;


                var result = _searcher.findMoves(initialState, targetState);
                Assert.NotZero(result.Count);
                return result;
            });
        }

        [Test]
        public void _5test_2balls_depth5_rnd1()
        {
            runTest(() =>
            {
                State targetState = new State();
                targetState._cells[0].color = Cell.Color.RED;
                targetState._cells[18].color = Cell.Color.RED;

                State initialState = new State();
                initialState._cells[7].color = Cell.Color.RED;
                initialState._cells[9].color = Cell.Color.RED;


                var result = _searcher.findMoves(initialState, targetState);
                Assert.NotZero(result.Count);
                return result;
            });
        }

        [Test]
        public void _6test_2balls_depthMax()
        {
            runTest(() =>
            {
                State targetState = new State();
                targetState._cells[0].color = Cell.Color.RED;
                targetState._cells[18].color = Cell.Color.RED;

                State initialState = new State();
                initialState._cells[6].color = Cell.Color.RED;
                initialState._cells[13].color = Cell.Color.RED;


                var result = _searcher.findMoves(initialState, targetState);
                Assert.NotZero(result.Count);
                return result;
            });
        }
    }
}