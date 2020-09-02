using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Collections.Generic;

namespace DSPAlgorithms.Algorithms
{
    public class QuantizationAndEncoding : Algorithm
    {
        // You will have only one of (InputLevel or InputNumBits), the other property will take a negative value
        // If InputNumBits is given, you need to calculate and set InputLevel value and vice versa
        public int InputLevel { get; set; }
        public int InputNumBits { get; set; }
        public Signal InputSignal { get; set; }
        public Signal OutputQuantizedSignal { get; set; }
        public List<int> OutputIntervalIndices { get; set; }
        public List<string> OutputEncodedSignal { get; set; }
        public List<float> OutputSamplesError { get; set; }

        public override void Run()
        {
            List<float> _SignalSamples = new List<float>();
            bool _periodic = false;
            OutputQuantizedSignal = new Signal(_SignalSamples, _periodic);
            OutputIntervalIndices = new List<int>();
            OutputEncodedSignal = new List<string>();
            OutputSamplesError = new List<float>();


            if (InputLevel > 0)
            {
                double n_bits = Math.Log(InputLevel, 2);
                if (n_bits % 1 != 0)
                    InputNumBits = Convert.ToInt32(n_bits) + 1;
                else
                    InputNumBits = Convert.ToInt32(n_bits);
            }

            else
            {
                InputLevel = 1;
                for (int i = 0; i < InputNumBits; ++i)
                    InputLevel *= 2;
            }
            float minimum = InputSignal.Samples.Min();
            float maximum = InputSignal.Samples.Max();
            float delta = (maximum - minimum) / InputLevel;

            List<float> midpoints = new List<float>();
            List<Tuple<float, float>> intervals = new List<Tuple<float, float>>();
            float l = minimum;
            float r = maximum;
            for(int i = 0; i < InputLevel; ++i)
            {
                r = l + delta;
                midpoints.Add((l + r) / 2);
                intervals.Add(new Tuple<float, float>(l, r));
                l = r;
            }

            for (int i = 0; i < InputSignal.Samples.Count; ++i)
            {
                int interval = InputLevel - 1;
                for (int j = 0; j < intervals.Count; ++j)
                    if (InputSignal.Samples[i] >= intervals[j].Item1 && InputSignal.Samples[i] <= intervals[j].Item2)
                        interval = j;

                OutputIntervalIndices.Add(interval + 1);
                OutputQuantizedSignal.Samples.Add(midpoints[interval]);
                string encoded = Convert.ToString(interval, 2);
                string rem = "";
                for (int j = 0; j < InputNumBits - encoded.Length; ++j)
                    rem += '0';

                OutputEncodedSignal.Add(rem + encoded);
                OutputSamplesError.Add(midpoints[interval] - InputSignal.Samples[i]);
               
            }
           
           
            
        }
    }
}
