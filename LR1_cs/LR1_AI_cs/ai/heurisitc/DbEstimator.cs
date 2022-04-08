using System;
using System.Collections.Generic;
using System.Linq;
using LR1_AI_cs.ai.heurisitc.dbHeuristic;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs.ai
{
    public class DbEstimator : IHeuristicEstimator
    {
        private readonly string _dbName;
        private readonly DB _db;
        private readonly List<TargetEstimator> _estimators = new List<TargetEstimator>();

        private int MAX_DEPTH = 10;

        private DbStateMapper _stateMapper;

        public DbEstimator(string dbName)
        {
            _dbName = dbName;
            _db = new DB(_dbName);
            _stateMapper = new DbStateMapper(_db);
            
            _db.findDistingTargetStates().ForEach(state => _estimators.Add(new TargetEstimator(state, _db)));
        }

        public int estimate(State initialState, State targetState)
        {
          
            try
            {
                var targetTemplate = toTemplate(targetState);
                var initialTemplate = toTemplate(initialState);
                Console.WriteLine("Starting to find  "+initialTemplate.toString()+" "+ targetTemplate.toString() );
                foreach (var estimator in _estimators)
                {
                    if (estimator.targetState.Equals(targetTemplate))
                    {
                        Console.WriteLine("Result for" + initialState.toString() +" "+targetState.toString()  + "  was found  instanly");
                        return estimator.estimate(initialTemplate);
                    }
                }
                //эстиматор для такого целевого состояния не найден
                return mapNewTargetState(initialState, targetState);
            }
            catch (Exception e)
            {
                //эстиматор нашелся, но расчитанной глубины для него не хватило
                return mapNewTargetState(initialState, targetState);
            }

        }

        private int mapNewTargetState(State initialState, State targetState)
        {

            var targetTemplate = toTemplate(targetState);
            var initialTemplate = toTemplate(initialState);
          
            int depth = 1;
            var estimatorForNewTargetState = new TargetEstimator(targetTemplate, _db);
            while (depth <= MAX_DEPTH)
            {
                Console.WriteLine("Trying to map state " + targetTemplate.toString() + " with depth " + depth);
                _stateMapper.map(targetTemplate, depth);
                try
                {
                    int result = estimatorForNewTargetState.estimate(initialTemplate);
                    //если не нашлось - выкинет исключение
                    Console.WriteLine("Result for" +initialTemplate.toString()+" "+ targetTemplate.toString() + "  was found  on depth " + depth);
                    _estimators.Add(estimatorForNewTargetState);
                    return result;
                }
                catch (Exception e)
                {
                    depth++;
                }
            }

            throw new Exception("State " + targetState + " is too deep for mapping!");
        }

        private State toTemplate(State state)
        {
            State template = new State(state);
            int amountOfColors = state._cells.GroupBy(cell => cell.color).Count();

            template._cells.ToList().ToList().ForEach(cell =>
            {
                if (cell.color == Cell.Color.GRAY)
                {
                    cell.color = Cell.Color.UNDEF;
                }
                //если цветов всего два - преобразовываем не-серые в синий
                else if (amountOfColors == 2)
                {
                    cell.color = Cell.Color.BLUE;
                }
                //Иначе считаем каждую комбинацию расположения шаров разных цветов уникальным целевым состоянием
            });
            return template;
        }
    }
}