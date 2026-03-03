using PICHexDisassembler.Instructions;
using System;
using System.Collections.Generic;

namespace PICHexDisassembler
{
    public abstract class Instruction
    {
        protected readonly ushort data;

        public Instruction(ushort data)
        {
            this.data = data;
        }

        public override string ToString()
        {
            return GetType().Name.ToUpper();
        }

        public byte FirstByte => (byte)(data >> 8);
        public byte SecondByte => (byte)data;

        private static InstructionMapping instructionMapping = new InstructionMapping
        {
            // Control / literal and miscellaneous
            { 0x0000, 0xFFFF, typeof(Nop) },
            { 0x0008, 0xFFFF, typeof(Return) },
            { 0x0009, 0xFFFF, typeof(Retfie) },
            { 0x0063, 0xFFFF, typeof(Sleep) },
            { 0x0064, 0xFFFF, typeof(Clrwdt) },
            { 0x2000, 0xF800, typeof(Call) },
            { 0x2800, 0xF800, typeof(Goto) },
            { 0x3000, 0x3F00, typeof(Movlw) },
            { 0x3400, 0x3F00, typeof(Retlw) },
            { 0x3800, 0x3F00, typeof(Iorlw) },
            { 0x3900, 0x3F00, typeof(Andlw) },
            { 0x3A00, 0x3F00, typeof(Xorlw) },
            { 0x3C00, 0x3F00, typeof(Sublw) },
            { 0x3E00, 0x3F00, typeof(Addlw) },

            // Bit-oriented
            { 0x1000, 0x3C00, typeof(Bcf) },
            { 0x1400, 0xFC00, typeof(Bsf) },
            { 0x1800, 0x3C00, typeof(Btfsc) },
            { 0x1C00, 0xFC00, typeof(Btfss) },

            // Byte-oriented
            { 0x0080, 0xFF80, typeof(Movwf) },
            { 0x0180, 0xFF80, typeof(Clrf) },
            { 0x0100, 0xFFFF, typeof(Clrw) },
            { 0x0200, 0x3F00, typeof(Subwf) },
            { 0x0300, 0x3F00, typeof(Decf) },
            { 0x0400, 0x3F00, typeof(Iorwf) },
            { 0x0500, 0x3F00, typeof(Andwf) },
            { 0x0600, 0x3F00, typeof(Xorwf) },
            { 0x0700, 0x3F00, typeof(Addwf) },
            { 0x0800, 0x3F00, typeof(Movf) },
            { 0x0900, 0x3F00, typeof(Comf) },
            { 0x0A00, 0x3F00, typeof(Incf) },
            { 0x0B00, 0x3F00, typeof(Decfsz) },
            { 0x0C00, 0x3F00, typeof(Rrf) },
            { 0x0D00, 0x3F00, typeof(Rlf) },
            { 0x0E00, 0xFF00, typeof(Swapf) },
            { 0x0F00, 0x3F00, typeof(Incfsz) },
        };

        internal static Instruction Parse(ushort dataBytes)
        {
            foreach (var item in instructionMapping)
            {
                if ((dataBytes & item.Item2) == item.Item1)
                {
                    return (Instruction)Activator.CreateInstance(item.Item3, dataBytes);
                }
            }

            return Unknown.Instance;
        }

        public static Dictionary<int, string> Registers = new Dictionary<int, string>
        {
            { 0, "W" },
            { 1, "F" }
        };

        public static Dictionary<int, string> BankRegisterFiles = new Dictionary<int, string>
        {
            { 0x00, "INDF" },
            { 0x01, "TMR0" },
            { 0x02, "PCL" },
            { 0x03, "STATUS" },
            { 0x04, "FSR" },
            { 0x05, "PORTA" },
            { 0x06, "PORTB" },
            { 0x0A, "PCLATH" },
            { 0x0B, "INTCON" },
            { 0x0C, "PIR1" },
            { 0x0E, "TMR1" },
            { 0x0F, "TMR1H" },
            { 0x10, "T1CON" },
            { 0x11, "TMR2" },
            { 0x12, "T2CON" },
            { 0x15, "CCPR1" },
            { 0x16, "CCPR1H" },
            { 0x17, "CCP1CON" },
            { 0x18, "RCSTA" },
            { 0x19, "TXREG" },
            { 0x1A, "RCREG" },
            { 0x1F, "CMCON" },
            { 0x81, "OPTION_REG" },
            { 0x85, "TRISA" },
            { 0x86, "TRISB" },
            { 0x8C, "PIE1" },
            { 0x8E, "PCON" },
            { 0x92, "PR2" },
            { 0x98, "TXSTA" },
            { 0x99, "SPBRG" },
            { 0x9A, "EEDATA" },
            { 0x9B, "EEADR" },
            { 0x9C, "EECON1" },
            { 0x9D, "EECON2" },
            { 0x9F, "VRCON" },
        };

        public static Dictionary<string, string[]> Bits = new Dictionary<string, string[]>
        {
            { "STATUS", new[] { "C", "DC", "Z", "NOT_PD", "NOT_TO", "RP0", "RP1", "IRP" } },
            { "PORTA", new[] { "RA0", "RA1", "RA2", "RA3", "RA4", "RA5", "RA6", "RA7" } },
            { "PORTB", new[] { "RB0", "RB1", "RB2", "RB3", "RB4", "RB5", "RB6", "RB7"} },
            { "INTCON", new[] { "RBIF", "INTF", "T0IF", "RBIE", "INTE", "T0IE", "PEIE", "GIE" } },
        };
    }
}
