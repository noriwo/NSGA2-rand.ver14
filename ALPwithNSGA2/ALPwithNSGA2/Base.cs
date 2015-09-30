using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALPwithNSGA2
{
	class Base
	{
		Config config;
		public int basex;
		public int basey;
		public int Basex
		{
			get { return basex; }
			set { basex = value; }
		}
		public int Basey
		{
			get { return basey; }
			set { basey = value; }
		}

		public Base( Config config )
		{
			this.config = config;
			basex = new int();
			basex = config.rand.Next( 25 );
			basey = new int();
			basey = config.rand.Next( 25 );
		}
		public Base( Base bas )
		{
			basex = bas.basex;
			basey = bas.basey;
		}
	}
}
