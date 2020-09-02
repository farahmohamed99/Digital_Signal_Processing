﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Shifter : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int ShiftingValue { get; set; }
        public Signal OutputShiftedSignal { get; set; }

        public override void Run()
        {
            OutputShiftedSignal = InputSignal;
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                if(InputSignal.Periodic == true)
                {
                    OutputShiftedSignal.SamplesIndices[i] = InputSignal.SamplesIndices[i] + ShiftingValue;
                }
                else
                {
                    OutputShiftedSignal.SamplesIndices[i] = InputSignal.SamplesIndices[i] - ShiftingValue;
                }
                
            }
            //throw new NotImplementedException();
        }
    }
}
