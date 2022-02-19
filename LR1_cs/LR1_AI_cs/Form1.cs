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
        public Form1()
        {
            InitializeComponent();
        }


        private void pictureBoxField_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox) sender;
            int postion = CellParser.parseCell(pictureBox).position;
            board.rotateAround(postion);
        }

        
    }
}