using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Datei auswählen";
                openFileDialog.Filter = "LST-Dateien (*.lst)|*.lst|Alle Dateien (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string dateipfad = openFileDialog.FileName;

                    try
                    {
                        var daten = LstDateiEinlesen(dateipfad);
                        dataGridView1.DataSource = daten;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Fehler beim Einlesen der Datei:\n" + ex.Message);
                    }
                }
            }
        }

        private List<LstEintrag> LstDateiEinlesen(string dateipfad)
        {
            var liste = new List<LstEintrag>();

            foreach (string zeile in File.ReadAllLines(dateipfad))
            {
                if (string.IsNullOrWhiteSpace(zeile))
                    continue;

                Match match = Regex.Match(zeile, @"^\s*([0-9A-Fa-f]+)\s+(.*)$");

                if (match.Success)
                {
                    liste.Add(new LstEintrag
                    {
                        Programcounter = match.Groups[1].Value,
                        Program = match.Groups[2].Value.Trim()
                    });
                }
            }

            return liste;
        }
     


        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Programcounter",
                HeaderText = "Programcounter",
                DataPropertyName = "Programcounter",
                Width = 120
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Program",
                HeaderText = "Program",
                DataPropertyName = "Program",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    public class LstEintrag
    {
        public string Programcounter { get; set; }
        public string Program { get; set; }
    }

}

    
