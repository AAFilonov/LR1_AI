using System;
using System.Collections.Generic;

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


        public override string ToString()
        {
            return $"{nameof(position)}: {position}, {nameof(color)}: {color}, {nameof(type)}: {type}";
        }

        private sealed class ColorEqualityComparer : IEqualityComparer<Cell>
        {
            public bool Equals(Cell x, Cell y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.color == y.color;
            }

            public int GetHashCode(Cell obj)
            {
                return (int) obj.color;
            }
        }

        public static IEqualityComparer<Cell> ColorComparer { get; } = new ColorEqualityComparer();
    }
}