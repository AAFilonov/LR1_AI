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

        public Cell(int pos,Color color)
        {
            this.position = pos;
            this.color = color;
        }
        public int position { get; set; }
        public Color color { get; set; } = Color.GRAY;
        public Type type { get; set; }  = Type.FIELD;
     

        public String constructTag()
        {
            return typeToString(type)+";"+ position + ";" +colorToString(color);
        }

        private string typeToString(Type type)
        {
            switch (type)
            {
                case Type.FIELD:
                    return "field";
                    break;
                case Type.TARGET:
                    return "target";
                    break; 
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private  static String colorToString(Color color)
        {
            switch (color)
            {
                case Color.GRAY:
                    return "gray";
                    break;
                case Color.RED:
                    return "red";
                    break;
                case Color.BLUE:
                    return "blue";
                    break;
                case Color.ORANGE:
                    return "orange";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(color), color, null);
            }

            ;
        }
    }
}