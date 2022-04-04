using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs.ai
{
    public class BoundedManhattenHeuristicSearcher : AbstractSolutionSearcher
    {
        private int _nodeLimit;
        private IHeuristicEstimator _heuristicEstimator = new ManhattenEstimator();

        public BoundedManhattenHeuristicSearcher(int nodeLimit)
        {
            _nodeLimit = nodeLimit;
        }

        public override List<State> findMoves(State inititalState, State targetState)
        {
            List<Tuple<int, State>> OpenNodes = new List<Tuple<int, State>>();
            List<Tuple<int, State>> ClosedNodes = new List<Tuple<int, State>>();


            OpenNodes.Add(new Tuple<int, State>(
                _heuristicEstimator.estimate(inititalState, targetState),
                inititalState
            ));
            var iterations = 0;
            
            
            
            while (OpenNodes.Count != 0)
            {
                iterations++;
                var currentNode = OpenNodes[0];
                OpenNodes.RemoveAt(0);


                if (currentNode.Item2.Equals(targetState))
                {
                    updateStatisitcs(OpenNodes.Count, ClosedNodes.Count, iterations);
                    return generateHistory(currentNode.Item2);
                }

                ClosedNodes.Add(currentNode);
                List<State> childNodes = openState(currentNode.Item2);
                foreach (var childNode in childNodes)
                {
                    if (ClosedNodes.Count + OpenNodes.Count >= _nodeLimit)
                    {
                        //удалить наихудший узел
                        var worstNode = OpenNodes.Last();
                        OpenNodes.RemoveAt(OpenNodes.Count - 1);
                        //зарезервировать его значение в его родительском узле
                        var parentNodeIndex = OpenNodes.FindIndex(tuple =>
                            tuple.Item2.toString().Equals(worstNode.Item2.parent.toString()));
                        if (parentNodeIndex != -1) //Родительский узел в списке закрытых
                        {
                            OpenNodes.Add(
                                new Tuple<int, State>(worstNode.Item1, OpenNodes[parentNodeIndex].Item2));
                        }

                        else
                        {
                            parentNodeIndex = ClosedNodes.FindIndex(tuple =>
                                tuple.Item2.toString().Equals(worstNode.Item2.parent.toString()));
                            OpenNodes.Add(new Tuple<int, State>(worstNode.Item1, ClosedNodes[parentNodeIndex].Item2));
                        }
                    }

                    var score = calcScore(childNode, targetState);

                    var openNode = OpenNodes.Find(tuple => tuple.Item1.Equals(childNode));
                    var closedNode = ClosedNodes.Find(tuple => tuple.Item1.Equals(childNode));
                    if (openNode != null && openNode.Item1 < score)
                    {
                        OpenNodes.Remove(openNode);
                        OpenNodes.Add(new Tuple<int, State>(score, childNode));
                    }
                    else if (closedNode != null && closedNode.Item1 < score)
                    {
                        ClosedNodes.Remove(closedNode);
                        OpenNodes.Add(new Tuple<int, State>(score, childNode));
                    }
                    else
                    {
                        OpenNodes.Add(new Tuple<int, State>(score, childNode));
                    }
                }

                OpenNodes.Sort((tuple1, tuple2) => tuple1.Item1.CompareTo(tuple2.Item1));
                // ClosedNodes.Sort((tuple1, tuple2) => tuple1.Item1.CompareTo(tuple2.Item1));
            }


            //no solution, return empty history
            return new List<State>();
        }

        private int calcScore(State stateToProcess, State target)
        {
            return calcWayToNode(stateToProcess) +
                   _heuristicEstimator.estimate(stateToProcess, target);
        }

        private int calcWayToNode(State stateToProcess)
        {
            int pathLength = 0;
            State currentState = stateToProcess;
            while (currentState.parent != null)
            {
                pathLength++;
                currentState = currentState.parent;
            }

            return pathLength;
        }
    }
}