using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KernelDeeps.IO.MNIST
{
	public class MNISTReader : BDReader
	{
		public MNISTReader(string inputsFile, string outputsFile)
			: base(inputsFile, outputsFile) { }

		public override Sample[] ReadNext(int count)
		{
			Sample[] samples = new Sample[count];

			BinaryReader inputsReader = new BinaryReader(File.OpenRead(inputsFile));
			BinaryReader outputsReader = new BinaryReader(File.OpenRead(outputsFile));

			// magic
			NextInt32(inputsReader);
			NextInt32(outputsReader);

			int size = NextInt32(inputsReader);
			int rows = NextInt32(inputsReader);
			int cols = NextInt32(inputsReader);

			if (size != NextInt32(outputsReader))
				throw new Exception("inputs size doesn't equals to outputs size");

			int imageSize = rows * cols;
			byte[] buffer;
			inputsReader.BaseStream.Position = Position + 16;
			outputsReader.BaseStream.Position = Position + 8;
			for (int i = 0; i < count; i++)
			{
				if (Position == size)
				{
					Position = 0;
					inputsReader.BaseStream.Position = 16;
					outputsReader.BaseStream.Position = 8;
				}

				// read inputs and normalize
				buffer = new byte[imageSize];
				inputsReader.Read(buffer, 0, imageSize);
				float[] pixels = new float[imageSize];
				for (int px = 0; px < pixels.Length; px++)
				{
					pixels[px] = buffer[px] / 255.0f;
				}

				// read outputs and normalize
				buffer = new byte[1];
				outputsReader.Read(buffer, 0, 1);
				float[] labels = new float[10];
				labels[buffer[0]] = 1.0f;

				samples[i] = new Sample(pixels, labels, "pixels", $"{buffer[0]}");

				Position++;
			}

			inputsReader.Close();
			inputsReader.Dispose();
			outputsReader.Close();
			outputsReader.Dispose();

			return samples;
		}
	}
}
