using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALPwithNSGA2
{
	class Program
	{
		static void Main( string[] args )
		{
			int seed = 10;
			Field field= new Field2();
            Field nextfid = new Field1();
            Config config = new Config(seed);
			OrderConfig orconfig = new OrderConfig(seed);
            Aircrafts air = new Aircrafts(config);
            air.Process(config);
            /*
			for( int i = 0; i < 4; i++ )
			{
				air.air[i].WritePop( i );
			}
           
            for (int i = 0; i < Config.First; i++)
            {
                air.air[i].SelectPath();    
            }
             */
            OrderPopulation pop = new OrderPopulation(air,orconfig);
			for( int i = 0; i < Config.First; i++ )
			{
				air.air[i].WritePop( i );//一回目の並び替え
			}
            for (int z = 0; z < 0; z++)
            {
                pop.Crossover(air);
            }
            pop.WritePop(air);

			/*
			air.WriteOnePath( 0, pop.Orpop[0].Gene[0] );
			air.WriteOnePath( 1, pop.Orpop[0].Gene[1] );
			air.WriteOnePath( 2, pop.Orpop[0].Gene[2] );
			air.WriteOnePath( 3, pop.Orpop[0].Gene[3] );
			air.WriteOneTrace( 0, pop.Orpop[0].Gene[0] );
			air.WriteOneTrace( 1, pop.Orpop[0].Gene[1] );
			air.WriteOneTrace( 2, pop.Orpop[0].Gene[2] );
			air.WriteOneTrace( 3, pop.Orpop[0].Gene[3] );
			
            
           // int x = pop.SearchIndex(air);//ゴールした航空機のIndexを表示
			double d = Config.Time;
			int dcount = air.Air.Count;
            int h = air.air.Count;
            for (int i = 0; i <h; i++)
            {
				if( air.Air[i].path[pop.Orpop[0].Gene[i]].Distance <= d )
				{
					air.Air.RemoveAt( i );
					dcount = dcount-1;
                    h = h - 1;
					i = 0;
	            }
				if( air.Air[i].path[pop.Orpop[0].Gene[i]].Distance > d )
                {
					air.StartReIni( d, pop.Orpop[0].Gene[i], i );
                    air.PathReIni(d);
					air.Air[i].RePop( d);
                 
                    for (int j = 0; j < air.Air[i].Path.Count(); j++) 
                    {
                        air.Air[i].Path[j].Distance = air.Air[i].Path[j].Distance - Config.Time;     
                    }    
                }
            }
 */
            double d = Config.Time;
            int dcount = air.Air.Count;
            int h = air.air.Count;
            Config.Rest = dcount;
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < air.Air[i].Path.Count(); j++)
                {
                    air.Air[i].Path[j].Distance = air.Air[i].Path[j].Distance - Config.Time;
                    for (int l = 0; l < air.Air[i].Path[j].subgene.Count(); l++)
                    {
                        air.Air[i].Path[j].subgene[l].Distance=air.Air[i].Path[j].subgene[l].Distance - Config.Time;
                    }
                }
 
            }
            pop.BeforePath(dcount);
           // pop.SubpathIni(dcount,d,air);
            air.ReProcess(dcount,config);
			for( int z = 0; z < 5000; z++ )
			{
				pop.Crossover(dcount,air,pop);//２回目の並び替え
			}
            for (int i = 0; i < Config.First + Config.Follow; i++)
            {
                air.WritePath(i);
            }
           
            
             h = air.air.Count;
             for (int i = 0; i < Config.First + Config.Follow; i++)
			{
				air.air[i].WritePop( i );
			}
            /*
            for (int i = 0; i < h; i++)
            {
                if (air.Air[i].path[pop.Orpop[0].Gene[i]].Distance <= d)
                {
                    air.Air.RemoveAt(i);
                    dcount = dcount - 1;
                    h = h - 1;
                    i = 0;
                }
                if (air.Air[i].path[pop.Orpop[0].Gene[i]].Distance > d)
                {
                    air.StartReIni(d, pop.Orpop[0].Gene[i], i);
                    air.PathReIni(d);
                    air.Air[i].RePop(d);

                    for (int j = 0; j < air.Air[i].Path.Count(); j++)
                    {
                        air.Air[i].Path[j].dcalc(air.air[i].Inix, air.air[i].Inix);
                    }

                }
            }
                 */
             for (int i = 0; i < Config.First + Config.Follow; i++)//コピーコンストラクタを作成する必要アリ
            {
                Console.WriteLine(air.air[i].Inix + "." + air.air[i].Iniy + ",ID," + air.air[i].Id);
            }

            pop.WritePop(air, Config.Follow);
            ;
			
        }
		}
	}

