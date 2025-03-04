using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Extensibility.ResultWriters
{
    internal class CsvResultWriter : IResultWriter
    {
        string _inputFileName;
        public CsvResultWriter(string _inputFileName) { this._inputFileName = _inputFileName; }
        public void Write(List<Person> persons)
        {
            string outFileName = Path.ChangeExtension(_inputFileName, "processed.txt");
            using (StreamWriter writer = new StreamWriter(outFileName))
            {
                foreach (Person p in persons)
                    writer.WriteLine($"{p.FirstName}; {p.LastName}; {p.State}; {p.City}; {p.Age}; {p.Weight}; {p.Decease}");
            }

            Console.WriteLine($"Output file generated ({outFileName})");
        }
    }
}
