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

        public Cell(int pos,Color color)
        {
            this.position = pos;
            this.color = color;
        }
        public int position { get; set; }
        public Color color { get; set; } = Color.GREY;
     

    }
}