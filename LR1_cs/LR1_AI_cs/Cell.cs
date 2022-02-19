namespace LR1_AI_cs
{
    public class Cell
    {
        public enum Color
        {
            GREY,
            RED,
            BLUE,
            ORANGE
        }

        public enum Type
        {
            FIELD,
            TARGET
        }

        public int position { get; set; }
        public Color color { get; set; }
        public Type type { get; set; }

    }
}