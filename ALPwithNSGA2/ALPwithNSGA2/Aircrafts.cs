using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ALPwithNSGA2
{
	class Aircrafts
	{
		Field field = new Field2();
		Field nextfid = new Field1();
		Config config;
		double s;
		double t;
		int idcount = 0;
		public List<Population> air = new List<Population>();
		public int[] count = new int[Config.trial];
		public List<Population> Air
		{
			get { return air; }
			set { air = value; }
		}
		private Base[] initial = new Base[20];
		public Base[] Initial
		{
			get { return initial; }
			set { initial = value; }
		}
		public void Process( Config config )
		{
			this.config = config;
			for( int i = 0; i < Config.First; i++ )
			{
                air[i].checkRelation();
                air[i].checkFront();
                air[i].checkCrowdingDistance();
                air[i].sortOrderByFrontThenByCD();
				for( int j = 0; j < Config.trial; j++ )
				{
					air[i].crossOver( Initial[i].basex, Initial[i].basey,config );
                    air[i].Mutation(Initial[i].basex, Initial[i].basey, config,field);
					air[i].checkRelation();
					air[i].checkFront();
					air[i].checkCrowdingDistance();
                    air[i].sortOrderByFrontThenByCD();
                    if (j % 1000 == 0)
                    {
                        air[i].WritePop(j);
                        Console.WriteLine("now {0} generation", j);
                    }
                    if (j % 50 == 0)
                    {
                        //air[i].WritePop( z );
                        air[i].SelectPath();
                        //WritePath( x );
                    }
                    if (j  == Config.trial)
                    {
                        //air[i].WritePop( z );
                        air[i].SelectPath();
                        //WritePath( x );
                    }   
         
				}
				air[i].crossOver( Initial[i].basex, Initial[i].basey,config );
                air[i].Mutation(Initial[i].basex, Initial[i].basey, config, field);
                air[i].checkRelation();
				air[i].checkFront();
				air[i].checkCrowdingDistance();
                air[i].popn.OrderBy(n => n.Sunev);
				air[i].deleteSameIndividual( Initial[i].basex, Initial[i].basey, field );
				air[i].WritePop( 1000 );
				air[i].SelectPath();

				
			}
            for (int i = 0; i < Config.First; i++)//コピーコンストラクタを作成する必要アリ
			{
				Console.WriteLine( air[i].Inix + "." + air[i].Iniy );
				//Writetrace(i);
				WritePath( i );

			}
		}
		public double FDistance( int a, int b )
		{
			double x;
			x = Air[a].Path[b].Distance;
			return x;
		}
		public double FitnessReturn( int a, int b )
		{
			double y;
			y = Air[a].Path[b].Fitness;
			return y;
		}
		public Aircrafts( Config config )//コンストラクタ
		{

			this.config = config;
			StartIni( config );
			for( int i = 0; i < Config.First; i++ )
			{
                
                    Config.Inix = Initial[i].basex;//スタート地点の初期化
                    Config.Iniy = Initial[i].basey;
                    
                
                

			}
            for (int i = 0; i < Config.First; i++)
			{
				air.Add( new Population( Initial[i].basex, Initial[i].basey, field, config, idcount ) );
				idcount = idcount + 1;
				IniSet( i, Initial[i].basex, Initial[i].basey );
				//air[i].crossOver( Initial[i].basex, Initial[i].basey );
				//air[i].Mutation( Initial[i].basex, Initial[i].basey );
				// air[i].count();
				//air[i].best[0] = air[i].popn[0].Distance;
				air[i].PathIni();
			}
            WriteIni(air.Count()); 
		}
		public void StartIni( Config config )
		{
			this.config = config;
			for( int i = 0; i < Initial.Length; i++ )
			{
				Initial[i] = new Base( config );
			}
            for (int i = 0; i < Config.First; i++)
            {
                
				if( i == 0 )
				{
					Initial[i].basex = config.rand.Next( 3 ) ;
					Initial[i].basey = config.rand.Next( 3 ) ;
					bool j = false;
					while( j == true )
					{

						if( field.f1( Initial[0].Basex, Initial[0].Basey ) == 0 && field.f2( Initial[0].Basex, Initial[0].Basey ) == 0 && field.f3( Initial[0].Basex, Initial[0].Basey ) == 0 && field.f4( Initial[0].Basex, Initial[0].Basey ) == 0 )
						{
							j = true;
						}
						else
						{
							Initial[i].basex = config.rand.Next( 3 ) ;
							Initial[i].basey = config.rand.Next( 3 ) ;
						}
					}
				}
				else
                {
                    while (field.f1(Initial[i].basex, Config.Iniy = Initial[i].basey) == 0 & field.f2(Initial[i].basex, Config.Iniy = Initial[i].basey) == 0 && field.f3(Initial[i].basex, Config.Iniy = Initial[i].basey) == 0)
                    {
                        int k = 0;
                        Initial[i].basex = config.rand.Next(5);
                        Initial[i].basey = config.rand.Next(5);
                        while (k == i)
                        {
                            if (Initial[i].basex != Initial[k].basex && Initial[i].basey != Initial[k].basey)
                            {
                                k++;
                            }
                            else
                            {
                                Initial[i].basex = config.rand.Next(5);
                                Initial[i].basey = config.rand.Next(5);
                                k = 1;
                            }
                        }
                    }
                }
            }
		}
		public void IniSet( int i, int x, int y )
		{
			Air[i].Inix = x;
			Air[i].Iniy = y;
		}
		public void StartReIni( double d, int x, int i ) //aircraftの初期位置をセットする
		{
			int g;
			g = ( int )Math.Round( d ) - 1;
			if( air[i].Path[x].Trace.Count < g )
			{
				Air[i].Inix = Config.XGoal - 1;
				Air[i].Iniy = Config.YGoal - 1;
			}
			else
			{
				Air[i].Inix = ( int )Math.Round( air[i].Path[x].Trace[g].basex );
				Air[i].Iniy = ( int )Math.Round( air[i].Path[x].Trace[g].basey );

			}

		}

		public void PathReIni( double d )//pathも初期化の必要がありここで初期化するよん
		{
			int g;
			g = ( int )Math.Round( d ) - 1;
			for( int i = 0; i < air.Count; i++ )
			{
				for( int x = 0; x < air[i].path.Count(); x++ )
				{
					for( int k = 0; k < g; k++ )
					{
						if( air[i].Path[x].Trace.Count == 0 )
						{
							break;
						}
						else
						{
							air[i].Path[x].Trace.RemoveAt( 0 );//次回のために進んだ分だけ削除します

						}
					}
				}
			}
		}
		public void ReAirCraft( int rest, Config config )//もらったIndexの航空機を際スケジューリングする 引数は残っている航空機の数
		{
			this.config = config;
	
			int x = air.Count();
			int nair = Config.Follow;//次のスケジューリングを決める航空機の数
			Console.WriteLine( "next aircraft is" + nair );
            for (int i = (Config.First - 1); i < (Config.First+Config.Follow); i++)
			{
				Initial[i].Basex = config.rand.Next( 3 );
				Initial[i].Basey = config.rand.Next( 3 );
				air.Add( new Population( Initial[i].Basex, Initial[i].Basey, nextfid, config, idcount ) );
				IniSet( i, Initial[i].Basex, Initial[i].Basey );

				idcount = idcount + 1;
			}
            for (int i = (Config.First ); i < (Config.First + Config.Follow); i++)
			{

				air[i].PathIni();
				for( int z = 0; z < Config.trial; z++ )
				{
                    air[i].crossOver(Initial[i].basex, Initial[i].basey,config);
                    air[i].Mutation(Initial[i].basex, Initial[i].basey, config,field);
                    air[i].checkRelation();
                    air[i].checkFront();
                    air[i].checkCrowdingDistance();
                    air[i].sortOrderByFrontThenByCD();
					if( z % 50 == 0 )
					{
						//air[i].WritePop( z );
						air[i].SelectPath();
						//WritePath( x );
                        
					}
                    if (z % 1000 == 0)
                    {
                        air[i].WritePop(z);
                        Console.WriteLine("now {0} generation", z);
                    }
					if( z == ( Config.trial - 1 ) )
					{
						//air[i].WritePop( z );
					}
				}
                air[i].crossOver(Initial[i].basex, Initial[i].basey,config);
                air[i].Mutation(Initial[i].basex, Initial[i].basey, config,field);
                air[i].checkRelation();
                air[i].checkFront();
                air[i].checkCrowdingDistance();
                air[i].sortOrderByFrontThenByCD();
                air[i].deleteSameIndividual(Initial[i].basex, Initial[i].basey, field);
                air[i].WritePop(1000);
                air[i].SelectPath();
				
			}

			for( int i = 0; i < air.Count(); i++ )//コピーコンストラクタを作成する必要アリ
			{
				Console.WriteLine( air[i].Inix + "." + air[i].Iniy + ",ID," + air[i].Id );
			}
            WriteIni(air.Count()); 
		}
		public void Write( int j )
		{
			DateTime dt;
			dt = DateTime.Now;
			string fileName = dt.ToString( "MMdd_hhmm" ) + ".csv";

			using( StreamWriter sw = new StreamWriter( fileName ) )
			{


				for( int i = 0; i < Config.trial; i++ )
				{
					sw.WriteLine( "Distance," + air[j].best[i] + "," + count[i] );

				}

			}
		}
        public void WriteIni(int j)
        {
            DateTime dt;
            dt = DateTime.Now;
            string fileName = "Ini" + "." + dt.ToString("MMdd_hhmm") + ".csv";
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                for (int i = 0; i < j; i++)
                {
                    sw.WriteLine("X," + Initial[i].Basex + "," + "Y," + Initial[i].Basey);
                    sw.WriteLine("setX," + air[i].Inix + "," + "setY," + air[i].Iniy);

                }

            }
        }
		public void Writetrace( int j )
		{
			DateTime dt;
			dt = DateTime.Now;
			string fileName = "TraceNO" + j + "." + dt.ToString( "MMdd_hhmm" ) + ".csv";

			using( StreamWriter sw = new StreamWriter( fileName ) )
			{


				for( int i = 0; i < air[j].popn[0].Trace.Count; i++ )
				{
					sw.WriteLine( "Xgrid," + air[j].popn[0].Trace[i].basex + "," + "Ygrid," + air[j].popn[0].Trace[i].basey );

				}

			}
		}
		public void WriteOneTrace( int j, int l )
		{
			DateTime dt;
			dt = DateTime.Now;
			string fileName = "TraceNO" + j + "." + dt.ToString( "MMdd_hhmm" ) + ".csv";

			using( StreamWriter sw = new StreamWriter( fileName ) )
			{


				for( int i = 0; i < air[j].Path[l].Trace.Count; i++ )
				{
					sw.WriteLine( "Xgrid," + air[j].Path[l].Trace[i].basex + "," + "Ygrid," + air[j].Path[l].Trace[i].basey );

				}

			}
		}
		public void WritePath( int j )
		{
			DateTime dt;
			dt = DateTime.Now;
			string fileName = "Path.NO" + ( j + 1 ) + "." + dt.ToString( "MMdd_hhmm" ) + ".csv";

			using( StreamWriter sw = new StreamWriter( fileName ) )
			{
				for( int i = 0; i < air[j].Path.Count(); i++ )
				{
					for( int k = 0; k < air[j].Path[i].Gene.Count; k++ )
					{
                        sw.WriteLine("Path[" + i + "]Xgrid" + "," + air[j].Path[i].gene[k].basex + "," + "Ygrid," + air[j].Path[i].gene[k].basey + "," + "Distance" + "," + air[j].Path[i].Distance + "," + "Fdis" + "," + air[j].Path[i].Fdis + "," + "Fitness" + "," + air[j].Path[i].Fitness + ",subcount," + air[j].Path[i].subpathct + ",Sunev," + air[j].Path[i].Sunev);
                        
					}
                    for (int m = 0; m < air[j].Path[i].subgene.Count(); m++)
                    {
                        for (int n = 0; n < air[j].Path[i].subgene[m].gene.Count; n++)
                        {
                            sw.Write("sub[" + n + "]Xgrid" + "," + air[j].Path[i].subgene[m].gene[n].basex + "," + "Ygrid," + air[j].Path[i].subgene[m].gene[n].basey + "," + air[j].Path[i].subgene[m].Distance + "," + "indicator" + "," + air[j].Path[i].subgene[m].Indicatorct + ",");
                        }
                        sw.WriteLine();
                    }
				}
			}
		}
		public void WriteOnePath( int x, int y )
		{
			DateTime dt;
			dt = DateTime.Now;
			string fileName = "Path.NO" + ( x + 1 ) + "." + dt.ToString( "MMdd_hhmm" ) + ".csv";
			using( StreamWriter sw = new StreamWriter( fileName ) )
			{

				for( int k = 0; k < air[x].Path[y].Gene.Count; k++ )
				{
					sw.WriteLine( "Path[" + x + "]Xgrid" + "," + air[x].Path[y].gene[k].basex + "," + "Ygrid," + air[x].Path[y].gene[k].basey + "," + "Distance" + "," + air[x].Path[y].Distance + "," + "Fdis" + "," + air[x].Path[y].Fdis + "," + "Fitness" + "," + air[x].Path[y].Fitness );

				}
			}
		}
		public void ReProcess( int d, Config config )//今現在残っている航空機の数を引数にする
		{
			this.config = config;
			ReAirCraft( d, config );
		}

	}
}
