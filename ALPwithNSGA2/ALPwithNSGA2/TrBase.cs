using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALPwithNSGA2
{
	class TrBase
	{
		public double basex;
		public double basey;
		public double Basex
		{
			get { return basex; }
			set { basex = value; }
		}
		public double Basey
		{
			get { return basey; }
			set { basey = value; }
		}
		public TrBase()
		{
			Basex = 0;
			Basey = 0;
		}
		public TrBase( TrBase tbas )
		{
			double basex = new double();
			double basey = new double();
			basex = tbas.basex;
			basey = tbas.basey;

		}

	}
}
