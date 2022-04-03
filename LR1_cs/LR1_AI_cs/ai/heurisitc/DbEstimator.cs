using System;
using System.Collections.Generic;
using LR1_AI_cs.ai.heurisitc.dbHeuristic;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs.ai
{
    public class DbEstimator : IHeuristicEstimator
    {
        private const string DB_NAME = "AI_DB_PROD.db";
        private DB _db = new DB(DB_NAME);
        private List<TargetEstimator> _estimators = new List<TargetEstimator>();
        private readonly List<State> _possibleTargets = new List<State>();

        private DbStateMapper stateMapper;

        public DbEstimator()
        {
            stateMapper = new DbStateMapper(_db);
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
            DbStateMapper mapper = new DbStateMapper(_db);

            mapper.mapPartial(_possibleTargets[0], 20);
            mapper.mapPartial(_possibleTargets[1], 20);
            mapper.mapPartial(_possibleTargets[2], 20);
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

            // целевое состояние не найдено - нужно найти!
            //нужно добавить в БД таблицу с парами полное состояние + частичный его шаблон 
            // при запуске читать из таблицы все такие записи и для каждой создавать эстиматор
            //когда натыкаемся на состояние, для которого эстиматора нет,
            //выполняем маппинг, создаем эстиматор в и заносим в бд запись об нем
            //тогда в соедующий раз эстиматор прочитается из базы - самополполняемая база!
            // stateMapper.mapPartial(targetState, 10);
        }
    }
}