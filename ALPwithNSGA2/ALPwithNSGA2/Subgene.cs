using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALPwithNSGA2
{
 class Subgene
 {
        double fdis;
        int time;
        public int id;
        public int subpathct;
        private double sunev;
        public int indicatorct;
        public int disct;
        private double subd;

        public double Subd
        {
            get { return subd; }
            set { subd = value; }
        }
        private List<Base> countsub = new List<Base>();

        public List<Base> Countsub
        {
            get { return countsub; }
            set { countsub = value; }
        }
        public int Indicatorct
        {
            get { return indicatorct; }
            set { indicatorct = value; }
        }
        private Disid disid;

        public Disid Disid
        {
            get { return disid; }
            set { disid = value; }
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
        public double distance;
        public double Distance
        {
            get { return distance; }
            set { distance = value; }
        }
     public double fitness;
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
        private List<Disid> ardis = new List<Disid>();

        public List<Disid> Ardis
        {
            get { return ardis; }
            set { ardis = value; }
        }

 

    
        public Subgene(int x, int y, Config config, Field field, int z)//コンストラクタ
		{
            this.Inix = x;
            this.Iniy = y;
			this.config = config;
            List<Disid> ardis = new List<Disid>();
			this.gene.Add( new Base( config ) );
			crowdingDistance = 0;
            this.indicatorct = 0;
			this.Subpathct = 0;
            dcalc(x, y);
            fcalc(x, y, field);
            List<Base> countsub = new List<Base>();
            this.countsub.Add(new Base(config));
		}
        	public Subgene( Subgene ind,Config config )
		{
			this.fitness = ind.Fitness;
			this.distance = ind.Distance;
            this.indicatorct = ind.indicatorct;
            this.subd = ind.subd;
			time = ind.Time;
			fdis = ind.Fdis;
			for( int i = 0; i < ind.gene.Count; i++ )
			{
				this.Gene.Add( new Base( ind.gene[i] ) );

			}
            for (int i = 0; i < ind.countsub.Count; i++)
            {
                this.countsub.Add(new Base(ind.countsub[i]));

            }
		    for (int i = 0; i < this.ardis.Count(); i++)
			{

                this.ardis[i].s = ind.ardis[i].s;
                this.ardis[i].id = ind.ardis[i].id;
			}

            this.subpathct = ind.subpathct;
            this.sunev = ind.Sunev;

		}//コピーコンストラクタ
            public Subgene(Individual ind ,Config config)
            {
                List<Disid> ardis = new List<Disid>();

                for (int i = 0; i < ind.gene.Count; i++)
			{
                this.gene.Add(new Base(ind.gene[i]));
			}
 
                
                this.fitness = ind.Fitness;
                this.distance = ind.Distance;
                time = ind.Time;
                fdis = ind.Fdis;
                front = ind.Front;
        
            }//コピーコンストラクタ
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
                this.fitness = 0;
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
                    kx = Math.Abs((double)this.Inix);
                    ky = Math.Abs((double)this.Iniy);
                    t = Math.Abs(Math.Sqrt((kx - tx) * (kx - tx) + (ky - ty) * (ky - ty)));
                    //t = t * 2;計算量を半分にしてみた
                    for (int u = 1; u < t - 1; u++)//スタート地点からwaypoint1までのリスク計算
                    {
                        xg = ((1 - (u / t)) * kx + (u / t) * tx);
                        yg = ((1 - (u / t)) * ky + (u / t) * ty);
                        //     if (div >= 1)
                        {
                            this.fitness = this.fitness + field.f1(xg, yg);
                        }

                        this.fitness = this.fitness + field.f2(xg, yg);
                        this.fitness = this.fitness + field.f3(xg, yg);
                        this.fitness = this.fitness + field.f4(xg, yg);
                        this.fitness = this.fitness + field.f5(xg, yg);
                        if (this.fitness > 0)
                        {
                            u = (int)(t - 1);
                            break;
                        }
                        if (u <0)
                        {
                            break;
                   
                        }
                        if (u > 10000)
                        {
                            break;
                        }
                    }
                    for (int j = 0; j < gene.Count; j++)//waypoint自身の評価
                    {
                        //  if (div >= 1)
                        // {
                        this.fitness = field.f1(gene[j].Basex, gene[j].Basey) + this.fitness;
                        // }

                        this.fitness = field.f2(gene[j].Basex, gene[j].Basey) + this.fitness;
                        this.fitness = field.f3(gene[j].Basex, gene[j].Basey) + this.fitness;
                        this.fitness = field.f4(gene[j].Basex, gene[j].Basey) + this.fitness;
                        this.fitness = field.f5(gene[j].Basex, gene[j].Basey) + this.fitness;
                        if (this.fitness > 0)
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
                                this.fitness = this.fitness + field.f1(xg, yg);
                                //     }
                                this.fitness = field.f2(xg, yg) + this.fitness;
                                this.fitness = field.f3(xg, yg) + this.fitness;
                                this.fitness = field.f4(xg, yg) + this.fitness;
                                this.fitness = field.f5(xg, yg) + this.fitness;
                                if (this.fitness > 0)
                                {
                                    v = (int)t;
                                }
                                if (v < 0)
                                {
                                    break;
                                }
                                if (v > 10000)
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
                        this.fitness = this.fitness + field.f1(xg, yg);
                        //}
                        this.fitness = field.f2(xg, yg) + this.fitness;
                        this.fitness = field.f3(xg, yg) + this.fitness;
                        this.fitness = field.f4(xg, yg) + this.fitness;
                        this.fitness = field.f5(xg, yg) + this.fitness;
                        if (this.fitness > 0)
                        {
                            v = (int)(t - 1);
                            break;
                        }
                        if (v <0)
                        {
                            break;
                        }
                        if (v >10000)
                        {
                            break;
                        }
                    }
                }
                if (this.fitness > 0)
                {
                    fdis = 1 / (distance * 5);
                }
                else
                {
                    fdis = 1 / Math.Abs(distance);
                }
            }
            public void ardisclear(Individual ind,int p )
            {
                for (int n = 0; n < this.ardis.Count(); n++)//初期化
                {
                    this.ardis.Clear();

                }
            }
            public void ardiscalc(Individual ind,int q)
            {

  
             
                  for (int j = 0; j < this.gene.Count(); j++)
                {
                    int label = 0;

                    for (int i = 0; i < ind.gene.Count(); i++)
                    {
                        if (ind.gene[i].basex == this.gene[j].basex && ind.gene[i].basey == this.gene[j].basey) 
                        {
                            label = 1;
                        }
                        if (ind.Nextx == this.gene[j].basex && ind.Nexty == this.gene[j].basey)
                        {
                            label = 1;
                        }
                        if (label != 1)//条件を満たすので計算 && ind.gene.Count() == i+1 
                        {
                            this.ardissum(j,ind);
                        }
                        label = 0;
                    }
                   
                }
            }
            public void ardissum(int j,Individual ind)//相対距離の計算
            {
                int label = 0;
                double dis = 0;
                double dissum = 0;
                disct = 0;

                int count = 0;
                for (int i = 0; i < ind.subgene.Count(); i++)
                {
                    Disid d = new Disid();
                    dis = 0;
                    label = 0;
                    count = 0;
                    for (int m = 0; m < ind.subgene[i].gene.Count; m++)
                    {
                        
                        for (int l = 0; l < ind.gene.Count(); l++)
                        {
                            label = 0;
                            dis = 0;//初期化
                            if (ind.subgene[i].gene[m].basex == ind.gene[l].basex && ind.subgene[i].gene[m].basey == ind.gene[l].basey)
                            {
                                label = 1;
                            }
                            if (ind.Nextx == ind.subgene[i].gene[m].basex && ind.Nexty == ind.subgene[i].gene[m].basey)
                            {
                                label = 1;
                            }

                            if (label == 0)
                            {
                                dis = Math.Sqrt((double)((ind.subgene[i].gene[m].basex - this.gene[j].basex) * (ind.subgene[i].gene[m].basex - this.gene[j].basex) + (ind.subgene[i].gene[m].basey - this.gene[j].basey) * (ind.subgene[i].gene[m].basey - this.gene[j].basey))) + dis;
                                count = count + 1;
                            }

                            if (l == ind.gene.Count() - 1)
                            {
                                if (count == 0)
                                {
                                    dis = 10000;
                                }
                                else
                                {
                                    dis = dis / (double)count;  //個々で各subgeneとの関係を計算している！
                                }
                            }
                            if (dis > 0 && dis < 10000)
                            {
                                this.disct = this.disct + 1;
                            }

                        }
                    }
              
                    d.s = dis;
                    d.id = i;
                    ardis.Add(d);//中に入れるんだな
                }
            }
                public void subdis(Individual ind,int c) //subgene　Cについての距離の評価をまとめたもの
                {
                    subd = 0;//初期化
                    double d = 0;
                    for (int i = 0; i < ind.subgene.Count; i++)
                    {
                        if (i != c)
                        {
                            for (int k = 0; k < ind.subgene[i].countsub.Count; k++)
			    {
			 
			    for (int m = 0; m < this.countsub.Count; m++)
			    {
                    d = Math.Sqrt((Math.Pow(ind.subgene[i].countsub[k].basex - this.countsub[m].basex, 2)) + (Math.Pow(ind.subgene[i].countsub[k].basey - this.countsub[m].basey, 2))) + d;
			    }

                d = d / this.countsub.Count;//一つのsubgeneに関しての距離の平均を求めいている。
   
                        
                }
                            Disid dist = new Disid();

                            dist.s = d;
                            dist.id = i;  //Subgene iの評価
                            ardis.Add(dist);//中に入れるんだな 
                        }
                         d = 0;
                    }
                }

    }
}
