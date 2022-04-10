using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGenerator
{
    public static class ProbabilityModule
    {
        public static int normalBuildingProbability = 10;
        public static int dynamicBuildingProbability = 1;
        public static Random randomSeed;
        public static double deltaForDoubleRandom = 0.2;
        static ProbabilityModule()
        {
            randomSeed = new Random();
        }

        public static double NextDouble(double min, double max)
        {
            var abs = Math.Abs(min - max);
            var segments = Convert.ToInt32(abs / deltaForDoubleRandom);
            
            return min + (deltaForDoubleRandom * randomSeed.Next(0, segments));
            //return min + (randomSeed.NextDouble() * (max - min));
        }
    }
}
