using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALPwithNSGA2
{
   public class Disid
    {
       public double s;

        public double S
        {
            get { return s; }
            set { s = value; }
        }
        public int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public Disid()
        {
            s=0;
           id=9999;
        }
    }
}
