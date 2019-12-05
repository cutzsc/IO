using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KernelDeeps.IO
{
	public class SimpleDataStream : DataStream
	{
		Sample[] samples;

		public SimpleDataStream(float[][][] data)
		{
			samples = new Sample[data.Length];
			for (int i = 0; i < data.Length; i++)
			{
				samples[i] = new Sample(data[i][0], data[i][1]);
			}
		}

		public override Sample[] ReadNext(StreamOptions options)
		{
			options.count = options.count < 1 ? 1 : options.count;
			Sample[] samples = new Sample[options.count];

			for (int i = 0; i < options.count; i++)
			{
				if (Position == this.samples.Length)
				{
					Position = 0;
				}

				samples[i] = this.samples[Position];

				Position++;
			}

			if (options.shuffle)
				Shuffle(samples);
			return samples;
		}
	}
}
