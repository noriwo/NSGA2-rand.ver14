using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ALPwithNSGA2
{
	class Population
	{
		public Individual[] path = new Individual[20];
	
		private int pathcount = 0;

		public int Pathcount
		{
			get { return pathcount; }
			set { pathcount = value; }
		}

		public Individual[] Path
		{
			get { return path; }
			set { path = value; }
		}
		Config config;
		int id = 0;
		public int Id
		{
			get { return id; }
			set { id = value; }
		}
		public Individual[] popn;
		private int inix;

		public int Inix
		{
			get { return inix; }
			set { inix = value; }
		}
		private int iniy;

		public int Iniy
		{
			get { return iniy; }
			set { iniy = value; }
		}
	

		public double[] best = new double[Config.Trial];
		public Individual[] Popn
		{
			get { return popn; }
			set { popn = value; }
		}
		Field field;
		public Population( int x, int y, Field f,Config config,int id )//コンストラクタ
		{
			this.id = id;
			this.inix = x;
			this.iniy = y;
			this.config = config;
			field = f;
			popn = new Individual[Config.Populationsize];
			for( int i = 0; i < Config.Populationsize; i++ )
			{
				popn[i] = new Individual(x,y, config, f );
			}
			for( int m = 0; m < 20; m++ )
			{
				path[m] = new Individual( popn[0],config );

				Path[m] = path[m];

			}

		}
		/* Front昇順にソート */
		public void sortOrderByFront()
		{
			popn = popn.OrderBy( n => n.Front ).ToArray();
		}
		/* Front昇順にソートしたあと, 各Front内でcrowding distanceで降順にソート */
		public void sortOrderByFrontThenByCD()
		{
			popn = popn.OrderBy( n => n.Front ).ThenByDescending( n => n.CrowdingDistance ).ToArray();
		}
		/* 支配関係を調べる. */
		public void checkRelation()
		{
			for( int i = 0; i < Config.Populationsize; i++ )
			{
				this.popn[i].Front = 0;
				this.popn[i].Dominating.Clear();
				this.popn[i].IsDominated = 0;
				this.popn[i].CrowdingDistance = 0;
			}

			for( int i = 0; i < Config.Populationsize; i++ )
			{
				for( int j = 0; j < Config.Populationsize; j++ )
				{
					if( i != j )
					{
						switch( this.popn[i].CompareTo( this.popn[j] ) )
						{
							case 1:
							this.popn[i].Dominating.Add( j );
							break;
							case -1:
							this.popn[i].IsDominated++;
							break;
							case 0:
							break;
						}
					}
				}
			}
		}
		/* 各individualがどのFrontに属しているか調べる. */
        public void checkFront() {
            for (int i = 0; i < Config.Populationsize; i++) {
                this.popn[i].Front = 0;
            }

            int front = 1;
            int count = 0;

            while (count != Config.Populationsize) {
                for (int i = 0; i < Config.Populationsize; i++) {
                    if (this.popn[i].IsDominated <= 0 && this.popn[i].Front == 0) {
                        this.popn[i].Front = front;
                        count++;
                    }
                }

                for (int i = 0; i < Config.Populationsize; i++) {
                    if (this.popn[i].Front == front) {
                        for (int j = 0; j < this.popn[i].Dominating.Count; j++) {
                            this.popn[(int)this.popn[i].Dominating[j]].IsDominated--;
                        }
                    }
                }
                front++;
            }
        }
		/* crowding distanceを計算する. Profit1に関して計算したあとProfit2に関して計算する. 面倒. */
		public void checkCrowdingDistance()
		{
			/* crowding distanceを0で初期化. */
			for( int i = 0; i < Config.Populationsize; i++ )
			{
				this.popn[i].CrowdingDistance = 0.0;
			}

			/* Frontで昇順にソートしたあとにProfit1で降順にソート. */
			this.popn = this.popn.OrderBy( n => n.Front ).ThenByDescending( n => n.Fdis ).ToArray();

			int count = 0; //frontが同じ個体数を数える.
			int front = 1; //front番号

			for( int i = 0; i < Config.Populationsize; i++ )
			{
				if( popn[i].Front == front )
				{
					count++;
				}
				else//もし一つ前のものと違ったら.
				{
					if( count <= 2 ) //同じfrontの個体数が2以下の場合全部cd=infinityでいい.
					{
						for( int j = 0; j < count; j++ )
						{
							this.popn[i - count + j].CrowdingDistance = config.Infinity;
						}
					}
					else //同じfrontの個体数が3個以上
					{
						/* 端っこのProfitをmax, minとする. */
                        double max = this.popn[i - count].Fdis;
						double min = this.popn[i - 1].Fdis;

						/* 端っこはcd=infinity. */
						this.popn[i - count].CrowdingDistance = config.Infinity;
						this.popn[i - 1].CrowdingDistance = config.Infinity;

						for( int j = 1; j < count - 1; j++ )
						{
							/* 前後のProfitを調べる. */
							double right = this.popn[i - count + j - 1].Fdis;
							double left = this.popn[i - count + j + 1].Fdis;

							/* 端っこと前後のProfitを使ってcdを計算する. */
							this.popn[i - count + j].calCD( right, left, max, min,config );
						}
					}

					if( i > Config.Populationsize - 2 )
					{
						for( int j = i; j < Config.Populationsize; j++ )
						{
							this.popn[j].CrowdingDistance = config.Infinity;
						}
					}

					front = this.popn[i].Front;

					count = 1;
				}
			}


			/* これ以下は上の計算をProfit2に対しておなっているだけなので, 詳しくは上を参照 */


			/* Frontで昇順にソートしたあとにProfit2で降順にソート. */
			this.popn = this.popn.OrderBy( n => n.Front ).ThenByDescending( n => n.Sunev ).ToArray();

			count = 0;//frontが同じ個体数を数える.
			front = 1;

			for( int i = 0; i < Config.Populationsize; i++ )
			{
				if( popn[i].Front == front )
				{
					count++;
				}
				else//もし一つ前のものと違ったら.
				{
					if( count <= 2 )
					{
						for( int j = 0; j < count; j++ )
						{
							this.popn[i - count + j].CrowdingDistance = config.Infinity;
						}
					}
					else//同じfrontの個体数が3個以上
					{
						double max = this.popn[i - count].Sunev;
                        double min = this.popn[i - 1].Sunev;

						this.popn[i - count].CrowdingDistance = config.Infinity;
						this.popn[i - 1].CrowdingDistance = config.Infinity;

						for( int j = 1; j < count - 1; j++ )
						{
                            double right = this.popn[i - count + j - 1].Sunev;
                            double left = this.popn[i - count + j + 1].Sunev;

							this.popn[i - count + j].calCD( right, left, max, min,config );
						}
					}

					if( i > Config.Populationsize - 2 )
					{
						for( int j = i; j < Config.Populationsize; j++ )
						{
							this.popn[j].CrowdingDistance = config.Infinity;
						}
					}

					front = this.popn[i].Front;
					count = 1;
				}
			}

		}
		public void print()
		{
			for( int i = 0; i < Config.Populationsize; i++ )
			{
				Console.WriteLine( "pop" + ( i + 1 ) + ":" );
				popn[i].Print();
				Console.WriteLine( "Fitness" + popn[i].Fitness );
				Console.WriteLine( "Distance" + popn[i].Distance );
			}



		}
		int popcount;
		public int Popcount
		{
			get { return popcount; }
			set { popcount = value; }
		}
		public void Mutation(int x,int y,Config config,Field field )
		{
            this.config = config;
            for (int i = Config.Populationsize / 2; i < Config.Populationsize; i++)
			{
                this.popn[i].Mutation(x, y, config, field);
			}

            /*
                
            IEnumerator I = popn.GetEnumerator();

            while( I.MoveNext() )
            {
                Individual current = ( Individual )I.Current;

                if( current.Front != 1 )
                {
                    current.Mutation(x,y,config,field);
                }
            }
             **/
        }
		public void Subplan( int x,int y,Field field,Config config)
		{
			  
			for( int i = 0; i < popn.Count(); i++ )
			{
				int k = popn[i].od( x, y );//全部のwaypointを多重化させるよ
				if( popn[i].gene.Count == 0 )
				{
					;
				}
				else 
				{
                    this.popn[i].Plan(i, x, y, popn[i], field, config, Config.Time); 
				}
			}
		}
		
		public void count()
		{
			popcount = 0;//初期化
			for( int i = 0; i < Config.Populationsize / 2; i++ )
			{
				if( Popn[i].Fitness == 0 )
				{
					popcount = popcount + 1;
				}
			}
		}
		public void WritePop( int n )
		{
			DateTime dt;
			dt = DateTime.Now;

			string fileName = "No" + n + "." + "gene" + dt.ToString( "MMdd_hhmm" ) + ".csv";

			using( StreamWriter sw = new StreamWriter( fileName ) )
			{

				for( int i = 0; i < this.popn.Length; i++ )
				{
					sw.WriteLine( "pop" + i + ":" );
					sw.Write( "Fitness:," + this.popn[i].Fitness + ",Distance," + this.popn[i].Distance + ",Front," + this.popn[i].Front );
					for( int j = 0; j < this.popn[i].Gene.Count; j++ )
					{
                        sw.Write(",gene[" + j + "]:," + this.popn[i].Gene[j].Basex + "," + this.popn[i].Gene[j].Basey + ",Sunev," + this.Popn[i].Sunev);
					}
					sw.WriteLine();
				}
			}
		}
		public void SelectPath()
		{
			for( int m = 0; m < 20; m++ )
			{
				if( Path[m].Fitness != 0 )
				{
					Path[m].Fdis = Path[m].Fdis -5000;
				}
			}
			Path = Path.OrderByDescending( n => n.Fdis ).ToArray();
			for( int j = 1; j < Config.Populationsize / 2; j++ )
			{
				for( int h = 0; h < Path.Count(); h++ )
				{
					if( Math.Min( Path[h].gene.Count, popn[j].gene.Count() ) == 1 )
					{
						double x = popn[j].Gene[0].Basex;
						double y = popn[j].Gene[0].Basey;
						if( ( Math.Sqrt( Math.Pow( ( x - Path[h].gene[0].basex ), 2 ) + Math.Pow( ( y - Path[h].gene[0].basey ), 2 ) ) < 1.5 ) )
						{
							h = Path.Count() - 1;
						}
						else if( ( Math.Sqrt( Math.Pow( ( x - Path[h].gene[0].basex ), 2 ) + Math.Pow( ( y - Path[h].gene[0].basey ), 2 ) ) > 1.5 ) && ( ( Path.Count() - 1 ) == h ) )
						{
							if( popn[j].Fitness == 0 )
							{
								//  for (int n = 0; n < pop[j].gene.Count; n++)
								//   {
                                if (this.Popn[j].Sunev!= 0) {
                                    
                                    Path[19] = new Individual(popn[j], config);
                                    Path[19].Fitness = popn[j].Fitness;
                                    Path[19].Subpathct = this.popn[j].Subpathct;
								    
                                }
								//Path[19].Gene[0].Basex = path[h + 1].Gene[0].Basex;
								//Path[19].Gene[0].Basey = path[h + 1].Gene[0].Basey;
								//path[19].gene[0].basex = pop[j].gene[0].basex;
								// Path[19].gene[0].basey = pop[j].gene[0].basey;
								//Path[19].Fitness = path[h + 1].Fitness;
								//path[19].Fitness = pop[j].Fitness;
								//  }
								for( int m = 0; m < 20; m++ )
								{
									if( Path[m].Fitness != 0 )
									{
                                        Path[m].Distance = Path[m].Distance + 100;
									}
								}
							//	Path = Path.OrderBy( n => n.Distance).ToArray();
                      			Path = Path.OrderByDescending( n => n.Fdis ).ToArray();
							}
							h = Path.Count() - 1;
							break;

						}
					}
					else if( Math.Min( Path[h].gene.Count, popn[j].gene.Count() ) != 1 )
					{
						for( int k = 0; k < Math.Min( Path[h].gene.Count(), popn[j].gene.Count() ); k++ )//少ないほうにあわせる
						{

							double x = popn[j].gene[k].basex;
							double y = popn[j].gene[k].basey;
							bool b = true;

							if( Math.Sqrt( Math.Pow( ( x - Path[h].gene[k].basex ), 2 ) + Math.Pow( ( y - Path[h].gene[k].basey ), 2 ) ) < 1.5 && k == Math.Min( Path[h].gene.Count(), popn[j].gene.Count() ) - 1 && b == true )
							{
								h = Path.Count() - 1;
								break;
							}
							else if( Math.Sqrt( Math.Pow( ( x - Path[h].gene[k].basex ), 2 ) + Math.Pow( ( y - Path[h].gene[k].basey ), 2 ) ) > 1.5 )
							{
								b = false;//違う経路を持っているのでマーキングしておく
							}
						}
					}
					else if( Math.Min( Path[h].gene.Count, popn[j].gene.Count() ) != 1 && ( ( Path.Count() - 1 ) == h ) )
					{
						for( int k = 0; k < Math.Min( Path[h].gene.Count(), popn[j].gene.Count() ); k++ )//少ないほうにあわせる
						{

                            if (Path[h].gene.Count != popn[j].gene.Count())
                            {
                                if (popn[j].Fitness == 0)
                                {
                                    // for (int n = 0; n < pop[j].gene.Count; n++)
                                    //{
                                    if (this.Popn[j].Sunev != 0)
                                    {
                                        path[19] = new Individual(popn[j], config);
                                        Path[19].Fitness = popn[j].Fitness;
                                        pathcount = pathcount + 1;
                                        //   Path[19].Gene[n].Basex = path[h + 1].Gene[n].Basex;
                                        //   Path[19].Gene[n].Basey = path[h + 1].Gene[n].Basey;
                                        //   Path[19].Fitness = path[h + 1].Fitness;
                                    }
                                    //}
                                    for (int m = 0; m < 20; m++)
                                    {
                                        if (Path[m].Fitness != 0)
                                        {
                                            Path[m].Fdis = Path[m].Fdis -5000;
                                        }
                                    }
                                    Path = Path.OrderByDescending(n => n.Fdis).ToArray();
                                }
                            }
                            else
                            {
							double x = popn[j].gene[k].basex;
							double y = popn[j].gene[k].basey;
							bool c = true;

							if( Math.Sqrt( Math.Pow( ( x - Path[h].gene[k].basex ), 2 ) + Math.Pow( ( y - Path[h].gene[k].basey ), 2 ) ) < 1.5 && k == Math.Min( Path[h].gene.Count(), popn[j].gene.Count() ) - 1 && c == true )
							{
								break;//ダブり発見このPopは捨てる       
							}
							else if( Math.Sqrt( Math.Pow( ( x - Path[h].gene[k].basex ), 2 ) + Math.Pow( ( y - Path[h].gene[k].basey ), 2 ) ) > 1.5 && k != Math.Min( Path[h].gene.Count(), popn[j].gene.Count() ) - 1 )
							{
								c = false;//違う経路を持っているのでマーキングしておく
							}
                            else if (Math.Sqrt(Math.Pow((x - Path[h].gene[k].basex), 2) + Math.Pow((y - Path[h].gene[k].basey), 2)) > 1.5 && k == Math.Min(Path[h].gene.Count(), popn[j].gene.Count()) - 1 && c == false)
                            {
                                if (popn[j].Fitness == 0)
                                {
                                    // for (int n = 0; n < pop[j].gene.Count; n++)
                                    //{
                                    if (this.Popn[j].Sunev != 0)
                                    {
                                        path[19] = new Individual(popn[j], config);
                                        Path[19].Fitness = popn[j].Fitness;
                                        pathcount = pathcount + 1;
                                        //   Path[19].Gene[n].Basex = path[h + 1].Gene[n].Basex;
                                        //   Path[19].Gene[n].Basey = path[h + 1].Gene[n].Basey;
                                        //   Path[19].Fitness = path[h + 1].Fitness;
                                    }
                                    //}
                                    for (int m = 0; m < 20; m++)
                                    {
                                        if (Path[m].Fitness != 0)
                                        {
                                            Path[m].Fdis = Path[m].Fdis -500;
                                        }
                                    }
                                    Path = Path.OrderByDescending(n => n.Fdis).ToArray();
                                }
                            }
								break;
							}

						}
					}

				}
			}

		}
		public void PathIni()
		{
			for( int m = 0; m < 20; m++ )
			{
				//path[m] = new Individual(pop[0]);
				path[m].Fitness = 100;
				path[m].Distance = 100;
				Path[m] = path[m];
			}
		}
		public void PathTrace()
		{
			for( int i = 0; i < 20; i++ )
			{
				path[i].TraceMap();
			}
		}
		public void RePop( double d )//ゴールする航空機のIni[]のなかの配列の値
		{
			for( int i = 0; i < Config.Populationsize; i++ )
			{
				Popn[i].ReGene( d );
			}
		}

		/* population内にかぶってるindividualが存在するとき片方を消す. */
		public void deleteSameIndividual(int x,int y, Field f )
		{
			this.Inix = x;
			this.Iniy = y;
			for( int i = 0; i < Config.Populationsize; i++ )
			{
				for( int j = 0; j < Config.Populationsize; j++ )
				{
					if( i != j )
					{
						if( popn[i].isSameIndividual( popn[j] ) )
						{
							popn[j] = new Individual(x,y, config, f );
						}
					}
				}
			}
		}

		/* 1回の交叉メソッド */
		public void crossOver( int i, int j, int k, int x,int y,Config config)
		{
			this.Inix = x;
			this.Iniy = y;
			int random = config.rand.Next();
			Individual child1 = new Individual( popn[i],config );
			Individual child2 = new Individual( popn[j],config);

			int length1 = 0;
			int length2 = 0;
			length1 = child1.Gene.Count;
			length2 = child2.Gene.Count;
			if( length1 == 1 && length2 == 1 )
			//if (pop[i].gene[1].Basex == pop[i+1].gene[1].Basex && pop[i].gene[1].Basey == pop[i+1].gene[1].Basey)
			{

				popn[k].Gene[0].Basex = ( child1.Gene[0].Basex + child2.Gene[0].Basex ) / 2; //中継点平均方式
				popn[k].Gene[0].Basey = ( child1.Gene[0].Basey + child2.Gene[0].Basey ) / 2;
               // popn[k].Mutation(x, y, config);
                popn[k].fcalc(Inix, Iniy, field);
                popn[k].dcalc(Inix, Iniy);
                popn[k].subpathct = 0;
                popn[k].Sunev = 0;
				if( popn[k].Fitness == 0 )
				{
                    Popn[k].Subplan(k, Inix, Iniy, field, config);
				}
			}
			if( ( length1 == 1 && length2 > 1 ) || ( length1 > 1 && length2 == 1 ) ) //複数対１の場合
			{
				if( length1 > length2 )
				{
					child1.Gene.Add( child2.Gene[0] );
					popn[k] = child1;
				}
				if( length1 < length2 )
				{
					child2.Gene.Add( child1.Gene[0] );
					popn[k] = child2;
				}

                popn[k].fcalc(Inix, Iniy, field);
                popn[k].dcalc(Inix, Iniy);
                popn[k].subpathct = 0;
                popn[k].Sunev = 0;
				if( popn[k].Fitness == 0 )
				{
                    Popn[k].Subplan(k, Inix, Iniy, field, config);
				}
			}
			if( length1 > 1 && length2 > 1 )//複数対複数の場合
			{
				Individual childc1 = new Individual( child1,config );
				Individual childc2 = new Individual( child2 ,config);
				if( length1 > length2 )
				{

					int a = config.rand.Next( length2 );

					for( int l = a; l < length1; l++ )
					{
						childc1.Gene.RemoveAt( a );

					}

					for( int l = a; l < length2; l++ )
					{
						childc1.Gene.Add( child2.Gene[l] );
					}
					popn[k] = childc1;
				}
				else
				{
					int a = config.rand.Next( length1 );
					for( int l = a; l < length2; l++ )
					{
						childc2.Gene.RemoveAt( a );

					}

					for( int l = a; l < length1; l++ )
					{
						childc2.Gene.Add( ( child1.Gene[l] ) );
					}
					popn[k] = childc2;
				}
                popn[k].Mutation(Inix, Iniy, config, field);
                popn[k].fcalc(Inix, Iniy, field);
                popn[k].dcalc(Inix, Iniy);
                   popn[k].subpathct = 0;
                   popn[k].Sunev = 0;
				if (popn[k].Fitness == 0)
	{
        Popn[k].Subplan(k, Inix, Iniy, field, config);
	}
			}


			if( popn[k].Gene.Count != 1 )
			{
				for( int g = 0; g < popn[k].Gene.Count - 1; g++ )
				{
					if( popn[k].Gene[g].Basex == popn[k].Gene[g + 1].Basex && popn[k].Gene[g].Basey == popn[k].Gene[g + 1].Basey )
					{
						popn[k].Gene.RemoveAt( g );
						g = 0; //もう一度被るかどうかみてみる
					}
					if( popn[k].Gene.Count != 1 )//このif文は中間発表後のプログラムには必要ない
					{
						for( int t = g; t < popn[k].Gene.Count - 1; t++ )
						{
							if( popn[k].Gene[g].Basex == popn[k].Gene[t + 1].Basex && popn[k].Gene[g].Basey == popn[k].Gene[t + 1].Basey )
							{
								popn[k].Gene.RemoveAt( g );
								t = g;
							}
						}
					}
				}

			}
            popn[k].dcalc(Inix, Iniy);
            popn[k].fcalc(Inix, Iniy, field);//now
			popn[k].Time = popn[j].Time + 1;

		}
		/* childrenが埋まるまで上の交叉メソッドを繰り返す. */
		public void crossOver(int x,int y,Config config)
		{
			int parent1, parent2;
            this.Inix = x;
            this.Iniy = y;
			for( int child = Config.Populationsize / 2; child < Config.Populationsize; child++ )
			{
				do
				{
					parent1 = this.selection();
					parent2 = this.selection();
				}
				while( parent1 == parent2 );

                this.crossOver(parent1, parent2, child, Inix, Iniy, config);
			}
		}
		/* 親選択メソッド */
		public int selection()
		{
			int candidate1, candidate2;

			int elected;

			/* 2つの候補が等しくない物同士になるまで乱数をふる. */
			do
			{
				candidate1 = config.rand.Next( Config.Populationsize / 2 );

				candidate2 = config.rand.Next( Config.Populationsize / 2 );
			}
			//while( individuals [ candidate1 ].isSameIndividual ( individuals [ candidate2 ] ) );
			/* ProfitとCDがかぶっていない条件はあってもなくても変わらなさそう. */
			while( candidate1 == candidate2 );

			/* 
			 * 2つの候補のFrontが同じであれば, crowding distanceの大小で決める.
			 * crowding distanceも同じであればランダムに選ぶ.
			 */
			if( this.popn[candidate1].Front == this.popn[candidate2].Front )
			{
				if( this.popn[candidate1].CrowdingDistance >
					this.popn[candidate2].CrowdingDistance )
				{
					elected = candidate1;
				}
				else if( this.popn[candidate1].CrowdingDistance <
						 this.popn[candidate2].CrowdingDistance )
				{
					elected = candidate2;
				}
				else
				{
					elected = ( config.rand.Next() % 2 == 0 ) ? candidate1 : candidate2;
				}
				return elected;
			}
			/* Frontが異なれば, Frontが小さい方を選ぶ. (else部分はありえないが念のための記述.) */
			else
			{
				if( this.popn[candidate1].Front < this.popn[candidate2].Front )
				{
					elected = candidate1;
				}
				else if( this.popn[candidate1].Front > this.popn[candidate2].Front )
				{
					elected = candidate2;
				}
				else
				{
					elected = ( config.rand.Next() % 2 == 0 ) ? candidate1 : candidate2;
				}

				return elected;
			}
		}

	}
}
