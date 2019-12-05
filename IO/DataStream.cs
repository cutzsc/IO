using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KernelDeeps.IO
{
    public abstract class DataStream
    {
		public readonly string inputsFile;
		public readonly string outputsFile;

		public int Position { get; protected set; }

		public abstract Sample[] ReadNext(StreamOptions options);

		protected int NextInt32(BinaryReader reader)
		{
			byte[] buffer = reader.ReadBytes(4);
			if (BitConverter.IsLittleEndian)
				Array.Reverse(buffer);
			return BitConverter.ToInt32(buffer, 0);
		}

		protected void Shuffle(Sample[] samples)
		{
			Random rand = new Random(samples.Length + DateTime.UtcNow.Millisecond);
			for (int i = samples.Length - 1; i > 0; i--)
			{
				Swap(samples, i, rand.Next(0, i));
			}
		}

		protected void Swap(Sample[] samples, int i, int j)
		{
			Sample temp = samples[i];
			samples[i] = samples[j];
			samples[j] = temp;
		}
	}
}
