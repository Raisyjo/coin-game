﻿/*--------------------------------------------------------------
 *                HTBLA-Leonding / Class: 1AKING
 *--------------------------------------------------------------
 *      Joey CHU,Tunahan Yildiz,Macanovic Radovan,Jakob Bachl 
 *--------------------------------------------------------------
 * Münzenspiel--- pyramide man zieht abwechselnd mit jedem zug kann man max
 * eine ganze reihe ziehen
 *---------------------------------------------------------------------------
*/

using System;
using System.Threading;


namespace PoseProjekt
{
    public static class Program
    {
        public static void Main()
        {
            CoinGame.Menü();
        }
    }
    public static class CoinGame
    {
        //joey
        public static void Menü()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string[] playerNames = { "1", "2" };
            string charackter = "\u058D";
            bool[,] field = {{ false, false, false, true, false, false, false },
                                { false, false,true, true, true, false, false },
                                { false, true, true, true, true, true, false },
                                { true, true, true, true, true, true, true }
                                                                            };
            int gameMode = 1;
            int count = 1;
            for (bool end = false; end == false;)
            {
                Console.Clear();


                Console.WriteLine("╔═══════════════════Menü════════════════════════════╗");
                Console.WriteLine("║ \u24F5 : Play Game   (Rules at the start)              ║");
                Console.WriteLine("║ \u24F6 : Play Gamemode                                 ║");
                Console.WriteLine("║ \u24F7 : Coin Inventory                                ║");
                Console.WriteLine("║ \u24F8 : Rules                                         ║");
                Console.WriteLine("╚═══════════════════════════════════════════════════╝");
                int input = Tools.ReadInput("Was möchtest du tun [1 - 4]: ", 4);


                switch (input)
                {
                    case 1:
                        Game(field, charackter, playerNames, gameMode, count);
                        break;
                    case 2:
                        Gamemode( ref gameMode);
                        break;
                    case 3:
                        Skin(ref charackter);
                        break;
                    case 4:
                        PrintRules(false);
                        break;
                }
            }
        }
        //Joey
        public static void Animate(int row, int count, int remainig, string charackter)
        {
            //22 + row
            //7 + col
            string shift = "";
            for (int i = 0; i < count; i++)
            {
                shift += charackter;
            }

            int width = Console.WindowWidth;
            Console.CursorVisible = false;
            for (; width - 1 > remainig - count + 12; remainig++)
            {

                if (width - 1 < remainig - count + 12 + shift.Length)
                {
                    shift = shift.Remove(shift.Length - 1, 1);
                }

                Console.SetCursorPosition(remainig - count + 12, 20 + row);
                Console.Write(" " + shift);

                Thread.Sleep(1);

            }
            Console.CursorVisible = true;

        }
       // jakob
        public static void Skin(ref string charackter)
        {
            Console.Clear();
           
            Console.WriteLine("╔═══════════════════Skin═════════════════════════════╗");
            Console.WriteLine("║ \u24F5 : \u058D   (Standard)                                 ║");
            Console.WriteLine("║ \u24F6 : \u2605                                              ║");
            Console.WriteLine("║ \u24F7 : \U0001F525                                             ║");
            Console.WriteLine("║ \u24F8 : \u2694                                              ║");
            Console.WriteLine("║ \u24F9 : \u2620                                              ║");
            Console.WriteLine("║ \u24FA : Back to the menu                               ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
            int input = Tools.ReadInput("Choose a skin: ", 6);


            switch (input)
            {
                case 1:
                   charackter = "\u058D";
                    break;
                case 2:
                    charackter = "\u2605";
                    break;
                case 3:
                    charackter = "\U0001F525";   
                    break;
                case 4:
                    charackter = "\u2694";
                    break;
                case 5:
                    charackter = "\u2620";
                    break;
            }
        }
       //Jakob
        public static void Game(bool[,] field, string charackter, string[] playerNames, int gameMode,int count)
        {
           
            int player = 1;
            bool winner = false;
            bool quit = false;
            
            switch (gameMode)
            {
                case 1:
                    field = new bool[,] { { false, false, false, true, false, false, false },
                                        { false, false,true, true, true, false, false },
                                        { false, true, true, true, true, true, false },
                                        { true, true, true, true, true, true, true } };
                    break;
                case 2:
                    field = new bool[,] { { true, true, true, true, true, true, true },
                                        { true, true, true, true, true, true, true  },
                                        { true, true, true, true, true, true, true  },
                                        { true, true, true, true, true, true, true } };
                    break;
            }
            
            do
            {
                PrintRules(true);
                CoinGame.PrintField(field, charackter);
                quit = CoinGame.Turn(field, ref player, charackter, playerNames);
                winner = CoinGame.IsWinner(false, field);
                if (winner)
                {
                   
                    Console.Write("\U0001F3C5");
                    Console.WriteLine(" Spieler " + (player == 1 ? playerNames[0] : playerNames[1]) + " hat gewonnen \U0001F3C6");
                    Console.ReadKey();
                }
            } while (!winner && !quit);
        }
        //Tunahan
        public static bool IsWinner(bool end, bool[,] field)
        {
            if (!end)
            {
                for (int i = 0; i < field.GetLength(0); i++)
                {
                    for (int j = 0; j < field.GetLength(1); j++)
                    {
                        if (field[i, j] == true)
                        {
                            return false; // ja bro
                        }
                    }
                }
            }
            return true;
        }
        // Tunahan
        public static void PrintField(bool[,] field, string charackter)
        {
            for (int row = 0; row < field.GetLength(0); row++)
            {
                Console.Write($"{row + 1}. Row ({RemainigInRow(field,row)}): ");
                for (int col = 0; col < field.GetLength(1); col++)
                {
                    if (field[row, col])
                        Console.Write(charackter);

                }
                Console.WriteLine();
            }
        }
       //Radovan
        public static void PrintRules(bool inGame)
        {
            Console.Clear();
            Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════╗", Console.ForegroundColor = ConsoleColor.Green);
            Console.WriteLine("║  REGELN                                                                 ║");
            Console.WriteLine("║                                                                         ║");
            Console.WriteLine("║ 1.Spieler ziehen abwechselnd                                            ║");
            Console.WriteLine("║ 2.Nur 1 Reihe pro Zug                                                   ║");
            Console.WriteLine("║ 3.Wer das letzte nimmt, verliert                                        ║");
            Console.WriteLine("║                                                                         ║");
            Console.WriteLine("║                                                                         ║");
            Console.WriteLine("║                                                                         ║");
            Console.WriteLine("║  Eingabe im Spiel                                                       ║");
            Console.WriteLine("║                                                                         ║");
            Console.WriteLine("║ Zuerst die Reihe angeben und dann wie viele genommen werden sollen      ║");
            Console.WriteLine("║    \"reihe, anzahl\"                                                      ║");
            Console.WriteLine("║ Bsp: 2,1 (Aus der 2 Reihe wird 1 genommen)                              ║");
            Console.WriteLine("║      4,6 (Aus der 4 Reihe werden 6 genommen)                            ║");
            Console.WriteLine("║                                                                         ║");
            Console.WriteLine("║ \"exit\" um das Spiel abzu brechen                                        ║");
            Console.WriteLine("║ Ungültige Eingaben werden  wiederholt                                   ║");
            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            

            if (!inGame)
            {
                Console.Write("Beliebige Taste drücken, um zum Menü zurück zu kehren...");
                Console.ReadKey();
            }
        }
        // Jakob
        public static bool Turn(bool[,] field, ref int player, string charackter, string[] playerNames)
        {
            bool valid;
            int row, count;
            string input;
            do
            {
                Console.CursorVisible = true;
                input = Tools.ReadInput("Player " + playerNames[player - 1] + ": ");
                valid = ValidInput(input, field, out row, out count);
                if (!valid && input != "exit")
                {
                    Console.WriteLine("\U0001F5F2 Ungültige Eingabe \U0001F5F2  ", Console.ForegroundColor = ConsoleColor.Red);
                    Console.ResetColor();
                }
            } while (!valid && input != "exit");

            if (input == "exit")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\U0001F3AE Game over \U0001F3AE ");
                Console.WriteLine("Spiel Abgebrochen");
                Console.ResetColor();
                Console.Write("Taste drücken um ins Menü zurück zu kehren....");
                Console.ReadKey();
                return true;
            }
            Animate(row, count, RemainigInRow(field, row - 1), charackter);
            UpdateField(field, row - 1, count);
            player = 1 == player ? 2 : 1;
            return false;
        }
       // Joey
        public static void Gamemode(ref int gameMode)
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════Game - Mode══════════════════════╗");
            Console.WriteLine("║ \u24F5 : Standard (Pyramide)                            ║");
            Console.WriteLine("║ \u24F6 : Block                                          ║");
            Console.WriteLine("║ \u24F7 : Zurück zum Menü                                ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
           int input = Tools.ReadInput("Wähle ein Spielmodus aus[1-3]: ", 3);
            if (input != 3)
            {
                gameMode = input;
            }
        }
       //Joey
        public static bool ValidInput(string input, bool[,] field, out int row, out int count)
        {
            string[] parts = input.Split(',');
            count = -1; 
            row = -1;
            if (input == "exit")
            {
                return true;
            }
            if (parts.Length != 2)
            {
                return false;
            }
                                 
            bool paseed = int.TryParse(parts[1].Trim(), out count) && int.TryParse(parts[0].Trim(), out row);
            int remainigInRow = RemainigInRow(field, row - 1);
            if ( paseed && row <= field.GetLength(0) && row >= 1 && count > 0 &&remainigInRow >= count)
            {
                return true;
            }
            return false;
        }
        // Radovan
        public static void UpdateField(bool[,] field, int row, int count)
        {
            for (int i = 0; count > 0; i++)
            {
                if (field[row , i])
                {
                    field[row , i] = false;
                    count--;
                }
            }
        }

        // Radovan
        public static int RemainigInRow(bool[,] field, int row)
        {
            int count = 0;
            for (int i = 0; i < field.GetLength(1) && row < field.GetLength(0) && row >= 0; i++)
            {
                if (field[row, i])
                {
                    count++;
                }
            }
            return count;
        }
    }
    public class Tools
    {
       // Radovan
        public static string ReadInput(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
        //Tunahan
        public static int ReadInput(string message, int max)
        {
            int input;
            bool valid = false;
            do
            {
                Console.Write(message);

                if (int.TryParse(Console.ReadLine(), out input))
                {
                    valid = input <= max && input > 0;
                }
                if (!valid)
                {
                    Console.WriteLine("\U0001F5F2 Ungültige Eingabe\U0001F5F2  ", Console.ForegroundColor = ConsoleColor.Red);
                    Console.ResetColor();
                }
            } while (!valid);
            return input;
        }
    }
}
