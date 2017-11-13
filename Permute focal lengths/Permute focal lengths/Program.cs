using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Permute_focal_lengths
{
    class Program
    {
        //----------------------
        public static double InputMax, InputMin;
        public static double MaxF1, MaxF2, MaxF3, MinF1, MinF2, MinF3, Maxa1, Maxa2, Maxb1, Maxb2, MaxMx, MaxMy, MaxMxratioMy;
        public static double Maxd1, Maxd2, Input;
        public static IList<double> MaxtrackList = new List<double>();
        public static IList<double> F1List = new List<double>();
        public static IList<double> F2List = new List<double>();
        public static IList<double> F3List = new List<double>();
        public static double[] Mx = new double[27];
        public static double[] My = new double[27];
        public static double[] a1 = new double[27];
        public static double[] a2 = new double[27];
        public static double[] b1 = new double[27];
        public static double[] b2 = new double[27];
        public static double[] MxratioMy = new double[27];
        public static double[] Maxtrack = new double[27];
        public static double[] Maxlengths = new double[27];
        public static double[] focallength1 = new double[] { 130, 140, 150 }; // Initialize array for focal length 1
        public static double[] focallength2 = new double[] { -19, -20, -21 }; // Initialize array for focal length 2
        public static double[] focallength3 = new double[] { 70, 1, 2 }; //Initialize array for focal length 3
        public static double[] Maxtrackstore = new double[27];
        public static double[] F1store = new double[27];
        public static double[] F2store = new double[27];
        public static double[] F3store = new double[27];
        public static double[] d1forMx = new double[27];
        public static double[] d2forMx = new double[27];
        public static double[] d1forMy = new double[27];
        public static double[] d2forMy = new double[27];
        public static double[] d1forMxratioMy = new double[27];
        public static double[] d2forMxratioMy = new double[27];
        public static double[] d1forInputMax = new double[27];
        public static double[] d2forInputMax = new double[27];
        public static double[] d1forInputMin = new double[27];
        public static double[] d2forInputMin = new double[27];


        public static double perm(double[] F1, double[] F2, double[] F3)
        {
            int i, j, k;

            for (i = 0; i < F1.Length; i++)
            {

                for (j = 0; j < F2.Length; j++)
                {
                    for (k = 0; k < F3.Length; k++)
                    {

                        a1[k] = Math.Round((double)F1[i] + F2[j], 4);

                        MxratioMy[k] = Math.Round((double)F1[i] / F3[k], 4);

                        a2[k] = Math.Round((double)F2[j] + F3[k], 4);

                        b1[k] = Math.Round((double)(F1[i] * F2[j]) / F3[k], 4);

                        b2[k] = Math.Round((double)(F2[j] * F3[k]) / F1[i], 4);

                        Mx[k] = Math.Round((double)-a2[k] / b2[k], 4);

                        My[k] = Math.Round((double)-b1[k] / a1[k], 4);

                        if ((Mx[k] > MxratioMy[k]) && (MxratioMy[k] > My[k]) && (InputMax <= Mx[k]) && (InputMin >= My[k]) && (InputMax > InputMin) && (InputMin < InputMax))
                        {
                            F1List.Add(F1[i]);

                            F2List.Add(F2[j]);

                            F3List.Add(F3[k]);

                            Maxtrack[k] = Math.Round((double)F1[i] + 2 * F2[j] + F3[k] + (F2[j] * (((F3[k] * MxratioMy[k]) / F1[i]) + F1[i] / (F3[k] * MxratioMy[k]))), 4);

                            MaxtrackList.Add(Maxtrack[k]);

                        }

                        else

                            if ((MxratioMy[k] > Mx[k]) || (My[k] > MxratioMy[k]) || (InputMax > Mx[k]) || (InputMin < My[k]) || (InputMax < InputMin))
                            {

                                // Do nothing here just ignore the values

                            }
                    }

                }
            }

            for(int d = 0; d < MxratioMy.Length; d++)
            {
                Console.WriteLine("MxratioMy = {0} \n", MxratioMy[d]);
            }

            //check for emptiness of a List for no suitable combination of focal length

            if (!MaxtrackList.Any())
            {
                Console.WriteLine("There is no suitable focal length in database for this configuration \n");

                return 0;
            }

            else

                // Display Tracklengths only once last element in the list is reached

                for (int p = 0; p < MaxtrackList.Count; p++)
                {

                    if (p == MaxtrackList.Count - 1)
                    {
                        // Get Maximum and Minimum value of Tracklength with respective Focal lengths  

                        Console.WriteLine("Maxtrackvalue = {0} with F1 = {1}, F2 = {2} and F3 = {3} \n", MaxtrackList.Max(), F1List[MaxtrackList.IndexOf(MaxtrackList.Max())], F2List[MaxtrackList.IndexOf(MaxtrackList.Max())], F3List[MaxtrackList.IndexOf(MaxtrackList.Max())]);

                        Console.WriteLine("Mintrackvalue = {0} with F1 = {1}, F2 = {2} and F3 = {3} \n", MaxtrackList.Min(), F1List[MaxtrackList.IndexOf(MaxtrackList.Min())], F2List[MaxtrackList.IndexOf(MaxtrackList.Min())], F3List[MaxtrackList.IndexOf(MaxtrackList.Min())]);

                    }
                }

            Console.WriteLine("\n");

            return userinputs(F1, F2, F3);
        }

        public static double userinputs(double[] F1, double[] F2, double[] F3)
        {
            Console.WriteLine("\n");

            Console.WriteLine("Choose Tracklength \n");

            Console.WriteLine("Press (a) for Maxtrack and (b) for Mintrack\n ");

            string choose = Console.ReadLine();

            Console.WriteLine("\n");


            switch (choose)
            {
                // Choosing Maxtracklength option for focallengths

                case "a":

                    return Maxtractcal(F1, F2, F3);

                case "b":

                    return Mintrackcal(F1, F2, F3);

                default:

                    Console.WriteLine("Please choose from (a) or (b) \n");

                    break;

            }

            return userinputs(F1, F2, F3);


        }

        public static double Maxtractcal(double[] F1, double[] F2, double[] F3)
        {
            int a = 1;

            if (a == 1)
            {
                Console.WriteLine("Focallength choosed with Maxtrack = {0} are: F1 = {1}, F2 = {2}, F3 = {3} \n", MaxtrackList.Max(), F1List[MaxtrackList.IndexOf(MaxtrackList.Max())], F2List[MaxtrackList.IndexOf(MaxtrackList.Max())], F3List[MaxtrackList.IndexOf(MaxtrackList.Max())]);

                Console.WriteLine("Please choose values between or equal to Max and Min Magnification \n");

                a = a + 1;
            }

            MaxF1 = F1List[MaxtrackList.IndexOf(MaxtrackList.Max())];

            MaxF2 = F2List[MaxtrackList.IndexOf(MaxtrackList.Max())];

            MaxF3 = F3List[MaxtrackList.IndexOf(MaxtrackList.Max())];


            Maxa1 = Math.Round((double)MaxF1 + MaxF2, 4);

            MaxMxratioMy = Math.Round((double)MaxF1 / MaxF3, 4);

            Maxa2 = Math.Round((double)MaxF2 + MaxF3, 4);

            Maxb1 = Math.Round((double)(MaxF1 * MaxF2 / MaxF3), 4);

            Maxb2 = Math.Round((double)(MaxF2 * MaxF3) / MaxF1, 4);

            MaxMx = Math.Round((double)-Maxa2 / Maxb2, 4);

            MaxMy = Math.Round((double)-Maxb1 / Maxa1, 4);


            while (true)
            {

                Console.WriteLine("Enter Magnification upto 4 decimal point  \n");

                // Check for value other than numerics

                while (!Double.TryParse(Console.ReadLine(), out Input))
                {

                    Console.WriteLine("Please enter numeric value \n");

                    Console.WriteLine("Enter Magnification upto 4 decimal point  \n");
                }


                Console.WriteLine("\n");


                if ((MaxMx > MaxMxratioMy) && (MaxMxratioMy > MaxMy) && (Input <= MaxMx) && (Input >= MaxMy) && (Input <= InputMax) && (Input >= InputMin))
                {

                    Console.WriteLine("Conditions satified \n");


                    //Calculate d1 and d2 for the Input Magnification

                    Maxd1 = Math.Round((double)MaxF1 + MaxF2 + ((MaxF1 * MaxF2) / (Input * MaxF3)), 4);

                    Maxd2 = Math.Round((double)MaxF2 + MaxF3 + ((MaxF2 * MaxF3 * Input) / (MaxF1)), 4);

                    if ((Maxd1 >= -0.012) && (Maxd1 < 0))
                    {
                        Maxd1 = 0;
                    }
                    else

                        if ((Maxd2 >= -0.012) && (Maxd2 < 0))
                        {
                            Maxd2 = 0;
                        }

                    Console.WriteLine("The system has d1 = {0} and d2 = {1} for the Input Magnification = {2} \n", Maxd1, Maxd2, Input);

                }

                else

                    if ((MaxMxratioMy > MaxMx) || (MaxMy > MaxMxratioMy) || (Input > MaxMx) || (Input < MaxMy) || (Input >= InputMax) || (Input <= InputMin))
                    {


                        Console.WriteLine("Conditions didn't satified \n");

                        Console.WriteLine("Please choose values between or equal to Max and Min Magnification \n");

                        return Maxtractcal(F1, F2, F3);

                    }

            }

            // return Maxtractcal(F1, F2, F3, MxratioMy, Maxtrack);
        }

        public static double Mintrackcal(double[] F1, double[] F2, double[] F3)
        {
            int a = 1;

            if (a == 1)
            {

                Console.WriteLine("Focallength choosed with Mintrack = {0} are: F1 = {1}, F2 = {2}, F3 = {3} \n", MaxtrackList.Min(), F1List[MaxtrackList.IndexOf(MaxtrackList.Min())], F2List[MaxtrackList.IndexOf(MaxtrackList.Min())], F3List[MaxtrackList.IndexOf(MaxtrackList.Min())]);

                Console.WriteLine("Please choose values between or equal to InputMax and InputMin Magnification \n");

                a = a + 1;

            }

            MinF1 = F1List[MaxtrackList.IndexOf(MaxtrackList.Min())];

            MinF2 = F2List[MaxtrackList.IndexOf(MaxtrackList.Min())];

            MinF3 = F3List[MaxtrackList.IndexOf(MaxtrackList.Min())];


            Maxa1 = Math.Round((double)MinF1 + MinF2, 4);

            MaxMxratioMy = Math.Round((double)MinF1 / MinF3, 4);

            Maxa2 = Math.Round((double)MinF2 + MinF3, 4);

            Maxb1 = Math.Round((double)(MinF1 * MinF2 / MinF3), 4);

            Maxb2 = Math.Round((double)(MinF2 * MinF3) / MinF1, 4);

            MaxMx = Math.Round((double)-Maxa2 / Maxb2, 4);

            MaxMy = Math.Round((double)-Maxb1 / Maxa1, 4);


            while (true)
            {

                Console.WriteLine("Enter Magnification upto 4 decimal point \n");

                // Check for value other than numerics

                while (!Double.TryParse(Console.ReadLine(), out Input))
                {
                    Console.WriteLine("Please enter numeric value \n");

                    Console.WriteLine("Enter Magnification upto 4 decimal point  \n");
                }



                if ((MaxMx > MaxMxratioMy) && (MaxMxratioMy > MaxMy) && (Input <= MaxMx) && (Input >= MaxMy) && (Input <= InputMax) && (Input >= InputMin))
                {

                    Console.WriteLine("Conditions satified \n");


                    //Calculate d1 and d2 for the Input Magnification

                    Maxd1 = Math.Round((double)MaxF1 + MaxF2 + ((MaxF1 * MaxF2) / (Input * MaxF3)), 4);

                    Maxd2 = Math.Round((double)MaxF2 + MaxF3 + ((MaxF2 * MaxF3 * Input) / (MaxF1)), 4);

                    if ((Maxd1 >= -0.012) && (Maxd1 < 0))
                    {
                        Maxd1 = 0;
                    }
                    else

                        if ((Maxd2 >= -0.012) && (Maxd2 < 0))
                        {
                            Maxd2 = 0;
                        }

                    Console.WriteLine("The system has d1 = {0} and d2 = {1} for the Input Magnification = {2} \n", Maxd1, Maxd2, Input);

                }

                else

                    if ((MaxMxratioMy > MaxMx) || (MaxMy > MaxMxratioMy) || (Input > MaxMx) || (Input < MaxMy) || (Input >= InputMax) || (Input <= InputMin))
                    {


                        Console.WriteLine("Conditions didn't satified \n");

                        Console.WriteLine("Please choose values between or equal to InputMax and InputMin Magnification \n");

                        return Mintrackcal(F1, F2, F3);

                    }

            }
            // return Mintrackcal(F1, F2, F3, MxratioMy, Maxtrack);
        }



        static void Main(string[] args)
        {


            int k, l, m, n = 0;

            while (true)
            {

                n = 0; // Make n(number of combination) = 0, in start and at the end of all the combination 

                Console.WriteLine("Enter Max Magnification upto 4 decimal point \n");

                while (!Double.TryParse(Console.ReadLine(), out InputMax))
                {
                    Console.WriteLine("Please only input numeric value \n");

                    Console.WriteLine("Enter Max Magnification upto 4 decimal point \n");
                }

                Console.WriteLine("\n");

                Console.WriteLine("Enter Min Magnification upto 4 decimal point \n");

                // Check for value other than numerics

                while (!Double.TryParse(Console.ReadLine(), out InputMin))
                {
                    Console.WriteLine("Please only input numeric value \n");

                    Console.WriteLine("Enter Min Magnification upto 4 decimal point \n");
                }

                Console.WriteLine("\n");

                Console.WriteLine("Enter 0 or 1 to see all combinations with each resutls or d1 and d2 for Max Mag respectively \n");

                string input = Console.ReadLine();

                Console.WriteLine("\n");

                switch (input)
                {
                    case "0":

                        //take f1, f2 and f3 to calculate Max magnification and compare with Mx and My magnifications

                        for (k = 0; k < focallength1.Length; k++) // take f1
                        {

                            for (l = 0; l < focallength2.Length; l++) // take f2
                            {


                                for (m = 0; m < focallength3.Length; m++) // take f3
                                {

                                    a1[m] = Math.Round((double)focallength1[k] + focallength2[l], 4);

                                    MxratioMy[m] = Math.Round((double)focallength1[k] / focallength3[m], 4);

                                    a2[m] = Math.Round((double)focallength2[l] + focallength3[m], 4);

                                    b1[m] = Math.Round((double)(focallength1[k] * focallength2[l]) / focallength3[m], 4);

                                    b2[m] = Math.Round((double)(focallength2[l] * focallength3[m]) / focallength1[k], 4);

                                    Mx[m] = Math.Round((double)-a2[m] / b2[m], 4);

                                    My[m] = Math.Round((double)-b1[m] / a1[m], 4);


                                    //comparison with Mx and My magnifications

                                    if ((Mx[m] > MxratioMy[m]) && (MxratioMy[m] > My[m]) && (InputMax <= Mx[m]) && (InputMin >= My[m]) && (InputMax > InputMin))
                                    {
                                        n = n + 1;

                                        Console.WriteLine("number of combination = {0} ", n);

                                        Console.WriteLine("Conditions satified for comination {0}", n);

                                        Console.WriteLine("take Mx as {0} and My as {1} with F1 as {2}, F2 as {3} and F3 as {4}", Mx[m], My[m], focallength1[k], focallength2[l], focallength3[m]);



                                        //Calculate d1 and d2 for the Input Max and Min Magnification

                                        d1forInputMax[m] = Math.Round((double)focallength1[k] + focallength2[l] + ((focallength1[k] * focallength2[l]) / (InputMax * focallength3[m])), 4);

                                        d2forInputMax[m] = Math.Round((double)focallength2[l] + focallength3[m] + ((focallength2[l] * focallength3[m] * InputMax) / (focallength1[l])), 4);

                                        // Check for very small negative distance value and convert them to zero

                                        if ((d1forInputMax[m] >= -0.012) && (d1forInputMax[m] < 0))
                                        {
                                            d1forInputMax[m] = 0;
                                        }
                                        else

                                            if ((d2forInputMax[m] >= -0.012) && (d2forInputMax[m] < 0))
                                            {
                                                d2forInputMax[m] = 0;
                                            }

                                        Console.WriteLine("The system has d1 = {0} and d2 = {1} for Max magnification Input = {2} ", d1forInputMax[m], d2forInputMax[m], InputMax);

                                        d1forInputMin[m] = Math.Round((double)focallength1[k] + focallength2[l] + ((focallength1[k] * focallength2[l]) / (InputMin * focallength3[m])), 4);

                                        d2forInputMin[m] = Math.Round((double)focallength2[l] + focallength3[m] + ((focallength2[l] * focallength3[m] * InputMin) / (focallength1[k])), 4);

                                        if ((d1forInputMin[m] >= -0.012) && (d1forInputMin[m] < 0))
                                        {
                                            d1forInputMin[m] = 0;
                                        }
                                        else

                                            if ((d2forInputMin[m] >= -0.012) && (d2forInputMin[m] < 0))
                                            {
                                                d2forInputMin[m] = 0;
                                            }

                                        Console.WriteLine("The system has d1 = {0} and d2 = {1} for Min magnification Input = {2} ", d1forInputMin[m], d2forInputMin[m], InputMin);


                                        // Calculate Max d1 and d2 for Max magnification

                                        d1forMx[m] = Math.Round((double)focallength1[k] + focallength2[l] + ((focallength1[k] * focallength2[l]) / (Mx[m] * focallength3[m])), 4);

                                        d2forMx[m] = Math.Round((double)focallength2[l] + focallength3[m] + ((focallength2[l] * focallength3[m] * Mx[m]) / (focallength1[k])), 4);

                                        if ((d1forMx[m] >= -0.012) && (d1forMx[m] < 0))
                                        {
                                            d1forMx[m] = 0;
                                        }
                                        else

                                            if ((d2forMx[m] >= -0.012) && (d2forMx[m] < 0))
                                            {
                                                d2forMx[m] = 0;
                                            }

                                        Console.WriteLine("The system has d1 = {0} and d2 = {1} for Maximum Magnification possible = {2} ", d1forMx[m], d2forMx[m], Mx[m]);


                                        // Calculate d1 and d2 for Minimum magnification

                                        d1forMy[m] = Math.Round((double)focallength1[k] + focallength2[l] + ((focallength1[k] * focallength2[l]) / (My[m] * focallength3[m])), 4);

                                        d2forMy[m] = Math.Round((double)focallength2[l] + focallength3[m] + ((focallength2[l] * focallength3[m] * My[m]) / (focallength1[k])), 4);

                                        if ((d1forMy[m] >= -0.012) && (d1forMy[m] < 0))
                                        {
                                            d1forMy[m] = 0;
                                        }
                                        else

                                            if ((d2forMy[m] >= -0.012) && (d2forMy[m] < 0))
                                            {
                                                d2forMy[m] = 0;
                                            }

                                        Console.WriteLine("The system has d1 = {0} and d2 = {1} for Mimimum Magnification possible = {2} ", d1forMy[m], d2forMy[m], My[m]);


                                        // Calculate Max track length and d1 and d2 for that

                                        Maxtrack[m] = Math.Round((double)focallength1[k] + 2 * focallength2[l] + focallength3[m] + (focallength2[l] * (((focallength3[m] * MxratioMy[m]) / focallength1[k]) + focallength1[k] / (focallength3[m] * MxratioMy[m]))), 4);


                                        Console.WriteLine("The total system length (d1+d2) = {0} for Magnification = {1} with F1 = {2}, F2 = {3} and F3 = {4} ", Maxtrack[m], MxratioMy[m], focallength1[k], focallength2[l], focallength3[m]);

                                        d1forMxratioMy[m] = Math.Round((double)focallength1[k] + focallength2[l] + ((focallength1[k] * focallength2[l]) / (MxratioMy[m] * focallength3[m])), 4);

                                        d2forMxratioMy[m] = Math.Round((double)focallength2[l] + focallength3[m] + ((focallength2[l] * focallength3[m] * MxratioMy[m]) / (focallength1[k])), 4);

                                        if ((d1forMxratioMy[m] >= -0.012) && (d1forMxratioMy[m] < 0))
                                        {
                                            d1forMxratioMy[m] = 0;
                                        }
                                        else

                                            if ((d2forMxratioMy[m] >= -0.012) && (d2forMxratioMy[m] < 0))
                                            {
                                                d2forMxratioMy[m] = 0;
                                            }

                                        Console.WriteLine("The system has maximum length with d1 = {0} and d2 = {1} and Magnification = {2} ", d1forMxratioMy[m], d2forMxratioMy[m], MxratioMy[m]);


                                    }


                                    else

                                        if ((MxratioMy[m] > Mx[m]) || (My[m] > MxratioMy[m]) || (InputMax > Mx[m]) || (InputMin < My[m]) || (InputMax < InputMin))
                                        {
                                            n = n + 1;

                                            Console.WriteLine("number of combination = {0} ", n);

                                            Console.WriteLine("Conditions didn't satified for comination {0}", n);

                                            Console.WriteLine("Can't choose InputMax = {0} and InputMin = {1} as InputMax ({0}) > Calculated Mx {2} or InputMin ({1}) < calculated My {3} with F1 as {4}, F2 as {5} and F3 as {6} ", InputMax, InputMin, Mx[m], My[m], focallength1[k], focallength2[l], focallength3[m]);

                                        }

                                }

                            }

                        }


                        break;

                    case "1":

                        perm(focallength1, focallength2, focallength3);

                        break;

                    default:

                        Console.WriteLine("Please choose from (0) or (1) \n");

                        break;
                }

            }


        }
    }
}