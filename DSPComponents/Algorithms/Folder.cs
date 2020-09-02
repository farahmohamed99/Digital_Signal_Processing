using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Folder : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputFoldedSignal { get; set; }

        public override void Run()
        {
            OutputFoldedSignal = InputSignal;
            List<float> before = new List<float>();
            List<float> after = new List<float>();
            List<float> conc = new List<float>();
            List<int> before_indices = new List<int>();
            List<int> after_indices = new List<int>();
            List<int> conc_indices = new List<int>();
            int origin = InputSignal.SamplesIndices.IndexOf(0);
            for (int i = 0; i < origin; i++)
            {
                before.Add(InputSignal.Samples[i]);
                before_indices.Add(-InputSignal.SamplesIndices[i]);
            }
            for (int i = origin + 1; i < InputSignal.Samples.Count; i++)
            {
                after.Add(InputSignal.Samples[i]);
                after_indices.Add(-InputSignal.SamplesIndices[i]);
            }

            before.Reverse();
            after.Reverse();
            before_indices.Reverse();
            after_indices.Reverse();

            for (int i = 0; i < after.Count; i++)
            {
                conc.Add(after[i]);
                conc_indices.Add(after_indices[i]);
            }
            conc.Add(InputSignal.Samples[origin]);
            conc_indices.Add(InputSignal.SamplesIndices[origin]);
            for (int i = 0; i < before.Count; i++)
            {
                conc.Add(before[i]);
                conc_indices.Add(before_indices[i]);
            }
            OutputFoldedSignal.Samples = conc;
            OutputFoldedSignal.SamplesIndices = conc_indices;
            OutputFoldedSignal.Periodic = !OutputFoldedSignal.Periodic;
            //throw new NotImplementedException();
        }
    }
}
