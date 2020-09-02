using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using DSPUI.Utilities;

namespace DSPUI.Components
{
    public class AccumulatorAttributes : ComponentAttributes
    {
        public Signal outputSignal { get; set; }
    }

    public class Accumulator : Component
    {

        public DSPAlgorithms.Algorithms.Accumulator mySolver { get; set; }
        private AccumulatorAttributes myData { get { return (AccumulatorAttributes)Data; } set { Data = value; } }

        public Accumulator()
        {
            this.InputComponents = new List<Component>();
            this.OutputComponents = new List<Component>();
            mySolver = new DSPAlgorithms.Algorithms.Accumulator();
            this.Data = new AccumulatorAttributes();
        }

        public override bool IsValidInputComponent(Component c)
        {
            return InputComponents.Count == 0 && ComponentUtility.ComponentAttributesHasSignal(c);
        }

        public override bool ComponentReady()
        {
            ErrorMessage = "";

            if (InputComponents.Count != 1)
            {
                ErrorMessage = "Need 1 Input Connection.";
                return false;
            }

            return true;
        }

        public override void StartJob()
        {
            // Construct Input for the algorithm 
            mySolver.InputSignal = ComponentUtility.GetSignalFromComponentAttributes(InputComponents[0].Data);

            // Run algorithm
            mySolver.Run();

            // Construct Component Data from algorithm Output
            myData.outputSignal = mySolver.OutputSignal;

            NotifyAllComponentOutputReady();
        }

        public override void ShowComponentsAttributesDialogue()
        {
        }

        public override string ToString()
        {
            return "Accumulator";
        }
    }
}