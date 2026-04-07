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
    // Form1 erbt von der Klasse Form, die die grundlegende Funktionalität für ein WinForm bereitstellt.
    // Die Klasse Form1 enthält die Logik und die Benutzeroberfläche für die Anwendung.
    public partial class Form1 : Form
    {
        /**************************************************/

        //FÜR DIE WERTE DER PORTS, STATUSREGISTER UND SPECIAL FUNCTION REGISTER

        //Objekte bzw. Labels für PortB
        private Label[] portBLabels = new Label[8];

        //Objekte bzw. Labels für PortB
        private Label[] portALabels = new Label[8];

        //Objekte bzw. Labels für STATUS_REGISTER
        private Label[] STATUSREGISTERLabels = new Label[8];

        //Objekte bzw. Labels für SPECIAL_REGISTER
        private Label[] SpecialRegisterLabels = new Label[9];

        //Hier liegt das wirkliche Programm, also dass was aus der LST-Datei eingelesen wird und im Grid angezeigt wird
        private List<LstEintrag> programmBefehle = new List<LstEintrag>();
        private int aktuellerBefehlIndex = 0;
        private int programmZaehler = 0;

        // --- Run/Stop Steuerung ---
        private bool isRunning = false;
        private System.Windows.Forms.Timer runTimer;

        private byte portB = 0x00;
        private byte portA = 0x00;
        private byte status = 0x00;

        /**************************************************/
        public Form1()
        {
            InitializeComponent(); // erzeugt Controls wie die Buttons, Data Grid Views, Labels etc. auf
        }

        // Datei Button (sender = wer hat das event ausgelöst, e = was ist das event) 
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
                        programmBefehle = LstDateiEinlesen(dateipfad);
                        dataGridView1.DataSource = programmBefehle;

                        programmZaehler = 0;
                        portB = 0;
                        AktualisierePortBAnzeige();
                        MarkiereAktuelleZeile();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Fehler beim Einlesen der Datei:\n" + ex.Message);
                    }
                }
            }
        }

        //NOCH UNNÖTIG, ABER HIER WIRD PORTB ANGEZEIGt
        private void AktualisierePortBAnzeige()
        {
            for (int bit = 0; bit < 8; bit++)
            {
                int value = (portB >> bit) & 1;
                portBLabels[bit].Text = value.ToString(); 
            }
        }
        

        private void MarkiereAktuelleZeile()
        {
            if (programmBefehle == null || programmBefehle.Count == 0) return;

            int rowIndex = programmBefehle.FindIndex(e => e.Adresse == programmZaehler);
            if (rowIndex < 0) return;

            dataGridView1.ClearSelection();
            dataGridView1.Rows[rowIndex].Selected = true;
            dataGridView1.CurrentCell = dataGridView1.Rows[rowIndex].Cells[0];

            if (rowIndex >= 0)
                dataGridView1.FirstDisplayedScrollingRowIndex = rowIndex;
        }

        private void StepOnce()
        {
            if (programmBefehle == null || programmBefehle.Count == 0) return;

            int idx = programmBefehle.FindIndex(e => e.Adresse == programmZaehler);
            if (idx < 0)
            {
                // PC zeigt auf keine Zeile -> anhalten
                StopRun();
                return;
            }

            var instr = programmBefehle[idx];

            // --- TODO: echte PIC16F84 Instruction Execution anhand instr.Opcode ---
            // DEMO: PortB hochzählen, damit du die Bits "leben" siehst
            portB++;

            // nächste Adresse (wenn LST-Wortadressen fortlaufend sind)
            programmZaehler = instr.Adresse + 1;

            AktualisierePortBAnzeige();
            MarkiereAktuelleZeile();
        }

        private void StartRun()
        {
            if (programmBefehle == null || programmBefehle.Count == 0)
            {
                MessageBox.Show("Bitte zuerst eine .lst-Datei laden.");
                return;
            }

            isRunning = true;
            runTimer.Start();
        }

        private void StopRun()
        {
            isRunning = false;
            runTimer.Stop();
        }

        //Wenn User klickt, wird die jeweilige Funktion aufgerufen, z.B. Starten, Stoppen oder Step-In
        private void btnGo_Click(object sender, EventArgs e)
        {
            StartRun();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopRun();
        }

        private void btnStepIn_Click(object sender, EventArgs e)
        {
            StopRun();   // Step nur im "Stop"-Modus
            StepOnce();
        }


        private List<LstEintrag> LstDateiEinlesen(string dateipfad)
        {
            //Leere Liste wird erstellt, um die Einträge der LST-Datei zu speichern
            var liste = new List<LstEintrag>();

            // Zeile für Zeile der LST-Datei einlesen und mit Regex die Adresse, den Opcode und den Assembler-Text extrahieren
            foreach (string zeile in File.ReadAllLines(dateipfad))
            {
                if (string.IsNullOrWhiteSpace(zeile))
                    continue;

                Match match = Regex.Match(
                    zeile,
                    @"^\s*([0-9A-Fa-f]{4})\s+([0-9A-Fa-f]{4})\s+\d+\s+(.*)$");

                if (!match.Success)
                {
                    match = Regex.Match(
                        zeile,
                        @"^\s*([0-9A-Fa-f]{4})\s+([0-9A-Fa-f]{4})\s+(.*)$");
                }

                if (match.Success)
                {
                    int adresse = Convert.ToInt32(match.Groups[1].Value, 16); //Adresse wird von Hex in int konvertiert
                    string opcode = match.Groups[2].Value.ToUpper();
                    string assemblerText = match.Groups[3].Value.Trim();

                    liste.Add(new LstEintrag
                    {
                        Adresse = adresse,
                        Opcode = opcode,
                        AssemblerText = assemblerText
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

            // Das automatische erzeugen von Spalten wird deaktiviert, damit die Spalten manuell definiert werden können. 
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
            //Hier werden die Controls zur Laufzeit aufgerufen und über die Funktionen definiert

            GUIFunctions.CreatePortAHorizontal(PORTA, portALabels);
            GUIFunctions.CreatePortBHorizontal(PORTB, portBLabels);
            //Hier wird das Status Register aufgerufen und definiert
            GUIFunctions.CreateStatusRegisterHorizontal(STATUS_REGISTER, STATUSREGISTERLabels);
            //Special Function Register
            GUIFunctions.CreateSpecialRegisterVertical(SPECIAL_REGISTER, SpecialRegisterLabels);

            /*************************************************************/
            // Run-Timer (läuft im UI-Thread -> UI Updates sind sicher)
            runTimer = new System.Windows.Forms.Timer();
            runTimer.Interval = 200; // ms (Geschwindigkeit)
            runTimer.Tick += (s, args) =>
            {
                if (!isRunning) return;
                StepOnce();
            };

        }

        /**************************************************************/

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

    // Datenklasse
    public class LstEintrag
    {
        public int Adresse { get; set; }
        public string Opcode { get; set; }
        public string AssemblerText { get; set; }

        public string Programcounter => Adresse.ToString("X4");

        public string Program => $"{Opcode}   {AssemblerText}";
    }

    // KLEINES BEISPIEL:
    /*  var e = new LstEintrag();
        e.Adresse = 10;                // set wird ausgeführt
        e.Opcode = "3001";             // set wird ausgeführt
        e.AssemblerText = "MOVLW 0x01"; // set wird ausgeführt

        Console.WriteLine(e.Programcounter); // get berechnet: "000A"
        Console.WriteLine(e.Program);        // get berechnet: "3001   MOVLW 0x01"
    */
}

    
