using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Extensibility.InputReaders
{
    internal class CsvInputReader : IInputReader
    {
        private string _inputFileName;
        public CsvInputReader(string filePath) { _inputFileName= filePath; }
        public List<Person> Read()
        {
            List<Person> persons = new List<Person>();
            int _personCount = 0;
            using (StreamReader reader = new(_inputFileName))
            {
                Console.WriteLine($"File has been opened ({_inputFileName})");

                // Process the file
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    ++_personCount;

                    // Split rows into columns - no need to understand regex here
                    System.Text.RegularExpressions.MatchCollection columns =
                        new System.Text.RegularExpressions.Regex("((?<=\")[^\"]*(?=\"(,|$)+)|(?<=,|^)[^,\"]*(?=,|$))").Matches(line);

                    persons.Add(new Person(firstName: columns[0].Value, lastName: columns[1].Value,
                        companyName: columns[2].Value, address: columns[3].Value, city: columns[4].Value, state: columns[6].Value,
                        age: columns[10].Value, weight: columns[11].Value, decease: columns[12].Value));
                }
            }

            return persons;

        }
    }
}
