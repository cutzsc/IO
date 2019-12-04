using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KernelDeeps.IO
{
    public abstract class BDReader
    {
		public readonly string inputsFile;
		public readonly string outputsFile;

		public int Position { get; protected set; }

		public BDReader(string inputsFile, string outputsFile)
		{
			this.inputsFile = inputsFile;
			this.outputsFile = outputsFile;
		}

		public abstract Sample[] ReadNext(int count);

		protected static int NextInt32(BinaryReader reader)
		{
			byte[] buffer = reader.ReadBytes(4);
			if (BitConverter.IsLittleEndian)
				Array.Reverse(buffer);
			return BitConverter.ToInt32(buffer, 0);
		}
	}
}
