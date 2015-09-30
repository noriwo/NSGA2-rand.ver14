using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALPwithNSGA2
{
	class OrderConfig
	{
		public Random random;
		public OrderConfig( int seed )
		{
			random = new Random( seed );
		}
	}
}
