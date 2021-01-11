using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace prac1
{
    class Daihugou
    {
        static void Main(string[] args)
        {
            // Test呼び出し
            TestConsoleOut("DJ");
            TestConsoleOut("H7,HK");
            TestConsoleOut("S3,D4D2");
            TestConsoleOut("S9,C8H4");
            TestConsoleOut("S6,S7STCK");
            TestConsoleOut("H4,SAS8CKH6S4");
            TestConsoleOut("ST,D6S8JoC7HQHAC2CK");
            TestConsoleOut("SA,HAD6S8S6D3C4H2C5D4CKHQS7D5");
            TestConsoleOut("S2,D8C9D6HQS7H4C6DTS5S6C7HAD4SQ");
            TestConsoleOut("Jo,HAC8DJSJDTH2");
            TestConsoleOut("S4Jo,CQS6C9DQH9S2D6S3");
            TestConsoleOut("CTDT,S9C2D9D3JoC6DASJS4");
            TestConsoleOut("H3D3,DQS2D6H9HAHTD7S6S7Jo");
            TestConsoleOut("D5Jo,CQDAH8C6C9DQH7S2SJCKH5");
            TestConsoleOut("C7H7,S7CTH8D5HACQS8JoD6SJS5H4");
            TestConsoleOut("SAHA,S7SKCTS3H9DJHJH7S5H2DKDQS4");
            TestConsoleOut("JoC8,H6D7C5S9CQH9STDTCAD9S5DAS2CT");
            TestConsoleOut("HTST,SJHJDJCJJoS3D2");
            TestConsoleOut("C7D7,S8D8JoCTDTD4CJ");
            TestConsoleOut("DJSJ,DTDKDQHQJoC2");
            TestConsoleOut("C3H3D3,CKH2DTD5H6S4CJS5C6H5S9CA");
            TestConsoleOut("D8H8S8,CQHJCJJoHQ");
            TestConsoleOut("H6D6S6,H8S8D8C8JoD2H2");
            TestConsoleOut("JoD4H4,D3H3S3C3CADASAD2");
            TestConsoleOut("DJHJSJ,SQDQJoHQCQC2CA");
            TestConsoleOut("H3D3Jo,D4SKH6CTS8SAS2CQH4HAC5DADKD9");
            TestConsoleOut("C3JoH3D3,S2S3H7HQCACTC2CKC6S7H5C7");
            TestConsoleOut("H5C5S5D5,C7S6D6C3H7HAH6H4C6HQC9");
            TestConsoleOut("H7S7C7D7,S5SAH5HAD5DAC5CA");
            TestConsoleOut("D4H4S4C4,S6SAH6HAD6DAC6CAJo");
            TestConsoleOut("DTCTSTHT,S3SQH3HQD3DQC3CQJo");
            TestConsoleOut("JoS8D8H8,S9DTH9CTD9STC9CAC2");
            
            Thread.Sleep(10000);
        }

        #region メソッド

        private static void TestConsoleOut(string input)
        {
            var output = GetNextIssueableCard(input);
            Console.WriteLine(string.Format("input ={0}, output={1}", input, output));
        }

        /// <summary>
        /// 次に出せるカードを出力する
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string GetNextIssueableCard(string input)
        {
            var fieldCardString = input.Split(',').First();  // 場のカード
            var fieldCards = new Cards(fieldCardString);

            var handCardString = input.Split(',').Last();    // 手札のカード
            //var handCards = new Cards(handCardString);

            // ランク上位を抜き出し
            var fieldLank = fieldCards.Rank;
            var handCards = new Cards(handCardString);
            var filterdHandCards = handCards.Where(x => x.IsUpperLankThan(fieldLank));
            
            var handCardCombinations = Combination.Enumerate(filterdHandCards, fieldCards.Count(), false);
            // 考えられる組み合わせから、同一ランクの組を抜き出す
            var sameLankCombiCards = new List<Cards>();
            foreach (var combi in handCardCombinations)
            {
                if (combi.Count() == 1)
                {
                    //全て採用
                    sameLankCombiCards.Add(new Cards(combi));
                }
                else if (combi.Count() == 2)
                {
                    // Jokerがあれば全て採用
                    if (combi.Any(x => x.IsJoker))
                    {
                        sameLankCombiCards.Add(new Cards(combi));
                    }
                    // 同一ランクを採用
                    else if (combi.First().Rank == combi.Last().Rank)
                    {
                        sameLankCombiCards.Add(new Cards(combi));
                    };
                }
                else if (combi.Count() == 3)
                {
                    // Jokerがある場合
                    if (combi.Any(x => x.IsJoker))
                    {
                        if (combi.GroupBy(x => x.Rank).Where(y => y.Count() == 2).Count() != 0)
                        {
                            sameLankCombiCards.Add(new Cards(combi));
                        }
                    }
                    else
                    {
                        if (combi.GroupBy(x => x.Rank).Where(y => y.Count() == 3).Count() != 0)
                        {
                            sameLankCombiCards.Add(new Cards(combi));
                        }
                    }
                }
                else if (combi.Count() == 4)
                {
                    // Jokerがある場合
                    if (combi.Any(x => x.IsJoker))
                    {
                        if (combi.GroupBy(x => x.Rank).Where(y => y.Count() == 3).Count() != 0)
                        {
                            sameLankCombiCards.Add(new Cards(combi));
                        }
                    }
                    else
                    {
                        if (combi.GroupBy(x => x.Rank).Where(y => y.Count() == 4).Count() != 0)
                        {
                            sameLankCombiCards.Add(new Cards(combi));
                        }
                    }
                }
            }

            //var fieldLank = Cards.GetLank(fieldCards);
            //var issuableCombiCards = sameLankCombiCards.Where(x => Cards.GetLank(x) > fieldLank);

            string output;
            if (sameLankCombiCards.Count() == 0)
            {
                output = "-";
            }
            else
            {
                output = string.Join(",", sameLankCombiCards.Select(x => x.Name));
            }
            return output;
        }

        #endregion

    }

}
