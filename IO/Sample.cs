using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KernelDeeps.IO
{
	public class Sample
	{
		public readonly float[] inputs;
		public readonly float[] outputs;

		public readonly string input;
		public readonly string output;

		public Sample(float[] inputs, float[] outputs)
			: this(inputs, outputs, inputs.ToString(), outputs.ToString()) { }

		public Sample(float[] inputs, float[] outputs, string input, string output)
		{
			this.inputs = inputs;
			this.outputs = outputs;
			this.input = input;
			this.output = output;
		}
	}
}
