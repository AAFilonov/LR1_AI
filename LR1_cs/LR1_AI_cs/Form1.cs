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
        private readonly Board _board;
        private readonly Dictionary<int, PictureBox> _fieldPictureBoxes = new Dictionary<int, PictureBox>();
        private readonly Dictionary<int, PictureBox> _targetPictureBoxes = new Dictionary<int, PictureBox>();
        private GameState _gameState = GameState.PREPARATION;
        private Cell.Color _selectedColor = Cell.Color.ORANGE;

        public Form1()
        {
            InitializeComponent();
            _board = new Board(this);
            initPictureBoxes();
        }

        private void initPictureBoxes()
        {
            foreach (PictureBox item in panel1.Controls.OfType<PictureBox>())
            {
                String tag = item.Tag.ToString();
                if (tag.StartsWith("field"))
                {
                    int position = CellParser.parseCell(item).position;
                    _fieldPictureBoxes.Add(position, item);
                }
            }

            foreach (Control item in panel2.Controls.OfType<PictureBox>())
            {
                String tag = item.Tag.ToString();
                if (tag.StartsWith("target"))
                {
                    PictureBox pictureBox = (PictureBox) item;
                    int position = CellParser.parseCell(pictureBox).position;
                    _targetPictureBoxes.Add(position, pictureBox);
                }
            }
        }


        private void pictureBoxField_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox) sender;
            Cell updatedCell = CellParser.parseCell(pictureBox);
            if (_gameState == GameState.PREPARATION)
            {
                updatedCell.color = _selectedColor;
                sync(updatedCell);
            }
            else if (_gameState == GameState.MANUAL)
            {
                _board.rotateClockwise(updatedCell.position);
            }
        }

        private void pictureBoxTarget_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox) sender;
            Cell updatedCell = CellParser.parseCell(pictureBox);
            if (_gameState == GameState.PREPARATION)
            {
                updatedCell.color = _selectedColor;
                sync(updatedCell);
            }
        }

        private void pictureBoxColor_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox) sender;
            String tag = pictureBox.Tag.ToString();
            _selectedColor = CellParser.parseColor(tag);
            changeSelectedColorLabel(_selectedColor);
        }

        public void sync(Cell cell)
        {
            PictureBox pbToSync = null;
            //TODO поправить костыль - нумерация в борде начинается с 1 
            if (cell.type == Cell.Type.FIELD)
            {
                pbToSync = _fieldPictureBoxes[cell.position];
                _board.currentState._cells[cell.position - 1].color = cell.color;
            }
            else if (cell.type == Cell.Type.TARGET)
            {
                pbToSync = _targetPictureBoxes[cell.position];
                _board.targetState._cells[cell.position - 1].color = cell.color;
            }

            chagePictureBoxColor(pbToSync, cell.color);
            pbToSync.Tag = cell.constructTag();
        }

        private void chagePictureBoxColor(PictureBox pictureBox, Cell.Color newColor)
        {
            var resources = new ComponentResourceManager(typeof(Form1));
            switch (newColor)
            {
                case Cell.Color.GRAY:
                    pictureBox.Image = (Image) resources.GetObject("pictureBox39.Image");
                    break;
                case Cell.Color.RED:
                    pictureBox.Image = (Image) resources.GetObject("pictureBox41.Image");
                    break;
                case Cell.Color.BLUE:
                    pictureBox.Image = (Image) resources.GetObject("pictureBox40.Image");
                    break;
                case Cell.Color.ORANGE:
                    pictureBox.Image = (Image) resources.GetObject("pictureBox42.Image");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newColor), newColor, null);
            }

            pictureBox.Refresh();
        }

        public void changeGameState(GameState newState)
        {
            _gameState = newState;
            switch (newState)
            {
                case GameState.PREPARATION:
                    labelGameState.Text = "Состояние: ПОДГОТОВКА";
                    break;
                case GameState.MANUAL:
                    labelGameState.Text = "Состояние: ИГРА";
                    break;
                case GameState.AUTO:
                    labelGameState.Text = "Состояние: АВТО";
                    break;
                case GameState.SOLUTION:
                    labelGameState.Text = "Состояние: РЕШЕНИЕ";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        private void changeSelectedColorLabel(Cell.Color newColor)
        {
            switch (newColor)
            {
                case Cell.Color.GRAY:
                    labelSelectedColor.Text = "Выбран: серый";
                    break;
                case Cell.Color.RED:
                    labelSelectedColor.Text = "Выбран: красный";
                    break;
                case Cell.Color.BLUE:
                    labelSelectedColor.Text = "Выбран: синий";
                    break;
                case Cell.Color.ORANGE:
                    labelSelectedColor.Text = "Выбран: оранжевый";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newColor), newColor, null);
            }
        }

        private void buttonManualStart_Click(object sender, EventArgs e)
        {
            changeGameState(GameState.MANUAL);
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            changeGameState(GameState.PREPARATION);
            _board.resetAll();
            foreach (var bp in _fieldPictureBoxes.Values)
            {
                chagePictureBoxColor(bp, Cell.Color.GRAY);
            }
            foreach (var bp in _targetPictureBoxes.Values)
            {
                chagePictureBoxColor(bp, Cell.Color.GRAY);
            }
        }

        private void buttonAutoStart_Click(object sender, EventArgs e)
        {
            changeGameState(GameState.AUTO);
        }
    }


    public enum GameState
    {
        PREPARATION,
        MANUAL,
        AUTO,
        SOLUTION
    }
}