using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EhkBackend.common
{
    public class CheckA
    {
        public static string checksum(string data)
        {
            int sum = 0;


            int len = data.Length;
            int[] jishu = { 1, 3, 5, 7, 9, 11, 13, 15, 17 };
            int module = 37;
            List<int> sumkey = new List<int>();

            for (int i = 0; i < len; i++)
            {
                int a = data[i] - '0';//ascll转数字值
                int b = jishu[i];
                int ab = a * b;
                sumkey.Add(ab);
            }
            int e1 = sumkey.Sum();
            string sum1;
            sum = module - (sumkey.Sum() % module);
            if (sum < 10)
            {
                sum1 = "0" + sum.ToString();
            }
            else
            {
                sum1 = sum.ToString();
            }
            return sum1;
        }
    }
}
