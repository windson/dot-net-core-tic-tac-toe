using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TicTacToe.Util;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        private static CurrentPlayer currentPlayer;
        private static WinStatus gameState;
        private Player Player1 { get; set; }
        private Player Player2 { get; set; }
        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            currentPlayer = CurrentPlayer.Player1;

            Player1 = new Player
            {
                Marks = new List<int>()
            };
            Player2 = new Player
            {
                Marks = new List<int>()
            };
            gameState = WinStatus.Playing;

            lblStatus.Text = string.Empty;
            lblStatus.ForeColor = Color.Lime;
            lblPlayer1.ForeColor = lblPlayer2.ForeColor = Color.Black;
            btn11.Text = btn12.Text = btn13.Text = btn21.Text = btn22.Text = btn23.Text = btn31.Text = btn32.Text = btn33.Text = string.Empty;
            btn11.BackColor = btn12.BackColor = btn13.BackColor = btn21.BackColor = btn22.BackColor = btn23.BackColor = btn31.BackColor = btn32.BackColor = btn33.BackColor = Control.DefaultBackColor;

        }
        private static Color currentButtonColor;
        private void btn_MouseEnter(object sender, EventArgs e)
        {
            currentButtonColor = (sender as Button).BackColor;
            (sender as Button).BackColor = Color.LightGreen;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            (sender as Button).BackColor = currentButtonColor;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (gameState == WinStatus.Win) return;

            var curBtn = (sender as Button);
            
            int currIdx;
            int.TryParse(curBtn.AccessibleName, out currIdx);
            
            if (!string.IsNullOrWhiteSpace(curBtn.Text))
            {
                MessageBox.Show("No Cheating");
                return;
            }

            switch (currentPlayer)
            {
                case CurrentPlayer.Player1:
                    curBtn.Text = "X";
                    currentButtonColor = curBtn.BackColor = Color.LimeGreen;
                    Player1.Marks.Add(currIdx);

                    lblPlayer2.ForeColor = Color.Green;
                    lblPlayer1.ForeColor = Color.Black;
                    break;
                case CurrentPlayer.Player2:
                    curBtn.Text = "O";
                    currentButtonColor = curBtn.BackColor = Color.Gold;
                    Player2.Marks.Add(currIdx);

                    lblPlayer1.ForeColor = Color.Green;
                    lblPlayer2.ForeColor = Color.Black;
                    break;
            }
            EvaluateWinner();
        }

        private void EvaluateWinner()
        {

            try
            {
                var playerToEvaluate = currentPlayer == CurrentPlayer.Player1 ? Player1 : Player2;

                if (playerToEvaluate.Marks.Count < 3) return;


                foreach (var winSequence in WinningCriterions)
                {
                    var matches = playerToEvaluate.Marks.Intersect(winSequence).ToList();
                    if (matches.Count == 3)
                    {
                        WinningButtons(matches);
                        gameState = WinStatus.Win;
                        playerToEvaluate.HasWon = true;
                        lblStatus.Text = currentPlayer.ToString() + " has Won 🎉";
                        lblPlayer1.ForeColor = lblPlayer2.ForeColor = Color.Black;
                        break;
                    }
                }

                if (gameState == WinStatus.Playing && Player1.Marks.Count + Player2.Marks.Count == 9)
                {
                    gameState = WinStatus.Draw;
                    lblPlayer1.ForeColor = lblPlayer2.ForeColor = Color.Red;
                    lblStatus.ForeColor = Color.Red;
                    lblStatus.Text = "Game Draw! :(";
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (gameState == WinStatus.Playing)
                {
                    currentPlayer = currentPlayer == CurrentPlayer.Player1 ? CurrentPlayer.Player2 : CurrentPlayer.Player1;
                    lblStatus.Text = currentPlayer.ToString() + "'s Turn";
                }
            }
        }

        private void WinningButtons(List<int> lstMatches)
        {
            foreach (Control control in this.Controls)
            {
                LoopControls(control, lstMatches);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            InitializeGame();
        }
        static void LoopControls(Control control, List<int> lstMatches)
        {
            switch (control)
            {
                case Button btn:
                    int idx;
                    int.TryParse(btn.AccessibleName, out idx);
                    if (lstMatches.Contains(idx))
                    {

                        currentButtonColor = btn.BackColor = Color.Violet;
                    }
                    // other stuf if you need to...
                    break;
                
            }
            foreach (Control child in control.Controls)
                LoopControls(child, lstMatches);
        }

    }
}
