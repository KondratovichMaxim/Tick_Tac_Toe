using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tick_Tac_Toe
{
    public partial class MainForm : Form
    {
        Button[,] field = new Button[3, 3];
        String player;
        String mode;
        int steps;
        bool history;
        public MainForm()
        {
            InitializeComponent();

            mode = "single";

            List<Button> list = new List<Button>();
            list.AddRange(this.Controls.OfType<Button>().ToList<Button>());
            list.RemoveAt(0);
            list.Reverse();

            for (int i = 0, z = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    field[i, j] = list[z++];
                    
                }
            }

            history = false;

            Restart();
        }

        void Restart()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    field[i, j].Text = "";
                    field[i,j].Enabled = true;
                }
            }

            player = "X";
            steps = 0;
            lblSteps.Text = "Steps: " + steps;

            ReloadListBox();

        }
        void ReloadListBox()
        {
            listBox1.Items.Clear();
            listBox1.Items.AddRange(File.ReadAllLines("History.txt"));
        }

        private void Button_Clicked(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            b.Text = player;
            b.Enabled = false;

            lblSteps.Text = "Steps: " + ++steps;


            if (WinCheck())
            {
                return;
            }
            else
                if (steps == 9)
                {
                    MessageBox.Show("Drow");
                    Restart();
                    return;
                }

            if (mode == "single")
            {
                player = "O";
                BotStep();

                lblSteps.Text = "Steps: " + ++steps;

                if (WinCheck())
                {
                    return;
                }
                player = "X";
            }
            else
            {
                if (player == "X")
                {
                    player = "O";
                }
                else
                {
                    player = "X";
                }
            }
        }

        void BotStep()
        {
            //From left to right   OR   form top to bottom

            #region Critical Attack

            for (int i = 0; i < 3; i++)
            {
                //Top Row                
                if (field[1, i].Text == field[2, i].Text && field[1, i].Text == player && field[0, i].Text == "")
                {
                    field[0, i].Text = player;
                    return;
                }
                //Middle Row
                if (field[0, i].Text == field[2, i].Text && field[0, i].Text == player && field[1, i].Text == "")
                {
                    field[1, i].Text = player;
                    return;
                }
                //Bottom Row
                if (field[0, i].Text == field[1, i].Text && field[0, i].Text == player && field[2, i].Text == "")
                {
                    field[2, i].Text = player;
                    return;
                }

                //Left
                if (field[i, 1].Text == field[i, 2].Text && field[i, 1].Text == player && field[i, 0].Text == "")
                {     //строка последних 2 эл.
                    field[i, 0].Text = player;
                    return;
                }
                //Middle
                if (field[i, 0].Text == field[i, 2].Text && field[i, 0].Text == player && field[i, 1].Text == "")
                {     //строка через 1
                    field[i, 1].Text = player;
                    return;
                }
                //Right
                if (field[i, 0].Text == field[i, 1].Text && field[i, 0].Text == player && field[i, 2].Text == "")
                {     //строка первых 2 эл.
                    field[i, 2].Text = player;
                    return;
                }

            }

            #region Top Left to Bottom Right Diagonal
            //Top Left
            if (field[1, 1].Text == field[2, 2].Text && field[1, 1].Text == player && field[0, 0].Text == "")
            {
                field[0, 0].Text = player;
                return;
            }
            //Center
            if (field[0, 0].Text == field[2, 2].Text && field[0, 0].Text == player && field[1, 1].Text == "")
            {
                field[1, 1].Text = player;
                return;
            }
            //Bottom Right
            if (field[0, 0].Text == field[1, 1].Text && field[0, 0].Text == player && field[2, 2].Text == "")
            {
                field[2, 2].Text = player;
                return;
            }
            #endregion Top Left to Bottom Right Diagonal

            #region Bottom Left to Top Right
            //Bottom Left
            if (field[0, 2].Text == field[1, 1].Text && field[1, 1].Text == player && field[2, 0].Text == "")
            {
                field[2, 0].Text = player;
                return;
            }
            //Center
            if (field[2, 0].Text == field[0, 2].Text && field[2, 0].Text == player && field[1, 1].Text == "")
            {
                field[1, 1].Text = player;
                return;
            }
            //Top Right
            if (field[2, 0].Text == field[1, 1].Text && field[2, 0].Text == player && field[0, 2].Text == "")
            {
                field[0, 2].Text = player;
                return;
            }
            #endregion Bottom Left to Top Right

            #endregion Critical Attack

            #region Defence

            for (int i = 0; i < 3; i++)
            {
                //Top Row                
                if (field[1, i].Text == field[2, i].Text && field[1, i].Text != "" && field[0, i].Text == "")
                {
                    field[0, i].Text = player;
                    return;
                }
                //Middle Row
                if (field[0, i].Text == field[2, i].Text && field[0, i].Text != "" && field[1, i].Text == "")
                {
                    field[1, i].Text = player;
                    return;
                }
                //Bottom Row
                if (field[0, i].Text == field[1, i].Text && field[0, i].Text != "" && field[2, i].Text == "")
                {
                    field[2, i].Text = player;
                    return;
                }

                //Left
                if (field[i, 1].Text == field[i, 2].Text && field[i, 1].Text != "" && field[i, 0].Text == "")
                {     //строка последних 2 эл.
                    field[i, 0].Text = player;
                    return;
                }
                //Middle
                if (field[i, 0].Text == field[i, 2].Text && field[i, 0].Text != "" && field[i, 1].Text == "")
                {     //строка через 1
                    field[i, 1].Text = player;
                    return;
                }
                //Right
                if (field[i, 0].Text == field[i, 1].Text && field[i, 0].Text != "" && field[i, 2].Text == "")
                {     //строка первых 2 эл.
                    field[i, 2].Text = player;
                    return;
                }

            }
            //Top Left to Bottom Right Diagonal
            if (field[0, 0].Text == field[1, 1].Text && field[0, 0].Text != "" && field[2, 2].Text == "")
            {
                field[2, 2].Text = player;
                return;
            }
            if (field[1, 1].Text == field[2, 2].Text && field[1, 1].Text != "" && field[0, 0].Text == "")
            {
                field[0, 0].Text = player;
                return;
            }
            if (field[0, 0].Text == field[2, 2].Text && field[0, 0].Text != "" && field[1, 1].Text == "")
            {
                field[1, 1].Text = player;
                return;
            }
            //Bottom Left to Top Right
            if (field[2, 0].Text == field[1, 1].Text && field[2, 0].Text != "" && field[0, 2].Text == "")
            {
                field[0, 2].Text = player;
                return;
            }
            if (field[2, 0].Text == field[0, 2].Text && field[2, 0].Text != "" && field[1, 1].Text == "")
            {
                field[1, 1].Text = player;
                return;
            }
            if (field[0, 2].Text == field[1, 1].Text && field[1, 1].Text != "" && field[2, 0].Text == "")
            {
                field[2, 0].Text = player;
                return;
            }


            #endregion Defence

            #region Random Attack

            Random r = new Random();
            int x, y;

            do
            {
                x = r.Next(3);
                y = r.Next(3);

            } while (field[x, y].Text != "");

            field[x, y].Text = player;
            field[x, y].Enabled = false;

            #endregion Random Attack

        }

        bool WinCheck()
        {
            for (int i = 0; i < 3; i++)
            {
                if (field[i, 0].Text == field[i, 1].Text && field[i, 1].Text == field[i, 2].Text && field[i, 0].Text == player)
                {
                    WinEvent();
                    return true;
                }
            }

            for (int j = 0; j < 3; j++)
            {
                if (field[0, j].Text == field[1, j].Text && field[1, j].Text == field[2, j].Text && field[0, j].Text == player)
                {
                    WinEvent();
                    return true;
                }
            }

            if (field[0, 0].Text == field[1, 1].Text && field[1, 1].Text == field[2, 2].Text && field[0, 0].Text == player)
            {
                WinEvent();
                return true;
            }

            if (field[0, 2].Text == field[1, 1].Text && field[1, 1].Text == field[2, 0].Text && field[1, 1].Text == player)
            {
                WinEvent();
                return true;
            }

            return false;

        }

        void WinEvent()
        {
            MessageBox.Show(player + " wins");

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter("History.txt", true))
            {
                file.WriteLine(DateTime.Now.ToString() + " " + player + " wins in " + steps + " steps");
            }

            Restart();
        }

        private void Mode_Changed(object sender, EventArgs e)
        {
            if ((ToolStripMenuItem)sender == singlevsBotToolStripMenuItem)
            {
                singlevsBotToolStripMenuItem.BackColor = SystemColors.ControlDark;
                multiplayerToolStripMenuItem.BackColor = SystemColors.Control;
                mode = "single";
            }
            else
            {
                multiplayerToolStripMenuItem.BackColor = SystemColors.ControlDark;
                singlevsBotToolStripMenuItem.BackColor = SystemColors.Control;
                mode = "multi";
            } 
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Restart();
        }

        private void bttnOpenHistory_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            
            if (!history)
            {
                this.Size = new Size(520, 307);

                listBox1.Visible = true;

                b.Location = new Point(467, 144);
                b.Text = "<";
                history = true;
            }
            else
            {
                this.Size = new Size(293,307);

                listBox1.Visible = false;

                b.Location = new Point(240,144);
                b.Text = ">";
                history = false;
            }
        }
    }
}
