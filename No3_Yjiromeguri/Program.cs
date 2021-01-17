using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Yjiromeguri
{
    // Y字路巡り 〜 横へな 2012.9.7
    // http://nabetani.sakura.ne.jp/hena/ord3ynode/

    class Program
    {
        static void Main(string[] args)
        {
            foreach (var testCase in TestCases)
            {
                var input = testCase.Key;
                var expected = testCase.Value;

                var result = Play(input);
                if (result == expected)
                {
                    Console.WriteLine("Success! (input = \"{0}\", result = \"{1}\")", input, result);
                }
                else
                {
                    Console.WriteLine("Failed! (input = \"{0}\", result = \"{1}\", expected = \"{2}\")", input, result, expected);
                }
            }

            Console.WriteLine("60[s]後に画面を閉じます。");
            Thread.Sleep(60000);
        }


        static public string Play(string input)
        {
            //初期値
            var moveMng = new MovementManager('B', 'A');
            var output = new StringBuilder();
            output.Append('A');

            foreach (var command in input)
            {
                if (command == 'r')
                {
                    moveMng.TurnRight();
                }
                else if(command == 'l')
                {
                    moveMng.TurnLeft();
                }
                else if(command == 'b')
                {
                    moveMng.Back();
                }
                else
                {
                    return string.Format("無効な文字\'{0}\'が含まれています", command);
                }
                output.Append(moveMng.NowNode);
            }

            return output.ToString();
        }

        #region テストケース

        static Dictionary<string, string> TestCases = new Dictionary<string, string>()
        {
            /*0*/ {"b", "AB" },
            /*1*/ {"l", "AD" },
            /*2*/ {"r", "AC" },
            /*3*/ {"bbb", "ABAB" },
            /*4*/ {"rrr", "ACBA" },
            /*5*/ {"blll", "ABCAB" },
            /*6*/ {"llll", "ADEBA" },
            /*7*/ {"rbrl", "ACADE" },
            /*8*/ {"brrrr", "ABEDAB" },
            /*9*/ {"llrrr", "ADEFDE" },
            /*10*/ {"lrlll", "ADFEDF" },
            /*11*/ {"lrrrr", "ADFCAD" },
            /*12*/ {"rllll", "ACFDAC" },
            /*13*/ {"blrrrr", "ABCFEBC" },
            /*14*/ {"brllll", "ABEFCBE" },
            /*15*/ {"bbrllrrr", "ABACFDEFD" },
            /*16*/ {"rrrrblll", "ACBACABCA" },
            /*17*/ {"llrlrrbrb", "ADEFCADABA" },
            /*18*/ {"rrrbrllrr", "ACBABEFCAD" },
            /*19*/ {"llrllblrll", "ADEFCBCADEB" },
            /*20*/ {"lrrlllrbrl", "ADFCBEFDFCB" },
            /*21*/ {"lllrbrrlbrl", "ADEBCBACFCAB" },
            /*22*/ {"rrrrrrlrbrl", "ACBACBADFDEB" },
            /*23*/ {"lbrbbrbrbbrr", "ADABABEBCBCFE" },
            /*24*/ {"rrrrlbrblllr", "ACBACFCACFDAB" },
            /*25*/ {"lbbrblrlrlbll", "ADADFDABCFDFED" },
            /*26*/ {"rrbbrlrlrblrl", "ACBCBADFEBEFDA" },
            /*27*/ {"blrllblbrrrrll", "ABCFDADEDABEDFE" },
            /*28*/ {"blrllrlbllrrbr", "ABCFDABCBEFDEDA" },
            /*29*/ {"lbrbbrllllrblrr", "ADABABEFCBEDEBCF" },
            /*30*/ {"rrrrbllrlrbrbrr", "ACBACABCFDEDADFC" },
        };

        #endregion

    }
}
