using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {

            int output = InputSignal1.Samples.Count + (InputSignal2.Samples.Count - 1);
            List<float> samples = new List<float>();
            List<int> acutal_indecies = new List<int>();
            int acutal_index = 0;
            if (InputSignal1.SamplesIndices[0] < 0 || InputSignal2.SamplesIndices[0] < 0)
            {
                acutal_index = InputSignal1.SamplesIndices[0] + InputSignal2.SamplesIndices[0];
            }
            for (int i = 0; i < output; i++)
            {
                float sum = 0;
                for (int j = 0; j < InputSignal1.Samples.Count; j++)
                {
                    if ((j > i) || (j >= InputSignal1.Samples.Count) || ((i - j) >= InputSignal2.Samples.Count))
                    {
                        sum += 0;
                    }

                    else
                    {
                        sum += InputSignal1.Samples[j] * InputSignal2.Samples[i - j];
                    }
                }
                samples.Add(sum);
                acutal_indecies.Add(acutal_index);
                acutal_index++;
            }

            int end = (samples.Count - 1);
            while (samples.Count > 0)
            {
                if (samples[end] == 0)
                {
                    samples.RemoveAt(end);
                    acutal_indecies.RemoveAt(end);
                }
                else
                {
                    break;
                }
                end--;
            }
            OutputConvolvedSignal = new Signal(samples, acutal_indecies, false);
            //throw new NotImplementedException();
        }
    }
}
