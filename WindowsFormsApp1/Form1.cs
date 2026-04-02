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
using System.Drawing.Text;
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        /**************************************************/

        //Objekte bzw. Labels für PortB
        private Label[] portBLabels = new Label[8];

        //Objekte bzw. Labels für PortB
        private Label[] portALabels = new Label[8];

        //Objekte bzw. Labels für STATUS_REGISTER
        private Label[] STATUSREGISTERLabels = new Label[8];

        //Objekte bzw. Labels für SPECIAL_REGISTER
        private Label[] SpecialRegisterLabels = new Label[9];

        private List<LstEintrag> programmBefehle = new List<LstEintrag>();
        private int aktuellerBefehlIndex = 0;
        private int programmZaehler = 0;

        /**************************************************/
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

                Match match = Regex.Match(zeile, @"^\s*([0-9A-Fa-f]{4})\s+([0-9A-Fa-f]{4})");

                if (match.Success)
                {
                    int adresse = Convert.ToInt32(match.Groups[1].Value, 16);
                    string opcode = match.Groups[2].Value.ToUpper();

                    liste.Add(new LstEintrag
                    {
                        Adresse = adresse,
                        Program = opcode
                    });
                }
            }

            return liste;
        }


        /**************************************************/
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

        /**************************************************************/
            //Hier werden die Ports zur Laufzeit aufgerufen und über die Funktionen definiert
            GUIFunctions.CreatePortAHorizontal(PORTA, portALabels);
            GUIFunctions.CreatePortBHorizontal(PORTB, portBLabels);
            //Hier wird das Status Register aufgerufen und definiert
            GUIFunctions.CreateStatusRegisterHorizontal(STATUS_REGISTER, STATUSREGISTERLabels);
            //Special Function Register
            GUIFunctions.CreateSpecialRegisterVertical(SPECIAL_REGISTER, SpecialRegisterLabels);

        }

        /**************************************************************/

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

    public class LstEintrag
    {
        public int Adresse { get; set; }
        public string Program { get; set; }

        public string Programcounter => Adresse.ToString("X4");
    }

}

    
