using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace DSPAlgorithms.Algorithms
{
    public class FastCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        public override void Run()
        { 
            List<Complex> l1_comp=new List<Complex>();
            List<Complex> l2_comp = new List<Complex>();

            bool nuu = false;
            List<decimal> out_not_norm = new List<decimal>();
            // out_not_norm = InputSignal1.Samples;
            List<decimal> l1 = new List<decimal>();
            List<decimal> l2 = new List<decimal>();

            float num = 0;
            bool p = InputSignal1.Periodic;

            if (InputSignal2 == null)
            {
                nuu = true;
                //InputSignal2 = InputSignal1;
                InputSignal2 = new Signal(new List<float>(), InputSignal1.Periodic);
                for (int i = 0; i < InputSignal1.Samples.Count; ++i)
                {
                    InputSignal2.Samples.Add((float)Math.Round((decimal)InputSignal1.Samples[i], 4));

                }
                for (int i = 0; i < InputSignal1.Samples.Count; ++i)
                {
                    l1.Add((decimal)InputSignal1.Samples[i]);
                    l2.Add((decimal)InputSignal1.Samples[i]);


                }


            }
            else
            {
                for (int i = 0; i < InputSignal1.Samples.Count; ++i)
                {
                    l1.Add((decimal)InputSignal1.Samples[i]);


                }
                for (int i = 0; i < InputSignal2.Samples.Count; ++i)
                {
                    l2.Add((decimal)InputSignal2.Samples[i]);


                }
            }
            int j = 0;
            if (l1.Count != l2.Count)
            {

                if (p == false)
                {
                    int diff = Math.Max(InputSignal1.Samples.Count, InputSignal2.Samples.Count) -
                               Math.Min(InputSignal1.Samples.Count, InputSignal2.Samples.Count);
                    for (int i = 0; i < diff; ++i)
                    {
                        if (l1.Count < l2.Count)
                            l1.Add(0);
                        else if (l1.Count > l2.Count)
                        {
                            l2.Add(0);
                        }

                    }
                }
                else
                {
                    int s = (InputSignal1.Samples.Count + InputSignal2.Samples.Count) - 1;
                    for (int i = 0; i < s; ++i)
                    {
                        if (l1.Count < s)
                            l1.Add(0);
                        if (l2.Count < s)
                        {
                            l2.Add(0);
                        }

                    }

                }

            

            }

            int size = l1.Count;

            for (int i = 0; i < size; ++i)
                out_not_norm.Add(0);
            for (int i = 0; i < size; ++i)
            {
                Complex c1=new Complex((float)l1[i],0);
                Complex c2=new Complex((float)l2[i],0);
                l1_comp.Add(c1);
                l2_comp.Add(c2);
            }
            FastFourierTransform f1=new FastFourierTransform();
            FastFourierTransform f2=new FastFourierTransform();
            f1.FFT(ref l1_comp,size);
            f2.FFT(ref l2_comp,size);
            List<Complex> mul=new List<Complex>();
            for (int i = 0; i < size; ++i)
            {
                Complex c=new Complex(l1_comp[i].Real, l1_comp[i].Imaginary*-1);
                mul.Add(c*l2_comp[i]);
            }
            InverseFastFourierTransform inv=new InverseFastFourierTransform();
            inv.IFFT(ref mul,size);
           OutputNonNormalizedCorrelation = new List<float>();
            OutputNormalizedCorrelation=new List<float>();
            for (int i = 0; i < size; ++i)
            {
               // out_not_norm[i] = (decimal) (mul[i].Real / size);
               float x = (float) (mul[i].Real / (mul.Count*mul.Count));
               OutputNonNormalizedCorrelation.Add(x);
            }



           


            decimal sum1 = 0;
            decimal sum2 = 0;
            for (int i = 0; i < l1.Count; ++i)
            {
                sum1 += (l1[i] * l1[i]);
            }
            for (int i = 0; i < l2.Count; ++i)
            {
                sum2 += (l2[i] * l2[i]);
            }

            float norm = (float)Math.Sqrt((double)((sum2 * sum1)));
            norm /= l1.Count;
            if (norm != 0)
                for (int i = 0; i < OutputNonNormalizedCorrelation.Count; ++i)
                {
                    OutputNormalizedCorrelation.Add((OutputNonNormalizedCorrelation[i] / norm));
                }
            //throw new NotImplementedException();
        }
    }
}