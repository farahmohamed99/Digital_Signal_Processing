using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class InverseDiscreteFourierTransform : Algorithm
    {
        public Signal InputFreqDomainSignal { get; set; }
        public Signal OutputTimeDomainSignal { get; set; }

        public override void Run()
        {
            List<Tuple<float, float>> real_imaginary = new List<Tuple<float, float>>();
            List<float> samples = new List<float>();
            for (int i = 0; i < InputFreqDomainSignal.FrequenciesAmplitudes.Count; i++)
            {
                float amp = InputFreqDomainSignal.FrequenciesAmplitudes[i];
                float phase = (float)Math.Tan(InputFreqDomainSignal.FrequenciesPhaseShifts[i]);
                float real = (float)Math.Sqrt((Math.Pow(amp, 2)) / (1 + Math.Pow(phase, 2)));
                float img = real * phase;
                real_imaginary.Add(new Tuple<float, float>(real, img));
            }
            for (int i = 0; i < InputFreqDomainSignal.FrequenciesAmplitudes.Count; i++)
            {
                float sample = 0;
                for (int j = 0; j < InputFreqDomainSignal.FrequenciesAmplitudes.Count; j++)
                {
                    float real_real = real_imaginary[j].Item1 * (float)(Math.Cos((2 * (Math.PI) * i * j) / InputFreqDomainSignal.FrequenciesAmplitudes.Count));
                    float real_img = real_imaginary[j].Item1 * (float)(Math.Sin((2 * (Math.PI) * i * j) / InputFreqDomainSignal.FrequenciesAmplitudes.Count));
                    float img_real = real_imaginary[j].Item2 * (float)(Math.Cos((2 * (Math.PI) * i * j) / InputFreqDomainSignal.FrequenciesAmplitudes.Count));
                    float img_img = real_imaginary[j].Item2 * (float)(Math.Sin((2 * (Math.PI) * i * j) / InputFreqDomainSignal.FrequenciesAmplitudes.Count));
                    sample = sample + (real_real + (-img_img));
                }
                samples.Add(sample / InputFreqDomainSignal.FrequenciesAmplitudes.Count);
            }
            samples.Reverse();
            OutputTimeDomainSignal = new Signal(samples, false);
        }
    }
}
