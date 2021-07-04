using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
	public partial class TickTackToe : Form
	{
		public TickTackToe()
		{
			InitializeComponent();
		}

		private string click = "O";

		private char[,] field = new char[3,3];

		private int countCkick = 0;

		private Color standart;

		private void CheckEndGame(char curr)
		{
			bool check = false;
			if (field[0,0] == field[0,1] && field[0,0] == field[0,2])
			{
				check = true;
			}
			if (field[1, 0] == field[1, 1] && field[1, 0] == field[1, 2])
			{
				check = true;
			}
			if (field[2, 0] == field[2, 1] && field[2, 0] == field[2, 2])
			{
				check = true;
			}

			if (field[0, 0] == field[1, 0] && field[0, 0] == field[2, 0])
			{
				check = true;
			}
			if (field[0, 1] == field[1, 1] && field[0, 1] == field[2, 1])
			{
				check = true;
			}
			if (field[0, 2] == field[1, 2] && field[0, 2] == field[2, 2])
			{
				check = true;
			}

			

		}

		private void Button_click(object sender, EventArgs e)
		{
			if (countCkick == 9)
			{
				
			}
			Button button = (Button)sender;
			if (button.Text == "")
			{
				
				if (click == "O")
				{
					button.Text = "X";
					field[Int32.Parse(button.Tag.ToString()) / 10, Int32.Parse(button.Tag.ToString()) % 10] = 'X';
				}
				else
				{
					button.Text = "O";
					field[(int)button.Tag / 10, (int)button.Tag % 10] = 'O';
				}
				countCkick++;
				click = button.Text;
			}
		}

		private void Button_MouseEnter(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			standart = button.BackColor;
			button.BackColor = Color.Aqua;
		}

		private void Button_MouseLeave(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			button.BackColor = standart;
		}
	}
}
