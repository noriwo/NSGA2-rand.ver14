using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALPwithNSGA2
{
	class OrderIndividual
	{
		double fitness;
		Aircrafts air;
		private double[] distances = new double[20];
		private double[] distancestrace = new double[20];
		double[] deltads = new double[20];

		public double[] Deltads
		{
			get { return deltads; }
			set { deltads = value; }
		}
		public double[] Distancestrace
		{
			get { return distancestrace; }
			set { distancestrace = value; }
		}
		public double[] Distances
		{
			get { return distances; }
			set { distances = value; }
		}
		public double Fitness
		{
			get { return fitness; }
			set { fitness = value; }
		}
		int[] gene;
		Random rand;
		public int[] Gene
		{
			get { return gene; }
			set { gene = value; }
		}

 
        public void mutation(Aircrafts air) {
            this.air = air;
            for (int i = 0; i < air.air.Count; i++) {
                if (rand.Next(10) == 1) {
                    if (air.air[i].Pathcount == 0) {
                        int x;
                        x = rand.Next(19);
                        Gene[i] = x; 
                    }
                    else {
                        int x;
                        if (air.air[i].Pathcount > 20) {
                            x = rand.Next(20);
                            Gene[i] = x;
                        }
                        else {
                            x = rand.Next(air.air[i].Pathcount - 1);
                            Gene[i] = x;
                        }
                    }
                }
            }
        }
        public void mutation(Aircrafts air,OrderPopulation orpop)
        {
            
            this.air = air;
            for (int i = 0; i < Config.First; i++)
            {
                if (rand.Next(10) == 1)
                {
                    
                        int x;
                        {
                            if (air.air[i].path[orpop.Savepath[i]].subpathct>0)
                            {
                                x = rand.Next((air.air[i].path[orpop.Savepath[i]].subpathct) - 1);      
                            }
                            else
                            {
                                x = 0;
                            }
                            Gene[i] = x;
                        }
                }
            }
           for (int i = Config.First; i < Config.First + Config.Follow; i++)
            {
                if (rand.Next(10) == 1)
                {
                    if (air.air[i].Pathcount == 0)
                    {
                        int x;
                        x = rand.Next(19);
                        Gene[i] = x;
                    }
                    else
                    {
                        int x;
                        if (air.air[i].Pathcount > 20)
                        {
                            x = rand.Next(20);
                            Gene[i] = x;
                        }
                        else
                        {
                            x = rand.Next(air.air[i].Pathcount - 1);
                            Gene[i] = x;
                        }
                    }
                }
            }
        }
		public void mutation( int y, Aircrafts air )
		{
			this.air = air;
            for (int i = y; i < Config.First + Config.Follow; i++)
			{
				if( rand.Next( 10 ) == 1 )
				{
					int x;
					x = rand.Next( 20 );
					Gene[i] = x;
				}
			}
		}
		public void print()
		{
			for( int i = 0; i < 4; i++ )
			{
				Console.Write( gene[i] + "." );
			}
			Console.WriteLine( "[Fitness]" + fitness );
		}
		public void odfcalc( OrderConfig config, Aircrafts air )
		{
			int x;
			fitness = 0;
			this.air = air;
			//for (int k = 0; k < 4; k++)
			//{
			//  int x = gene[k];
			//fitness = fitness + air.FDistance(k,x);
			// }
			double rmax = air.FDistance( 0, gene[0] );
			double rmin = air.FDistance( 0, gene[0] );
            for (int k = 0; k < air.Air.Count; k++)
            {
                x = gene[k];
                distances[k] = air.FDistance(k, x);
                Distancestrace[k] = distances[k];

                if (rmax < distances[k])
                {
                    rmax = distances[k];
                }
                if (rmin > distances[k])
                {
                    rmin = distances[k];
                }

                fitness = distances[k] + fitness;

            }
         //   fitness = rmax - rmin;
			/*
					 for (int i = 0; i < air.air.Count; i++)
					 {
						 x = gene[i];
						 if (air.Air[i].Path[x].Fitness >0)
						 {
							 fitness = fitness + 100;//コスト条件を満たさないものを落としてあげる
						 }
					 }
			 */
            distances = distances.OrderByDescending(n => n).ToArray();
            for (int t = 0; t < air.air.Count - 1; t++) {
                deltads[t] = distances[t] - distances[t + 1];


                if (deltads[t] < 1) {
                    fitness = fitness + 10000;
                }
            }
		}

		public void odfcalc( OrderConfig config, int y, Aircrafts air ,OrderPopulation Orpop)
		{
			int x;
			fitness = 0;
            double[] deltads = new double[20];
            x = gene[0];
            distances[0] = air.air[0].Path[Orpop.Savepath[0]].subgene[x].Distance;
            double rmax = distances[0];
            double rmin = distances[0];
            for (int k = 0; k < Config.First; k++)
            {
                x = gene[k];
                distances[k] = air.air[k].Path[Orpop.Savepath[k]].subgene[x].Distance;
                distances[k] = air.air[k].Path[Orpop.Savepath[k]].subgene[x].Fitness + distances[k];
                fitness = distances[k] + fitness;



                if (rmax < distances[k])
                {
                    rmax = distances[k];
                }
                if (rmin > distances[k])
                {
                    rmin = distances[k];
                }

                fitness = distances[k] + fitness;

            }
            for (int k = Config.First; k < Config.First + Config.Follow; k++)
			{
				x = gene[k];
				distances[k] = air.FDistance( k, x );
				Distancestrace[k] = distances[k];
                fitness = distances[k] + fitness;
                if (rmax < distances[k])
                {
                    rmax = distances[k];
                }
                if (rmin > distances[k])
                {
                    rmin = distances[k];
                }
			}
	
            distances = distances.OrderByDescending(n => n).ToArray();

            //fitness = rmax - rmin;
            for (int t = 0; t < Config.First+Config.Follow-1; t++)
            {
                deltads[t] = Math.Abs(distances[t] - distances[t + 1]);

        
                if (deltads[t] < 1)
                {
                    fitness = fitness + 10000;
                }
            }
			
           
 

		}
		public OrderIndividual( OrderIndividual ind )
		{
			gene = new int[ind.gene.Count()];
			rand = ind.rand;
			fitness = ind.fitness;

			for( int i = 0; i < ind.gene.Count(); i++ )
			{
				gene[i] = ind.gene[i];
				Distances[i] = ind.Distances[i];
			}
		}
        public OrderIndividual(OrderConfig config, Aircrafts air) {
            this.air = air;
            rand = config.random;
            gene = new int[20];
            for (int i = 0; i < air.air.Count; i++) {
                if (air.air[i].Pathcount == 0) {
                    gene[i] = rand.Next(19);
                }
                else {
                    int x;
                    if (air.air[i].Pathcount > 20) {
                        x = rand.Next(19);
                        Gene[i] = x;
                    }
                    else {
                        x = rand.Next(air.air[i].Pathcount - 1);
                        Gene[i] = x;
                    }
                }
            }
            odfcalc(config, air);
        }//コンストラクタ]
		public void OrderInterference( int i, int j, int x, int y )//waypointの干渉判定
		{
			bool b = false;

			if( air.Air[i].Inix < air.Air[j].Path[y].gene[0].basex && air.Air[j].Inix < air.Air[i].Path[x].gene[0].basex && air.Air[i].Iniy < air.Air[j].Path[y].gene[0].basey && air.Air[j].Iniy < air.Air[i].Path[x].gene[0].basey )
			{
				b = true;    //やったね干渉してるかもね！                 
			}
			for( int l = 0; l < air.Air[i].Path[x].Gene.Count() - 1; l++ )
			{
				for( int m = 0; m < air.Air[j].Path[x].Gene.Count() - 1; m++ )//geneの数を決めるfor文ダヨ
				{
					if( air.Air[i].Path[x].Gene[m].basex < air.Air[j].Path[y].gene[0].basex )
					{

					}
				}
			}
		}
	}
}
