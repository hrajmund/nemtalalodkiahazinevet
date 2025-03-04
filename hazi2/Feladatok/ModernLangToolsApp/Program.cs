namespace ModernLangToolsApp;

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.ConstrainedExecution;

public class Program
{
    [Description("Feladat2")]
    public static void LoadAndSave() {
        var anakin = new Jedi() { Name = "Anakin Skywalker", MidiChlorianCount = 23000 };
        var serializer = new XmlSerializer(typeof(Jedi));
        var stream = new FileStream("jedi.txt", FileMode.Create);
        serializer.Serialize(stream, anakin);
        stream.Close();
        var serializerb = new XmlSerializer(typeof(Jedi));
        var streamb = new FileStream("jedi.txt", FileMode.Open);
        var clone = (Jedi)serializerb.Deserialize(streamb);
        streamb.Close();
        Debug.Assert(clone.Name == anakin.Name);
        Debug.Assert(clone.MidiChlorianCount == anakin.MidiChlorianCount);

    }

    private static void MessageReceived(string message)
    {
        Console.WriteLine(message);
    }

    [Description("Feladat3")]
    public static void JediCouncilTest() {
        
        var jediCouncil = new JediCouncil();
        var obiWan = new Jedi() { Name = "Obi-Wan Kenobi", MidiChlorianCount = 16000 };
        var anakinSkywalker = new Jedi() { Name = "Anakin Skywalker", MidiChlorianCount = 20000 };
        var windu = new Jedi() { Name = "Mace Windu", MidiChlorianCount = 13600 };
        
        jediCouncil.CouncilChanged += new CouncilChangedDelegate(MessageReceived);
        jediCouncil.Add(obiWan);
        jediCouncil.Add(anakinSkywalker);
        jediCouncil.Add(windu);
        jediCouncil.Remove();
        jediCouncil.Remove();
        jediCouncil.Remove();

    }

    [Description("Feladat4")]
    public static void TestingDelegate()
    {
        JediCouncil s = new JediCouncil();

        s.Add(new Jedi { Name = "Obi-Wan", MidiChlorianCount = 1100 });
        s.Add(new Jedi { Name = "Anakin", MidiChlorianCount = 2300 });
        s.Add(new Jedi { Name = "Windu", MidiChlorianCount = 1500 });
        
        var resource = s.FindAll_Delegate();

        foreach (var j in resource) {
            Console.WriteLine($"{j.Name}");
        }
    }

    [Description("Feladat5")]
    public static void TestingLambda()
    {
        JediCouncil s = new JediCouncil();

        s.Add(new Jedi { Name = "Obi-Wan", MidiChlorianCount = 1100 });
        s.Add(new Jedi { Name = "Anakin", MidiChlorianCount = 2300 });
        s.Add(new Jedi { Name = "Windu", MidiChlorianCount = 1500 });

        var resource = s.FindAll_Lambda();

        foreach (var j in resource)
        {
            Console.WriteLine($"{j.Name}");
        }
    }

    [Description("Feladat6")]
    static void test6()
    {
        var employees = new Person[] { new Person("Joe", 20), new Person("Jill", 30) };

        ReportPrinter reportPrinter = new ReportPrinter(
            employees,
            () => Console.WriteLine("Employees"),
            person => Console.WriteLine($"Name: {person.Name} (Age: {person.Age})"),
            () => Console.WriteLine($"Number of Employees: {employees.Count()}")
        );

        reportPrinter.PrintReport();
    }

    [Description("Feladat6B")]
    static void test6b()
    {
        var employees = new Person[] { new Person("Joe", 20), new Person("Jill", 30) };
        
        ReportBuilder reportBuilder = new ReportBuilder(
            employees,
            () => "Employees",
            person => $"Name: {person.Name} (Age: {person.Age})",
            () => "Number of Employees: " + employees.Length
        );

        reportBuilder.BuildReport();

        var result = reportBuilder.GetResult();
        Console.WriteLine(result);
    }
    
    public static void Main(string[] args)
    {
        LoadAndSave();
        JediCouncilTest();
        TestingDelegate();
        TestingLambda();
        test6();
        test6b();
    }
}
