namespace LR1_AI_cs.Properties
{
    public class HeuristicState: State
    {
        public int score { get; set; } = 0;

        public HeuristicState(State state, int score)
        {
            this.parent = state.parent;
            this._cells = state._cells;
            this.score = score;
        }
    }
}