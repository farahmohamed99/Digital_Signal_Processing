using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Numerics;

namespace DSPAlgorithms.Algorithms
{
    public class InverseFastFourierTransform : Algorithm
    {


        public void IFFT(ref List<Complex> input_samples, int n)
        {
            if (n <= 1)
            {
                return;
            }

            List<Complex> even_input_samples = new List<Complex>();
            List<Complex> odd_input_samples = new List<Complex>();
            int N = n/2;
            for (int i = 0; i < N; i++)
            {
               even_input_samples.Add(input_samples[i * 2]);
                odd_input_samples.Add(input_samples[i * 2 + 1]);
            }
            IFFT(ref even_input_samples, N);
            IFFT(ref odd_input_samples, N);
            for (int j = 0; j < N; j++)
            {
                double real = (double)Math.Cos((2 * Math.PI * j) / ((double)n));
                double img = (double)Math.Sin((2 * Math.PI * j) / ((double)n));
                Complex c = new Complex(real, img);
                c = c * odd_input_samples[j];
                input_samples[j] = even_input_samples[j] + c;
                input_samples[j + N] = even_input_samples[j] - c;
            }
        }
        public Signal InputFreqDomainSignal { get; set; }
        public Signal OutputTimeDomainSignal { get; set; }
        public override void Run()
        {
            List<Complex> harmonic = new List<Complex>();
            List<float> samples = new List<float>();
            List<float> phase_shift = new List<float>();
            for (int i = 0; i < InputFreqDomainSignal.FrequenciesAmplitudes.Count; i++)
            {

                double phase = InputFreqDomainSignal.FrequenciesPhaseShifts[i];
                double amp = InputFreqDomainSignal.FrequenciesAmplitudes[i];


                double real = amp * Math.Cos(phase);
                double img = amp * Math.Sin(phase);

               
                Complex c = new Complex((float)real, (float)img);
                harmonic.Add(c);
            }
            IFFT(ref harmonic, harmonic.Count);
            for (int i = 0; i < harmonic.Count; i++)
            {
                double x = (double)harmonic[i].Real / (double)harmonic.Count;
                samples.Add((float)Math.Round(x,3));
            }
            OutputTimeDomainSignal = new Signal(samples, false);
            //throw new NotImplementedException();
        }

    }
}
