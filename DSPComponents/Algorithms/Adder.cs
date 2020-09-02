using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Adder : Algorithm
    {
        public List<Signal> InputSignals { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            OutputSignal = InputSignals[0];
            for(int i = 1; i < InputSignals.Count; i+=2)
            {

                for(int j=0; j < InputSignals[i].Samples.Count;++j)
                {
                    OutputSignal.Samples[j] = OutputSignal.Samples[j] + InputSignals[i].Samples[j];
                }
            }
        }
    }
}