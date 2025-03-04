using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ModernLangToolsApp
{
    [XmlRoot("Jedi")]
    public class Jedi
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }


        [XmlAttribute("MidiChlorianCount")]
        public int MidiChlorianCount
        {
            get { return midiChlorianCount; }
            set
            {
                if (value <= 35)
                {
                    throw new ArgumentException("You are not a true Jedi!");
                }
                midiChlorianCount = value;
            }
        }
        private int midiChlorianCount;

    }

    public delegate void CouncilChangedDelegate(string message);
    public class JediCouncil
    {

        List<Jedi> members = new List<Jedi>();
        public event CouncilChangedDelegate CouncilChanged;
        public void Add(Jedi j)
        {
            CouncilChanged?.Invoke($"{j.Name} has joined to our council!");
            members.Add(j);
        }

        public void Remove()
        {
            members.RemoveAt(members.Count -1);
            if (members.Count == 0)
            {
                CouncilChanged?.Invoke("Our council empty, it is!");
            }
            else
            {
                CouncilChanged?.Invoke("Our council decided to not have a seat to the Jedi.");
            }
                
        }

        private static bool MidiChlorianCountUnder(Jedi x) {
            return x.MidiChlorianCount < 530;
        }

        public IEnumerable<Jedi> FindAll_Delegate()
        {
            return members.FindAll(MidiChlorianCountUnder);
        }

        public IEnumerable<Jedi> FindAll_Lambda() => members.FindAll(x => x.MidiChlorianCount < 1000);
    }

    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
    class ReportPrinter
    {
        private readonly IEnumerable<Person> people;
        private readonly Action headerPrinter;
        private readonly Action<Person> personFormatter;
        private readonly Action footerPrinter;

        public ReportPrinter(IEnumerable<Person> people, Action headerPrinter, Action<Person> personFormatter, Action footerPrinter)
        {
            this.people = people;
            this.headerPrinter = headerPrinter;
            this.personFormatter = personFormatter;
            this.footerPrinter = footerPrinter;
        }

        public void PrintReport()
        {
            headerPrinter();
            Console.WriteLine("-----------------------------------------");
            int i = 0;
            foreach (var person in people)
            {
                Console.Write($"{++i}. ");
                personFormatter(person);
            }
            Console.WriteLine("--------------- Summary -----------------");
            footerPrinter();
        }
    }

    class ReportBuilder
    {
        private readonly IEnumerable<Person> people;
        private readonly StringBuilder reportStringBuilder = new StringBuilder();
        private readonly Func<string> headerProvider;
        private readonly Func<Person, string> personFormatter;
        private readonly Func<string> footerProvider;

        public ReportBuilder(IEnumerable<Person> people, Func<string> headerProvider, Func<Person, string> personFormatter, Func<string> footerProvider)
        {
            this.people = people;
            this.headerProvider = headerProvider;
            this.personFormatter = personFormatter;
            this.footerProvider = footerProvider;
        }

        public void BuildReport()
        {
            reportStringBuilder.AppendLine(headerProvider());
            reportStringBuilder.AppendLine("-----------------------------------------");

            int i = 1;
            foreach (var person in people)
            {
                reportStringBuilder.AppendLine($"{i}. {personFormatter(person)}");
                i++;
            }

            reportStringBuilder.AppendLine("--------------- Summary -----------------");
            reportStringBuilder.AppendLine(footerProvider());
        }

        public string GetResult()
        {
            return reportStringBuilder.ToString();
        }
    }
}

