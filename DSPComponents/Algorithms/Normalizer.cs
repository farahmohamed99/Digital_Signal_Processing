using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Normalizer : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputMinRange { get; set; }
        public float InputMaxRange { get; set; }
        public Signal OutputNormalizedSignal { get; set; }

        public override void Run()
        {
            OutputNormalizedSignal = InputSignal;
            float mini = InputSignal.Samples.Min();
            float maxi = InputSignal.Samples.Max();

            for(int i = 0; i < InputSignal.Samples.Count; ++i)
            {
                OutputNormalizedSignal.Samples[i] = InputMinRange + ((InputSignal.Samples[i] - mini) / (maxi - mini)) * (InputMaxRange - InputMinRange);
            }

        }
    }
}
