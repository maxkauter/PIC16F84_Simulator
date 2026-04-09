using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*************************************************/
// BAUT DIE ANZEIGE WAS ANGEZEIGT WIRD
/*************************************************/

namespace WindowsFormsApp1
{
    public static class GUIFunctions
    {
        // PORTA Initialisierung und Beschriftung
        // Von statischen Klassen kann kein Objekt erzeugt werden
        public static void CreatePortAHorizontal(GroupBox PORTA, Label[] portALabels)
        {
            int startX = 10;
            int startY = 20;
            int spacing = 40;

            for (int i = 7; i >= 0; i--)
            {
                int index = 7 - i;

                Label lblName = new Label();
                Label lblValue = new Label();

                // PORTA hat nur RA0–RA4 → Rest leer
                if (i <= 4)
                {
                    lblName.Text = $"RA{i}";
                }
                else
                {
                    lblName.Text = "LEER";
                }

                lblName.Location = new Point(startX + index * spacing, startY);
                lblName.AutoSize = true;

                // untere Zeile immer X
                lblValue.Text = "X";
                lblValue.Location = new Point(startX + index * spacing, startY + 20);
                lblValue.AutoSize = true;

                portALabels[i] = lblValue;

                PORTA.Controls.Add(lblName);
                PORTA.Controls.Add(lblValue);
            }
        }

        // PORTB Initialisierung und Beschriftung
        public static void CreatePortBHorizontal(GroupBox PORTB, Label[] portBLabels)
        {
            int startX = 10;
            int startY = 20;
            int spacing = 40;

            for (int i = 7; i >= 0; i--)
            {
                int index = 7 - i;

                // Das ist für die obere Zeile der Port B Pins
                Label lblName = new Label();
                lblName.Text = $"RB{i}";
                lblName.Location = new Point(startX + index * spacing, startY);
                lblName.AutoSize = true;

                // Das ist für die untere Zeile der Port B Pins
                Label lblValue = new Label();
                lblValue.Text = "X";
                lblValue.Location = new Point(startX + index * spacing, startY + 20);
                lblValue.AutoSize = true;

                portBLabels[i] = lblValue;

                PORTB.Controls.Add(lblName);
                PORTB.Controls.Add(lblValue);

            }
        }

        // STATUS_REGISTER Funktion Initialisieren und Beschriftung
        public static void CreateStatusRegisterHorizontal(GroupBox STATUS_REGISTER, Label[] STATUSREGISTERLabels)
        {
            int startX = 10;
            int startY = 20;
            int spacing = 40;

            string[] names = { "IRP", "RP1", "RP0", "TO", "PD", "Z", "DC", "C" };

            for (int i = 0; i < names.Length; i++)
            {
                Label lblName = new Label();
                lblName.Text = names[i];
                lblName.Location = new Point(startX + i * spacing, startY);
                lblName.AutoSize = true;

                Label lblValue = new Label();
                lblValue.Text = "X";
                lblValue.Location = new Point(startX + i * spacing, startY + 20);
                lblValue.AutoSize = true;

                STATUSREGISTERLabels[i] = lblValue;

                STATUS_REGISTER.Controls.Add(lblName);
                STATUS_REGISTER.Controls.Add(lblValue);
            }
        }

        public static void CreateSpecialRegisterVertical(GroupBox SPECIAL_REGISTER, Label[] SPECIALREGISTERLabels)
        {
            int startX = 10;
            int startY = 25;
            int spacingY = 25;
            string[] names = { "W", "FSR", "PCLATH", "PCL", "STATUS", "TIMER 0", "Option", "FSR", "Prescaler" };
            Label[] valueLabels = new Label[names.Length];

            for (int i = 0; i < names.Length; i++)
            {
                Label lblName = new Label();
                lblName.Text = names[i];
                lblName.Location = new Point(startX, startY + i * spacingY);
                lblName.AutoSize = true;

                Label lblValue = new Label();
                lblValue.Text = "X";
                lblValue.Location = new Point(startX + 120, startY + i * spacingY);
                lblValue.AutoSize = true;

                SPECIALREGISTERLabels[i] = lblValue;

                SPECIAL_REGISTER.Controls.Add(lblName);
                SPECIAL_REGISTER.Controls.Add(lblValue);
            }
        }


        /*************************************************/
    }
}
