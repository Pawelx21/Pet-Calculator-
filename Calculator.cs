using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCalculator2
{
    public class Caluclator
    {
        public const int CURRENT_LEVEL = 0;
        public const int LEVEL_TO_KNOW = 1;
        public const int HEALTH = 0;
        public const int MONSTERS = 1;
        public const int ATTACK = 2;
        public static void Run()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("------------------------------------------------\n");
            Console.ForegroundColor = ConsoleColor.White;

            int[] iStatsPerLevel = new int[3];
            iStatsPerLevel[HEALTH] = 5;
            iStatsPerLevel[MONSTERS] = 7;
            iStatsPerLevel[ATTACK] = 4;

            double[] dPercentUp = new double[3];
            double[] dCurrentPercent = new double[3];

            string sInput;
            int[] iLevel = new int[2];
            string[] sLevelsInfo =
            {
                "Aktualny poziom zwierzaka",
                "Na jaki poziom podać wartości"
            };
            string[] sCurrentPercentInfo =
            {
                "Aktualny % HP (np. 1,6)",
                "Aktualny % Moby (np. 1,6)",
                "Aktualny % Atak (np. 1,6)"
            };
            string[] sPercentUpInfo =
            {
                "Przeskok % HP (np. 0,6)",
                "Przeskok % Moby (np. 0,6)",
                "Przeskok % Atak (np. 0,6)"
            };
            for (int i = 0; i < 2; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"[$] {sLevelsInfo[i]}?: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                sInput = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                if (int.TryParse(sInput, out iLevel[i]))
                {
                    if (iLevel[i] < 1 || iLevel[i] > 120)
                    {
                        WantRestart("Podano nieprawidłowy poziom zwierzaka. (Min. 1 | Max. 120)");
                        return;
                    }
                }
                else
                {
                    WantRestart("Podana wartość nie jest cyfrą lub liczbą.");
                    return;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"[$] {sCurrentPercentInfo[i]}?: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                sInput = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;

                if (!double.TryParse(sInput, out dCurrentPercent[i]))
                {
                    WantRestart("Podana wartość nie jest liczbą dziesiętną.");
                    return;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"[$] {sPercentUpInfo[i]}?: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                sInput = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                if (!double.TryParse(sInput, out dPercentUp[i]))
                {
                    WantRestart("Podana wartość nie jest liczbą dziesiętną.");
                    return;
                }
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n------------------------------------------------\n");
            Console.ForegroundColor = ConsoleColor.White;
            double[] dDefaultValues = new double[3];
            double[] dCalculatedValues = new double[3];
            double dResult;
            int iResult;
            for (int i = 0; i < dCalculatedValues.Length; i++)
            {
                dResult = (iLevel[CURRENT_LEVEL] / iStatsPerLevel[i]);
                iResult = (int)Math.Floor(dResult);
                dDefaultValues[i] = dCurrentPercent[i] - (iResult * dPercentUp[i]);
                dResult = iLevel[LEVEL_TO_KNOW] / iStatsPerLevel[i];
                iResult = (int)Math.Floor(dResult);
                dCalculatedValues[i] = dDefaultValues[i] + (iResult * dPercentUp[i]);
                if (double.IsNaN(dCalculatedValues[i]))
                    dCalculatedValues[i] = 2137.420;
            }
            Console.Write($"[$] Wartości peta na poziomie: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(iLevel[LEVEL_TO_KNOW] + "\n"); ;
            Console.ForegroundColor = ConsoleColor.White;

            string[] sFields =
            {
                "HP %",
                "Moby",
                "Atak %"
            };
            for (int i = 0; i < 3; i++)
            {
                Console.Write($"[$] {sFields[i]}: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(String.Format("{0:N1}", dCalculatedValues[i]) + "\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n------------------------------------------------\n");
            Console.ForegroundColor = ConsoleColor.White;
            WantRestart("");
        }
        public static void WantRestart(string sError)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(sError);
            Console.ForegroundColor = ConsoleColor.White;

            int iChoice = 0;
            Console.WriteLine("Czy chcesz zacząć od nowa? ( 0 - Tak | 1 - Nie )");
            Console.Write("Jaki jest twój wybór?: ");
            ConsoleKeyInfo cKey = Console.ReadKey();
            Console.WriteLine("");
            if (char.IsDigit(cKey.KeyChar))
            {
                iChoice = int.Parse(cKey.KeyChar.ToString());
                if (iChoice == 0)
                    Run();
            }
            else WantRestart("Nie wpisałeś cyfry.");
        }
    }
}