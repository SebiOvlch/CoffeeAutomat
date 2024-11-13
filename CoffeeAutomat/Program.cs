using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoffeeAutomat
{
    internal class Program
    {
        // Global variables: 
        public static class Globals
        {
            public static bool flag = false;
            public static bool Flag
            {
                get { return flag; }
                set { flag = value; }
            }

            
            public static float priceOfDrink = 0;
            public static float PriceOfDrink
            {
                get { return priceOfDrink; }
                set { priceOfDrink = value; }
            }

            public static string drink;

            public static int option;
            public static int Option
            {
                get{ return option; }
                set { option = value; }
            }

            public static float balance = 0;
            public static float Balance
            {
                get { return balance; }
                set { balance = value; }
            }

            private static int password = 784512;
            public static int Password
            {
                get { return password; }
            }

            private static float fundBalance;
            public static float FundBalance
            {
                get
                {
                    return fundBalance;
                }
                set
                {
                    fundBalance = value;
                }
            }

        }

        // User interface 
        public static int Options()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"Your balance is {Globals.balance}$");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Please select a drink!");
            Console.WriteLine("1. Espresso - 1$");
            Console.WriteLine("2. Coffee - 1$");
            Console.WriteLine("3. Latte Machiato - 1.5$");
            Console.WriteLine("4. Americano - 1.5$");
            Console.WriteLine("5. Capuccino - 2$");
            Console.WriteLine("6. Irish coffee - 2.25$");
            Console.WriteLine("7. Classic tea - 1.5$");
            Console.WriteLine("8. Mint tea - 1.5$");

            Console.WriteLine("Select (9.) to close...");
            Console.ForegroundColor = ConsoleColor.White;

            Globals.flag = false;
            while (Globals.flag != true)
            {
                try
                {

                    int opt = int.Parse(Console.ReadLine());
                    Globals.option = opt;
                    if (opt > 0 && opt <= 8)
                    {
                        Globals.flag = true;
                        return opt;
                    }
                    else if (opt == 9) 
                    {
                        Globals.FundBalance = Globals.FundBalance - Globals.balance;
                        Console.WriteLine("Thank you! Have a nice day!");
                        Thread.Sleep(2000);
                        System.Environment.Exit(0);
                    } 
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Beep();
                        Console.WriteLine("The option you gave is incorrect.\nPlease try again!");

                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                catch {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Beep();
                    Console.WriteLine("Incorrect input! Please try again!");
                    Console.ForegroundColor = ConsoleColor.White;
                }

            } 
            return 0;
        }

        // Cash input function
        public static float InsertCash()
        {
            float balance = 0;
            Console.WriteLine("Please insert cash!" +
                "\nThe machine only works with 1 or 5 dollar bill, or 5, 10 or 50 Cents");
            Console.WriteLine("Press (9) to Exit...");

            bool flag = false;
            while (flag != true)
            {
                try
                {
                    balance = float.Parse(Console.ReadLine());

                    if (balance > 0f && balance < 9f)
                    {
                        Globals.Balance += balance;
                        Globals.FundBalance = Globals.FundBalance + balance;
                        Options();
                        WantSugar();
                        flag = true;
                        Globals.flag = false;
                        return balance;
                    }else if(balance == 9)
                    {
                        Console.WriteLine("Thank you! Have a nice day!");
                        Globals.FundBalance = Globals.FundBalance + balance;
                        Thread.Sleep(2000);
                        System.Environment.Exit(0);
                    }
                    else if(balance == 7245)
                    {
                        ServiceMode();
                    }
                    else
                    {
                        if (balance > 9)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Beep();
                            Console.WriteLine("Please try with a smaller amount!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Beep();
                            Console.WriteLine("ERROR! Try again!");
                            Console.ForegroundColor = ConsoleColor.White;
                        
                        }
                    }
                }
                catch {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Beep();
                    Console.WriteLine("Incorrect input! Please try again!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            Globals.flag = false;
            return balance;
        }

        private static void WantSugar()
        {
            Console.Clear();
            int sugar = 5;
            bool flag = true;

            while (flag) {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("How much sugar do you want(set with left or rigth arrow, then press (Enter): ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(sugar);
                Console.ForegroundColor = ConsoleColor.White;
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if(sugar > 0)
                            sugar--;
                        Console.Clear();
                        break;
                    case ConsoleKey.RightArrow:
                        if(sugar < 6)
                            sugar++;
                        Console.Clear();
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        Processing();
                        flag = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Try again!");
                        break;
                }
            }
        }

        // Tranzaction processing
        public static void Processing()
        {
            
            switch (Globals.option)
            {
                case 1: 
                    {
                        Globals.priceOfDrink = 1;
                        Globals.drink = "Espresso";
                        break;
                    }
                case 2: 
                    {
                        Globals.priceOfDrink = 1;
                        Globals.drink = "Coffee";
                        break;                     
                    }
                case 3:
                    {
                        Globals.priceOfDrink = 1.5f;
                        Globals.drink = "Latte Machiato";
                        break;
                    }
                case 4:
                    { 
                        Globals.priceOfDrink = 1.5f;
                        Globals.drink = "Americano";
                        break; 
                    }
                case 5:
                    {
                        Globals.priceOfDrink = 2;
                        Globals.drink = "Capuccino";
                        break;
                    }
                case 6:
                    {
                        Globals.priceOfDrink = 2.25f;
                        Globals.drink = "Irish coffee";
                        break;
                    }
                case 7:
                    {
                        Globals.priceOfDrink = 1.5f;
                        Globals.drink = "Classic tea";

                        break;
                    }
                case 8:
                    {
                        Globals.priceOfDrink = 1.5f;
                        Globals.drink = "Mint tea";
                        break;
                    }
            }

            if (Globals.priceOfDrink > Globals.balance)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Beep();
                Console.WriteLine("Insufficient balance. Please try again...");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(2000);
                InsertCash();

            }else
            {
                Console.Clear();
                Console.WriteLine("Please wait...");

                string text = $"{Globals.drink} - {Globals.priceOfDrink}$";
                DateTime dateTime = DateTime.Now;
                File.AppendAllText(@"..\..\PrvPurch.txt", text + "   " + dateTime + Environment.NewLine);


                // **********
                for (int i = 0; i < 10; i++)
                {
                    Console.Write('*');
                    Thread.Sleep(500);
                }
                Console.WriteLine();

                bool ok = false;
                while (ok != true)
                {
                    Console.WriteLine("Press (Enter) to continue purchase or (Esc) to get the change!");
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        ok = true;
                        Console.Clear();
                        float change = Globals.Balance - Globals.priceOfDrink;
                        Globals.FundBalance -= change;
                        Globals.balance = 0;
                        Console.WriteLine("Thank you for the purchase!\n" +
                            $"Your change is {change}$");

                        // **********
                        for (int i = 0; i < 10; i++)
                        {
                            Console.Write('*');
                            Thread.Sleep(500);
                        }
                        Console.Clear();
                    }
                    else if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        ok = true;
                        Globals.Balance -= Globals.priceOfDrink;
                        Options();
                        WantSugar();
                    }
                    else
                    {
                        Console.Clear();
                        Console.Beep();
                        Console.WriteLine("Try again!");
                        ok = false;
                        continue;
                    }
                }
            }
        }

        public static void ServiceMode()
        {
            int input;
            bool ok = false;
            Console.Clear();
            Console.WriteLine("Welcome into service mode!\n" +
                "Please type in the password: ");
            while (ok != true)
            {
                try
                {
                    input = int.Parse(Console.ReadLine());

                    if (input == Globals.Password)
                    {
                        ServiceOptions();
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong Password! Try again!");
                        Console.ForegroundColor = ConsoleColor.White;

                        ok = false;
                    }
                    
                }
                catch
                {
                    Console.Clear();
                    Console.Beep();
                    Console.WriteLine("Try again!");
                }
            }
            ok = false;

        }

        private static void ServiceOptions()
        {
            Console.Clear();
            Console.WriteLine($"The Fund Balance is: {Globals.FundBalance}$");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Select an option:\n" +
                "1. Reset Fund Balance\n" +
                "2. Reset Purchase memory\n" +
                "3. Get the recent purhases\n" +
                "4. Exit service mode");
            Console.ForegroundColor = ConsoleColor.White;

            int opt = 0;
            bool ok2 = false;
            while (!ok2)
            {
                try
                {
                    opt = int.Parse(Console.ReadLine());
                    if (opt > 4 && opt <= 0)
                    {
                        Console.WriteLine("Invalid input. Try again!");
                        ok2 = false;
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid input. Try again!");
                    Thread.Sleep(500);
                    ok2 = false;
                }
                switch (opt)
                {
                    case 1:
                        Globals.FundBalance = 0;
                        TextWriter save = new StreamWriter(@"..\..\FundBalance.txt");
                        save.Write('0');
                        save.Close();
                        break;
                    case 2:
                        TextWriter write = new StreamWriter(@"..\..\PrvPurch.txt");
                        write.Write("");
                        write.Close();
                        break;
                    case 3:
                        ViewPrv();
                        break;

                    case 4:
                        Console.WriteLine("Goodbye!");
                        Thread.Sleep(2000);
                        Console.Clear();
                        ok2 = true;
                        Start();
                        break;
                    default:
                        Console.WriteLine("Error");
                        break;
                }

            }
            
        }

        private static void ViewPrv()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Press (Esc) to go back.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            string s = File.ReadAllText(@"..\..\PrvPurch.txt");
            Console.WriteLine(s);
            bool ok = false;
            while (!ok)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    ServiceOptions();
                    ok = true;
                }
                else
                {
                    Console.Beep();
                    Console.WriteLine("Try again!");
                    ok = false;
                }
            }
        }

        public static void Start(){
            Globals.flag = false;
            TextReader textReader = new StreamReader(@"..\..\FundBalance.txt");
            try
            {
                Globals.FundBalance = float.Parse(textReader.ReadToEnd());
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                for (int i = 5; i > 0; i--) {
                    Console.Write(i + " ");
                    Thread.Sleep(1000);
                }
                System.Environment.Exit(0);
            }
            textReader.Close();

            while (Globals.flag != true)
            {
                InsertCash();
                //Console.WriteLine(Globals.FundBalance);
                using(StreamWriter save = new StreamWriter(@"..\..\FundBalance.txt"))
                {
                    save.Write(Globals.FundBalance);
                }
            } 
        }
        

        static void Main(string[] args)
        {   
            Start();
        }
    }
}
