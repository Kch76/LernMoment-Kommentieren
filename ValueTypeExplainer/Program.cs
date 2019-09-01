using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValueTypeExplainer
{
    class Program
    {
        static void Main(string[] args)
        {
            FieldValueExtractor<short> shortExtractor = new FieldValueExtractor<short>();
            FieldValueExtractor<ushort> positiveShortExtractor = new FieldValueExtractor<ushort>();
            FieldValueExtractor<int> intExtractor = new FieldValueExtractor<int>();
            FieldValueExtractor<double> doubleExtractor = new FieldValueExtractor<double>();

            Console.WriteLine($"int16.MaxValue = {shortExtractor.GetFieldInfoForValueType("MaxValue")}");
            Console.WriteLine($"uint16.MaxValue = {positiveShortExtractor.GetFieldInfoForValueType("MaxValue")}");
            Console.WriteLine($"int.MaxValue = {intExtractor.GetFieldInfoForValueType("MaxValue")}");
            Console.WriteLine($"double.MaxValue = {doubleExtractor.GetFieldInfoForValueType("MaxValue")}");

            Console.WriteLine();
            Console.WriteLine("Anwendung mit 'Enter' beenden!");
            Console.ReadLine();
        }
    }
}
