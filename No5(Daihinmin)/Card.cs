using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prac1
{
    /// <summary>
    /// カード単体に対するオブジェクト
    /// </summary>
    public class Card
    {
        #region フィールド／プロパティ

        /// <summary>
        /// カードのスート
        /// </summary>
        public char Suit { get; }

        /// <summary>
        /// カードのランク
        /// </summary>
        public CardLank Rank { get; } 

        /// <summary>
        /// ジョーカーであることを表す値
        /// </summary>
        public bool IsJoker { get; } = false;

        /// <summary>
        /// カード名(文字列)
        /// </summary>
        public string Name
        {
            get
            {
                if (Rank == CardLank.Joker) { return "Jo"; }
                var pair = _cardLank.FirstOrDefault(x => x.Value == Rank);
                return Suit.ToString() + pair.Key.ToString();
            }
        }

        /// <summary>
        /// ランク名とランクのディクショナリ
        /// </summary>
        private Dictionary<char, CardLank> _cardLank = new Dictionary<char, CardLank>()
        {
            {'3', CardLank.Three },
            {'4', CardLank.Four },
            {'5', CardLank.Five },
            {'6', CardLank.Six },
            {'7', CardLank.Seven },
            {'8', CardLank.Eight },
            {'9', CardLank.Nine },
            {'T', CardLank.Ten },
            {'J', CardLank.Jack },
            {'Q', CardLank.Queen },
            {'K', CardLank.King },
            {'A', CardLank.Ace },
            {'2', CardLank.Two },
        };

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="cardName">カード名</param>
        /// <remarks>
        /// 入力カード名は、"D2"のように2文字固定とする
        /// </remarks>
        public Card(string cardName)
        {
            if (cardName == "Jo")
            {
                Suit = ' ';
                Rank = CardLank.Joker;
                IsJoker = true;
            }
            else
            {
                Suit = cardName[0];
                Rank = _cardLank[cardName[1]];
            }
        }

        #endregion

        #region メソッド

        /// <summary>
        /// 比較対象のカードのランクよりも上位であるか判別する
        /// </summary>
        /// <param name="x">比較対称のカード</param>
        /// <returns>true: 上位である, false: 同位もしくは下位である</returns>
        public bool IsUpperLankThan(Card x)
        {
            return this.Rank > x.Rank;
        }

        /// <summary>
        /// 比較対象のカードのランクよりも上位であるか判別する
        /// </summary>
        /// <param name="x">比較対称のカード</param>
        /// <returns>true: 上位である, false: 同位もしくは下位である</returns>
        public bool IsUpperLankThan(CardLank x)
        {
            return this.Rank > x;
        }

        /// <summary>
        /// 比較対象のカードのスートと同じであるか判別する
        /// </summary>
        /// <param name="x">比較対象のカード</param>
        /// <returns>true: 同じスートである, false: 異なるスートである</returns>
        public bool IsSameSuitAs(Card x)
        {
            return this.Suit == x.Suit;
        }

        #endregion
    }
}
