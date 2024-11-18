using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
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

            public static int coffee;
            public static int water;
            public static int milk;
            public static int sugar;
            public static int classicTea;
            public static int mintTea;


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

            Console.WriteLine("Select (9.) to cancel...");
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
                        float change = Globals.Balance - Globals.priceOfDrink;
                        Globals.FundBalance -= change;
                        Globals.balance = 0;
                        Console.Clear();
                        Console.WriteLine("Goodbye!");
                        Thread.Sleep(2000);
                        Console.Clear();
                        InsertCash();
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
                            Console.Clear();
                            Console.WriteLine("Please try with a smaller amount!");
                            Console.ForegroundColor = ConsoleColor.White;
                            Thread.Sleep(1500);
                            Console.Clear();
                            InsertCash();

                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Beep();
                            Console.Clear();
                            Console.WriteLine("ERROR! Try again!");
                            Console.ForegroundColor = ConsoleColor.White;
                            Thread.Sleep(1500);
                            Console.Clear();
                            InsertCash();

                        }
                    }
                }
                catch {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Beep();
                    Console.Clear();
                    Console.WriteLine("Incorrect input! Please try again!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(1500);
                    Console.Clear();
                    InsertCash();
                }
            }
            Globals.flag = false;
            return balance;
        }

        private static void WantSugar()
        {
            Console.Clear();
            int sugar = 3;
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
                        //Console.WriteLine("Try again!");
                        break;
                }
            }

            switch (sugar) 
            {
                case 1:
                    Globals.sugar -= 5;
                    break;
                case 2:
                    Globals.sugar -= 10;
                    break;
                case 3:
                    Globals.sugar -= 15;
                    break;
                case 4:
                    Globals.sugar -= 20;
                    break;
                case 5:
                    Globals.sugar -= 25;
                    break;
                case 6:
                    Globals.sugar -= 30;
                    break;
            }
        }

        // Tranzaction processing
        public static void Processing()
        {
            if (Globals.water < 150 || Globals.sugar < 100 || Globals.coffee < 100 || Globals.classicTea < 100 ||
                Globals.milk < 75 || Globals.mintTea < 100)
            {
                while (true)
                {
                    Console.WriteLine("The machine is low on stock\nPress (Enter) to go to service mode...");
                    ConsoleKeyInfo keyInfo1 = Console.ReadKey();
                    if (keyInfo1.Key == ConsoleKey.Enter)
                        Refill();
                    else
                        Console.Clear();
                }
            }
            else
            {
                switch (Globals.option)
                {
                    case 1: 
                        {
                            Globals.priceOfDrink = 1;
                            Globals.drink = "Espresso";
                            Globals.coffee -= 50;
                            Globals.water -= 65;
                            break;
                        }
                    case 2: 
                        {
                            Globals.priceOfDrink = 1;
                            Globals.drink = "Coffee";
                            Globals.coffee -= 50;
                            Globals.water -= 85;
                            break;                     
                        }
                    case 3:
                        {
                            Globals.priceOfDrink = 1.5f;
                            Globals.drink = "Latte Machiato";
                            Globals.coffee -= 50;
                            Globals.water -= 65;
                            Globals.milk -= 50;
                            break;
                        }
                    case 4:
                        { 
                            Globals.priceOfDrink = 1.5f;
                            Globals.drink = "Americano";
                            Globals.coffee -= 50;
                            Globals.water -= 100;
                            break; 
                        }
                    case 5:
                        {
                            Globals.priceOfDrink = 2;
                            Globals.drink = "Capuccino"; 
                            Globals.coffee -= 50;
                            Globals.water -= 65;
                            Globals.milk -= 75;
                            break;
                        }
                    case 6:
                        {
                            Globals.priceOfDrink = 2.25f;
                            Globals.drink = "Irish coffee";
                            Globals.coffee -= 50;
                            Globals.water -= 75;
                            Globals.milk -= 75;

                            break;
                        }
                    case 7:
                        {
                            Globals.priceOfDrink = 1.5f;
                            Globals.drink = "Classic tea";
                            Globals.classicTea -= 75;
                            Globals.water -= 150;

                            break;
                        }
                    case 8:
                        {
                            Globals.priceOfDrink = 1.5f;
                            Globals.drink = "Mint tea";
                            Globals.mintTea -= 75;
                            Globals.water -= 150;
                            break;
                        }
                }
            }

            if (Globals.priceOfDrink > Globals.balance)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Beep();
                Console.WriteLine("Insufficient balance. Please try again...");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(2000);
                Console.Clear();
                InsertCash();
            }
            else{
                Console.Clear();
                Console.WriteLine("Please wait...");

                string text = $"{Globals.drink} - {Globals.priceOfDrink}$";
                DateTime dateTime = DateTime.Now;
                File.AppendAllText(@"..\..\PrvPurch.txt", text + "   " + dateTime + Environment.NewLine);
                File.WriteAllText(@"..\..\FundBalance.txt", $"{Globals.FundBalance}");
                File.WriteAllText(@"..\..\Stocks.txt", $"{Globals.coffee} : {Globals.water} : {Globals.milk} : {Globals.sugar} : {Globals.classicTea} : {Globals.mintTea}");


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
                        InsertCash();
                    }
                    else if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        if (Globals.water < 150 || Globals.sugar < 100 || Globals.coffee < 100 || Globals.classicTea < 100 ||
                Globals.milk < 75 || Globals.mintTea < 100)
                        {
                            while (true)
                            {
                                Console.WriteLine("The machine is low on stock\nPress (Enter) to go to service mode...");
                                ConsoleKeyInfo keyInfo1 = Console.ReadKey();
                                if (keyInfo1.Key == ConsoleKey.Enter)
                                    Refill();
                                else
                                    Console.Clear();
                            }
                        }
                        else
                        {
                            ok = true;
                            Globals.Balance -= Globals.priceOfDrink;
                            Options();
                            WantSugar();
                        }
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

        private static void Refill()
        {
            int input;

            Console.Clear();
            Console.WriteLine("Welcome to Refill mode!\nType in the password:");
            try
            {
                input = int.Parse(Console.ReadLine());
                if(input == Globals.Password)
                {
                    bool ok = true;
                    int opt;
                    while (ok)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"water: {Globals.water}\n" +
                                        $"coffee: {Globals.coffee}\n" +
                                        $"milk: {Globals.milk}\n" +
                                        $"sugar: {Globals.sugar}\n" +
                                        $"classic tea: {Globals.classicTea}\n" +
                                        $"mint tea: {Globals.mintTea}");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nSelect an option:\n" +
                            "1. Refill Water\n" +
                            "2. Refill Coffee\n" +
                            "3. Refill Milk\n" +
                            "4. Refill Sugar\n" +
                            "5. Refill Classic Tea\n" +
                            "6. Refill Mint Tea\n" +
                            "7. Back to Service mode");
                        Console.ForegroundColor = ConsoleColor.White;
                        try
                        {
                            //coffee = 5000;
                            //water = 45;
                            //milk = 500;
                            //sugar = 1000;
                            //classicTea = 500;
                            //mintTea = 500;
                            opt = int.Parse(Console.ReadLine());
                            switch(opt)
                            {
                                case 1:
                                    Globals.water = 5000;
                                    break;
                                case 2:
                                    Globals.coffee = 3000;
                                    break;
                                case 3:
                                    Globals.milk = 1000;
                                    break;
                                case 4:
                                    Globals.sugar = 1500;
                                    break;
                                case 5:
                                    Globals.classicTea = 1000;
                                    break;
                                case 6:
                                    Globals.mintTea = 1000;
                                    break;
                                case 7:
                                    Console.Clear();
                                    Thread.Sleep(1500);
                                    ServiceOptions();
                                    break;
                                default:
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Try again!");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Thread.Sleep(1500);
                                    break;
                            }
                            File.WriteAllText(@"..\..\Stocks.txt", $"{Globals.coffee} : {Globals.water} : {Globals.milk} : {Globals.sugar} : {Globals.classicTea} : {Globals.mintTea}");
                        }
                        catch
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Try again!");
                            Console.ForegroundColor = ConsoleColor.White;
                            Thread.Sleep(1500);
                        }
                    }
                }
                else if (Globals.water < 150 || Globals.sugar < 100 || Globals.coffee < 100 || Globals.classicTea < 100 ||
                Globals.milk < 75 || Globals.mintTea < 100)
                {
                    bool ok = false;
                    while (!ok)
                    {
                        Console.WriteLine("The machine is low on stock\nPress (Enter) to go to service mode...");
                        ConsoleKeyInfo keyInfo = Console.ReadKey();
                        if (keyInfo.Key == ConsoleKey.Enter)
                            Refill();
                        else
                            Console.Clear();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Try again!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(1500);
                    Refill();
                }
            }
            catch {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Try again!");
                Console.ForegroundColor= ConsoleColor.White;
                Thread.Sleep(1500);
                Refill();
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
                "4. Refill the stocks\n" +
                "5. Exit service mode\n" +
                "6. Close the app");
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
                        Console.Clear();
                        ServiceOptions();
                        ok2 = false;
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid input. Try again!");
                    Thread.Sleep(500);
                    Console.Clear();
                    ServiceOptions();
                    ok2 = false;
                }
                switch (opt)
                {
                    case 1:
                        Globals.FundBalance = 0;
                        TextWriter save = new StreamWriter(@"..\..\FundBalance.txt");
                        save.Write('0');
                        save.Close();
                        Console.Clear();
                        ServiceOptions();
                        break;
                    case 2:
                        TextWriter write = new StreamWriter(@"..\..\PrvPurch.txt");
                        write.Write("");
                        write.Close();
                        Console.Clear();
                        ServiceOptions();
                        break;
                    case 3:
                        ViewPrv();
                        break;
                    case 4:
                        Refill();
                        break;

                    case 5:
                        Console.Clear();
                        Console.WriteLine("Goodbye!");
                        Thread.Sleep(2000);
                        ok2 = true;
                        Start();
                        break;
                    case 6:
                        Console.Clear();
                        Console.WriteLine("Goodbye!");
                        Thread.Sleep(2000);
                        System.Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Error");
                        Console.Clear();
                        ServiceOptions();
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
            
            Console.Clear();
            Globals.flag = false;
            TextReader textReader = new StreamReader(@"..\..\FundBalance.txt");
            try
            {
                Globals.FundBalance = float.Parse(textReader.ReadToEnd());
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                Thread.Sleep(1500);
                System.Environment.Exit(0);
            }
            textReader.Close();


            string stocks = File.ReadAllText(@"..\..\Stocks.txt");
            bool ok1 = false;
            while (!ok1)
            {
                try
                {
                    ok1 = true;

                    Regex r = new Regex("(\\d+) : (\\d+) : (\\d+) : (\\d+) : (\\d+) : (\\d+)");
                    Match m = r.Match(stocks);

                    Globals.coffee = int.Parse(m.Groups[1].Value);
                    Globals.water = int.Parse(m.Groups[2].Value);
                    Globals.milk = int.Parse(m.Groups[3].Value);
                    Globals.sugar = int.Parse(m.Groups[4].Value);
                    Globals.classicTea = int.Parse(m.Groups[5].Value);
                    Globals.mintTea = int.Parse(m.Groups[6].Value);
                }
                catch( Exception e ) {
                    Console.WriteLine(e);                    
                }
            }

            while (Globals.flag != true)
            {
                
                if (Globals.water < 150 || Globals.sugar < 100 || Globals.coffee < 100 || Globals.classicTea < 100 ||
                Globals.milk < 75 || Globals.mintTea < 100)
                {
                    bool ok = false;
                    while (!ok) {
                        Console.WriteLine("The machine is low on stock\nPress (Enter) to go to service mode...");
                        ConsoleKeyInfo keyInfo = Console.ReadKey();
                        if (keyInfo.Key == ConsoleKey.Enter)
                            Refill();
                        else
                            Console.Clear();
                    }
                }
                InsertCash();
                using(StreamWriter saveBalance = new StreamWriter(@"..\..\FundBalance.txt"))
                {
                    saveBalance.Write(Globals.FundBalance);
                }
                using(StreamWriter saveStocks = new StreamWriter(@"..\..\Stocks.txt"))
                {
                    saveStocks.WriteLine($"{Globals.coffee} : {Globals.water} : {Globals.milk} : {Globals.sugar} : {Globals.classicTea} : {Globals.mintTea}");
                }
                
                
            } 
        }
        

        static void Main(string[] args)
        {
            Start();
        }
    }
}
