using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Numerics;

namespace DSPAlgorithms.Algorithms
{
   public class FastFourierTransform : Algorithm
    {
        public void FFT(ref List<Complex> input_samples, int n)
        {
            if (n <= 1)
            {
                return;
            }

            List<Complex> even_input_samples = new List<Complex>();
            List<Complex> odd_input_samples = new List<Complex>();
            int N = n / 2;
            for (int i = 0; i < N; i++)
            {
                even_input_samples.Add(input_samples[i * 2]);
                odd_input_samples.Add(input_samples[i * 2 + 1]);
            }
            FFT(ref even_input_samples, N);
            FFT(ref odd_input_samples, N);
            for (int j = 0; j < N; j++)
            {
                double real = (double)Math.Cos((-2 * Math.PI * j) / ((double)n));
                double img = (double)Math.Sin((-2 * Math.PI * j) / ((double)n));
                Complex c = new Complex(real, img);
                c = c * odd_input_samples[j];
                input_samples[j] = even_input_samples[j] + c;
                input_samples[j + N] = even_input_samples[j] - c;
            }
        }
        public Signal InputTimeDomainSignal { get; set; }
        public int InputSamplingFrequency { get; set; }
        public Signal OutputFreqDomainSignal { get; set; }

        public override void Run()
        {
            List<float> amp = new List<float>();
            List<float> phase = new List<float>();
            List<float> frequency = new List<float>();
            List<Complex> samples = new List<Complex>();

            for (int i = 0; i < InputTimeDomainSignal.Samples.Count; i++)
            {
                samples.Add(InputTimeDomainSignal.Samples[i]);
            }

            FFT(ref samples, samples.Count);

            for (int i = 0; i < InputTimeDomainSignal.Samples.Count; i++)
            {
                amp.Add((float)samples[i].Magnitude);
                phase.Add((float)samples[i].Phase);
                frequency.Add(i);
            }
            OutputFreqDomainSignal = new Signal(false, frequency, amp, phase);
            //throw new NotImplementedException();
        }
    }
}
