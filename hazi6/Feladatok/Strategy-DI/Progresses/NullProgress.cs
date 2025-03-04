using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Extensibility.Progresses
{
    public class NullProgress : IProgress
    {
        public void Report(int count, int index)
        {
            // Do nothing
        }
    }
}
