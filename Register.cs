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

namespace WindowsFormsApp4
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();

            UserName.Text = "Введите имя";
            UserName.ForeColor = Color.Gray;
            UserSurname.Text = "Введите фамилию";
            UserSurname.ForeColor = Color.Gray;
            textBox8.Text = "Придумайте логин";
            textBox8.ForeColor = Color.Gray;
            textBox7.Text = "Придумайте пароль";
            textBox7.ForeColor = Color.Gray;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Close_MouseEnter(object sender, EventArgs e)
        {
            Close.ForeColor = Color.Red;
        }

        private void Close_MouseLeave(object sender, EventArgs e)
        {
            Close.ForeColor = Color.Black;
        }

        Point lastPoint;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void UserName_Enter(object sender, EventArgs e)
        {
            if (UserName.Text == "Введите имя")
            {
                UserName.Text = "";
                UserName.ForeColor = Color.Black;
            }
        }

        private void UserName_Leave(object sender, EventArgs e)
        {
            if (UserName.Text == "")
            {
                UserName.Text = "Введите имя";
                UserName.ForeColor = Color.Gray;
            }
        }

        private void UserSurname_Enter(object sender, EventArgs e)
        {
            if (UserSurname.Text == "Введите фамилию")
            {
                UserSurname.Text = "";
                UserSurname.ForeColor = Color.Black;
            }
        }

        private void UserSurname_Leave(object sender, EventArgs e)
        {
            if (UserSurname.Text == "")
            {
                UserSurname.Text = "Введите фамилию";
                UserSurname.ForeColor = Color.Gray;
            }

        }

        private void textBox8_Enter(object sender, EventArgs e)
        {
            if (textBox8.Text == "Придумайте логин")
            {
                textBox8.Text = "";
                textBox8.ForeColor = Color.Black;
            }
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            if (textBox8.Text == "")
            {
                textBox8.Text = "Придумайте логин";
                textBox8.ForeColor = Color.Gray;
            }
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            if (textBox7.Text == "Придумайте пароль")
            {
                textBox7.Text = "";
                textBox7.ForeColor = Color.Black;
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (textBox7.Text == "")
            {
                textBox7.Text = "Придумайте пароль";
                textBox7.ForeColor = Color.Gray;
            }
        }

        private void on_Click(object sender, EventArgs e)
        {

            if (UserName.Text == "Введите имя")
            {
                MessageBox.Show("Введите имя");
                return;
            }

            if (UserSurname.Text == "Введите фамилию")
            {
                MessageBox.Show("Введите фамилию");
                return;
            }

            if (textBox8.Text == "Придумайте логин")
            {
                MessageBox.Show("Придумайте логин");
                return;
            }

            if (textBox7.Text == "Придумайте пароль")
            {
                MessageBox.Show("Придумайте пароль");
                return;
            }

            if (isUserExists())
                return;

            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`Имя`, `Фамилия`, `login`, `Пароль`) VALUES (@Имя, @Фамилия, @login, @Пароль )", db.getConnection());

            command.Parameters.Add("@Имя", MySqlDbType.VarChar).Value = UserName.Text;
            command.Parameters.Add("@Фамилия", MySqlDbType.VarChar).Value = UserSurname.Text;
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = textBox8.Text;
            command.Parameters.Add("@Пароль", MySqlDbType.VarChar).Value = textBox7.Text;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Успешная регистрация аккаунта");
            else
                MessageBox.Show("Аккаунт не был создан");

            db.closeConnection();

        }
        public Boolean isUserExists()
        {
            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL", db.getConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = textBox8.Text;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой логин уже занят другим пользователем, придумайте новый логин");
                return true;
            }
            else
                return false;
        }

        private void RegisterLabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }
    }
}
