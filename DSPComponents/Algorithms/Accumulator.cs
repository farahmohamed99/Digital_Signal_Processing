using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Accumulator : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            OutputSignal = new Signal(new List<float>(), new List<int>(), false);
            
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                float sum = 0;
                for (int j = 0; j <= i; j++)
                {
                    sum += InputSignal.Samples[j];
                }
                OutputSignal.Samples.Add(sum);
            }
            OutputSignal.SamplesIndices = InputSignal.SamplesIndices;
        }
    }
}