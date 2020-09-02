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
            OutputTimeDomainSignal = InputFreqDomainSignal;
            OutputTimeDomainSignal.Samples = new List<float>();
            int N = InputFreqDomainSignal.FrequenciesAmplitudes.Count;
            List<float> reversed = new List<float>();
            for (int k = 0; k < N; ++k)
            {
                float sum = 0.0f;
                for (int n = 0; n < N; ++n)
                {
                    float real = (float)Math.Cos((2 * Math.PI * k * n) / N);
                    float imaginary = (float)Math.Sin((2 * Math.PI * k * n) / N);

                    float amplitude = InputFreqDomainSignal.FrequenciesAmplitudes[n];
                    float phase_shift = (float) Math.Tan(InputFreqDomainSignal.FrequenciesPhaseShifts[n]);

                    float real_ = (float) Math.Sqrt((amplitude * amplitude) / (1 + phase_shift * phase_shift));
                    float imaginary_ = phase_shift * real_;

                    sum += real * real_ + imaginary * imaginary_ * -1;
                }
                reversed.Add(sum/N);
            }
            reversed.Reverse();
            OutputTimeDomainSignal.Samples = reversed;
        }
    }
}
