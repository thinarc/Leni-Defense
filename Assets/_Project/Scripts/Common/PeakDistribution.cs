using UnityEngine;

namespace _Project.Scripts.Common
{
    public static class PeakDistribution
    {
        public static int[] CalculateDistribution(int total, int groups)
        {
            var result = new int[groups];
            var remaining = total;

            for (var i = 0; i < groups; i++)
            {
                var maxAllowed = (i == 0) ? remaining : result[i - 1];
                var ideal = Mathf.CeilToInt((float)remaining / (groups - i));

                var value = Mathf.Min(maxAllowed, ideal);
                result[i] = value;
                remaining -= value;
            }

            return result;
        }
    }
}