## PIC16F Mid‑Range (14‑bit) Instruction Set – Summary

This project targets the standard **mid‑range 14‑bit PIC16F core** (e.g. PIC16F84A/16F877A‑class devices). Instructions are 14‑bit words, grouped as:

- **Byte‑oriented file register operations**: operate on a file register `f` and accumulator `W`, with destination bit `d` selecting `W` or `f`.
- **Bit‑oriented file register operations**: operate on a single bit `b` of a file register `f`.
- **Literal and control operations**: use an 8‑bit literal `k` or control program flow.

The table below summarizes the standard non‑enhanced mid‑range PIC16F instruction set that this disassembler aims to support. For each mnemonic we show the **general encoding pattern** (in binary), and how we derive a **hex base opcode** and **mask** for decoding.

### Byte‑oriented file register operations

Pattern: `00 oooo d ffffff`

- Upper 6 bits (`oooo d`) select the operation and destination.
- Lower 7 bits (`ffffff`) hold the file register address `f`.

| Mnemonic | Function (informal)              | Binary pattern (bits 13..0)   | Base hex (upper bits) | Decode mask |
|----------|----------------------------------|--------------------------------|------------------------|-------------|
| MOVWF    | `f ← W`                          | `00 0000 1 ffffff`            | `0x0080`              | `0xFF80`    |
| CLRF     | `f ← 0`                          | `00 0001 1 ffffff`            | `0x0180`              | `0xFF80`    |
| CLRW     | `W ← 0` (alias of `CLR f,W`)     | `00 0001 0 000000`            | `0x0100`              | `0xFFFF`    |
| SUBWF    | `dest ← f − W`                   | `00 0010 d ffffff`            | `0x0200`              | `0x3F00`    |
| DECF     | `dest ← f − 1`                   | `00 0011 d ffffff`            | `0x0300`              | `0x3F00`    |
| IORWF    | `dest ← f OR W`                  | `00 0100 d ffffff`            | `0x0400`              | `0x3F00`    |
| ANDWF    | `dest ← f AND W`                 | `00 0101 d ffffff`            | `0x0500`              | `0x3F00`    |
| XORWF    | `dest ← f XOR W`                 | `00 0110 d ffffff`            | `0x0600`              | `0x3F00`    |
| ADDWF    | `dest ← f + W`                   | `00 0111 d ffffff`            | `0x0700`              | `0x3F00`    |
| MOVF     | `dest ← f`                       | `00 1000 d ffffff`            | `0x0800`              | `0x3F00`    |
| COMF     | `dest ← ~f`                      | `00 1001 d ffffff`            | `0x0900`              | `0x3F00`    |
| INCF     | `dest ← f + 1`                   | `00 1010 d ffffff`            | `0x0A00`              | `0x3F00`    |
| DECFSZ   | `dest ← f − 1; skip if zero`     | `00 1011 d ffffff`            | `0x0B00`              | `0x3F00`    |
| RRF      | `dest ← f rotated right (via C)` | `00 1100 d ffffff`            | `0x0C00`              | `0x3F00`    |
| RLF      | `dest ← f rotated left (via C)`  | `00 1101 d ffffff`            | `0x0D00`              | `0x3F00`    |
| SWAPF    | `dest ← f with nibbles swapped`  | `00 1110 d ffffff`            | `0x0E00`              | `0xFF00`    |
| INCFSZ   | `dest ← f + 1; skip if zero`     | `00 1111 d ffffff`            | `0x0F00`              | `0x3F00`    |

### Bit‑oriented file register operations

Pattern: `01 bbop b ffffff`

- Bits 13..10 (`01bb`) select the bit operation.
- Bits 9..7 carry the bit number `b` (0–7).
- Lower 7 bits (`ffffff`) hold the file register address `f`.

| Mnemonic | Function (informal)                       | Binary pattern             | Base hex | Decode mask |
|----------|-------------------------------------------|----------------------------|----------|-------------|
| BCF      | Clear bit `b` of `f`                      | `01 00 bbb ffffff`         | `0x1000` | `0x3C00`    |
| BSF      | Set bit `b` of `f`                        | `01 01 bbb ffffff`         | `0x1400` | `0xFC00`    |
| BTFSC    | Skip next if bit `b` of `f` is clear      | `01 10 bbb ffffff`         | `0x1800` | `0x3C00`    |
| BTFSS    | Skip next if bit `b` of `f` is set        | `01 11 bbb ffffff`         | `0x1C00` | `0xFC00`    |

### Literal and control operations

Literal operations: `11 1ooo kk kkkkkk` (upper 6 bits select operation, lower 8 bits literal `k`).  
Control transfers:  `10 ckkk kkkkkkk` (high bits select CALL vs GOTO, lower 11 bits address `k`).

| Mnemonic | Function (informal)                 | General pattern                      | Base hex | Decode mask |
|----------|-------------------------------------|--------------------------------------|----------|-------------|
| NOP      | No operation                        | `0000 0000 0000 00`                  | `0x0000` | `0xFFFF`    |
| RETURN   | Return from subroutine              | `0000 0000 0010 00`                  | `0x0008` | `0xFFFF`    |
| RETFIE   | Return from interrupt               | `0000 0000 0010 01`                  | `0x0009` | `0xFFFF`    |
| CLRWDT   | Clear watchdog timer                | fixed control opcode                 | `0x0064` | `0xFFFF`    |
| SLEEP    | Enter low‑power sleep               | fixed control opcode                 | `0x0063` | `0xFFFF`    |
| CALL     | Call subroutine at address `k`      | `10 0 kkkkkkkkkkk`                   | `0x2000` | `0xF800`    |
| GOTO     | Jump to address `k`                 | `10 1 kkkkkkkkkkk`                   | `0x2800` | `0xF800`    |
| MOVLW    | `W ← k`                             | `11 00 xx kkkkkkkk`                  | `0x3000` | `0x3F00`    |
| RETLW    | `W ← k; return`                     | `11 01 00 kkkkkkkk`                  | `0x3400` | `0x3F00`    |
| IORLW    | `W ← W OR k`                        | `11 10 00 kkkkkkkk`                  | `0x3800` | `0x3F00`    |
| ANDLW    | `W ← W AND k`                       | `11 10 01 kkkkkkkk`                  | `0x3900` | `0x3F00`    |
| XORLW    | `W ← W XOR k`                       | `11 10 10 kkkkkkkk`                  | `0x3A00` | `0x3F00`    |
| SUBLW    | `W ← k − W`                         | `11 11 0x kkkkkkkk`                  | `0x3C00` | `0x3F00`    |
| ADDLW    | `W ← W + k`                         | `11 11 1x kkkkkkkk`                  | `0x3E00` | `0x3F00`    |

> **Note**  
> Hex bases and masks here are chosen to work well with the `InstructionMapping` decoder used in this project: a word `w` decodes as a given instruction when `(w & mask) == base`. Variable fields (`f`, `b`, `d`, `k`) are intentionally left out of the mask.

