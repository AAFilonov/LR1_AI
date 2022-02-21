using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs
{
    
    public partial class Form1 : Form
    {
        private Board board = new Board();
        private Dictionary<int, PictureBox> fieldPictureBoxes = new Dictionary<int, PictureBox>();
        private Dictionary<int, PictureBox> targetPictureBoxes = new Dictionary<int, PictureBox>();
        public Form1()
        {
            InitializeComponent();
            
            foreach (Control item in this.Controls)
            {
                String tag = item.Tag.ToString();
                if (tag.StartsWith("field"))
                {
                    PictureBox pictureBox = (PictureBox) item;
                    int position = CellParser.parseCell(pictureBox).position;
                    fieldPictureBoxes.Add(position,pictureBox);        
                }
                else if(tag.StartsWith("target"))
                {
                    PictureBox pictureBox = (PictureBox) item;
                    int position = CellParser.parseCell(pictureBox).position;
                    targetPictureBoxes.Add(position,pictureBox);  
                }
            } 
        }


        private void pictureBoxField_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox) sender;
            int postion = CellParser.parseCell(pictureBox).position;
            board.rotateAround(postion);
        }
        public void sync(Cell cell)
        {
            PictureBox pictureBox = fieldPictureBoxes[cell.position];
            chagePictureBoxColor (fieldPictureBoxes[cell.position],cell.color);
            pictureBox.Tag = cell.constructTag();
        }

        private void chagePictureBoxColor(PictureBox pictureBox, Cell.Color newColor)
        {
            switch (newColor)
            {
                case Cell.Color.GRAY:
                    ///pictureBox.Image;
                    break;
                case Cell.Color.RED:
                    break;
                case Cell.Color.BLUE:
                    break;
                case Cell.Color.ORANGE:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newColor), newColor, null);
            }
        }
    }
}