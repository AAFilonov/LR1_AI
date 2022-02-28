using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LR1_AI_cs
{
    public partial class Form1
    {
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

        public void updateGameStateLabel(GameState newState)
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
    }
}