using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALPwithNSGA2
{
	class Config
	{
		public static int trial =1000;
        private static int subpath = 20;
        private static int follow = 0;
        private static int first = 1;
        int nearct = 3;
        int indct = 5;

        public int Indct
        {
            get { return indct; }
            set { indct = value; }
        }
        public int Nearct
        {
            get { return nearct; }
            set { nearct = value; }
        }
        public static int First
        {
            get { return Config.first; }
            set { Config.first = value; }
        }
        public static int Follow
        {
            get { return Config.follow; }
            set { Config.follow = value; }
        }
        public static int Subpath
        {
            get { return Config.subpath; }
            set { Config.subpath = value; }
        }
		public static int Trial
		{
			get { return trial; }
			set { trial = value; }
		}

		private static int populationsize = 100;//母体数の2倍
		public Random rand;
		private static int inix = 0;
		public static int Inix
		{
			get { return Config.inix; }
			set { Config.inix = value; }
		}
		private static int iniy = 0;
		public static int Iniy
		{
			get { return Config.iniy; }
			set { Config.iniy = value; }
		}
		public static int Populationsize
		{
			get { return Config.populationsize; }
			set { Config.populationsize = value; }
		}
		private static int xgrid = 25;
		public static int Xgrid
		{
			get { return Config.xgrid; }
			set { Config.xgrid = value; }
		}
		private static int ygrid = 25;
		public static int Ygrid1
		{
			get { return Config.ygrid; }
			set { Config.ygrid = value; }
		}
		private static int xGoal = 24;
		public static int XGoal
		{
			get { return Config.xGoal; }
			set { Config.xGoal = value; }
		}
		private static int yGoal = 24;
		public static int YGoal
		{
			get { return Config.yGoal; }
			set { Config.yGoal = value; }
		}
		private static int nextx = 0;
		public static int Nextx
		{
			get { return Config.nextx; }
			set { Config.nextx = value; }
		}
		private static int nexty = 0;
		public static int Nexty
		{
			get { return Config.nexty; }
			set { Config.nexty = value; }
		}
        private static int time = 3;

		public static int Time
		{
			get { return Config.time; }
			set { Config.time = value; }
		}
		private static int rest;
		private static int generation = 5000;

		public int Generation
		{
			get { return generation; }
		}

		public static int Rest
		{
			get { return Config.rest; }
			set { Config.rest = value; }
		}
		public readonly int Infinity = 1000;
		public Config( int seed )
		{
			rand = new Random( seed );

		}
      
	}
}
