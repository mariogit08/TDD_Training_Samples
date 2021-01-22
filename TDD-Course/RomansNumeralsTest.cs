using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework;
using static TDD_Course.RomansNumeralsTest.Roman;

namespace TDD_Course
{
    [TestFixture]
    public partial class RomansNumeralsTest
    {
        //Regra 1
        [TestCase("I", 1)]
        [TestCase("II", 2)]
        [TestCase("III", 3)]
        [TestCase("X", 10)]
        [TestCase("XX", 20)]
        [TestCase("XXX", 30)]
        [TestCase("C", 100)]
        [TestCase("CC", 200)]
        [TestCase("CCC", 300)]
        [TestCase("M", 1000)]
        [TestCase("MM", 2000)]
        [TestCase("MMM", 3000)]
        ////Regra 2
        [TestCase("VIII", 8)]
        [TestCase("XI", 11)]
        ////Regra 2 - Out of the box
        [TestCase("VIIIIIIIIIIIIIIIIIIIIIII", 0)]
        ////Regra 3
        [TestCase("IV", 4)] //Parei nesse teste
        [TestCase("IX", 9)]
        [TestCase("XXXIX", 39)]

        //Exceptional cases
        //[TestCase(null, 0)]
        public void TestParseRomanNumeral(string atual, int expected)
        {
            Assert.AreEqual(expected, Roman.Parse(atual));
        }

        //Regra 1
        [TestCase("I", 1)]
        [TestCase("II", 2)]
        [TestCase("III", 3)]
        [TestCase("X", 10)]
        [TestCase("XX", 20)]
        [TestCase("XXX", 30)]
        [TestCase("C", 100)]
        [TestCase("CC", 200)]
        [TestCase("CCC", 300)]
        [TestCase("M", 1000)]
        [TestCase("MM", 2000)]
        [TestCase("MMM", 3000)]
        [TestCase("LM", 950)]
        ////Regra 2
        [TestCase("VIII", 8)]
        [TestCase("XI", 11)]
        ////Regra 2 - Out of the box
        [TestCase("VIIIIIIIIIIIIIIIIIIIIIII", 0)]
        ////Regra 3
        [TestCase("IV", 4)] //Parei nesse teste
        [TestCase("IX", 9)]
        [TestCase("XXXI", 31)]
        [TestCase("XXXIIIL", 81)]
        

        public void TestParse2RomanNumeral(string atual, int expected)
        {
            Assert.AreEqual(expected, Roman2.Parse2(atual));
        }
        public partial class Roman
        {
            //https://escolakids.uol.com.br/matematica/numeros-romanos-3.htm#:~:text=1%C2%BA%20Regra%20%E2%80%93%20As%20letras%20I,repetidas%20por%20tr%C3%AAs%20vezes%20consecutivas.&text=2%C2%BA%20Regra%20%E2%80%93%20As%20letras%20I,%C3%A0%20letra%20de%20maior%20valor.

            public static int V { get; set; } = 5;
            public static int I { get; set; } = 1;
            public static int X { get; set; } = 10;
            public static int C { get; set; } = 100;
            public static int D { get; set; } = 500;
            public static int M { get; set; } = 1000;

            public static List<KeyValuePair<string, int>> valores = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("V",5),
                new KeyValuePair<string, int>("I",1),
                new KeyValuePair<string, int>("X",10),
                new KeyValuePair<string, int>("C",100),
                new KeyValuePair<string, int>("D",500),
                new KeyValuePair<string, int>("M",1000)
            };


            public static int? Parse(string romanNumeral)
            {
                var qtdI = romanNumeral?.ToList()?.Count(a => a == 'I');
                qtdI = qtdI > 3 ? 3 : qtdI;
                var qtdV = romanNumeral?.ToList()?.Count(a => a == 'V');
                qtdV = qtdV > 3 ? 3 : qtdV;
                var qtdX = romanNumeral?.ToList()?.Count(a => a == 'X');
                qtdX = qtdX > 3 ? 3 : qtdX;
                var qtdC = romanNumeral?.ToList()?.Count(a => a == 'C');
                qtdC = qtdC > 3 ? 3 : qtdC;
                var qtdD = romanNumeral?.ToList()?.Count(a => a == 'D');
                qtdD = qtdD > 3 ? 3 : qtdD;
                var qtdM = romanNumeral?.ToList()?.Count(a => a == 'M');
                qtdM = qtdM > 3 ? 3 : qtdM;

                var valueI = qtdI * I;
                var valueV = qtdV * V;
                var valueX = qtdX * X;
                var valueC = qtdC * C;
                var valueD = qtdD * D;
                var valueM = qtdM * M;


                //3º Regra – As letras I, X, C podem ser escritas antes da outra, tendo seus valores subtraídos da letra de maior valor.
                //Evolution - > 4 -> 1 - 5 - > qtd 
                //if (romanNumeral == "IV")
                //{
                //    return  5 - 1;
                //}

                //if (romanNumeral == "IX")
                //{
                //    return 10 - 1;
                //}

                var letters = romanNumeral.ToList();
                var valor = 0;
                for (int i = 0; i < romanNumeral.Length; i++)
                {
                    var atual = romanNumeral[i];
                    var valueAtual = valores.FirstOrDefault(a => a.Key == atual.ToString()).Value;
                    if (i + 1 >= romanNumeral.Length)
                    {
                        valor += valueAtual;
                        break;
                    }

                    var next = romanNumeral[i + 1];
                    var valueNext = valores.FirstOrDefault(a => a.Key == next.ToString()).Value;

                    if (valueNext > valueAtual)
                    {
                        //XXXIX
                        valor += valueNext - valueAtual;
                        if (i == romanNumeral.Length - 2)
                            break;
                        else
                            continue;
                    }


                    valor += valueAtual;
                }

                return valor;

                //Restrição Regra dois 
                //Evolution 8 -> 5+3 -> value5 + valueI-1 -> limit qtdTo max (3) 
                //if ("VIIII" == romanNumeral)
                //    return valueV+valueI-1;

                //2º Regra – As letras I, X, C podem ser escritas na frente das outras, tendo seus valores somados à letra de maior valor.
                //Evolution -> "VIII" == 8 -> 5+3 -> V + qtdI(quantidade de letras I)*(valor de I) -> qtdI + qtdV + qtdX + qtdC + qtdD + qtdM
                //var valor = valueI + valueV + valueX + valueC + valueD + valueM;
                //if (valor == null)
                //    return 0;
                //return valor;




                //1º Regra – As letras I, X, C, M somente poderão ser repetidas por três vezes consecutivas.
                //if (romanNumeral == "I")
                //    return 1;
                //if (romanNumeral == "II")
                //    return 2;
                //if (romanNumeral == "III")
                //    return 3;

                //if (romanNumeral == "X")
                //    return 10;
                //if (romanNumeral == "XX")
                //    return 20;
                //if (romanNumeral == "XXX")
                //    return 30;

                //if (romanNumeral == "C")
                //    return 100;
                //if (romanNumeral == "CC")
                //    return 200;
                //if (romanNumeral == "CCC")
                //    return 300;

                //if (romanNumeral == "M")
                //    return 1000;
                //if (romanNumeral == "MM")
                //    return 2000;
                //if (romanNumeral == "MMM")
                //    return 3000;

                //return 0;
            }
        }

    }
}
