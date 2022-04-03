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
        private List<State> _history = new List<State>();

        private List<ISolutionFinder> searchers = new List<ISolutionFinder>();
        private ISolutionFinder _solutionFinder;

        public Form1()
        {
            InitializeComponent();
            initPictureBoxes();
            searchers.Add(new InDepthSearchSearcher());
            searchers.Add(new InWidthSearcher());
            searchers.Add(new ManhattenHeuristicSearcher());
            searchers.Add(new HammingHeuristicSearcher());
            searchers.Add(new DbHeuristicSearcher());

            _solutionFinder = searchers[0];

            comboBoxSearcher.Items.Add("In depth search");
            comboBoxSearcher.Items.Add("In width search");
            comboBoxSearcher.Items.Add("Manhatten H search");
            comboBoxSearcher.Items.Add("Hamming Hsearch");
            comboBoxSearcher.Items.Add("DB search");

            comboBoxSearcher.SelectedIndex = 0;
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


        private void pictureBoxField_leftClick(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs) e;

            PictureBox pictureBox = (PictureBox) sender;
            Cell updatedCell = CellParser.parseCell(pictureBox);
            if (_gameState == GameState.PREPARATION)
            {
                updatedCell.color = _selectedColor;
                sync(updatedCell);
            }
            else if (_gameState == GameState.MANUAL)
            {
                if (me.Button == MouseButtons.Left)
                    doClockwiseTurn(updatedCell);
                if (me.Button == MouseButtons.Right)
                    doCounterClockwiseTurn(updatedCell);
            }
        }


        private void doClockwiseTurn(Cell updatedCell)
        {
            _board.moveClockwise(updatedCell.position);
            syncState(_board.currentState);
            _history.Add(_board.currentState);
            if (_board.isWin())
            {
                updateHistoryNumeric();
            }
        }

        private void doCounterClockwiseTurn(Cell updatedCell)
        {
            _board.moveCounterclockwise(updatedCell.position);
            syncState(_board.currentState);
            _history.Add(_board.currentState);
            if (_board.isWin())
            {
                updateHistoryNumeric();
            }
        }

        private void updateHistoryNumeric()
        {
            updateGameStateLabel(GameState.SOLUTION);

            _board.setCurrent(_history.Last());
            syncState(_board.currentState);
            _stateIndex = _history.Count - 1;
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
            _history.Add(_board.currentState);
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

            _history.Clear();
            _stateIndex = 0;
            numericUpDownHistory.Value = _stateIndex;
        }

        private async void buttonAutoStart_Click(object sender, EventArgs e)
        {
            //TODO костыль с асинхроном, сделать нормальное обновление
            updateGameStateLabel(GameState.AUTO);
            var history = _solutionFinder.findMoves(_board.currentState, _board.targetState);
            _history = history;
            string message = _history.Any() ? "Решение найдено" : "Нет решения";
            MessageBox.Show(message, "Поиск завершен");
            updateHistoryNumeric();
        }

        private void buttonHistoryBack_Click(object sender, EventArgs e)
        {
            if (_gameState == GameState.SOLUTION && _stateIndex > 0)
            {
                _stateIndex--;
                numericUpDownHistory.Value = _stateIndex;
                _board.setCurrent(_history[_stateIndex]);
                syncState(_board.currentState);
            }
        }

        private void buttonHistoryForward_Click(object sender, EventArgs e)
        {
            if (_gameState == GameState.SOLUTION && _stateIndex < _history.Count - 1)
            {
                _stateIndex++;
                numericUpDownHistory.Value = _stateIndex;
                _board.setCurrent(_history[_stateIndex]);
                syncState(_board.currentState);
            }
        }

        private void syncState(State state)
        {
            foreach (var cell in state._cells)
                sync(cell);
        }

        private void buttonRndGenerate_Click(object sender, EventArgs e)
        {
            int depth = (int) numericUpDownDepth.Value;
            State generatedState = StateRandomizer.generate(_board.targetState, depth);
            _board.setCurrent(generatedState);
            syncState(_board.currentState);
        }

        private void comboBoxSearcher_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = comboBoxSearcher.SelectedIndex;
            this._solutionFinder = searchers[selectedIndex];
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