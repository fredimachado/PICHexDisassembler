using PICHexDisassembler;

if (args.Length != 1)
{
    Console.Error.WriteLine("Usage: PICHexDisassembler.Cli <firmware.hex>");
    return 1;
}

var path = args[0];

if (!File.Exists(path))
{
    Console.Error.WriteLine($"File not found: {path}");
    return 1;
}

var lines = File.ReadAllLines(path);
var parser = new HexParser();
var records = parser.ParseLines(lines);

Console.WriteLine(records.ToString());

return 0;
