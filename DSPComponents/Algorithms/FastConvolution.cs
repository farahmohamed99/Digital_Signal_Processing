using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Numerics;

namespace DSPAlgorithms.Algorithms
{
    public class FastConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            List<Complex> InputSignal1_complex = new List<Complex>();
            List<Complex> InputSignal2_complex = new List<Complex>();
            List<Complex> conc = new List<Complex>();
            List<float> samples = new List<float>();
            int n = (InputSignal1.Samples.Count + InputSignal2.Samples.Count) - 1;
            for (int i = 0; i < InputSignal1.Samples.Count; i++)
            {
                Complex c = new Complex(InputSignal1.Samples[i], 0);
                InputSignal1_complex.Add(c);
            }
            for (int i = InputSignal1.Samples.Count; i < n; i++)
            {
                Complex c = new Complex(0, 0);
                InputSignal1_complex.Add(c);
            }
            for (int i = 0; i < InputSignal2.Samples.Count; i++)
            {
                Complex c = new Complex(InputSignal2.Samples[i], 0);
                InputSignal2_complex.Add(c);
            }
            for (int i = InputSignal2.Samples.Count; i < n; i++)
            {
                Complex c = new Complex(0, 0);
                InputSignal2_complex.Add(c);
            }
            FastFourierTransform fft1 = new FastFourierTransform();
            FastFourierTransform fft2 = new FastFourierTransform();
            fft1.FFT(ref InputSignal1_complex, n);
            fft2.FFT(ref InputSignal2_complex, n);
            for (int i = 0; i < n; i++)
            {
                conc.Add(InputSignal1_complex[i] * InputSignal2_complex[i]);
            }
            InverseFastFourierTransform ifft = new InverseFastFourierTransform();
            ifft.IFFT(ref conc, n);
            for (int i = 0; i < n; i++)
            {
                samples.Add((float)conc[i].Real/n );
            }
            OutputConvolvedSignal = new Signal(samples, false);
            //throw new NotImplementedException();
        }
        }
    }
