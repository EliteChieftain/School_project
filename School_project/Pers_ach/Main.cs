using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace Pers_ach
{
    public partial class Main : Form
    {
        private string file_name;
        private string conn_str;
        private string cmd_str;

        private string new_cmd_str;


        private OpenFileDialog openFileDialog;

        private MySqlConnection sql_conn;
        private MySqlCommand sql_cmd;
        private DataSet sql_ds;
        private MySqlDataAdapter sql_da;


        public Main() //инициализация
        {
            InitializeComponent();

        }

        private void textBox3_TextChanged(object sender, EventArgs e) //чары для пароля
        {
            textBox3.PasswordChar = '*';
        }

        public void button1_Click(object sender, EventArgs e) //открытие проводника для выбора файла
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Comma Separated Value(*.csv) | *.csv"; //только csv


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                file_name = openFileDialog.FileName;
                MessageBox.Show(file_name);
            }
        }

        public void button2_Click(object sender, EventArgs e) //загрузка файла в БД
        {
           
        }

        private void button3_Click(object sender, EventArgs e) //подключение по нажатию кнопки "подключиться"
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) 
                || string.IsNullOrWhiteSpace(textBox3.Text) || comboBox1.SelectedIndex == -1 
                || comboBox2.SelectedIndex == -1) // проверка заполненности полей
            {
                MessageBox.Show("Проверьте, заполнены ли все поля!");
            }
            else
            {
                conn_str = "server=" + textBox1.Text + ";user=" + textBox2.Text + " ;password=" + textBox3.Text + ";database=" + comboBox1.SelectedItem + ";old guids = true;";
                cmd_str = "SELECT * FROM " + comboBox2.SelectedItem;

                sql_conn = new MySqlConnection(conn_str);
                sql_conn.Open();

                sql_cmd = new MySqlCommand(cmd_str, sql_conn);
                sql_da = new MySqlDataAdapter(cmd_str, sql_conn);
                sql_ds = new DataSet();
                sql_da.Fill(sql_ds);

                dataGridView1.DataSource = sql_ds.Tables[0].DefaultView;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void button4_Click(object sender, EventArgs e) //выполнение сортировки и поиска
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) 
                || string.IsNullOrWhiteSpace(textBox3.Text) || comboBox1.SelectedIndex == -1 
                || comboBox2.SelectedIndex == -1) // проверка заполненности полей
            {
                MessageBox.Show("Проверьте, заполнены ли все поля!");
            }
            else
            {
                if (radioButton1.Checked == true)
                {
                    new_cmd_str = "SELECT * FROM " + comboBox2.SelectedItem + " WHERE Класс = \"" + textBox5.Text + "\"";
                }

                MySqlCommand new_sql_cmd = new MySqlCommand(new_cmd_str, sql_conn);
                sql_da = new MySqlDataAdapter(new_cmd_str, sql_conn);
                sql_ds = new DataSet();
                sql_da.Fill(sql_ds);

                dataGridView1.DataSource = sql_ds.Tables[0].DefaultView;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
    }
}
