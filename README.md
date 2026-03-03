# PICHexDisassembler [![Build status](https://ci.appveyor.com/api/projects/status/hor0qtsu18fot39o?svg=true) ![Test status](http://teststatusbadge.azurewebsites.net/api/status/Fredi/pichexdisassembler)](https://ci.appveyor.com/project/Fredi/pichexdisassembler)

Simple HEX disassembler for PIC 16FXXX microcontrollers.

It parses Intel HEX (I32HEX) records produced by PIC toolchains and converts
them into a human‑readable assembly listing for a subset of PIC16 instructions.

- **Input**: Intel HEX firmware file (`.hex`)
- **Output**: Disassembled PIC assembly as text

The core logic lives in `HexParser`, `Hex32Record` and the various instruction
classes under `PICHexDisassembler.Instructions`.

## Getting started

### Requirements

- **.NET SDK**: Any recent .NET SDK (LTS recommended), with `dotnet` available on your `PATH`.

### Build

From the repository root:

```bash
dotnet build
```

### Running tests

The project includes an `xUnit` test suite that exercises the HEX parsing and
disassembly logic (see `PICHexDisassembler.Tests/HexParserTests.cs`).

Run all tests with:

```bash
dotnet test
```

## Usage

PICHexDisassembler is currently structured as a library. A typical usage pattern
from a .NET application looks like this:

```csharp
using System.IO;
using PICHexDisassembler;

var hexLines = File.ReadAllLines("firmware.hex");
var parser = new HexParser();
var records = parser.ParseLines(hexLines);

// Get a full assembly listing for the parsed records
var assemblyListing = records.ToString();

System.Console.WriteLine(assemblyListing);
```

For reference examples of the output format, see the expectations in
`HexParserTests`, for instance the multi‑line assertions such as:

```text
RETFIE
CALL 0x2C
BSF STATUS, RP0
BCF STATUS, RP1
BCF PORTB, RB5
BCF STATUS, RP0
BCF STATUS, RP1
BSF PORTB, RB5
```

## About the PIC HEX format (I32HEX)

Intel HEX is a file format that conveys binary information in ASCII text form.
It is commonly used for programming microcontrollers, EPROMs, and other types
of programmable logic devices.

### Record structure

A record (line of text) consists of six fields (parts) that appear in order
from left to right:

1. **Start code**, one character, an ASCII colon ':'.
2. **Byte count**, two hex digits, indicating the number of bytes (hex digit
   pairs) in the data field. The maximum byte count is 255 (0xFF). 16 (0x10)
   and 32 (0x20) are commonly used byte counts.
3. **Address**, four hex digits, representing the 16-bit beginning memory
   address offset of the data. The physical address of the data is computed by
   adding this offset to a previously established base address, thus allowing
   memory addressing beyond the 64 kilobyte limit of 16-bit addresses. The
   base address, which defaults to zero, can be changed by various types of
   records. Base addresses and address offsets are always expressed as big
   endian values.
4. **Record type**, two hex digits, 00 to 05, defining the meaning of the data
   field.
5. **Data**, a sequence of n bytes of data, represented by 2n hex digits. Some
   records omit this field (n equals zero). The meaning and interpretation of
   data bytes depends on the application.
6. **Checksum**, two hex digits, a computed value that can be used to verify
   the record has no errors.

**_Reference: https://en.wikipedia.org/wiki/Intel_HEX_**

## Contribution

Just send a pull request! :)

## License

Copyright (c) 2016 Fredi Machado. See the LICENSE file for license rights and
limitations (MIT).
