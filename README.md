# Antlr4Calculator
Simple Antlr4 "HelloWorld" Calculator

# Build

1. Install the ANTLR 4 Runtime and ANTLR 4 C# Target NuGet packages. 
2. Build the `Rubberduck.CaclulatorParser` project to generate the lexer,parser, and visitors.
  These files are stored under `CalculatorParser\obj\` and linked into the `Antlr` directory of the `Rubberduck.Math` project.
  They are not in the repository, you must build them.
3. Compile the `Rubberduck.Math` project.

# About

At time of writing, the Calculator evaluates expression of multiplication, division, addition, and subtraction. 
The best place to learn about what the Calculator can and can't do would be the *.g4 grammar file and the unit tests.
