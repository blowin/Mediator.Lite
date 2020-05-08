using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Mediator.Lite.Sample
{
    public class SampleRunner
    {
        private string[] _sampleNames;
        private Dictionary<string, ISample> _samples;
        
        public SampleRunner(string[] sampleNames)
        {
            _samples = GetAllSamples();
            _sampleNames = NeedRunAll(sampleNames) ? 
                _samples.Keys.ToArray() : 
                sampleNames;
        }

        public void PrintSampleList()
        {
            Console.WriteLine("Sample list: ");
            foreach (var (sampleNumber, sampleName) in _samples.Select((pair, i) =>  (i + 1, pair.Key)))
                Console.WriteLine($"\t{sampleNumber} - {sampleName}");
            
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - -");
            Console.WriteLine();
        }
        
        public void Run()
        {
            foreach (var sampleName in _sampleNames)
            {
                if (_samples.TryGetValue(sampleName, out var sample))
                {
                    Console.WriteLine();
                    Console.WriteLine($"START ************************ {sampleName} *****************************");
                    sample.Run();
                    Console.WriteLine($"END ************************ {sampleName} *****************************");
                }
                else
                {
                    Console.WriteLine($"Not found sample '{sampleName}'");
                }
                
                Console.WriteLine();
            }
        }
        
        private bool NeedRunAll(string[] args)
        {
            return args == null || args.Length == 0;
        }
        
        private static Dictionary<string, ISample> GetAllSamples()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => !x.IsInterface && !x.IsAbstract && typeof(ISample).IsAssignableFrom(x))
                .Select(s => (GetDescription(s), (ISample) Activator.CreateInstance(s)))
                .ToDictionary(s => s.Item1, s => s.Item2);

            string GetDescription(Type t)
            {
                return t.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .Cast<DescriptionAttribute>()
                    .Select(s => s.Description)
                    .Single();
            }
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var runner = new SampleRunner(args);
            
            runner.PrintSampleList();

            runner.Run();
        }
    }
}