using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;

namespace DSPAlgorithms.Algorithms
{
    public class FIR : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public FILTER_TYPES InputFilterType { get; set; }
        public float InputFS { get; set; }
        public float? InputCutOffFrequency { get; set; }
        public float? InputF1 { get; set; }
        public float? InputF2 { get; set; }
        public float InputStopBandAttenuation { get; set; }
        public float InputTransitionBand { get; set; }
        public Signal OutputHn { get; set; }
        public Signal OutputYn { get; set; }


        public void low_filter(double num, List<float> H, List<float> WI, List<int> ind, List<float> HW, string type)
        {
            List<float> temp = new List<float>();
            float fc = ((float)InputCutOffFrequency + (InputTransitionBand / 2))/ InputFS;
            for (int i = 0; i <= num / 2; i++)
            {
                if (type == "rec")
                {
                    WI.Add(1);
                }
                else if (type == "han")
                {
                    WI.Add((float)(0.5 + (0.5 * Math.Cos((2 * Math.PI * i) / num))));
                }
                else if (type == "ham")
                {
                    WI.Add((float)(0.54 + (0.46 * Math.Cos((2 * Math.PI * i) / num))));
                }
                else
                {
                    WI.Add((float)(0.42 + (float)(0.5 * Math.Cos((2 * Math.PI * i) / (num - 1))) + (float)(0.08 * Math.Cos((4 * Math.PI * i) / (-1)))));
                }

                if (i == 0)
                {
                    H.Add(2 * fc);
                }
                else
                {
                    H.Add((float)((2 * fc) * (Math.Sin(2 * Math.PI * fc * i)) / (2 * Math.PI * fc * i)));
                }
                
            }
            for (int j = 0; j < WI.Count; j++)
            {
                HW.Add((float)(WI[j] * H[j]));
                temp.Add((float)(WI[j] * H[j]));
            }

            HW.Reverse();
            for (int j = 1; j < temp.Count; j++)
            {
                HW.Add((float)(temp[j]));
            }
            for (int j = ( (int)(-num)) / 2; j <= (num / 2); j++)
            {
                ind.Add(j);
            }
            OutputHn = new Signal(HW, ind, false);
            DirectConvolution conv = new DirectConvolution();
            conv.InputSignal1 = InputTimeDomainSignal;
            conv.InputSignal2 = OutputHn;
            conv.Run();
            OutputYn = conv.OutputConvolvedSignal;
           //OutputHn.Samples.RemoveRange((int)(num), (int)(OutputHn.Samples.Count - num));
           // OutputHn.SamplesIndices.RemoveRange((int)(num), (int)(OutputHn.SamplesIndices.Count - num));
        }
        public void high_filter(double num, List<float> H, List<float> WI, List<int> ind, List<float> HW, string type)
        {
            List<float> temp = new List<float>();
            float fc = ((float)InputCutOffFrequency - (InputTransitionBand / 2))/InputFS;
            for (int i = 0; i <= num / 2; i++)
            {
                if (type == "rec")
                {
                    WI.Add(1);
                }
                else if (type == "han")
                {
                    WI.Add((float)(0.5 + (0.5 * Math.Cos((2 * Math.PI * i) / num))));
                }
                else if (type == "ham")
                {
                    WI.Add((float)(0.54 + (0.46 * Math.Cos((2 * Math.PI * i) / num))));
                }
                else
                {
                    WI.Add((float)(0.42 + (float)(0.5 * Math.Cos((2 * Math.PI * i) / (num - 1))) + (float)(0.08 * Math.Cos((4 * Math.PI * i) / (num - 1)))));
                }
                if (i == 0)
                {
                    H.Add(1 - (2 * fc));
                }
                else
                {
                    H.Add((float)((-2 * fc) * (Math.Sin(2 * Math.PI * fc * i)) / (2 * Math.PI * fc * i)));
                }
            }

            for (int j = 0; j < WI.Count; j++)
            {
                HW.Add((float)(WI[j] * H[j]));
                temp.Add((float)(WI[j] * H[j]));
            }

            HW.Reverse();
            for (int j = 1; j < temp.Count; j++)
            {
                HW.Add((float)(temp[j]));
            }
            for (int j = ((int)(-num)) / 2; j <= (num / 2); j++)
            {
                ind.Add(j);
            }
            OutputHn = new Signal(HW, ind, false);
            DirectConvolution conv = new DirectConvolution();
            conv.InputSignal1 = InputTimeDomainSignal;
            conv.InputSignal2 = OutputHn;
            conv.Run();
            OutputYn = conv.OutputConvolvedSignal;
        }
        public void band_pass(double num, List<float> H, List<float> WI, List<int> ind, List<float> HW, string type)
        {
            List<float> temp = new List<float>();
            float fc1 = ((float)InputF1 - (InputTransitionBand / 2))/InputFS;
            float fc2 = ((float)InputF2 + (InputTransitionBand / 2))/InputFS;
            for (int i = 0; i <= num / 2; i++)
            {
                if (type == "rec")
                {
                    WI.Add(1);
                }
                else if (type == "han")
                {
                    WI.Add((float)(0.5 + (0.5 * Math.Cos((2 * Math.PI * i) / num))));
                }
                else if (type == "ham")
                {
                    WI.Add((float)(0.54 + (0.46 * Math.Cos((2 * Math.PI * i) / num))));
                }

                else
                {
                    WI.Add((float)(0.42 + (float)(0.5 * Math.Cos((2 * Math.PI * i) / (num - 1))) + (float)(0.08 * Math.Cos((4 * Math.PI * i) / (num - 1)))));
                }
                if (i == 0)
                {
                    H.Add(2 * (fc2 - fc1));
                }
                else
                {
                    H.Add((float)(((2 * fc2) * (Math.Sin(2 * Math.PI * fc2 * i)) / (2 * Math.PI * fc2 * i)) - ((2 * fc1) * (Math.Sin(2 * Math.PI * fc1 * i)) / (2 * Math.PI * fc1 * i))));
                }
                
            }

            for (int j = 0; j < WI.Count; j++)
            {
                HW.Add((float)(WI[j] * H[j]));
                temp.Add((float)(WI[j] * H[j]));
            }

            HW.Reverse();
            for (int j = 1; j < temp.Count; j++)
            {
                HW.Add((float)(temp[j]));
            }
            for (int j = ((int)(-num)) / 2; j <= (num / 2); j++)
            {
                ind.Add(j);
            }
            OutputHn = new Signal(HW, ind, false);
            DirectConvolution conv = new DirectConvolution();
            conv.InputSignal1 = InputTimeDomainSignal;
            conv.InputSignal2 = OutputHn;
            conv.Run();
            OutputYn = conv.OutputConvolvedSignal;
            
        }

        public void band_stop(double num, List<float> H, List<float> WI, List<int> ind, List<float> HW, string type)
        {
            List<float> temp = new List<float>();
            float fc1 = ((float)InputF1 + (InputTransitionBand / 2))/ InputFS;
            float fc2 = ((float)InputF2 - (InputTransitionBand / 2))/ InputFS;

            for (int i = 0; i <= num / 2; i++)
            {
                if (type == "rec")
                {
                    WI.Add(1);
                }
                else if (type == "han")
                {
                    WI.Add((float)(0.5 + (0.5 * Math.Cos((2 * Math.PI * i) / num))));
                }
                else if (type == "ham")
                {
                    WI.Add((float)(0.42 + (float)(0.5 * Math.Cos((2 * Math.PI * i) / (num - 1))) + (float)(0.08 * Math.Cos((4 * Math.PI * i) / (num - 1)))));
                }
                else
                {
                    WI.Add((float)(0.42 + (float)(0.5 * Math.Cos((2 * Math.PI * i) / (num - 1))) + (float)(0.08 * Math.Cos((4 * Math.PI * i) / (num - 1)))));
                }

                if (i == 0)
                {
                    H.Add(1 - (2 * (fc2 - fc1)));
                }
                else
                {
                    H.Add((float)(((2 * fc1) * (Math.Sin(2 * Math.PI * fc1 * i)) / (2 * Math.PI * fc1 * i)) - ((2 * fc2) * (Math.Sin(2 * Math.PI * fc2 * i)) / (2 * Math.PI * fc2 * i))));
                }
               
            }
            for (int j = 0; j < WI.Count; j++)
            {
                HW.Add((float)(WI[j] * H[j]));
                temp.Add((float)(WI[j] * H[j]));
            }

            HW.Reverse();
            for (int j = 1; j < temp.Count; j++)
            {
                HW.Add((float)(temp[j]));
            }
            for (int j = ((int)(-num)) / 2; j <= (num / 2); j++)
            {
                ind.Add(j);
            }
            OutputHn = new Signal(HW, ind, false);
            DirectConvolution conv = new DirectConvolution();
            conv.InputSignal1 = InputTimeDomainSignal;
            conv.InputSignal2 = OutputHn;
            conv.Run();
            OutputYn = conv.OutputConvolvedSignal;
        }
        public override void Run()
        {
            double num = 0;
            List<float> H = new List<float>();
            List<float> WI = new List<float>();
            List<int> ind = new List<int>();
            List<float> HW = new List<float>();
            string type = "";

            if (InputStopBandAttenuation < 22)
            {
                type = "rec";
                num = Math.Ceiling(0.9 * InputFS / InputTransitionBand); 
            }
            else if ((InputStopBandAttenuation > 21) && (InputStopBandAttenuation < 45))
            {
                type = "han";
                num = Math.Ceiling(3.1 * InputFS / InputTransitionBand);
            }
            else if ((InputStopBandAttenuation > 44) && (InputStopBandAttenuation < 54))
            {
                type = "ham";
                num = Math.Ceiling(3.3 * InputFS / InputTransitionBand);
            }
            else if ((InputStopBandAttenuation > 53) && (InputStopBandAttenuation < 75))
            {
                type = "black";
                num = Math.Ceiling(5.5 * InputFS / InputTransitionBand);
                
            }
            if (num % 2 == 0)
            {
                num++;
            }
  
            if (InputFilterType == FILTER_TYPES.LOW)
            {
                low_filter(num, H, WI, ind, HW, type);
            }
            else if (InputFilterType == FILTER_TYPES.HIGH)
            {
                high_filter(num, H, WI, ind, HW, type);
            }
            else if (InputFilterType == FILTER_TYPES.BAND_PASS)
            {
                band_pass(num, H, WI, ind, HW, type);
            }
            else
            {
                band_stop(num, H, WI, ind, HW, type);
            }
        }
    }
}