using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALPwithNSGA2
{
	public abstract class Field
	{
		//コンストラクタ
		protected Field()
		{

		}

		//サブクラスで実装するメソッド
		//コスト計算関数 必要な分だけ宣言する
		abstract public double f1( double x, double y );  //コスト計算関数 その1
		abstract public double f2( double x, double y );  //コスト計算関数 その2
		abstract public double f3( double x, double y );  //コスト計算関数 その3
		abstract public double f4( double x, double y );  //コスト計算関数 その4
		abstract public double f5( double x, double y );  //コスト計算関数 その5

	}
	//実際のフィールド その1
	class Field1 : Field
	{
        public Field1()
        {

        }

        //コスト計算関数 その1
        public override double f1(double x, double y)
        {
            if (10 < x && x < 12 && 10 < y && y < 14)
            {
                return 0;
            }

            return 0;
        }

        //コスト計算関数 その2
        public override double f2(double x, double y)
        {
            //return (x - 5) * (x - 5) + (y - 5) * (y - 5) - 5;
            if ( x < 5 && 20 < y )
            {
                return 1000;
            }
            return 0;
        }

        //コスト計算関数 その3
        public override double f3(double x, double y)
        {
            if (20 < x && y < 8)
            {
                return 1000;
            }
            return 0;
        }
        //コスト計算関数 その4
        public override double f4(double x, double y)
        {
            //return (x - 5) * (x - 5) + (y - 5) * (y - 5) - 5;
            if (x == 0 && y == 0)
            {
                return 10000;
            }
            return 0;
        }
        //コスト計算関数 その5
        public override double f5(double x, double y)
        {
            //return (x - 5) * (x - 5) + (y - 5) * (y - 5) - 5;
            if (x == 24 && y == 24)
            {
                return 10000;
            }
            return 0;
        }
	}

	//実際のフィールド その2
	class Field2 : Field
	{
        //コンストラクタ
        public Field2() {

        }

        //コスト計算関数 その1
        public override double f1(double x, double y) {
            if (5 <= x && x <= 8 && 15 <= y && y <= 20) {
                return 1000;
            }

            return 0;
        }

        //コスト計算関数 その2
        public override double f2(double x, double y) {
            if (8<= x && x <= 15 && 8 <= y && y <= 18) {
                return 1000;
            }
            return 0;
        }

        //コスト計算関数 その3
        public override double f3(double x, double y) {
            //return (x - 5) * (x - 5) + (y - 5) * (y - 5) - 5;
            if (15 <= x && x <= 20 && 5 <= y && y <= 16) {
                return 1000;
            }
            return 0;
        }
        //コスト計算関数 その4
        public override double f4(double x, double y)
        {
            //return (x - 5) * (x - 5) + (y - 5) * (y - 5) - 5;
            if (x == 0 && y == 0)
            {
                return 10000;
            }
            return 0;
        }
        //コスト計算関数 その5
        public override double f5(double x, double y)
        {
            //return (x - 5) * (x - 5) + (y - 5) * (y - 5) - 5;
            if (x == 24 && y == 24)
            {
                return 10000;
            }
            return 0;
        }
	}

	//実際のフィールド その3
	class Field3 : Field
	{
		//コンストラクタ
		public Field3()
		{

		}

		//コスト計算関数 その1
		public override double f1( double x, double y )
		{
			if( x < 8 && 10 < y )
			{
				return 1000;
			}

			return 0;
		}

		//コスト計算関数 その2
		public override double f2( double x, double y )
		{
			//return (x - 5) * (x - 5) + (y - 5) * (y - 5) - 5;
			if( 6 < x && x < 18 && 14 < y && y < 18 )
			{
				return 1000;
			}
			return 0;
		}

		//コスト計算関数 その3
		public override double f3( double x, double y )
		{
			if( 16 < x && y < 8 )
			{
				return 1000;
			}
			return 0;
		}
		//コスト計算関数 その4
		public override double f4( double x, double y )
		{
			//return (x - 5) * (x - 5) + (y - 5) * (y - 5) - 5;
			if( x == 0 && y == 0 )
			{
				return 10000;
			}
			return 0;
		}
		//コスト計算関数 その5
		public override double f5( double x, double y )
		{
			//return (x - 5) * (x - 5) + (y - 5) * (y - 5) - 5;
			if( x == 24 && y == 24 )
			{
				return 10000;
			}
			return 0;
		}
	}

	//実際のフィールド その4
	class Field4 : Field
	{
		//コンストラクタ
		public Field4()
		{

		}

		//コスト計算関数 その1
		public override double f1( double x, double y )
		{

			if( x < 8 && 8 < y )
			{
				return 1000;
				//1000 * Math.Abs(Math.Sin(angle));
			}

			return 0;
		}

		//コスト計算関数 その2
		public override double f2( double x, double y )
		{
			//return (x - 5) * (x - 5) + (y - 5) * (y - 5) - 5;

			if( 8 < x && x < 15 && 8 < y && y < 16 )
			{
				return 1000;
			}
			return 0;
		}

		//コスト計算関数 その3
		public override double f3( double x, double y )
		{
			if( 16 < x && y < 15 )
			{
				return 1000;
			}
			return 0;
		}
		//コスト計算関数 その4
		public override double f4( double x, double y )
		{
			//return (x - 5) * (x - 5) + (y - 5) * (y - 5) - 5;
			if( x == 0 && y == 0 )
			{
				return 10000;
			}
			return 0;
		}
		//コスト計算関数 その5
		public override double f5( double x, double y )
		{
			//return (x - 5) * (x - 5) + (y - 5) * (y - 5) - 5;
			if( x == 24 && y == 24 )
			{
				return 10000;
			}
			return 0;
		}
	}
}
