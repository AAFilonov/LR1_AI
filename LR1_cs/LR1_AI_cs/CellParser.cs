using System;
using System.Windows.Forms;

namespace LR1_AI_cs
{
    public class CellParser
    {
        public static Cell parseCell(string tag)
        {
            var args = tag.Split(';');
     
            int position = Int32.Parse(args[1]);
            Cell.Color color  = parseColor(args[2]);
            return new Cell(position,color);
        }

        private static Cell.Color parseColor(string strToParse)
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
                    throw new Exception("invalid possible color");
            }
        }

        public static Cell parseCell(PictureBox pictureBox)
        {
           return  parseCell(pictureBox.Tag.ToString());
        }


      
    }
}