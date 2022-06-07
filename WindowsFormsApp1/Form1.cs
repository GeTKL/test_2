using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Timer timer = new Timer();
        dataBase dataBase = new dataBase();
        List<string[]> data = new List<string[]>();
        string i = Console.ReadLine();
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        public Form1()
        {
            InitializeComponent();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer1_Tick);
            timer.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int h = DateTime.Now.Hour;
            int m = DateTime.Now.Minute;
            int s = DateTime.Now.Second;
            int l = DateTime.Now.Hour;
            string time = "";
            if (h<10)
            {
                time += "0" + h;
            }
            else
            {
                time += h;
            }

            time += ":";

            if (m < 10)
            {
                time += "0" + m;
            }
            else
            {
                time += m;
            }

            time += ":";

            if (s < 10)
            {
                time += "0" + s;
            }
            else
            {
                time += s;
            }
            label1.Text = time;

            if(l < 5 || l >= 22)
            {
                label2.Text = "Доброй ночи!";
                
            }
            else
            {
                if(l < 12)
                {
                    label2.Text = "Доброе утро!";
                }
                else
                {
                    if(l < 18)
                    {
                        label2.Text = "Добрый день!";
                    }
                    else
                    {
                        label2.Text = "Добрый вечер";
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            load();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int s = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString());
            textBox5.Text = s.ToString();
            string qs = $"select login_user, password_user, fsp, ID_Role from Beguns2 where id_begun = '{s}'";
            SqlCommand comm = new SqlCommand(qs, dataBase.getConnection());
            dataBase.openConnection();
            SqlDataReader read = comm.ExecuteReader();

            while (read.Read())
            {
                textBox1.Text = read["login_user"].ToString();
                textBox2.Text = read["password_user"].ToString();
                textBox3.Text = read["fsp"].ToString();
                textBox4.Text = read["ID_Role"].ToString();
            }
            read.Close();
            dataBase.closedConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string t = textBox1.Text;
            string v = textBox2.Text;
            string z = textBox3.Text;
            string g = textBox4.Text;
            string INS = $"INSERT Beguns2(login_user, password_user, fsp, id_role) values ('{t}', '{v}', '{z}', '{g}')";
            SqlCommand comm = new SqlCommand(INS, dataBase.getConnection());
            adapter.SelectCommand = comm;
            adapter.Fill(table);
            MessageBox.Show("Данные добавлены");



        }

        private void clear()
        {
            dataGridView1.Rows.Clear();
        }

        private void load()
        {
            string qs = $"select * from Beguns2";
            SqlCommand comm = new SqlCommand(qs, dataBase.getConnection());
            dataBase.openConnection();
            SqlDataReader read = comm.ExecuteReader();


            while (read.Read())
            {
                data.Add(new string[5]);

                data[data.Count - 1][0] = read[0].ToString();
                data[data.Count - 1][1] = read[1].ToString();
                data[data.Count - 1][2] = read[2].ToString();
                data[data.Count - 1][3] = read[3].ToString();
                data[data.Count - 1][4] = read[4].ToString();
            }
            read.Close();
            dataBase.closedConnection();

            foreach (string[] s in data)
            {
                dataGridView1.Rows.Add(s);
            }

            data.Clear();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            clear();
            load();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int s = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString());
            string QS = $"DELETE From Beguns2 where id_Begun = '{s}'";
            SqlCommand comm = new SqlCommand(QS, dataBase.getConnection());
            adapter.SelectCommand = comm;
            adapter.Fill(table);
            MessageBox.Show("Данные удалены");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int s = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString());
            string t = textBox1.Text;
            string v = textBox2.Text;
            string z = textBox3.Text;
            string g = textBox4.Text;
            string QS = $"UPDATE Beguns2 SET login_user = '{t}', password_user = '{v}', fsp = '{z}', ID_Role = '{g}' where id_begun = '{s}'";

            SqlCommand comm = new SqlCommand(QS, dataBase.getConnection());
            adapter.SelectCommand = comm;
            adapter.Fill(table);
            MessageBox.Show("Данные изменены, я спать!");
        }

        private void button3_Click(object sender, EventArgs e) // для диаграммы 
        {
            chart1.Series[0].Points.AddXY(Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox7.Text));
        }
        List<string[]> data2 = new List<string[]>();
    }
}
