using System;
using System.Collections.Generic;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs.ai.heurisitc.dbHeuristic
{
    public class DbStateMapper
    {
        private DB _db;

        public DbStateMapper(DB db)
        {
            _db = db;
        }

        public void map(State targetState, int maxDepth)
        {
            Queue<State> OpenQueue = new Queue<State>();
            Queue<State> CloseQueue = new Queue<State>();

            OpenQueue.Enqueue(new State(targetState));

            int depth = 0;
            while (OpenQueue.Count != 0 && depth <= maxDepth)
            {
                State currentState = OpenQueue.Dequeue();

                var childs = Game.getAllChildren(currentState);
                childs.RemoveAll(state => Utils.containsValue(CloseQueue, state) ||
                                          Utils.containsValue(OpenQueue, state));
                foreach (var child in childs)
                {
                    OpenQueue.Enqueue(child);
                }

                depth = AbstractSolutionSearcher.generateHistory(currentState).Count-1;
                _db.save(currentState, targetState, depth);
                CloseQueue.Enqueue(currentState);
            }

            Console.WriteLine("Open: " + OpenQueue.Count + " Closed: " + CloseQueue.Count);
        }
    }
}