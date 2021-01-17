using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yjiromeguri
{
    /// <summary>
    /// 移動を管理するオブジェクト
    /// </summary>
    public class MovementManager
    {
        /// <summary>
        /// ある接点に隣接する接点を、反時計回りに全て合わせた文字列を返すディクショナリ
        /// </summary>
        public static Dictionary<char, string> NextNode = new Dictionary<char, string>()
        {
            { 'A',"BCD" },
            { 'B',"AEC" },
            { 'C',"ABF" },
            { 'D',"AFE" },
            { 'E',"BDF" },
            { 'F',"CED" },
        };

        #region フィールド／プロパティ

        /// <summary>
        /// 一つ前に止まっていた接点名
        /// </summary>
        private char _postNode;

        /// <summary>
        /// 現在止まっている接点名
        /// </summary>
        public char NowNode { get; private set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="fromDef">出発点(初期値)</param>
        /// <param name="toDef">移動点(初期値)</param>
        public MovementManager(char fromDef, char toDef)
        {
            _postNode = fromDef;
            NowNode = toDef;
        }

        #endregion

        #region メソッド

        /// <summary>
        /// 右折させる
        /// </summary>
        /// <remarks>
        /// NextNodesにおいて、post点の文字から一つ次の文字が右折点
        /// 例)NextNodesが"BCD"の場合、post点が'C'なら次の文字は'D', post点が'D'なら次の文字は'B'
        /// </remarks>
        public void TurnRight()
        {
            var postNodeTmp = NowNode;

            var nextNodes = NextNode[NowNode];
            var nextNodesNum = nextNodes.Count();
            for (var i = 0; i < nextNodesNum; i++)
            {
                if (_postNode == nextNodes[i])
                {
                    // 次の文字を取り出す(終端の場合は最初の文字)
                    NowNode = (i == nextNodesNum - 1) ? nextNodes[0]
                                                      : nextNodes[i + 1];
                }
            }
            _postNode = postNodeTmp;
        }

        /// <summary>
        /// 左折させる
        /// </summary>
        /// <remarks>
        /// NextNodesにおいて、post点の文字から一つ前の文字が左折点
        /// 例)NextNodesが"ABF"の場合、post点が'F'なら前の文字は'B', post点が'A'なら前の文字は'F'
        /// </remarks>
        public void TurnLeft()
        {
            var postNodeTmp = NowNode;

            var nextNodes = NextNode[NowNode];
            var nextNodesNum = nextNodes.Count();
            for (var i = 0; i < nextNodesNum; i++)
            {
                if (_postNode == nextNodes[i])
                {
                    // 前の文字を取り出す(始端の場合は最後の文字)
                    NowNode = (i == 0) ? nextNodes[nextNodesNum -1]
                                       : nextNodes[i - 1];
                }
            }
            _postNode = postNodeTmp;
        }

        /// <summary>
        /// 一つ元に戻る
        /// </summary>
        public void Back()
        {
            // nowとpostを入れ替える
            var nowNodeTmp = _postNode;
            _postNode = NowNode;
            NowNode = nowNodeTmp;
        }

        #endregion
    }
}
