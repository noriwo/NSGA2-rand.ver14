using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALPwithNSGA2
{
    class Individual
    {
        Field field;
        double fitness;
        double distance;
        double redis;
        double onedis;
        int discount;
        double d;
        double fdis;
        int time;
        int ct = 0;

        public int Ct
        {
            get { return ct; }
            set { ct = value; }
        }
        public List<Subgene> subgene = new List<Subgene>();
        public int subpathct;
        private double sunev;
        double subev;
        public double Subev
        {
            get { return subev; }
            set { subev = value; }
        }
        int nextx;


        public int Nextx
        {
            get { return nextx; }
            set { nextx = value; }
        }
        int nexty;

        public int Nexty
        {
            get { return nexty; }
            set { nexty = value; }
        }
        public double Sunev
        {
            get { return sunev; }
            set { sunev = value; }
        }
        private int subsum;

        public int Subsum
        {
            get { return subsum; }
            set { subsum = value; }
        }
        public int Subpathct
        {
            get { return subpathct; }
            set { subpathct = value; }
        }
        int inix;

        public int Inix
        {
            get { return inix; }
            set { inix = value; }
        }
        int iniy;
        public int Iniy
        {
            get { return iniy; }
            set { iniy = value; }
        }
        private int front;
        public int Front
        {
            get { return front; }
            set { front = value; }
        }
        private double crowdingDistance;
        public double CrowdingDistance
        {
            get { return crowdingDistance; }
            set { crowdingDistance = value; }
        }
        Config config;
        public int Time
        {
            get { return time; }
            set { time = value; }
        }
        public double Fdis
        {
            get { return fdis; }
            set { fdis = value; }
        }
        public double Distance
        {
            get { return distance; }
            set { distance = value; }
        }
        public double Fitness
        {
            get { return fitness; }
            set { fitness = value; }
        }
        Base base1;
        public Base Base1
        {
            get { return base1; }
            set { base1 = value; }
        }
        public List<Base> gene = new List<Base>();
        public List<Base> Gene
        {
            get { return gene; }
            set { gene = value; }
        }
        private List<TrBase> trace = new List<TrBase>();
        public List<TrBase> Trace
        {
            get { return trace; }
            set { trace = value; }
        }
        /* 支配されている数 */
        private int isDominated;
        public int IsDominated
        {
            get { return isDominated; }
            set { isDominated = value; }
        }
        /* 支配している解番号の保存用リスト */
        private List<int> dominating = new List<int>();
        public List<int> Dominating
        {
            get { return dominating; }
            set { dominating = value; }
        }

        //   li[4] = 3;
        //   li.Insert(4, 5);
        public void Print()
        {
            int a = gene.Count;
            for (int i = 0; i < a; i++)
            {
                Console.Write("gene[" + i + "]:" + gene[i].Basex);
                Console.Write(",");
                Console.WriteLine(gene[i].Basey);

                //   Console.WriteLine(gene.Count);//Geneの長さ                    
            }

        }
        public void

            Plan(int i, int x, int y, Individual ind, Field field, Config config, int d)
        {
            this.sunev = 0;
            for (int k = 5; k < this.subgene.Count; k++)
            {
                this.subgene.Clear();
            }

           // this.subgene.Add(new Subgene(ind, config)); 
            this.subpathct= this.subgene.Count();            //   this.subgene.Clear();
            /*
            for (int l = 0; l < ind.gene.Count; l++)
            {
                this.subgene[0].gene.Add(new Base(ind.gene[l]));
            }
            */
            Trace.Clear();
            TraceMap(x, y);
            int g;
            g = (int)(d * 2);
            int Inix = (int)Math.Round(Trace[g].basex);
            int Iniy = (int)Math.Round(Trace[g].basey);
            nextx = inix;
            nexty = iniy;
            if (Inix == 0 && Iniy == 0)
            {
                Inix = 5;
            }


            if (gene.Count() == 0)
            {
                ;
            }
            else //これははじめと一つ目のwaypoint
            {
                this.subsum = 1;
                int r = 0;
                int s = 0;
                //if ((k == 0))
                {
                    double theta;
                    double t = (x + gene[0].basex) / 2;
                    double u = (y + gene[0].basey) / 2;
                    /*
                    if ((x - gene[0].basex) == 0)
                    {
                        theta = Math.PI / 2;
                        theta = theta + Math.PI / 2;
                    }
                    else
                    {
                        theta = Math.Atan((y - gene[0].basey) / (x - gene[0].basex));
                        theta = theta + Math.PI / 2;
                    }
                    */
                    int count = this.subgene.Count();
                    this.subpathct = this.subgene.Count();
                    for (int m = count + 1; m < count + 20; m++)
                    {
                        //subgene[m].gene.Clear();
                        int p = 0;
                        int q = 0;

                        /*
                        do
                        {
                            r = config.rand.Next(20) - 10;
                            p = (int)(t + r * Math.Cos(theta));
                            q = (int)(u + r * Math.Sin(theta));
                            if (0 <= q && q <= 24 && 0 <= p && p <= 24)
                            {
                                break;
                            }
                        } while (true);
                          */
                        do
                        {
                            r = config.rand.Next(10) - 5;
                            s = config.rand.Next(10) - 5;
                            p = (int)(t + r);
                            q = (int)(u + s);
                            if (0 <= q && q <= 24 && 0 <= p && p <= 24)
                            {
                                break;
                            }
                        } while (true);
                        this.subgene.Add(new Subgene(ind, config));
                        Base b = new Base(config);
                        b.basex = p;
                        b.basey = q;
                        Base c = new Base(config);
                        c.basex = Inix;
                        c.basey = Iniy;
                        this.subgene[subgene.Count - 1].gene.Insert(0, b);
                        this.subgene[subgene.Count - 1].gene.Insert(0, c);
                        this.subgene[subgene.Count - 1].fcalc(Inix, Iniy, field);
                        this.subgene[subgene.Count - 1].dcalc(x, y);
                        this.Subsum = this.Subsum + 1;
                        this.subgene[subgene.Count - 1].Countsub.Insert(0, b);//新しい計算法
                        if (this.subgene[subgene.Count() - 1].fitness == 0)
                        {
                            this.subpathct = this.subpathct + 1;
                        }

                    }
                }
              
                //else
                if (gene.Count > 1)
                {
                    for (int l = 1; l < gene.Count(); l++)
                    {
                        double t = (gene[l].basex + gene[l - 1].basex) / 2;
                        double u = (gene[l].basey + gene[l - 1].basey) / 2;
                        double theta;
                        /*
                        if (gene[l].basex - gene[l].basex == 0)
                        {
                            theta = Math.PI / 2;
                            theta = theta + Math.PI / 2;
                        }
                        else
                        {
                            theta = Math.Atan((gene[l].basey - gene[l].basey) / (gene[l].basex - gene[l].basex));
                            theta = theta + Math.PI / 2;
                        }
                        */
                        int count = this.subgene.Count();
                        this.subpathct = this.subgene.Count();
                        for (int m = count + 1; m < count + 20; m++)
                        {
                            int p = -1;
                            int q = -1;
                            /*
                             do
                             {
                                 r = config.rand.Next(20) - 10;
                                 p = (int)(t + r * Math.Cos(theta));
                                 q = (int)(u + r * Math.Sin(theta));
                                 if (0 <= q && q <= 24 && 0 <= p && p <= 24)
                                 {
                                     break;
                                 }
                             } while (true);
                             */
                            do
                            {
                                r = config.rand.Next(10) - 5;
                                s = config.rand.Next(10) - 5;
                                p = (int)(t + r);
                                q = (int)(u + s);
                                if (0 <= q && q <= 24 && 0 <= p && p <= 24)
                                {
                                    break;
                                }
                            } while (true);
                            this.subgene.Add(new Subgene(ind, config));
                            Base b = new Base(config);
                            b.basex = p;
                            b.basey = q;
                            this.subgene[subgene.Count - 1].gene.Add(b);
                            this.subgene[subgene.Count - 1].dcalc(x, y);
                            this.subgene[subgene.Count - 1].fcalc(Inix, Iniy, field);
                            this.subsum = subsum + 1;
                            this.subgene[subgene.Count - 1].Countsub.Insert(0, b);//新しい計算法
                            if (this.subgene[subgene.Count - 1].fitness == 0)
                            {
                                this.subpathct = this.subpathct + 1;
                            }

                        }
                    }

                }
                //else if( k == gene.Count() )//last
                {
                    int gc = gene.Count() - 1;
                    double theta;
                    double t = (Config.XGoal + gene[gc].basex) / 2;
                    double u = (Config.YGoal + gene[gc].basey) / 2;
                    if ((Config.XGoal - gene[gc].basex) == 0)
                    {
                        theta = Math.PI / 2;
                        theta = theta + Math.PI / 2;
                    }
                    else
                    {
                        theta = Math.Atan((Config.YGoal - gene[gc].basey) / (Config.XGoal - gene[gc].basex));
                        theta = theta + Math.PI / 2;

                    }
                    int c = this.subgene.Count(); 
                    this.subpathct = this.subgene.Count();
                    for (int m = c + 1; m < c + 20; m++)
                    {
                        int p = 0;
                        int q = 0;
                        /*
                        do
                         * 
                        {
                            r = config.rand.Next(20) - 10;
                            p = (int)(t + r * Math.Cos(theta));
                            q = (int)(u + r * Math.Sin(theta));
                            if (0 <= q && q <= 24 && 0 <= p && p <= 24)
                            {
                                break;
                            }
                        } while (true);
                        */
                        do
                        {
                            r = config.rand.Next(10) - 5;
                            s = config.rand.Next(10) - 5;
                            p = (int)(t + r);
                            q = (int)(u + s);
                            if (0 <= q && q <= 24 && 0 <= p && p <= 24)
                            {
                                break;
                            }
                        } while (true);
                        this.subgene.Add(new Subgene(ind, config));
     
                        Base b = new Base(config);
                        b.basex = p;
                        b.basey = q;
                        //this.subgene[m].gene.Add(b);
                    
                        this.subgene[subgene.Count() - 1].gene.Add(b);
                        this.subgene[subgene.Count() - 1].dcalc(x, y);
                        this.subgene[subgene.Count() - 1].fcalc(Inix, Iniy, field);
                        this.subsum = subsum + 1;
                        this.subgene[subgene.Count - 1].Countsub.Insert(0, b);//新しい計算法
                        if (this.subgene[subgene.Count() - 1].fitness == 0)
                        {
                            this.subpathct = this.subpathct + 1;
                        }
                    }
                }
            }

            this.subgene = this.subgene.OrderByDescending(n => n.fitness).ToList();
            for (int o = 0; o < subgene.Count; o++)
            {
                if (subgene[o].fitness > 0)
                {
                    this.subgene.RemoveAt(o);
                    o = 0;
                    subpathct = subpathct - 1;
                }
            }
            this.subpathct = this.subgene.Count();
            /*
            if (this.subpathct + 5 == this.subgene.Count)
            {
                this.subpathct = this.subpathct + 5;
            }
             * */
            ct = 0;
            /*
                for (int v = 0; v < subgene.Count; v++)
                {

                    this.subgene[v].id = ct; 
                }
             */
            for (int c = 0; c < subgene.Count(); c++)
            {
                this.subgene[c].ardisclear(this, c);
                this.subgene[c].subdis(this,c);//subgene[c]の評価をチェック
            }
            /*
            this.Subpathct = this.subpathct;
            for (int m = 0; m < subgene.Count(); m++)
            {
                subgene[m].indicatorct = 0;
                subgene[m].Sunev = 0;
                this.subev = 0;
            }
            for (int p = 0; p < subgene.Count(); p++)
            {

                this.subgene[p].ardisclear(this, p);
                this.subgene[p].ardiscalc(this, p);
                this.subgene[p].Ardis = this.subgene[p].Ardis.OrderByDescending(n => n.s).ToList();

                for (int m = 0; m < config.Nearct; m++)
                {
                  if (this.subgene[p].Ardis.Count == m)
                    {
                        break;

                    }
                  
                    if (this.subgene[p].Ardis[m].S > 100)
                    {
                        break;
                    }
                    this.subev = this.subgene[p].Ardis[m].S + this.subev;
                   // if (subgene[p].gene.Count() != 0)
                   // {
                   //     this.subev = this.subev / subgene[p].gene.Count();                        
                   // }

                    this.subgene[subgene[p].Ardis[m].Id].Indicatorct = this.subgene[subgene[p].Ardis[m].Id].Indicatorct + 1;
                }
              
        
            }
             * */
            this.subev = 0;
            for (int p = 0; p < subgene.Count(); p++)
            {
                 
                this.subgene[p].Ardis = this.subgene[p].Ardis.OrderBy(n => n.s).ToList();

                for (int m = 0; m < config.Nearct; m++)
                {
                    if (this.subgene[p].Ardis.Count == m)
                    {
                        break;

                    }
                    this.subev = this.subgene[p].Ardis[m].S + this.subev;
                    this.subgene[p].Subd = this.subgene[p].Subd + this.subgene[p].Ardis[m].S;
                    this.subgene[subgene[p].Ardis[m].Id].Indicatorct = this.subgene[subgene[p].Ardis[m].Id].Indicatorct + 1;
                }
            }
            this.subev = this.subev / (config.Nearct*subgene.Count());
           //this.subgene= this.subgene.OrderByDescending(n => n.indicatorct).ToList();
            this.subgene = this.subgene.OrderByDescending(n => n.Subd).ToList();
            //this.sunev = (double)((double)this.subpathct / (double)this.subsum);
            //for (int f = 0; f < subgene.Count; f++)
            // {

            //   this.sunev = this.sunev + this.Subev;   
            //}
            this.sunev = this.subev;


        }
        public void Change(Config config, int j)
        {

            this.config = config;
            Gene[j].Basex = config.rand.Next(25);
            Gene[j].Basey = config.rand.Next(25);
        }
        public Individual(int x, int y, Config config, Field field)//コンストラクタ
        {
            this.Inix = x;
            this.Iniy = y;
            this.config = config;
            this.gene.Add(new Base(config));

            front = 0;
            crowdingDistance = 0;
            this.subgene = new List<Subgene>(Config.Subpath);
            for (int i = 0; i < Config.Subpath; i++)
            {

                this.subgene.Add(new Subgene(x, y, config, field, i));
            }
            this.Subpathct = 0;
            dcalc(x, y);
            fcalc(x, y, field);

        }
        public Individual(int x, int y, Config config, Field field, int z)//コンストラクタ
        {
            this.Inix = x;
            this.Iniy = y;
            this.config = config;
            this.gene.Add(new Base(config));
            dcalc(x, y);
            fcalc(x, y, field);
            front = 0;
            crowdingDistance = 0;

            this.Subpathct = 0;

        }
        public Individual(Individual ind, Config config)
        {
            fitness = ind.fitness;
            distance = ind.distance;
            time = ind.time;
            fdis = ind.fdis;
            front = ind.front;
            this.crowdingDistance = ind.crowdingDistance;
            this.isDominated = ind.isDominated;
            this.dominating = ind.dominating;
            for (int i = 0; i < ind.trace.Count; i++)
            {
                this.trace.Add(new TrBase(ind.trace[i]));
            }
            for (int i = 0; i < ind.gene.Count; i++)
            {
                this.gene.Add(new Base(ind.gene[i]));

            }
            //     for (int i = 0; i < ind.subgene.Count; i++)
            //    {
            //       ind.subgene.RemoveAt(0);
            //  }
            for (int j = 0; j < ind.subgene.Count; j++)
            {

                this.subgene.Add(new Subgene(ind.subgene[j], config));

                // this.subgene.ToArray();

                this.subgene[j].Fitness = ind.subgene[j].Fitness;
                this.subgene[j].Distance = ind.subgene[j].Distance;
                this.subgene[j].subpathct = ind.subgene[j].subpathct;
                this.subgene[j].Indicatorct = ind.subgene[j].Indicatorct;
                //for( int i = 0; i < ind.subgene[j].Gene.Count; i++ )
                //{
                //	this.subgene[j].gene.Add( new Base(ind.subgene[j].gene[i]) );

                //				}
            }

            this.sunev = ind.Sunev;


        }//コピーコンストラクタ
        public Individual(Config config)
        {
            this.gene.Add(new Base(config));
            front = 0;
            crowdingDistance = 0;
            this.Sunev = 0;
            this.Subsum = 0;
            this.Subpathct = 0;


        }
        public void dcalc(int x, int y)
        {
            distance = 0;
            if (gene.Count() == 0)
            {
                distance = Math.Sqrt((x - Config.XGoal) * (x - Config.XGoal) + (y - Config.YGoal) * (y - Config.YGoal));
            }
            else
            {

                distance = Math.Sqrt((gene[0].Basex - x) * (gene[0].Basex - x) + (gene[0].Basey - y) * (gene[0].Basey - y));
                if (gene.Count > 1)
                {
                    for (int i = 1; i < gene.Count; i++)
                    {
                        distance = distance + Math.Sqrt((gene[i].Basex - gene[i - 1].Basex) * (gene[i].Basex - gene[i - 1].Basex) + (gene[i].Basey - gene[i - 1].Basey) * (gene[i].Basey - gene[i - 1].Basey));
                    }
                }
                distance = distance + Math.Sqrt((gene[gene.Count - 1].Basex - Config.XGoal) * (gene[gene.Count - 1].Basex - Config.XGoal) + (gene[gene.Count - 1].Basey - Config.YGoal) * (gene[gene.Count - 1].Basey - Config.YGoal));

            }
        }
        public void fcalc(int x, int y, Field field)
        {
            // double div;
            // div = time /1000;
            fitness = 0;
            for (int i = 0; i < Config.Populationsize; i++)
            {
                double kx;
                double ky;
                double tx;
                double ty;
                double t;
                double xg;
                double yg;
                tx = (double)gene[0].Basex;
                ty = (double)gene[0].Basey;
                kx = (double)x;
                ky = (double)y;
                t = Math.Abs(Math.Sqrt((kx - tx) * (kx - tx) + (ky - ty) * (ky - ty)));
                //t = t * 2;計算量を半分にしてみた
                for (int u = 1; u < t - 1; u++)//スタート地点からwaypoint1までのリスク計算
                {
                    xg = ((1 - (u / t)) * kx + (u / t) * tx);
                    yg = ((1 - (u / t)) * ky + (u / t) * ty);
                    //     if (div >= 1)
                    {
                        fitness = fitness + field.f1(xg, yg);
                    }

                    fitness = fitness + field.f2(xg, yg);
                    fitness = fitness + field.f3(xg, yg);
                    fitness = fitness + field.f4(xg, yg);
                    fitness = fitness + field.f5(xg, yg);
                    if (fitness > 0)
                    {
                        u = (int)(t - 1);
                        break;
                    }
                    if (u < 0)
                    {
                        break;
                    }
                    if (u > 100000)
                    {
                        break;
                    }
                }
                for (int j = 0; j < gene.Count; j++)//waypoint自身の評価
                {
                    //  if (div >= 1)
                    // {
                    fitness = field.f1(gene[j].Basex, gene[j].Basey) + fitness;
                    // }

                    fitness = field.f2(gene[j].Basex, gene[j].Basey) + fitness;
                    fitness = field.f3(gene[j].Basex, gene[j].Basey) + fitness;
                    fitness = field.f4(gene[j].Basex, gene[j].Basey) + fitness;
                    fitness = field.f5(gene[j].Basex, gene[j].Basey) + fitness;
                    if (fitness > 0)
                    {
                        j = gene.Count - 1;
                    }
                }
                if (gene.Count != 1)//一つ以上の遺伝子を保持しているかどうか
                {
                    for (int j = 0; j < gene.Count - 1; j++)
                    {
                        kx = (double)gene[j].Basex;
                        ky = (double)gene[j].Basey;
                        tx = (double)gene[j + 1].Basex;
                        ty = (double)gene[j + 1].Basey;
                        t = Math.Sqrt((kx - tx) * (kx - tx) + (ky - ty) * (ky - ty));
                        //t = t * 2;計算量を半分にしてみた
                        for (int v = 0; v < t; v++)
                        {

                            xg = ((1 - (v / t)) * kx + (v / t) * tx);
                            yg = ((1 - (v / t)) * ky + (v / t) * ty);
                            // if (div <10)
                            //       {
                            fitness = fitness + field.f1(xg, yg);
                            //     }
                            fitness = field.f2(xg, yg) + fitness;
                            fitness = field.f3(xg, yg) + fitness;
                            fitness = field.f4(xg, yg) + fitness;
                            fitness = field.f5(xg, yg) + fitness;
                            if (fitness > 0)
                            {
                                v = (int)t;
                            }
                            if (v < 0)
                            {
                                break;
                            }
                            if (v > 100000)
                            {
                                break;
                            }
                        }
                    }
                }
                kx = (double)gene[(gene.Count) - 1].Basex;
                ky = (double)gene[(gene.Count) - 1].Basey;
                tx = Config.XGoal;//ゴール
                ty = Config.YGoal;//ゴール
                t = Math.Sqrt((kx - tx) * (kx - tx) + (ky - ty) * (ky - ty));
                t = t * 2;
                for (int v = 1; v < t - 1; v++)
                {
                    xg = ((1 - (v / t)) * kx + (v / t) * tx);
                    yg = ((1 - (v / t)) * ky + (v / t) * ty);

                    //  if (div >= 1)
                    //{
                    fitness = fitness + field.f1(xg, yg);
                    //}
                    fitness = field.f2(xg, yg) + fitness;
                    fitness = field.f3(xg, yg) + fitness;
                    fitness = field.f4(xg, yg) + fitness;
                    fitness = field.f5(xg, yg) + fitness;
                    if (fitness > 0)
                    {
                        v = (int)(t - 1);
                        break;
                    }
                    if (v < 0)
                    {
                        break;
                    }
                    if (v > 100000)
                    {
                        break;
                    }

                }
            }
            if (fitness > 0)
            {
                fdis = 1 / (distance * 5);
            }
            else
            {
                fdis = 1 / Math.Abs(distance);
            }
        }
        public int od(int x, int y)
        {
            double dist = 0;
            int c = 0;
            for (int i = 0; i < gene.Count; i++)
            {

                if (gene.Count == 0)
                {
                    dist = Math.Sqrt((x - Config.XGoal) * (x - Config.XGoal) + (y - Config.YGoal) * (y - Config.YGoal));
                    c = 0;
                }
                else if (gene.Count == 1)
                {
                    dist = Math.Sqrt((gene[i].Basex - x) * (gene[i].Basex - x) + (gene[i].Basey - y) * (gene[i].Basey - y));

                    c = i;


                    if (dist < Math.Sqrt((gene[gene.Count - 1].Basex - Config.XGoal) * (gene[gene.Count - 1].Basex - Config.XGoal) + (gene[gene.Count - 1].Basey - Config.YGoal) * (gene[gene.Count - 1].Basey - Config.YGoal)))
                    {
                        dist = Math.Sqrt((gene[gene.Count - 1].Basex - Config.XGoal) * (gene[gene.Count - 1].Basex - Config.XGoal) + (gene[gene.Count - 1].Basey - Config.YGoal) * (gene[gene.Count - 1].Basey - Config.YGoal));
                        c = gene.Count;
                    }

                }
                else
                {
                    if (i == 0)
                    {
                        dist = Math.Sqrt((gene[i].Basex - x) * (gene[i].Basex - x) + (gene[i].Basey - y) * (gene[i].Basey - y));

                        c = i;
                    }
                    else if (dist < Math.Sqrt((gene[i].Basex - gene[i - 1].Basex) * (gene[i].Basex - gene[i - 1].Basex) + (gene[i].Basey - gene[i - 1].Basey) * (gene[i].Basey - gene[i - 1].Basey)) && i > 0)
                    {
                        dist = Math.Sqrt((gene[i].Basex - gene[i - 1].Basex) * (gene[i].Basex - gene[i - 1].Basex) + (gene[i].Basey - gene[i - 1].Basey) * (gene[i].Basey - gene[i - 1].Basey));
                        c = i;
                    }

                    else if (dist < Math.Sqrt((gene[gene.Count - 1].Basex - Config.XGoal) * (gene[gene.Count - 1].Basex - Config.XGoal) + (gene[gene.Count - 1].Basey - Config.YGoal) * (gene[gene.Count - 1].Basey - Config.YGoal)) && i == gene.Count - 1)
                    {
                        dist = Math.Sqrt((gene[gene.Count - 1].Basex - Config.XGoal) * (gene[gene.Count - 1].Basex - Config.XGoal) + (gene[gene.Count - 1].Basey - Config.YGoal) * (gene[gene.Count - 1].Basey - Config.YGoal));
                        c = gene.Count;
                    }

                }

            }
            return c;
        }

        public void calCD(double right, double left, double max, double min, Config config)
        {
            if (this.crowdingDistance != config.Infinity)
            {
                this.crowdingDistance += (double)(right - left) / (double)(max - min);
            }
        }//CrowdingDistanceを計算する
        /*
         * 支配関係を調べる.
         * indを支配していたら1を返す.
         * indに支配されていたら-1を返す.
         * 支配関係になかったら(同じFrontなら)0を返す.
         */
        public int CompareTo(Individual ind)
        {
            if (this.fdis > ind.fdis && this.sunev > ind.sunev)
            {
                return 1;
            }
            else if (this.fdis < ind.fdis && this.sunev < ind.sunev)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
        /*
         * indとprofit,crowding distanceが同じなら,
         * true, そうでないならfalseを返す.
         */
        public bool isSameIndividual(Individual ind)
        {
            if (this.distance == ind.distance)
            {
                if (this.sunev == ind.sunev)
                {
                    if (this.crowdingDistance == ind.crowdingDistance)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public void ReGene(double d)//ゴールした航空機に対してGeneを再構成してあげる Initial[]のなかの値を使ってね
        {

            Redis(d);


        }
        public void SearchFcalc(Field field, double s)
        {
            // double div;
            // div = time /1000;
            fitness = 0;
            for (int i = 0; i < Config.Populationsize; i++)
            {
                double kx;
                double ky;
                double tx;
                double ty;
                double t;
                double xg;
                double yg;
                tx = (double)gene[0].Basex;
                ty = (double)gene[0].Basey;
                kx = (double)Config.Inix;
                ky = (double)Config.Iniy;
                t = Math.Sqrt((kx - tx) * (kx - tx) + (ky - ty) * (ky - ty));
                t = t * 2;
                for (int u = 1; u < t - 1; u++)//スタート地点からwaypoint1までのリスク計算
                {
                    xg = ((1 - (u / t)) * kx + (u / t) * tx);
                    yg = ((1 - (u / t)) * ky + (u / t) * ty);
                    //     if (div >= 1)
                    {
                        fitness = fitness + field.f1(xg, yg);
                    }
                    fitness = fitness + field.f2(xg, yg);
                    fitness = fitness + field.f3(xg, yg);
                    fitness = fitness + field.f4(xg, yg);
                    fitness = fitness + field.f5(xg, yg);
                }
                for (int j = 0; j < gene.Count; j++)//waypoint自身の評価
                {
                    //  if (div >= 1)
                    // {
                    fitness = field.f1(gene[j].Basex, gene[j].Basey) + fitness;
                    // }
                    fitness = field.f2(gene[j].Basex, gene[j].Basey) + fitness;
                    fitness = field.f3(gene[j].Basex, gene[j].Basey) + fitness;
                    fitness = field.f4(gene[j].Basex, gene[j].Basey) + fitness;
                    fitness = field.f5(gene[j].Basex, gene[j].Basey) + fitness;
                }
                if (gene.Count != 1)//一つ以上の遺伝子を保持しているかどうか
                {
                    for (int j = 0; j < gene.Count - 1; j++)
                    {
                        kx = (double)gene[j].Basex;
                        ky = (double)gene[j].Basey;
                        tx = (double)gene[j + 1].Basex;
                        ty = (double)gene[j + 1].Basey;
                        t = Math.Sqrt((kx - tx) * (kx - tx) + (ky - ty) * (ky - ty));
                        t = t * 2;
                        for (int v = 0; v < t; v++)
                        {

                            xg = ((1 - (v / t)) * kx + (v / t) * tx);
                            yg = ((1 - (v / t)) * ky + (v / t) * ty);
                            // if (div <10)
                            //       {
                            fitness = fitness + field.f1(xg, yg);
                            //     }
                            fitness = field.f2(xg, yg) + fitness;
                            fitness = field.f3(xg, yg) + fitness;
                            fitness = field.f4(xg, yg) + fitness;
                            fitness = field.f5(xg, yg) + fitness;
                        }
                    }
                }
                kx = (double)gene[(gene.Count) - 1].Basex;
                ky = (double)gene[(gene.Count) - 1].Basey;
                tx = Config.XGoal;//goal
                ty = Config.YGoal;
                t = Math.Sqrt((kx - tx) * (kx - tx) + (ky - ty) * (ky - ty));
                t = t * 2;
                for (int v = 1; v < t - 1; v++)
                {
                    xg = ((1 - (v / t)) * kx + (v / t) * tx);
                    yg = ((1 - (v / t)) * ky + (v / t) * ty);
                    //  if (div >= 1)
                    //{
                    fitness = fitness + field.f1(xg, yg);
                    //}
                    fitness = field.f2(xg, yg) + fitness;
                    fitness = field.f3(xg, yg) + fitness;
                    fitness = field.f4(xg, yg) + fitness;
                    fitness = field.f5(xg, yg) + fitness;
                }
            }
            if (fitness > 0)
            {
                fdis = distance * 1.5;
            }
            else
            {
                fdis = Math.Abs(s - distance);
            }

        }
        public void Redis(double d)
        {
            this.d = d;
            discount = 0;//初期化
            onedis = 0;//初期化
            if (gene.Count != 0)
            {
                if (distance - d < 0)//ゴールに到達してるから削除していいよん
                {
                    while (gene.Count == 0)
                    {
                        Gene.RemoveAt(0);
                    }

                }
                else
                {
                    redis = distance - d;
                    onedis = Math.Sqrt((gene[gene.Count - 1].Basex - Config.XGoal) * (gene[gene.Count - 1].Basex - Config.XGoal) + (gene[gene.Count - 1].Basey - Config.YGoal) * (gene[gene.Count - 1].Basey - Config.YGoal));
                    if (redis - onedis < 0)
                    {
                        for (int i = 0; i < gene.Count; i++)
                        {
                            Gene.RemoveAt(0);
                        }
                    }
                    else if (gene.Count() == 1)
                    {
                        ;
                    }
                    else
                    {
                        discount = discount + 1;
                        redis = redis - onedis;
                        for (int i = gene.Count; i < 1; i++)
                        {
                            onedis = Math.Sqrt((gene[i].Basex - gene[i - 1].Basex) * (gene[i].Basex - gene[i - 1].Basex) + (gene[i].Basey - gene[i - 1].Basey) * (gene[i].Basey - gene[i - 1].Basey));
                            if (redis - onedis < 0)
                            {
                                break;
                            }
                            else
                            {
                                redis = redis - onedis;
                                discount = discount + 1;
                            }
                        }
                        for (int i = 0; i < discount; i++)
                        {
                            Gene.RemoveAt(0);
                        }

                    }
                }
            }
        }
        public void TraceMap()
        {
            Trace.Clear();
            double kx;
            double
                ky;
            double tx;
            double ty;
            double t;
            double xg;
            double yg;
            tx = (double)gene[0].Basex;
            ty = (double)gene[0].Basey;
            kx = (double)Config.Inix;
            ky = (double)Config.Iniy;
            t = Math.Sqrt((kx - tx) * (kx - tx) + (ky - ty) * (ky - ty));
            t = t * 2;
            for (int u = 1; u <= t - 1; u++)//スタート地点からwaypoint1までのリスク計算
            {
                TrBase trbasex = new TrBase();
                xg = ((1 - (u / t)) * kx + (u / t) * tx);
                yg = ((1 - (u / t)) * ky + (u / t) * ty);
                trbasex.basex = Math.Round(xg, 2);
                trbasex.basey = Math.Round(yg, 2);
                Trace.Add(trbasex);
            }
            if (gene.Count != 1)//一つ以上の遺伝子を保持しているかどうか
            {
                for (int j = 0; j < gene.Count - 1; j++)
                {
                    kx = (double)gene[j].Basex;
                    ky = (double)gene[j].Basey;
                    tx = (double)gene[j + 1].Basex;
                    ty = (double)gene[j + 1].Basey;
                    t = Math.Sqrt((kx - tx) * (kx - tx) + (ky - ty) * (ky - ty));
                    t = t * 2;
                    for (int v = 0; v <= t - 1; v++)
                    {
                        xg = ((1 - (v / t)) * kx + (v / t) * tx);
                        yg = ((1 - (v / t)) * ky + (v / t) * ty);
                        TrBase trbasey = new TrBase();
                        trbasey.basex = Math.Round(xg, 2);
                        trbasey.basey = Math.Round(yg, 2);
                        Trace.Add(trbasey);
                    }
                }
            }
            kx = (double)gene[(gene.Count) - 1].Basex;
            ky = (double)gene[(gene.Count) - 1].Basey;
            tx = 24;
            ty = 24;
            t = Math.Sqrt((kx - tx) * (kx - tx) + (ky - ty) * (ky - ty));
            t = t * 2;
            for (int v = 1; v <= t - 1; v++)
            {
                TrBase trbasez = new TrBase();
                xg = ((1 - (v / t)) * kx + (v / t) * tx);
                yg = ((1 - (v / t)) * ky + (v / t) * ty);
                trbasez.basex = Math.Round(xg, 2);
                trbasez.basey = Math.Round(yg, 2);
                Trace.Add(trbasez);
            }

        }
        public void Subplan(int k, int x, int y, Field field, Config config)
        {
            int dis = 0;
            this.inix = x;
            this.iniy = y;
            dis = od(x, y);
            Plan(k, x, y, this, field, config, Config.Time);

        }

        public void Mutation(int x, int y, Config config, Field field)
        {
            this.config = config;
            this.inix = x;
            this.iniy = y;
            //for( int i = Config.Populationsize / 2; i < Config.Populationsize; i++ )
            //{
            for (int j = 0; j < Gene.Count; j++)
            {
                if ((config.rand.NextDouble() * 100 < 30) && Gene.Count != 1)
                {
                    Gene.RemoveAt(j);
                }
                else if ((config.rand.NextDouble() * 100) < 10)
                {
                    Gene.Add(new Base(config));
                }
                else if ((config.rand.NextDouble() * 100) < 50)
                {
                    Change(config, j);
                }
                //}


            }
            /*
			for( int j = 0; j < Config.Populationsize; j++ )
			{
				if( Gene.Count != 1 )
				{
					for( int k = 0; k < Gene.Count - 1; k++ )
					{
						if( Gene[k].Basex == Gene[k + 1].Basex && Gene[k].Basey == Gene[k + 1].Basey )
						{
							Gene.RemoveAt( k );
							k = 0; //もう一度被るかどうかみてみる
						}
						if( Gene.Count != 1 )
						{
							for( int t = k; t < Gene.Count - 1; t++ )//このif文は中間発表後のプログラムには必要ない
							{
								if( Gene[k].Basex == Gene[t + 1].Basex && Gene[k].Basey == Gene[t + 1].Basey )
								{
									Gene.RemoveAt( k );
									t = k; ; //もう一度被るかどうかみてみる
								}
							}
						}
					}
				}

				
			}*/
            dcalc(x, y);
            fcalc(x, y, field);//now
            subpathct = 0;
            if (Fitness == 0)
            {
                int k = 0;
                Subplan(k, inix, iniy, field, config);
            }
        }


        public void TraceMap(int x, int y)
        {
            Trace.Clear();
            double kx;
            double
                ky;
            double tx;
            double ty;
            double t;
            double xg;
            double yg;
            tx = (double)gene[0].Basex;
            ty = (double)gene[0].Basey;
            kx = (double)x;
            ky = (double)y;
            TrBase trbas = new TrBase();
            trbas.basex = kx;
            trbas.basey = ky;
            Trace.Add(trbas);
            t = Math.Sqrt((kx - tx) * (kx - tx) + (ky - ty) * (ky - ty));
            t = t * 2;
            for (int u = 1; u <= t; u++)//スタート地点からwaypoint1までのリスク計算
            {
                TrBase trbasex = new TrBase();
                xg = ((1 - (u / t)) * kx + (u / t) * tx);
                yg = ((1 - (u / t)) * ky + (u / t) * ty);
                trbasex.basex = Math.Round(xg, 2);
                trbasex.basey = Math.Round(yg, 2);
                Trace.Add(trbasex);
            }
            if (gene.Count != 1)//一つ以上の遺伝子を保持しているかどうか
            {
                for (int j = 0; j < gene.Count - 1; j++)
                {
                    kx = (double)gene[j].Basex;
                    ky = (double)gene[j].Basey;
                    tx = (double)gene[j + 1].Basex;
                    ty = (double)gene[j + 1].Basey;
                    t = Math.Sqrt((kx - tx) * (kx - tx) + (ky - ty) * (ky - ty));
                    t = t * 2;
                    for (int v = 0; v <= t; v++)
                    {
                        xg = ((1 - (v / t)) * kx + (v / t) * tx);
                        yg = ((1 - (v / t)) * ky + (v / t) * ty);
                        TrBase trbasey = new TrBase();
                        trbasey.basex = Math.Round(xg, 2);
                        trbasey.basey = Math.Round(yg, 2);
                        Trace.Add(trbasey);
                    }
                }
            }
            kx = (double)gene[(gene.Count) - 1].Basex;
            ky = (double)gene[(gene.Count) - 1].Basey;
            tx = 24;
            ty = 24;
            t = Math.Sqrt((kx - tx) * (kx - tx) + (ky - ty) * (ky - ty));
            t = t * 2;
            for (int v = 1; v <= t; v++)
            {
                TrBase trbasez = new TrBase();
                xg = ((1 - (v / t)) * kx + (v / t) * tx);
                yg = ((1 - (v / t)) * ky + (v / t) * ty);
                trbasez.basex = Math.Round(xg, 2);
                trbasez.basey = Math.Round(yg, 2);
                Trace.Add(trbasez);
            }

        }
    }


}
