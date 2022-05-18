using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuthorisationFormApp
{
	public partial class LoginForm : Form
	{
		public LoginForm()
		{
			InitializeComponent();

			this.passField.AutoSize = false;
			this.passField.Size = new Size(this.passField.Size.Width, 50);
		}

		private void closeButton_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void closeButton_MouseEnter(object sender, EventArgs e)
		{
			closeButton.ForeColor = Color.Blue;
		}

		private void closeButton_MouseLeave(object sender, EventArgs e)
		{
			closeButton.ForeColor = Color.White;
		}

		Point lastPoint;
		private void mainPanel_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.Left += e.X - lastPoint.X;
				this.Top += e.Y - lastPoint.Y;
			}
		}

		private void mainPanel_MouseDown(object sender, MouseEventArgs e)
		{
			lastPoint = new Point(e.X, e.Y);
		}

		private void headPanel_MouseMove(object sender, MouseEventArgs e)
		{
			this.Left += e.X - lastPoint.X;
			this.Top += e.Y - lastPoint.Y;
		}

		private void headPanel_MouseDown(object sender, MouseEventArgs e)
		{
			lastPoint = new Point(e.X, e.Y);
		}

		private void buttonLogin_Click(object sender, EventArgs e)
		{
			String loginUser = loginField.Text;
			String passUser = passField.Text;

			DataBase db = new DataBase();

			DataTable table = new DataTable();

			MySqlDataAdapter adapter = new MySqlDataAdapter();

			MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL AND `pass` = @uP", db.getConnection());
			command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginUser;
			command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = passUser;

			adapter.SelectCommand = command;
			adapter.Fill(table);

			if(table.Rows.Count > 0)
			{
				this.Hide();
				MainForm mainForm = new MainForm();
				mainForm.Show();
			}
			else
			{
				MessageBox.Show("User is not found");
			}
		}

        private void registerLabel_Click(object sender, EventArgs e)
        {
			this.Hide();
			RegisterForm registerForm = new RegisterForm();
			registerForm.Show();
		}

        private void registerLabel_MouseEnter(object sender, EventArgs e)
        {
			registerLabel.ForeColor = Color.Blue;
		}

        private void registerLabel_MouseLeave(object sender, EventArgs e)
        {
			registerLabel.ForeColor = Color.Black;
		}
    }
}
