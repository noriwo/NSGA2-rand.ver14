using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALPwithNSGA2
{
	class Trace
	{
		List<TrBase> trace;


		public Trace()//Traceのコンストラクタ
		{
			trace = new List<TrBase>();

		}

		public TrBase this[int i]//Traceのインデクサ
		{
			get
			{
				return trace[i];
			}

			set
			{
				trace[i] = value;
			}

		}

	}
}
