using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace TIC_TAC_TOE
{
   

    public class ScoreBoard
    {
        static int player;
        public Dictionary<string, int> scoreboard = new Dictionary<string, int>();
        public void Files()
        {
            using (var sr = new StreamReader("Scores.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] Content = line.Split(";");
                    string player = Content[0];
                    int score = int.Parse(Content[1]);
                    scoreboard.Add(player, score);
                }
            }
        }
        public void ScoreForTies(string name1, string name2)
        {
            scoreboard[name1] += 5;
            scoreboard[name2] += 5;
        }
        public void Scoreforplayers(string name1, string name2)
        {
            if (player== 'O')
            {
                scoreboard[name1] += 30;
            }
            else
            {
                scoreboard[name2] += 30;
            }
        }
        public void Addplayer(string name, int NewScore = 0)
        {
            if (!scoreboard.ContainsKey(name))
            {
                scoreboard.Add(name, 0);
            }
        }
        public void Totalscore()
        {
            using (var sr = new StreamWriter(@"C:\Users\HP\source\repos\assignment06-Tic_Tac_Toe\bin\Debug\netcoreapp3.1\Scores.txt"))
            {
                foreach (var entry in scoreboard)
                {
                    sr.WriteLine($"{entry.Key} ; {entry.Value}");
                    sr.Flush();
                }
            }
        }
        public void ShowScore()
        {
            foreach (var entry in scoreboard)
                Console.WriteLine($"player; {entry.Key} scored {entry.Value}");
        }
        class Program
        {
            static int flag = 0;
            static int choice; // position choice
            static int ChoiceOnMenu;
            static char[] section = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            static int player = 1;
            static string player1, player2;



            public static void Main(string[] args) // what will actually run 
            {


                while (true)
                {
                    Gamemenu();

                    ChoiceOnMenu = Convert.ToChar(Console.ReadLine());

                    MenuCases();

                    Console.ReadLine();
                }
            }
           
            // after menu choice the code below will execute whatever action was chosen. contains switch cases
            static void MenuCases()
            {
                ScoreBoard scoreBoard = new ScoreBoard();
                scoreBoard.Files();
                switch (ChoiceOnMenu)
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine(" player1 name: ");
                        player1 = Console.ReadLine();
                        scoreBoard.Addplayer(player1);
                        Console.WriteLine("player2 name: ");
                        player2 = Console.ReadLine();
                        scoreBoard.Addplayer(player2);


                        Console.WriteLine("game board");
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Welcome to tic-tac-toe!");
                            Console.WriteLine("player 1 is X and plaer2 is O");
                            Console.WriteLine("\n");


                            // who gets to play 
                            if (player % 2 == 0)
                            {
                                Console.WriteLine("player 2 chance");

                            }
                            else
                            {
                                Console.WriteLine("player 1 chance ");
                            }
                            Console.Write("\n");

                            gameBoard();

                            choice = int.Parse(Console.ReadLine());


                            if (section[choice] != 'x' && section[choice] != 'o')
                            {
                                if (player % 2 == 0)
                                {
                                    section[choice] = '0';

                                    player++;
                                }
                                else
                                {
                                    section[choice] = 'x';
                                    player++;
                                }
                            }

                            else
                            {// in case of illigal moves 
                                Console.WriteLine("sorry the row {0} is already marked with {1} ", choice, section[choice]);
                                Console.WriteLine("\n");
                                Console.WriteLine("please wait ");


                            }
                            flag = whoWon(); // will check who won 

                            Console.Clear();
                            gameBoard();

                            if (flag == 1)
                            {
                                Console.WriteLine(" player {0} won", (player % 2) + 1);

                                scoreBoard.Scoreforplayers(player1, player2);
                            }
                            else
                            {
                                Console.WriteLine("it's a draw !"); // if the value get to be -1 it is a draw
                                scoreBoard.ScoreForTies(player1, player2);
                            }


                        } while (flag != 1 && flag != -1); // run as lomg as board is not full 

                        scoreBoard.Totalscore();
                        Console.WriteLine("To go to menu press the key!");
                        Console.ReadKey();
                        Console.Clear();
                        break;



                    case '2':
                        Console.WriteLine("the author of this game is Pacifique IRADUKUNDA. She did what she could, ain't gonna lie! THIS IS AWESOME ! ");
                        Console.WriteLine("Press any botton");
                        Console.ReadKey();
                        Console.Clear();
                        break;


                    case '3':
                        Console.Clear();
                        scoreBoard.ShowScore();
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case '4':
                        Console.WriteLine("are you sure you want to quit ? (y/n)");
                        choice = Convert.ToChar(Console.ReadLine());
                        switch (choice)
                        {
                            case 'y':
                                Console.WriteLine("Thank you for playing! ");
                                System.Environment.Exit(-1);
                                break;

                            case 'n':
                                Console.Clear();
                                break;

                        }
                        break;
                }
            }

            // will display memu options 
            static void Gamemenu()
            {
                Console.WriteLine(" WELCOME TO TIC TAC TOE ");
                Console.WriteLine("Press any key to access menu!");
                Console.WriteLine("1. Start the game! ");
                Console.WriteLine("2. About the author ");
                Console.WriteLine("3. player scoreboard ");
                Console.WriteLine("4. Exit the game ");
            }


            // this is a board for the game 
            static void gameBoard()
            {


                Console.WriteLine(" {0} | {1} | {2} ", section[1], section[2], section[3]);
                Console.WriteLine("---+---+---");
                Console.WriteLine(" {0} | {1} | {2} ", section[4], section[5], section[6]);
                Console.WriteLine("---+---+---");
                Console.WriteLine(" {0} | {1} | {2} ", section[7], section[8], section[9]);
            }


            // this will check for the winner 
            private static int whoWon()
            { //rows 
                if (section[1] == section[2] && section[2] == section[3])
                {
                    return 1;
                }

                else if (section[4] == section[5] && section[5] == section[6])
                {
                    return 1;
                }
                else if (section[6] == section[7] && section[7] == section[8])
                {
                    return 1;
                }

                // first column 
                else if (section[1] == section[4] && section[4] == section[7])
                {
                    return 1;
                }

                // second clumn
                else if (section[2] == section[5] && section[5] == section[8])
                {
                    return 1;
                }

                //third column
                else if (section[3] == section[6] && section[6] == section[9])
                {
                    return 1;
                }

                // diagonal 
                else if (section[1] == section[5] && section[5] == section[9])
                {
                    return 1;
                }


                else if (section[3] == section[5] && section[5] == section[7])
                {
                    return 1;
                }

                else if (section[1] != '1' && section[2] != '2' && section[3] != '3' && section[4] != '4' && section[5] != '5' && section[6] != '6' && section[7] != '7' && section[8] != '8' && section[9] != '9')
                {
                    return -1;

                }
                else
                {
                    return 0;
                }

               // scoreBoard.Scoreforplayers(player1, player2);

            }
        }

    }
}














