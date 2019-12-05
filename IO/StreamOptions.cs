using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KernelDeeps.IO
{
	public struct StreamOptions
	{
		public int count;
		public bool shuffle;

		public StreamOptions(int count, bool shuffle)
		{
			this.count = count;
			this.shuffle = shuffle;
		}
	}
}
