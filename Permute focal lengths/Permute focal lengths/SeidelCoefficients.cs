//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Permute_focal_lengths
//{
    
//   public class SeidelCoefficients
//    {
//       public double primaryWavelength { get; set; }
//        public double w040 { get; set; }
//        public double w131 { get; set; }
//        public double w222 { get; set; }
//        public double w220 { get; set; }
//        public double w311 { get; set; }
//        public double w020 { get; set; }
//        public double w111 { get; set; }
//        //A=njb*im
//        public double[] A { get; set; }

//        //B=njb*ic
//        public double[] B { get; set; }

//        //del = um/nj-umb/njb
//        public double[] del { get; set; }

//        //deln=(nf-nc)/nd
//        public double[] deln { get; set; }

//        //delnb
//        public double[] delnb { get; set; }

//        //LG=nj*(uc*ym-yc*um)
//        public double[] LG { get; set; }

//        //P=-(nk-nkb)/(nk*nkb*radk)
//        public double[] P { get; set; }

//        public double[] Q { get; set; }
//        public double[,] MarginalRayHA { get; set; }
//        public double[,] ChiefRayHA { get; set; }
//        public double focalLengthDiff { get; set; }

//        public SeidelCoefficients()
//        {
//            w040 = 0;
//            w131 = 0;
//            w222 = 0;
//            w220 = 0;
//            w311 = 0;
//            w020 = 0;
//            w111 = 0;
           
//        }
//        public SeidelCoefficients(int surfaceNumber)
//        {
//            A = new double[surfaceNumber];
//            B = new double[surfaceNumber];
//            del = new double[surfaceNumber];
//            deln = new double[surfaceNumber];
//            delnb = new double[surfaceNumber];
//            LG = new double[surfaceNumber];
//            P = new double[surfaceNumber];
//            Q = new double[surfaceNumber];
//            MarginalRayHA = new double[surfaceNumber, 2];
//            ChiefRayHA = new double[surfaceNumber, 2];
//            primaryWavelength = primaryWavelength;
//        }
//        public SeidelCoefficients seidelCalculation(Element[] SysElement, double[,] MarginalRayHA, double[,] ChiefRayHA, OpticalSystem sys)
//        {
//            // ******************************
//            // Calculate Seidel coefficients.
//            // Use the middle refractive index in the database as the primary wavelength.
//            // Refer to the macro in "Introduction to aberrations in optical imaging systems"
//            // Return Seidel coefficients.
//            // ******************************
//            SeidelCoefficients Seidel = new SeidelCoefficients();
//            int TotalElementNumber = SysElement.GetLength(0);
//            double SurfaceRadius = 0;
//            int SurfaceNumber = 0;

//            // calculate surface number
//            for (int i = 0; i < TotalElementNumber; i = i + 1)
//            {
//                SurfaceNumber = SurfaceNumber + SysElement[i].ElementNumber + 1;
//            }

//            Seidel = new SeidelCoefficients(SurfaceNumber);
//            Seidel.primaryWavelength = sys.primaryWavelength;

//            int SurfCount = 0;

//            // Calculate Seidel coefficients.
//            for (int i = 0; i < TotalElementNumber; i = i + 1)
//            {
//                for (int j = 0; j < SysElement[i].ElementNumber + 1; j = j + 1)
//                {
//                    Seidel.MarginalRayHA[j + SurfCount, 0] = MarginalRayHA[j + SurfCount, 0];
//                    Seidel.MarginalRayHA[j + SurfCount, 1] = MarginalRayHA[j + SurfCount, 1];
//                    Seidel.ChiefRayHA[j + SurfCount, 0] = ChiefRayHA[j + SurfCount, 0];
//                    Seidel.ChiefRayHA[j + SurfCount, 1] = ChiefRayHA[j + SurfCount, 1];
//                    Seidel.LG[j + SurfCount] = -SysElement[i].refractiveIndex[j, 1] * (MarginalRayHA[SurfCount + j, 1] * ChiefRayHA[SurfCount + j, 0] - ChiefRayHA[SurfCount + j, 1] * MarginalRayHA[SurfCount + j, 0]);
//                    if (SysElement[i].Radius[j] == 0) // radius check
//                    {
//                        SurfaceRadius = 100000000000;
//                    }
//                    else
//                    {
//                        SurfaceRadius = SysElement[i].Radius[j];
//                    }
//                    if (j == 0)
//                    {
//                        if (i == 0)
//                        {
//                            // first surface of the system 
//                            // njb =1, umb=0, 
//                            //P=-(nk-nkb)/(nk*nkb*radk)
//                            Seidel.A[j + SurfCount] = 1 * (MarginalRayHA[SurfCount + j, 0] / SurfaceRadius + 0);
//                            Seidel.del[j + SurfCount] = MarginalRayHA[SurfCount + j, 1] / SysElement[i].refractiveIndex[j, 1];
//                            Seidel.B[j + SurfCount] = 1 * (ChiefRayHA[SurfCount + j, 0] / SurfaceRadius + Math.Tan(sys.fieldOfView / 180 * Math.PI));
//                            Seidel.P[j + SurfCount] = -(SysElement[i].refractiveIndex[j, 1] - 1) / (SysElement[i].refractiveIndex[j, 1] * 1.0 * SurfaceRadius);
//                            Seidel.Q[j + SurfCount] = -(SysElement[i].refractiveIndex[j, 1] * SysElement[i].refractiveIndex[j, 1] - 1 * 1) / (SysElement[i].refractiveIndex[j, 1] * SysElement[i].refractiveIndex[j, 1] * 1 * 1);
//                        }
//                        else
//                        {
//                            // other first surface of each lens
//                            // njb =1

//                            Seidel.A[j + SurfCount] = 1 * (MarginalRayHA[SurfCount + j, 0] / SurfaceRadius + MarginalRayHA[SurfCount + j - 1, 1]);
//                            Seidel.B[j + SurfCount] = 1 * (ChiefRayHA[SurfCount + j, 0] / SurfaceRadius + ChiefRayHA[SurfCount + j - 1, 1]);
//                            Seidel.del[j + SurfCount] = MarginalRayHA[SurfCount + j, 1] / SysElement[i].refractiveIndex[j, 1] - MarginalRayHA[SurfCount + j - 1, 1];
//                            Seidel.P[j + SurfCount] = -(SysElement[i].refractiveIndex[j, 1] - 1) / (SysElement[i].refractiveIndex[j, 1] * 1.0 * SurfaceRadius);
//                            Seidel.Q[j + SurfCount] = -(SysElement[i].refractiveIndex[j, 1] * SysElement[i].refractiveIndex[j, 1] - 1 * 1) / (SysElement[i].refractiveIndex[j, 1] * SysElement[i].refractiveIndex[j, 1] * 1 * 1);
//                        }
//                        Seidel.deln[j + SurfCount] = (SysElement[i].refractiveIndex[j, 0] - SysElement[i].refractiveIndex[j, 2]) / SysElement[i].refractiveIndex[j, 1];
//                        Seidel.delnb[j + SurfCount] = 0;


//                    }
//                    else
//                    {

//                        Seidel.A[j + SurfCount] = SysElement[i].refractiveIndex[j - 1, 1] * (MarginalRayHA[SurfCount + j, 0] / SurfaceRadius + MarginalRayHA[SurfCount + j - 1, 1]);
//                        Seidel.B[j + SurfCount] = SysElement[i].refractiveIndex[j - 1, 1] * (ChiefRayHA[SurfCount + j, 0] / SurfaceRadius + ChiefRayHA[SurfCount + j - 1, 1]);
//                        Seidel.del[j + SurfCount] = MarginalRayHA[SurfCount + j, 1] / SysElement[i].refractiveIndex[j, 1] - MarginalRayHA[SurfCount + j - 1, 1] / SysElement[i].refractiveIndex[j - 1, 1];
//                        Seidel.deln[j + SurfCount] = (SysElement[i].refractiveIndex[j, 0] - SysElement[i].refractiveIndex[j, 2]) / SysElement[i].refractiveIndex[j, 1];
//                        Seidel.delnb[j + SurfCount] = (SysElement[i].refractiveIndex[j - 1, 0] - SysElement[i].refractiveIndex[j - 1, 2]) / SysElement[i].refractiveIndex[j - 1, 1];
//                        Seidel.P[j + SurfCount] = -(SysElement[i].refractiveIndex[j, 1] - SysElement[i].refractiveIndex[j - 1, 1]) / (SysElement[i].refractiveIndex[j, 1] * SysElement[i].refractiveIndex[j - 1, 1] * SurfaceRadius);
//                        Seidel.Q[j + SurfCount] = -(SysElement[i].refractiveIndex[j, 1] * SysElement[i].refractiveIndex[j, 1] - SysElement[i].refractiveIndex[j - 1, 1] * SysElement[i].refractiveIndex[j - 1, 1]) / (SysElement[i].refractiveIndex[j, 1] * SysElement[i].refractiveIndex[j, 1] * SysElement[i].refractiveIndex[j - 1, 1] * SysElement[i].refractiveIndex[j - 1, 1]);
//                    }
//                }
//                SurfCount = SurfCount + (SysElement[i].ElementNumber + 1);
//            }
//            return Seidel;
//        }
//        public double SPHA()
//        {
//            //w040=(-1/8)*A*A*del*ym
//            double SPHA = 0;
//            double tmpSPHA = 0;
//            for (int i = 0; i < this.A.GetLength(0); i = i + 1)
//            {
//                tmpSPHA = +(-1.0 / 8.0) * this.A[i] * this.A[i] * this.del[i] * this.MarginalRayHA[i, 0];
//                SPHA = SPHA + tmpSPHA;
//            }
//            this.w040 = SPHA / this.primaryWavelength;
//            return this.w040;
//        }
//        public double COMA()
//        {
//            //w131=(-1/2)*A*B*del*ym
//            double COMA = 0;
//            double tmpCOMA = 0;
//            for (int i = 0; i < this.A.GetLength(0); i = i + 1)
//            {
//                tmpCOMA = -1.0 / 2.0 * this.A[i] * this.B[i] * this.del[i] * this.MarginalRayHA[i, 0];
//                COMA = COMA + tmpCOMA;
//            }
//            this.w131 = COMA / this.primaryWavelength;
//            return this.w131;

//        }
//        public double ASTI()
//        {
//            //w222=(-1/2)*B*B*del*ym
//            double ASTI = 0;
//            double tmpASTI = 0;
//            for (int i = 0; i < this.A.GetLength(0); i = i + 1)
//            {
//                tmpASTI = -1.0 / 2.0 * this.B[i] * this.B[i] * this.del[i] * this.MarginalRayHA[i, 0];
//                ASTI = ASTI + tmpASTI;
//            }
//            this.w222 = ASTI / this.primaryWavelength;
//            return this.w222;
//        }
//        public double DIST()
//        {
//            //w311=-(1/2)*(B*B*B*ym*Q-B*(LG+B*ym)*yc*P)
//            double DIST = 0;
//            double tmpDIST = 0;
//            for (int i = 0; i < this.A.GetLength(0); i = i + 1)
//            {
//                tmpDIST = -1.0 / 2.0 * (this.B[i] * this.B[i] * this.B[i] * this.MarginalRayHA[i, 0] * this.Q[i] - this.B[i] * (this.LG[i] * this.B[i] * this.MarginalRayHA[i, 0]) * this.ChiefRayHA[i, 0] * this.P[i]);
//                DIST = DIST + tmpDIST;
//            }
//            this.w311 = DIST / this.primaryWavelength;
//            return this.w311;

//        }
//        public double AXCL()
//        {
//            //w020L=-(1/2)*A*ym*(deln-delnb)
//            double AXCL = 0;
//            double tmpAXCL = 0;
//            for (int i = 0; i < this.A.GetLength(0); i = i + 1)
//            {
//                tmpAXCL = -1.0 / 2.0 * this.A[i] * this.MarginalRayHA[i, 0] * (this.deln[i] - this.delnb[i]);
//                AXCL = AXCL + tmpAXCL;
//            }
//            this.w020 = AXCL / this.primaryWavelength;
//            return this.w020;

//        }
//        public double LONA()
//        {
//            //w111T=-B*ym*(deln-delnb)
//            double LONA = 0;
//            double tmpLONA = 0;
//            for (int i = 0; i < this.A.GetLength(0); i = i + 1)
//            {
//                tmpLONA = -this.B[i] * this.MarginalRayHA[i, 0] * (this.deln[i] - this.delnb[i]);
//                LONA = LONA + tmpLONA;
//            }
//            this.w111 = LONA / this.primaryWavelength;
//            return this.w111;
//        }
//        public double PETC()
//        {
//            //W220=-(1/4)*LG*LG*P+w222/2
//            //w222 = -(1/2)*B*B*delm*ym
//            double PETC = 0;
//            double tmpPETC = 0;
//            for (int i = 0; i < this.A.GetLength(0); i = i + 1)
//            {
//                tmpPETC = -1 / 4.0 * this.LG[i] * this.LG[i] * this.P[i]; ;
//                PETC = PETC + tmpPETC;
//            }
//            this.w220 = PETC / this.primaryWavelength;
//            return this.w220;
//        }
//        public double SUM()
//        {
//            return w040 * w040 + w131 * w131 + w222 * w222 + w220 * w220 + w020 * w020 + w111 * w111;
//        }

//    }
//}
