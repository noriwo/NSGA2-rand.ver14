using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ALPwithNSGA2
{
	class OrderPopulation
	{
		Aircrafts air;
		Field field = new Field2();
		OrderConfig config;
		OrderIndividual[] orpop;
        List<int> savepath = new List<int>();

        public List<int> Savepath
        {
            get { return savepath; }
            set { savepath = value; }
        }
		public OrderIndividual[] Orpop
		{
			get { return orpop; }
			set { orpop = value; }
		}

        public void SubpathIni(int dcount,double d, Aircrafts air)
        {
            this.air = air;
            for (int i = 0; i < dcount; i++)
            {

                for (int j = 0; j < air.air[i].Path.Count(); j++)
                {
                    for (int k = 0; k < air.air[i].Path[j].subgene.Count(); k++)
                    {
                        air.air[i].Path[j].subgene[k].Distance = air.air[i].Path[j].subgene[k].Distance - d;
                    }

                }

            }


        }
        public void BeforePath(int Aircount)
        {
            for (int i = 0; i < Aircount; i++)
            {
                this.savepath.Add( (this.orpop[0].Gene[i]));
            }
            
        }
        OrderIndividual Selector()
		{
			int select1 = config.random.Next( 10 ) + 10;
			int select2 = config.random.Next( 10 ) + 10;
			if( orpop[select1].Fitness < orpop[select2].Fitness )
			{
				return orpop[select1];
			}
			else
			{
				return orpop[select2];
			}
		}
		public OrderPopulation( Aircrafts air, OrderConfig config )
		{
			this.config = config;
			this.air = air;
			orpop = new OrderIndividual[1000];
			for( int i = 0; i < 1000; i++ )
			{
				orpop[i] = new OrderIndividual( config, air );

			}
		}
		public void Crossover( Aircrafts air )
		{
			this.air = air;

			for( int i = 0; i < 500; i++ )
			{
				OrderIndividual parent1 = Selector();
				OrderIndividual parent2 = Selector();
				OrderIndividual child1 = new OrderIndividual( parent1 );
				OrderIndividual child2 = new OrderIndividual( parent2 );
                for (int j = 0; j < config.random.Next(Config.First - 1) + 1; j++)
				{
					int k = child1.Gene[j];
					child1.Gene[j] = child2.Gene[j];
					child2.Gene[j] = k;
				}
				child1.mutation( air );
				child2.mutation( air );
				child1.odfcalc( config, air );
				child2.odfcalc( config, air );
				if( child1.Fitness < child2.Fitness )
				{
					orpop[i] = child1;
				}
				else
				{
					orpop[i] = child2;
				}
			}
			orpop = orpop.OrderBy( n => n.Fitness ).ToArray();
		}
		public void Crossover( int x, Aircrafts air,OrderPopulation Orpop )
		{
			this.air = air;

			for( int i = 0; i < 500; i++ )
			{
				OrderIndividual parent1 = Selector();
				OrderIndividual parent2 = Selector();
				OrderIndividual child1 = new OrderIndividual( parent1 );
				OrderIndividual child2 = new OrderIndividual( parent2 );
                for (int j = 1; j < config.random.Next(Config.First + Config.Follow - 1) + 1; j++)
				{
					int k = child1.Gene[j];
					child1.Gene[j] = child2.Gene[j];
					child2.Gene[j] = k;
				}
                child1.mutation(air,Orpop);
				child2.mutation(air,Orpop);
              //  child1.Gene[0] = 10;//geneは10固定
             //   child2.Gene[0] = 10;//geneは10固定
				child1.odfcalc( config, x,air,Orpop );
				child2.odfcalc( config,x, air,Orpop );
				if( child1.Fitness < child2.Fitness )
				{
					orpop[i] = child1;
				}
				else
				{
					orpop[i] = child2;
				}
			}
			orpop = orpop.OrderBy( n => n.Fitness ).ToArray();
		}
		public void print()
		{
			Console.WriteLine( "結果" );
			for( int i = 0; i < 20; i++ )
			{

				orpop[i].print();
			}
		}
		public void WritePop( Aircrafts air )
		{
			this.air = air;
			DateTime dt;
			dt = DateTime.Now;

			string fileName = "Order." + dt.ToString( "MMdd_hhmmss" ) + ".csv";

			using( StreamWriter sw = new StreamWriter( fileName ) )
			{
				for( int i = 0; i < this.orpop.Length; i++ )
				{
					sw.WriteLine( "pop" + i + ":" );

					for( int j = 0; j < air.air.Count; j++ )
					{
						sw.WriteLine( "gene[" + j + "]:," + this.orpop[i].Gene[j] + ",distance," + orpop[i].Distances[j]+ ",ID," + air.FDistance(j, this.orpop[i].Gene[j])  );
					}
					
					sw.WriteLine( "Fitness:," + this.orpop[i].Fitness );

				}
			}
		}
        public void WritePop(Aircrafts air,int follow)
        {
            this.air = air;
            DateTime dt;
            dt = DateTime.Now;

            string fileName = "Order." + dt.ToString("MMdd_hhmmss") + ".csv";

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                for (int i = 0; i < this.orpop.Length; i++)
                {
                    sw.WriteLine("pop" + i + ":");

                    for (int j = 0; j <Config.First ; j++)
                    {
                        sw.WriteLine("gene[" + j + "]:," + this.orpop[i].Gene[j] + ",distance," + orpop[i].Distances[j]);
                    }
                    for (int j = Config.First; j < Config.First + Config.Follow; j++)
                    {
                        sw.WriteLine("gene[" + j + "]:," + this.orpop[i].Gene[j] + ",distance," + orpop[i].Distances[j] + ",ID," + air.FDistance(j, this.orpop[i].Gene[j]) );
                    }
                    sw.WriteLine("Fitness:," + this.orpop[i].Fitness);

                }
            }
        }
		public double Searchdis()//経路の最小を求める
		{
			return Orpop[0].Distances.Min();
		}
		public int SearchIndex( Aircrafts air )
		{
			this.air = air;
			Orpop[0].odfcalc( config, air );
			int x = 0;
			double d = Orpop[0].Distancestrace[0];
			for( int i = 0; i < 3; i++ )
			{
				if( d > Orpop[0].Distancestrace[i + 1] )
				{
					d = Orpop[0].Distancestrace[i + 1];
					x = i + 1;
				}

			}
			return x;

		}// ゴールした航空機のIndexを返す
		public void SearchBest( int x )//xは入れ替えた航空機を指している
		{

			double[] fitcase = new double[20];
			for( int i = 0; i < 20; i++ )
			{
				Orpop[0].Gene[x] = i;
				Orpop[0].odfcalc( config, air );
				fitcase[i] = Orpop[0].Fitness;
			}
			double d = fitcase[0];//初期値の設定
			int best = 0;　　　　　　//最初は0に設定しておく　　　
			for( int i = 1; i < 20; i++ )
			{
				if( d > fitcase[i] )
				{
					best = i;//ベストの更新
					d = fitcase[i];
				}
			}
			Orpop[0].Gene[x] = best;
			Orpop[0].odfcalc( config, air );
		}
	}
}
