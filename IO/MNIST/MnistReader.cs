using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KernelDeeps.IO.MNIST
{
	public class MNISTReader : DataStream
	{
		public int Width { get; private set; }
		public int Height { get; private set; }
		public int ImageSize { get; private set; }
		public int SamplesCount { get; private set; }

		BinaryReader inputsReader;
		BinaryReader outputsReader;

		public MNISTReader(string inputsFile, string outputsFile)
		{
			inputsReader = new BinaryReader(File.OpenRead(inputsFile));
			outputsReader = new BinaryReader(File.OpenRead(outputsFile));
			
			// magic
			NextInt32(inputsReader);
			NextInt32(outputsReader);

			SamplesCount = NextInt32(inputsReader);
			Width = NextInt32(inputsReader);
			Height = NextInt32(inputsReader);

			ImageSize = Width * Height;

			if (SamplesCount != NextInt32(outputsReader))
				throw new Exception("inputs size doesn't equals to outputs size");
		}

		~MNISTReader()
		{
			inputsReader.Close();
			inputsReader.Dispose();
			outputsReader.Close();
			outputsReader.Dispose();
		}

		public override Sample[] ReadNext(StreamOptions options = default)
		{
			options.count = options.count < 1 ? 1 : options.count;

			Sample[] samples = new Sample[options.count];
			byte[] buffer;

			for (int i = 0; i < options.count; i++)
			{
				if (Position == SamplesCount)
				{
					Position = 0;
					inputsReader.BaseStream.Position = 16;
					outputsReader.BaseStream.Position = 8;
				}

				// read inputs and normalize
				buffer = new byte[ImageSize];
				inputsReader.Read(buffer, 0, ImageSize);
				float[] pixels = new float[ImageSize];
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

			if (options.shuffle)
				Shuffle(samples);

			return samples;
		}
	}
}
