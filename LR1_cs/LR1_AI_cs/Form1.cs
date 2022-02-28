using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LR1_AI_cs.ai;
using LR1_AI_cs.Properties;

namespace LR1_AI_cs
{
    public partial class Form1 : Form
    {
        private readonly Board _board = new Board();
        private readonly Dictionary<int, PictureBox> _fieldPictureBoxes = new Dictionary<int, PictureBox>();
        private readonly Dictionary<int, PictureBox> _targetPictureBoxes = new Dictionary<int, PictureBox>();
        private GameState _gameState = GameState.PREPARATION;
        private Cell.Color _selectedColor = Cell.Color.ORANGE;
        private int _stateIndex = 0;
        private History _history = new History();
        private ISolutionFinder _solutionFinder = new InDepthSearchFinder();

        public Form1()
        {
            InitializeComponent();
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
                doTurn(updatedCell);
            }
        }

        private void doTurn(Cell updatedCell)
        {
            _board.rotateClockwise(updatedCell.position);
            syncState(_board.currentState);
            _history.addState(_board.currentState);
            if (_board.isWin())
            {
                updateHistoryNumeric();
            }
        }

        private void updateHistoryNumeric()
        {
            updateGameStateLabel(GameState.SOLUTION);
            syncState(_history.getLastState());
            _stateIndex = _history.getHistoryDepth();
            numericUpDownHistory.Value = _stateIndex;
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

        private void buttonManualStart_Click(object sender, EventArgs e)
        {
            updateGameStateLabel(GameState.MANUAL);
            _history.addState(_board.currentState);
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            updateGameStateLabel(GameState.PREPARATION);
            _board.resetAll();
            foreach (var bp in _fieldPictureBoxes.Values)
            {
                chagePictureBoxColor(bp, Cell.Color.GRAY);
                
            }

            foreach (var bp in _targetPictureBoxes.Values)
            {
                chagePictureBoxColor(bp, Cell.Color.GRAY);
            }

            _history.reset();
            _stateIndex = 0;
            numericUpDownHistory.Value = _stateIndex;
        }

        private async void buttonAutoStart_Click(object sender, EventArgs e)
        {
            //TODO костыль с асинхроном, сделать нормальное обновление
            updateGameStateLabel(GameState.AUTO);
            _history = await _solutionFinder.findAsync(_board.currentState,_board.targetState);
            string message = _history.isEmpty() ? "Нет решения" : "Решение найдено";
            MessageBox.Show(message, "Поиск завершен");
            updateHistoryNumeric();
        }

        private void buttonHistoryBack_Click(object sender, EventArgs e)
        {
            if (_gameState == GameState.SOLUTION && _stateIndex > 0)
            {
                _stateIndex--;
                numericUpDownHistory.Value = _stateIndex;
                _board.setCurrent(_history.getState(_stateIndex));
                syncState(_board.currentState);
            }
        }

        private void buttonHistoryForward_Click(object sender, EventArgs e)
        {
            if (_gameState == GameState.SOLUTION && _stateIndex < _history.getHistoryDepth())
            {
                _stateIndex++;
                numericUpDownHistory.Value = _stateIndex;
                _board.setCurrent(_history.getState(_stateIndex));
                syncState(_board.currentState);
            }
        }

        private void syncState(State state)
        {
            state._cells.ToList().ForEach(cell => sync(cell));
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