using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prac1
{
    /// <summary>
    /// カード束に対するオブジェクト
    /// </summary>
    public class Cards : List<Card>
    {
        public CardLank Rank
        {
            get
            {
                if (this.Count() == 1)
                {
                    return this.First().Rank;
                }
                else
                {
                    // Joker以外のカードのランクを返す
                    return this.Where(x => !x.IsJoker).First().Rank;
                }
            }
        }

        public string Name
        {
            get
            {
                var cardNames = this.Select(x => x.Name);
                return string.Join("", cardNames);
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="cardNameString">カード束の文字列</param>
        public Cards(string cardNameString)
        {
            // 2文字ずつ抜き出してカードを定義
            for (var i = 0; i < cardNameString.Count() / 2; i++)
            {
                this.Add(new Card(cardNameString.Substring(2 * i, 2)));
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="cards">カード束のIEnumerable</param>
        public Cards(IEnumerable<Card> cards)
        {
            foreach(var card in cards)
            {
                this.Add(card);
            }
        }
        
        /// <summary>
        /// 束のランクを取得する
        /// </summary>
        public static CardLank GetLank(Cards cards)
        {
            if (cards.Count() == 1)
            {
                return cards.First().Rank;
            }
            else
            {
                // Joker以外のカードのランクを返す
                return cards.Where(x => !x.IsJoker).First().Rank;
            }
        }

        /// <summary>
        /// カード束の文字列を取得する
        /// </summary>
        public static string GetCardsNameString(Cards cards)
        {
            var cardNames = cards.Select(x => x.Name);
            return string.Join("", cardNames);
        }
    }
}

