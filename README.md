# PICHexDisassembler
Simple Hex disassembler for PIC 16FXXX microcontrollers

## About the PIC HEX format (I32HEX)
Intel HEX is a file format that conveys binary information in ASCII text form. It is commonly used for programming microcontrollers, EPROMs, and other types of programmable logic devices.

### Record structure
A record (line of text) consists of six fields (parts) that appear in order from left to right:

1. **Start code**, one character, an ASCII colon ':'.
2. **Byte count**, two hex digits, indicating the number of bytes (hex digit pairs) in the data field. The maximum byte count is 255 (0xFF). 16 (0x10) and 32 (0x20) are commonly used byte counts.
3. **Address**, four hex digits, representing the 16-bit beginning memory address offset of the data. The physical address of the data is computed by adding this offset to a previously established base address, thus allowing memory addressing beyond the 64 kilobyte limit of 16-bit addresses. The base address, which defaults to zero, can be changed by various types of records. Base addresses and address offsets are always expressed as big endian values.
4. **Record type**, two hex digits, 00 to 05, defining the meaning of the data field.
5. **Data**, a sequence of n bytes of data, represented by 2n hex digits. Some records omit this field (n equals zero). The meaning and interpretation of data bytes depends on the application.
6. **Checksum**, two hex digits, a computed value that can be used to verify the record has no errors.

**_Reference: https://en.wikipedia.org/wiki/Intel_HEX_**

## License
Copyright (c) 2016 Fredi Machado. See the LICENSE file for license rights and
limitations (MIT).
