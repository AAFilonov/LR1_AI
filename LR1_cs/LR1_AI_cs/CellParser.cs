using System;
using System.Drawing;
using System.Windows.Forms;

namespace LR1_AI_cs
{
    public class CellParser
    {
        public static Cell parseCell(string tag)
        {
            var args = tag.Split(';');
     
            Cell.Type type = parseType(args[0]);
            int position = Int32.Parse(args[1]);
            Cell.Color color  = parseColor(args[2]);
            return new Cell(type, position,color);
        }

        public static Cell.Color parseColor(string strToParse)
        {
            switch (strToParse)
            {
                case "gray":
                    return Cell.Color.GRAY;
                    break;
                case "red":
                    return Cell.Color.RED;
                    break;
                case "blue":
                    return Cell.Color.BLUE;
                    break;
                case "orange":
                    return Cell.Color.ORANGE;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(strToParse), strToParse, null);
            }
        }
        public  static String colorToString(Cell.Color color)
        {
            switch (color)
            {
                case Cell.Color.GRAY:
                    return "gray";
                    break;
                case Cell.Color.RED:
                    return "red";
                    break;
                case Cell.Color.BLUE:
                    return "blue";
                    break;
                case Cell.Color.ORANGE:
                    return "orange";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(color), color, null);
            }

            ;
        }
        public static string typeToString(Cell.Type type)
        {
            switch (type)
            {
                case Cell.Type.FIELD:
                    return "field";
                    break;
                case Cell.Type.TARGET:
                    return "target";
                    break; 
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        public static Cell.Type parseType(string strToParse)
        {
            switch (strToParse)
            {
                case "field":
                    return Cell.Type.FIELD;
                    break;
                case "target":
                    return Cell.Type.TARGET;
                default:
                    throw new ArgumentOutOfRangeException(nameof(strToParse), strToParse, null);
            }
        }

        public static Cell parseCell(PictureBox pictureBox)
        {
           return  parseCell(pictureBox.Tag.ToString());
        }


      
    }
}