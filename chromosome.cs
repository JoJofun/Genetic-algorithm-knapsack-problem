using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapsackGenetic
{
    class chromosome//I know chromosome isnt the proper terms but I couldnt think of anything else to name this class
    {
        List<item> items = new List<item>();

        //these are the actual chromosomes, calling it chrom for short
        List<List<bool>> chroms = new List<List<bool>>();
        int numofchroms;
        int chromlength;
        int weight_limit;
        double mut;//mutation weight

        int bestfit = int.MinValue;
        List<bool> bestchrom;

        public chromosome(int[] Weights, int[] Values, int limit, int NumOfChroms, double Mut)
        {
            items.Clear();
            if (Weights.Length != Values.Length)
            {
                Console.WriteLine("failed creation of chromosome because of unmatching Weights length and Values length");
                return;
            }
            numofchroms = NumOfChroms;
            chromlength = Weights.Length;
            weight_limit = limit;
            mut = Mut;
            for (int i = 0; i < chromlength; i++)
            {
                items.Add(new item(Weights[i], Values[i]));
            }

            //generate initial chromosomes
            Random rnd = new Random();
            for (int i = 0; i < numofchroms; i++)
            {
                //string test = "";
                int seed = rnd.Next(int.MaxValue);
                chroms.Add(new List<bool>());
                for (int j = 0; j < chromlength; j++)
                {
                    chroms[i].Add(new bool());
                    chroms[i][j] = (seed % 2) == 1;
                    seed /= 2;
                    //test += chroms[i][j] + ", ";
                    
                    //not needed for this assignment but if I were to use this for an actual release I would have a check that for lists > 31
                    //generate a new seed every 31st, if I dont then items 32 -> infinity will be 0

                }
                if (fitness(i) > bestfit)
                {
                    bestfit = fitness(i);
                    bestchrom = chroms[i];
                }
               // Console.WriteLine(test + "Fitness: " + fitness(i));
            }

        }

        public void new_generation()
        {
            Random rnd = new Random();
            List<List<bool>> newchroms = new List<List<bool>>();
            for (int i = 0; i < numofchroms; i += 2)
            {
                //tournament selection
                int parent11 = rnd.Next(numofchroms);
                int parent12 = rnd.Next(numofchroms);
                if (fitness(parent11) < fitness(parent12))
                {
                    parent11 = parent12;
                }
                //Console.WriteLine(str_chrom(parent11));

                int parent21 = rnd.Next(numofchroms);
                int parent22 = rnd.Next(numofchroms);
                if (fitness(parent21) < fitness(parent22))
                {
                    parent21 = parent22;
                }
                //Console.WriteLine(str_chrom(parent21));

                List<bool> child1 = new List<bool>();
                List<bool> child2 = new List<bool>();

                int crossoverP = rnd.Next(1, chromlength - 1);
                for (int j = 0; j < chromlength; j++)
                {
                    if (j < crossoverP)
                    {
                        child1.Add(chroms[parent11][j]);
                        child2.Add(chroms[parent21][j]);
                    }
                    else
                    {
                        child2.Add(chroms[parent11][j]);
                        child1.Add(chroms[parent21][j]);
                    }
                }

                //CHECK FOR MUTATIONS HERE
                //child1
                double mutant = rnd.NextDouble();
                if (mutant < mut)
                {
                    int temp = rnd.Next(chromlength);
                    child1[temp] = !child1[temp];
                }
                //child2
                mutant = rnd.NextDouble();
                if (mutant < mut)
                {
                    int temp = rnd.Next(chromlength);
                    child2[temp] = !child2[temp];
                }
                newchroms.Add(child1);
                newchroms.Add(child2);
            }
            chroms.Clear();
            for (int i = 0; i < numofchroms; i++)
            {
                chroms.Add(newchroms[i]);
                if (fitness(i) > bestfit)
                {
                    bestfit = fitness(i);
                    bestchrom = chroms[i];
                }
            }
        }

        public int fitness(int i) //checks the fitness of the i'th chromosome
        {
            if (i >= numofchroms)//dummyproofing
            {
                return -1;
            }

            int tempW = 0;
            int tempV = 0;

            for (int j = 0; j < chromlength; j++)
            {
                if (chroms[i][j])
                {
                    tempW += items[j].get_w();
                    tempV += items[j].get_v();
                }
                if (tempW > weight_limit)
                {
                    return 0;
                }
            }
            return tempV;
        }

        public string str_chrom(int i)//returns a string of the chromosome in binary representation ex:1001100...
        {
            string toret = "";
            for(int j = 0; j < chromlength; j++)
            {
                if (chroms[i][j])
                {
                    toret += '1';
                }
                else
                {
                    toret += '0';
                }
            }
            return toret;
        }

        public string str_best()
        {
            string toret = "";
            for (int j = 0; j < chromlength; j++)
            {
                if (bestchrom[j])
                {
                    toret += '1';
                }
                else
                {
                    toret += '0';
                }
            }

            return("Best: " + toret + " Fitness: " + bestfit);
        }

    }
}
