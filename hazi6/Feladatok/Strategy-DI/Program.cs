using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lab_Extensibility.AnonymizerAlgorithms;
using Lab_Extensibility.InputReaders;
using Lab_Extensibility.Progresses;
using Lab_Extensibility.ResultWriters;

namespace Lab_Extensibility;

static class Program
{
    static void Main(string[] args)
    {
        // We can use the same Anonymizer with any combination of strategies

        /*Anonymizer p1 = new("us-500.csv",
            new NameMaskingAnonymizerAlgorithm("***"),
            new SimpleProgress(), new CsvInputReader("us-500.csv"), new CsvResultWriter("us-500.csv"));
        p1.Run();

        Console.WriteLine("--------------------");

        Anonymizer p2 = new("us-500.csv",
            new NameMaskingAnonymizerAlgorithm("***"),
            new PercentProgress(), new CsvInputReader("us-500.csv"), new CsvResultWriter("us-500.csv"));
        p2.Run();

        Console.WriteLine("--------------------");

        Anonymizer p3 = new("us-500.csv",
            new AgeAnonymizerAlgorithm(20),
            new SimpleProgress(), new CsvInputReader("us-500.csv"), new CsvResultWriter("us-500.csv"));
        p3.Run(); */
        
        Anonymizer p1 = new Anonymizer("us-500.csv", 
            new NameMaskingAnonymizerAlgorithm("***"), 
            new CsvInputReader("us-500.csv"), 
            new CsvResultWriter("us-500.csv"),
            (count, index) =>
        {
            Console.WriteLine($"Feldolgozott személyek: {index}/{count}");
        });
        p1.Run();

        Console.WriteLine("--------------------");

        Anonymizer p2 = new Anonymizer("us-500.csv",
            new NameMaskingAnonymizerAlgorithm("***"), 
            new CsvInputReader("us-500.csv"), 
            new CsvResultWriter("us-500.csv"),
            AllProgresses.SimpleProgress);
        p2.Run();

        Console.WriteLine("--------------------");

        Anonymizer p3 = new Anonymizer("us-500.csv", 
            new NameMaskingAnonymizerAlgorithm("***"), 
            new CsvInputReader("us-500.csv"), 
            new CsvResultWriter("us-500.csv"),
            AllProgresses.PercentProgress);
        p3.Run();
    }
}