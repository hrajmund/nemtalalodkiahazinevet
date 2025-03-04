using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Extensibility.InputReaders
{
    public interface IInputReader
    {
        List<Person> Read();
    }
}
