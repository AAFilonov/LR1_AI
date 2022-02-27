using System;

namespace LR1_AI_cs
{
    public class Cell
    {
        public enum Color
        {
            GRAY,
            RED,
            BLUE,
            ORANGE
            
        }   

        public enum Type
        {
            FIELD,
            TARGET
        }

        public Cell( int position, Color color)
        {
            this.type = Type.FIELD;
            this.position = position;
            this.color = color;
        }
        public Cell(Type type, int position, Color color)
        {
            this.type = type;
            this.position = position;
            this.color = color;
        }
        public int position { get; set; }
        public Color color { get; set; } = Color.GRAY;
        public Type type { get; set; }  = Type.FIELD;
     

        public String constructTag()
        {
            return CellParser.typeToString(type)+";"+ position + ";" +CellParser.colorToString(color);
        }

       
       
    }
}