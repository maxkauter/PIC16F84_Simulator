using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*************************************************/
// DIESE KLASSE DIENT ZUM HOLEN DES OPCODES UND DES DEKODIEREN UND AUSFÜHREN DES PASSENDEN BEFEHLS
/*************************************************/
namespace WindowsFormsApp1
{
    // Von einer sealed Class kann nicht vererbt werden
    public sealed class CPU
    {
        public RegisterFile Regs { get; } = new RegisterFile();

        // Programmzähler, welcher immer auf die nächste Adresse zeigt, was ausgefürt werden soll und initial 0 ist
        public ushort PC { get; set; } = 0;
        // W-Register (Lesen und schreiben erlaubt durch get und set)
        public byte W { get; set; } = 0;

        // Hier wollen wir den Stack und den SP resetten ebenfalls
        // CPU zurücksetzen bzw. in definierten Zustand vor der Ausfürhung
        public void Reset()
        {
            Regs.ResetPowerOn(); // Darüber werden alle Register zurückgesetzt (Liegt in der RegiserFile Klasse)
            PC = 0;
            W = 0;
        }

        //ir enthält den aktuellen Befehl als Variable
        public void Step(ushort ir)
        {
            //Maskieren der unteren 14 Bit und Typumwandlung in ushort (0x3FFF == 0011 1111 1111 1111)
            ir = (ushort)(ir & 0x3FFF);

            // MOVLW
            // Zuerst maskieren der Bits "X" 00XX XXXX 0000 0000
            // Nach der Maskierung wird geschaut ob maske == 0x3000 also ob Bit 13 und 14 = 1 sind, denn ist es ein movlw befehl
            if((ir & 0x3F00) == 0x3000)
            {
                // MOVLW
                // Hier überpfüen wir um welche Instruktion es sich handelt, ein movlw Befehl hat das Muster 1100 xxxx xxxx
                // Das Literal also dass was in das W-Register muss sind lediglich die bits 0-7, weshalb wir k mit 0x00FF maskeiren
                byte k = (byte)(ir & 0x00FF);
                W = k;
                PC++;
                return;
            }
            // MOVWF
            // Maskieren der Bits "X" 00XX XXXX X000 0000
            // Dannach anschließend == 0000 0000 1000 0000, dann movwf Befehl da dort Bit 7 = 1
            if ((ir & 0x3F80) == 0x0080)
            {
                // Maskieren des Literals f, was sich aus den Bits 0-6 ergibt
                byte f = (byte)(ir & 0x007F);
                Regs.Write(f, W);
                PC++;
                return;
            }

            // Nach jedem Schritt wird der Programmcounter erhöht, wodurch der nächste Befehl ausgeführt wird
            PC++;
        }
    }
}
