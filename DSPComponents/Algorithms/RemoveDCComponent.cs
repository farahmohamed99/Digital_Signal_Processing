using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class RemoveDCComponent : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            OutputSignal = InputSignal;
            FastFourierTransform fft = new FastFourierTransform();
            fft.InputTimeDomainSignal = InputSignal;
            fft.Run();
            fft.OutputFreqDomainSignal.FrequenciesAmplitudes[0] = 0;
            fft.OutputFreqDomainSignal.FrequenciesAmplitudes[0] = 0;
            InverseFastFourierTransform ifft = new InverseFastFourierTransform();
            ifft.InputFreqDomainSignal = fft.OutputFreqDomainSignal;
            ifft.Run();
            OutputSignal = ifft.OutputTimeDomainSignal;            
        }
    }
}
