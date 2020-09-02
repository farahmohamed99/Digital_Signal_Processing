using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.IO;

namespace DSPAlgorithms.Algorithms
{
    public class DiscreteFourierTransform : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public float InputSamplingFrequency { get; set; }
        public Signal OutputFreqDomainSignal { get; set; }

        public override void Run()
        {
            
            List<float> frequency = new List<float>();
            List<float> phaseShift = new List<float>();
            List<float> amplitude = new List<float>();
            List<Tuple<double, double>> harmonic = new List<Tuple<double, double>>();
            for (int i = 0; i < InputTimeDomainSignal.Samples.Count; i++)
            {
                frequency.Add(i*(1/InputSamplingFrequency));
                double real = 0;
                double img = 0;
                for (int j = 0; j < InputTimeDomainSignal.Samples.Count; j++)
                {
                    real = real + (((double)(InputTimeDomainSignal.Samples[j])) * (Math.Cos((2 * (Math.PI) * i * j) / InputTimeDomainSignal.Samples.Count)));
                    img = img - ((double)(InputTimeDomainSignal.Samples[j]) * (Math.Sin((2 * (Math.PI) * i * j) / InputTimeDomainSignal.Samples.Count)));
                }
                harmonic.Add(new Tuple<double, double>(real, img));

                double phase1 = Math.Atan2(harmonic[i].Item2, harmonic[i].Item1);
                float phase2 = (float)Math.Round(phase1, 5);
                phaseShift.Add(phase2);

                float amp = (float)Math.Sqrt(Math.Pow(harmonic[i].Item1, 2) + Math.Pow(harmonic[i].Item2, 2));
                amplitude.Add(amp);

            }
            OutputFreqDomainSignal = new Signal(false, frequency, amplitude, phaseShift);
            //throw new NotImplementedException();
        }
    }
}
