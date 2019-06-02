using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace 水准网平差实验1
{    
   
    class Level
    {
        
        int m;     //高差总数
        int n;      //总点数
        int n0;     //已知点数
        int []StartP=new int[100];
        int[] EndP = new int[100];
        string[] Pname = new string[100];
        double[] h = new double[100];
        double[] H = new double[100];
        double[] S = new double[100];//线路长度
        double[,] P = new double[100,100];
        double[,] BTPB = new double[100,100];
        double[] BTPL = new double[100];
        double[] dx = new double[100];
        double[] v = new double[100];
        
        public Level(int m,int n,int n0 )
        {
            this.m = m;
            this.n = n;
            this.n0 = n0;
        }

        public int GetstationName(string []name, string[] Pname)
        {
           for(int i=1;i<=n;i++)
               if(Pname[i]!=null)
               {
                   if(name[i].Equals(Pname[i])) return i;
               }         
               else
               {
                   //int len = name.Length;
                   //P[i] = new string[len + 1];
                   //String.CopyTo()

               }
           return -1;
        }


       
        public void ca_H0(double[] H, double[] h, int[] StartP, int[] EndP)
       {

           //for (int i = n0 + 1; i <= n; i++)
           //    for (int j = 1; j <= m; j++)
           //        H[i] += h[j];

           for (int i = 1; i <= m; i++)
           {
               int k1 = StartP[i];
               int k2 = EndP[i];
               H[k2] = H[k1] + h[i];
           }


           //for (int i = n0+1; i <= n; i++) H[i] = -9999.9;
           //int jj = 0;                  //用于统计要计算近似高程的点数
           //for (int ii = 1; ; ii++)
           //{
           //    for (int i = 1; i <= m; i++)
           //    {
           //        int k1 = StartP[i];
           //        int k2 = EndP[i];
           //        if (H[k2] > -9999.9 && H[k2] < -9999.0)
           //        {
           //            H[k2] = H[k1] + h[i];
           //            jj++;
           //        }
           //        else if (H[k1] < -9999.9 && H[k2] > -9999.9)
           //        {
           //            H[k1] = H[k2] - h[i];
           //            jj++;
           //        }

           //    }
           //    if (jj == n - n0) break;
           //}
       }

        public void ca_P(double[] P,double[] S)
       {
           for (int i = 1; i <= m; i++)
               for (int j = 1; j <= 1;j++ )       
                    P[i] = 1 / S[i];
       }


        public void ca_BTPB(double[] H, double[] h, int[] StartP, int[] EndP, double[,] BTPB, double[] BTPL,double []P)
       {
           int t = n;
           for (int i = 1; i <= t; i++)
               for (int j = 1; j <= t;j++ )
                   BTPB[i,j] = 0;
           for (int i = 1; i <=t; i++) BTPL[i] = 0;
          
           for(int k=1;k<=m;k++)
            {
                int i = StartP[k];
                int j = EndP[k];
                double Pk = P[k];
                double Lk = h[k] - (H[j] - H[i]);
                BTPL[i] -= Pk * Lk;
                BTPL[j] += Pk * Lk;
                BTPB[i,i] += Pk;
                BTPB[j,j] += Pk;
                BTPB[i,j] -= Pk;
                BTPB[j,i] -= Pk;
            }

       }

        public void ca_dx(double[] H, double[] dx, double[,] BTPB, double[] BTPL)
        {
            for(int i=1;i<=n;i++)
            {
                double xi = 0;
                for(int j=1;j<=n;j++)
                {
                    xi = BTPB[i,j] * BTPL[j];
                }
                dx[i] = xi;
                H[i] += xi;
            }

        }

        public double ca_V(double[] H, double[] h, int[] StartP, int[] EndP,double[]v,double[]P,double pvv)
        {
            
            for(int i=1;i<=m;i++)
            {
                int k1 = StartP[i];
                int k2 = EndP[i];
                v[i] = H[k2] - H[k1] - h[i];
                pvv += v[i] * v[i] * P[i];
            }
            return pvv;
        }

    }
}
