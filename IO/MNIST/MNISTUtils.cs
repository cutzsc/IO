using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KernelDeeps.IO.MNIST
{
	public static class MNISTUtils
	{
		public static Bitmap ToBitmap(int width, int height, float[] pixels)
		{
			Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
			for (int y = 0, px = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++, px++)
				{
					byte pixel = 255;
					if (pixels[px] != 0)
						pixel = (byte)(byte.MaxValue - Convert.ToByte(pixels[px] * 255));
					Color c = Color.FromArgb(pixel, pixel, pixel);
					bmp.SetPixel(x, y, c);
				}
			}
			return bmp;
		}
	}
}
