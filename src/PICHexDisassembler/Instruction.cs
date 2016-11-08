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
            { 0x2800, 0xF800, typeof(Goto) },   // mask: 0010100000000000 opcodeMask: 1111100000000000
            { 0x2000, 0xF800, typeof(Call) },   // mask: 0010000000000000 opcodeMask: 1111100000000000
            { 0x0009, 0xFFFF, typeof(Retfie) }, // mask: 0000000000001001 opcodeMask: 1111111111111111
            { 0x1400, 0xFC00, typeof(Bsf) },    // mask: 0001010000000000 opcodeMask: 1111110000000000
            { 0x1000, 0xFC00, typeof(Bcf) },    // mask: 0001000000000000 opcodeMask: 1111110000000000
            { 0x0080, 0xFF80, typeof(Movwf) },  // mask: 0000000010000000 opcodeMask: 1111111110000000
            { 0x0E00, 0xFF00, typeof(Swapf) },  // mask: 0000111000000000 opcodeMask: 1111111100000000
            { 0x1C00, 0xFC00, typeof(Btfss) },  // mask: 0001110000000000 opcodeMask: 1111110000000000
            { 0x3000, 0xFC00, typeof(Movlw) },  // mask: 0011000000000000 opcodeMask: 1111110000000000
            { 0x0064, 0xFFFF, typeof(Clrwdt) }, // mask: 0000000001100100 opcodeMask: 1111111111111111
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
