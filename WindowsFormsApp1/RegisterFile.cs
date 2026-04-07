using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{

    // Hier werden die Register implementiert, also Bank 0 (von 0x00 bis 0x7F) und Bank 1 (von 0x80 bis 0xFF)
    // Des weiteren wird das Special Function Register und das General Purpose Register implementiert
    // SFR ist byte 

    public sealed class RegisterFile
    {
        private readonly byte[] _sfr = new byte[256]; // von 00h-0bh sind die SFRs, von 0ch-7fh die GPRs, von 80h-ffh die Bank1 Register
                                                      //Entnommen aus dem Datenblatt aus dem 3.Semester
        private readonly byte[] _gpr = new byte[0x80 - 0x0C];

        // Initialisierung des Bank0 Registers
        public const byte INDF = 0x00;
        public const byte TMR0 = 0x01;
        public const byte PCL = 0x02;
        public const byte STATUS = 0x03;
        public const byte FSR = 0x04;
        public const byte PORTA = 0x05;
        public const byte PORTB = 0x06;
        public const byte PCLATH = 0x0A;
        public const byte INTCON = 0x0B;

        //Initialisierung des Bank1 Registers (Infos aus dem Register File Summary des PIC16F8X; 3.Semester)
        public const byte OPTION_REG = 0x81;
        public const byte TRISA = 0x85;
        public const byte TRISB = 0x86;
        // 0x87 ist unimplementiert
        public const byte EECON1 = 0x88;
        public const byte EECON2 = 0x89; //kein physisches Register, sondern nur ein Kontrollregister


        public const int STATUS_RP0_BIT = 5; // Bit, worüber man die Speicherbank ansteuern/wechseln kann

        public RegisterFile()
        {
            ResetPowerOn();
            
        }

        public void ResetPowerOn()
        {
            Array.Clear(_sfr, 0, _sfr.Length);
            Array.Clear(_gpr, 0, _gpr.Length);

            // Definierter/Initialer Zustand STATUS Register = TO = 1, PD = 1, RP0 = 0 (Bank 0), RP1 = 0 (Bank 0)
            //Aus Datenblatt PIC16F8X
            _sfr[STATUS] = 0b00110000; //Dadurch wird dieser erreicht
            _sfr[OPTION_REG] = 0b11111111;
            _sfr[TRISA] = 0b00011111;
            _sfr[TRISB] = 0b11111111;
        }

        public bool RP0
        {
            get
            {
                // STATUS Bit5 = RP0
                if ((_sfr[STATUS] & 0b0010_0000) != 0)
                    return true;
                else
                    return false;
            }
        }
        public byte Read(byte f)
        {
            // INDF special handling (indirect)
            if ((f & 0x7F) == INDF)
                return ReadIndirect();

            byte addr = ResolveDirectAddress(f);
            return ReadAbs(addr);
        }

        public void Write(byte f, byte value)
        {
            // INDF special handling (indirect)
            if ((f & 0x7F) == INDF)
            {
                WriteIndirect(value);
                return;
            }

            byte addr = ResolveDirectAddress(f);
            WriteAbs(addr, value);
        }

        // -------- Absolute read/write (already banked address 0x00..0xFF) --------
        public byte ReadAbs(byte addr)
        {
            addr = ResolveMirrors(addr);

            // Unimplemented locations: 0x07 / 0x87 read as 0
            if (addr == 0x07 || addr == 0x87)
                return 0x00;

            // GPR mapping:
            // - Bank0 GPR: 0x0C..0x7F
            // - Bank1 GPR: 0x8C..0xFF (maps to same GPR RAM)
            if (IsGpr(addr))
                return _gpr[GprIndex(addr)];

            // Otherwise SFR slot
            return _sfr[addr];
        }

        public void WriteAbs(byte addr, byte value)
        {
            addr = ResolveMirrors(addr);

            if (addr == 0x07 || addr == 0x87)
                return; // ignore writes

            if (IsGpr(addr))
            {
                _gpr[GprIndex(addr)] = value;
                return;
            }

            _sfr[addr] = value;
        }

        // -------- Indirect addressing via FSR/INDF --------
        public byte ReadIndirect()
        {
            byte fsr = _sfr[FSR];

            if (fsr == 0x00)
                return 0x00; // simplified typical behavior

            byte addr = ResolveMirrors(fsr);
            return ReadAbs(addr);
        }

        public void WriteIndirect(byte value)
        {
            byte fsr = _sfr[FSR];

            if (fsr == 0x00)
                return;

            byte addr = ResolveMirrors(fsr);
            WriteAbs(addr, value);
        }

        // -------- Helpers --------
        private byte ResolveDirectAddress(byte f)
        {
            // f is 7-bit in the instruction
            byte bank = (byte)(RP0 ? 0x80 : 0x00); // Wenn RP0 gesetzt ist (RP0==true) dann 0x80 bzw. Bank 1, sonst 0x00 bzw.Bank0
            byte addr = (byte)(bank | (f & 0x7F)); // Bit 0-6 werden maskiert (Weil Bank von 0-127 geht und nicht bis 256)
            return ResolveMirrors(addr);
        }

        private static byte ResolveMirrors(byte addr)
        {
            switch (addr)
            {
                case 0x80: return 0x00; // INDF
                case 0x82: return 0x02; // PCL
                case 0x83: return 0x03; // STATUS
                case 0x84: return 0x04; // FSR
                case 0x8A: return 0x0A; // PCLATH
                case 0x8B: return 0x0B; // INTCON
                default: return addr;
            }
        }

        private static bool IsGpr(byte addr)
        {
            // Bank0 GPR
            if (addr >= 0x0C && addr <= 0x7F) return true;
            // Bank1 GPR (shadowed)
            if (addr >= 0x8C && addr <= 0xFF) return true;
            return false;
        }

        private static int GprIndex(byte addr)
        {
            // Map bank1 GPR 0x8C..0xFF -> bank0 range 0x0C..0x7F
            if (addr >= 0x80) addr = (byte)(addr - 0x80);
            // now addr is 0x0C..0x7F
            return addr - 0x0C;
        }


    }
}
