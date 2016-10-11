using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Rewards
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //GetRewards();

            List<Prize> prizes = new List<Prize>();
            //prizes.Add(new Prize() { Key = "电脑", Poll = 1 });
            //prizes.Add(new Prize() { Key = "机柜", Poll = 2 });
            //prizes.Add(new Prize() { Key = "鼠标", Poll = 3 });
            //prizes.Add(new Prize() {Key = "小白.手机壳1个",Poll = 0});
            //prizes.Add(new Prize() { Key = "谢谢惠顾", Poll = 5 });


            prizes.Add(new Prize() { Key = "魔豆50个					", Poll = 5 });
            prizes.Add(new Prize() { Key = "魔豆10个					", Poll = 10 });
            prizes.Add(new Prize() { Key = "魔豆100 个				", Poll = 3 });
            prizes.Add(new Prize() { Key = "魔衣50元					", Poll = 5 });
            prizes.Add(new Prize() { Key = "魔法商店全场20元			", Poll = 10 });
            prizes.Add(new Prize() { Key = "高端肖像画.无边框油画 200元", Poll = 5 });
            prizes.Add(new Prize() { Key = "高端肖像画 100元			", Poll = 8 });
            prizes.Add(new Prize() { Key = "桌上肖像画 50元			", Poll = 13 });
            prizes.Add(new Prize() { Key = "星座杯 30元				", Poll = 15 });
            prizes.Add(new Prize() { Key = "手机系列 50元				", Poll = 13 });
            prizes.Add(new Prize() { Key = "惊喜奖					", Poll = 3 });
            prizes.Add(new Prize() { Key = "心情魔镜.手机背贴1个		", Poll = 0 });
            prizes.Add(new Prize() { Key = "小魔飞吻1个,么么哒~		", Poll = 10 });
            prizes.Add(new Prize() { Key = "小白.手机壳1个			", Poll = 0 });
            prizes.Add(new Prize() { Key = "谢谢惠顾", Poll = 50 });

            //string lp1 = Prize.LunkyBox(prizes, 6);
            //Console.WriteLine(lp1);

            for (int i = 0; i < 100; i++)
            {
                
                string lp2 = Prize.Roulette(prizes.Where(t => t.Poll > 0).ToList());
                Console.WriteLine(lp2);
            }

            Console.Read();
        }

        private static void GetRewards()
        {
            //各物品的概率保存在数组里
            float[] area = new float[]
            {
                0.980f,
                0.550f,
                0.230f, 0.230f, 0.230f, 0.230f, 0.230f,
                0.010f
            };

            //单次测试
            //Console.WriteLine(Get(area));

            //批量测试
            int[] result = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < 100; i++) //为了比对结果方便，这里循环的次数是总概率的1000倍
            {
                int n = Get(area); //本次抽奖结果
                //Console.CursorLeft = 0;
                //Console.CursorTop = 0;
                Thread.Sleep(100);
                Console.WriteLine(n);
                result[n]++; //统计抽到的次数
            }
            Console.WriteLine("结果：");
            foreach (int times in result)
            {
                Console.WriteLine(times);
            }
        }

        /// <summary>
        /// 获取抽奖结果
        /// </summary>
        /// <param name="prob">各物品的抽中概率</param>
        /// <returns>返回抽中的物品所在数组的位置</returns>
        private static int Get(float[] prob)
        {
            int result = 0;
            var sum = prob.Sum();
            int n = (int)(sum * 1000);           //计算概率总和，放大1000倍
            Random r = rnd;
            float x = (float)r.Next(0, n) / 1000;       //随机生成0~概率总和的数字

            for (int i = 0; i < prob.Count(); i++)
            {
                float pre = prob.Take(i).Sum();         //区间下界
                float next = prob.Take(i + 1).Sum();    //区间上界
                if (x >= pre && x < next)               //如果在该区间范围内，就返回结果退出循环
                {
                    result = i;
                    break;
                }
            }
            return result;
        }

        private static Random rnd = new Random();
    }
}