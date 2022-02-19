using System;
using System.Windows.Forms;

namespace LR1_AI_cs
{
    public class CellParser
    {
        public static Cell parseCell(string tag)
        {
            var args = tag.Split(';');
            Cell cell = new Cell();
            cell.type = args[0] == "field" ? Cell.Type.FIELD : Cell.Type.TARGET;
            cell.position = Int32.Parse(args[1]);
            cell.color = parseColor(args[2]);

            return cell;
        }

        private static Cell.Color parseColor(string strToParse)
        {
            switch (strToParse)
            {
                case "gray":
                    return Cell.Color.GREY;
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
                    throw new Exception("invalid possible color");
            }
        }

        public static void parseCell(PictureBox pictureBox)
        {
            parseCell(pictureBox.Tag.ToString());
        }

        public static void sync(Cell cell, PictureBox pb)
        {
            
        }
    }
}