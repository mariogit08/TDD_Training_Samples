using System.Collections.Generic;

namespace TDD_Course
{
    public partial class RomansNumeralsTest
    {
        public partial class Roman
        {
            public class Roman2
            {
                private static readonly Dictionary<char, int> map =
                    new Dictionary<char, int>()
                    {
                        {'I', 1 },
                        {'V', 5 },
                        {'X', 10 },
                        {'L', 50 },
                        {'C', 100 },
                        {'D', 500 },
                        {'M', 1000 },
                    };

                public static int? Parse2(string roman)
                {
                    //XXXIX
                    int result = 0;
                    for (int i = 0; i < roman.Length; i++)
                    {
                        if (i + 1 < roman.Length && IsSubtractive(roman[i], roman[i + 1])){
                            result -= map[roman[i]];
                        }
                        else
                        {
                            result += map[roman[i]];
                        }                       
                    }
                    return result;
                }
                private static bool IsSubtractive(char c1, char c2)
                {
                    return map[c1] < map[c2];
                }
            }
        }

    }
}
