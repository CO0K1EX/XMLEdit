using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace XMLEdit
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Заполните все поля!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            } else
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = textBox1.Text;
                dataGridView1.Rows[n].Cells[1].Value = numericUpDown1.Value;
                dataGridView1.Rows[n].Cells[2].Value = comboBox1.Text;
            }
        }


        private void button6_Click(object sender, EventArgs e)
        {
            Stream Mystream;
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog1.Filter = "All files (*.*)|*.*|xml files (*.xml)|*.xml";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((Mystream = saveFileDialog1.OpenFile()) != null)
                {
                 try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt.TableName = "Employee";
                dt.Columns.Add("Name");
                dt.Columns.Add("Age");
                dt.Columns.Add("Programmer");
                ds.Tables.Add(dt);

                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    DataRow row = ds.Tables["Employee"].NewRow();
                    row["Name"] = r.Cells[0].Value;
                    row["Age"] = r.Cells[1].Value;
                    row["Programmer"] = r.Cells[2].Value;
                    ds.Tables["Employee"].Rows.Add(row);
                }
                ds.WriteXml(Mystream);
                MessageBox.Show("XML файл успешно сохранен!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Невозможно сохранить XML файл.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                }
            }

            
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Filter = "All files (*.*)|*.*|xml files (*.xml)|*.xml";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var fileStream = openFileDialog1.FileName;

                using (StreamReader reader = new StreamReader(fileStream))
                {
                     if (dataGridView1.Rows.Count > 0)
            {
                MessageBox.Show("Очистите поле перед загрузкой нового файла.", "Сообщение",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            } else
            {
                if (File.Exists(fileStream))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(fileStream); 

                    foreach (DataRow item in ds.Tables["Employee"].Rows)
                    {
                        int n = dataGridView1.Rows.Add();
                        dataGridView1.Rows[n].Cells[0].Value = item["Name"];
                        dataGridView1.Rows[n].Cells[1].Value = item["Age"];
                        dataGridView1.Rows[n].Cells[2].Value = item["Programmer"];
                    }
                }
                else
                {
                    MessageBox.Show("XML файл не найден.", "Сообщение",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }
            }
                }

            }

           
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            int n = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value);
            numericUpDown1.Value = n;
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int n = dataGridView1.SelectedRows[0].Index;
                dataGridView1.Rows[n].Cells[0].Value = textBox1.Text;
                dataGridView1.Rows[n].Cells[1].Value = numericUpDown1.Value;
                dataGridView1.Rows[n].Cells[2].Value = comboBox1.Text;
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования.", "Сообщение",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index); //удаление
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления.", "Сообщение",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
            }
            else
            {
                MessageBox.Show("Таблица пустая.", "Сообщение",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.ShowDialog();
        }
    }
}
