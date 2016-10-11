using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewards
{
    /// <summary>
    /// 抽奖
    /// </summary>
    public class Prize
    {
        /// <summary>
        /// 奖品关键字
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 权重/数量
        /// </summary>
        public int Poll { get; set; }


        /// <summary>
        /// 中奖区间
        /// </summary>
        class Area
        {
            /// <summary>
            /// 奖品关键字
            /// </summary>
            public string Key { get; set; }

            /// <summary>
            /// 开始索引位置
            /// </summary>
            public int Start { get; set; }

            /// <summary>
            /// 截止索引位置
            /// </summary>
            public int Over { get; set; }
        }

        /// <summary>
        /// 随机种子
        /// </summary>
        static Random Rand = new Random((int)DateTime.Now.Ticks);


        /// <summary>
        /// 轮盘抽奖，权重值(在轮盘中占的面积大小)为中奖几率
        /// </summary>
        /// <param name="prizeList">礼品列表（如果不是百分百中奖则轮空需要加入到列表里面）</param>
        /// <returns></returns>
        public static string Roulette(List<Prize> prizeList)
        {
            if (prizeList == null || prizeList.Count == 0) return string.Empty;
            if (prizeList.Any(x => x.Poll < 1)) throw new ArgumentOutOfRangeException("poll权重值不能小于1");
            if (prizeList.Count == 1) return prizeList[0].Key; //只有一种礼品

            Int32 total = prizeList.Sum(x => x.Poll); //权重和               
            if (total > 1000) throw new ArgumentOutOfRangeException("poll权重和不能大于1000"); //数组存储空间的限制。最多一千种奖品（及每种奖品的权重值都是1）

            List<int> speed = new List<int>(); //随机种子
            for (int i = 0; i < total; i++) speed.Add(i);

            int pos = 0;
            Dictionary<int, string> box = new Dictionary<int, string>();
            foreach (Prize p in prizeList)
            {
                for (int c = 0; c < p.Poll; c++) //权重越大所占的面积份数就越多
                {
                    pos = Prize.Rand.Next(speed.Count); //取随机种子坐标
                    box[speed[pos]] = p.Key; //乱序 礼品放入索引是speed[pos]的箱子里面
                    speed.RemoveAt(pos); //移除已抽取的箱子索引号
                }
            }
            return box[Prize.Rand.Next(total)];
        }

        /// <summary>
        /// 奖盒抽奖，每个参与者对应一个奖盒，多少人参与就有多少奖盒
        /// </summary>
        /// <param name="prizeList">礼品列表</param>
        /// <param name="peopleCount">参与人数</param>
        /// <returns></returns>
        public static string LunkyBox(List<Prize> prizeList, int peopleCount)
        {
            if (prizeList == null || prizeList.Count == 0) return string.Empty;
            if (prizeList.Any(x => x.Poll < 1)) throw new ArgumentOutOfRangeException("poll礼品数量不能小于1个");
            if (peopleCount < 1) throw new ArgumentOutOfRangeException("参数人数不能小于1人");
            if (prizeList.Count == 1 && peopleCount <= prizeList[0].Poll) return prizeList[0].Key; //只有一种礼品且礼品数量大于等于参与人数

            int pos = 0;
            List<Area> box = new List<Area>();
            foreach (Prize p in prizeList)
            {
                box.Add(new Area() { Key = p.Key, Start = pos, Over = pos + p.Poll }); //把礼品放入奖盒区间
                pos = pos + p.Poll;
            }

            int total = prizeList.Sum(x => x.Poll); //礼品总数
            int speed = Math.Max(total, peopleCount); //取礼品总数和参数总人数中的最大值

            pos = Prize.Rand.Next(speed);
            Area a = box.FirstOrDefault(x => pos >= x.Start && pos < x.Over); //查找索引在奖盒中对应礼品的位置

            return a == null ? string.Empty : a.Key;
        }

    }


    /*
    List<Prize> prizes = new List<Prize>();
    prizes.Add(new Prize() { Key = "电脑", Poll = 1 });
    prizes.Add(new Prize() { Key = "机柜", Poll = 2 });
    prizes.Add(new Prize() { Key = "鼠标", Poll = 3 });


    string lp1 = Prize.LunkyBox(prizes, 6);
    Console.WriteLine(lp1);


    prizes.Add(new Prize() { Key = "谢谢惠顾", Poll = 5 });
    string lp2 = Prize.Roulette(prizes);      
    Console.WriteLine(lp2);
    */
}
