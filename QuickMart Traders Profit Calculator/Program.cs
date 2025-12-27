using System;

namespace QuickMart_Traders_Profit_Calculator
{
    /// <summary>
    /// Entry point and menu handler for QuickMart Traders Profit Calculator.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Serves as the entry point for the QuickMart Traders console application.
        /// </summary>
        /// <remarks>Displays a menu-driven interface that allows users to create new transactions, view
        /// the last transaction, calculate profit or loss, or exit the application. The method runs an interactive loop
        /// until the user chooses to exit.</remarks>
        static void Main()
        {
            bool isRunning = true;

            while (isRunning)
            {
                #region Menu Display

                Console.WriteLine("================== QuickMart Traders ==================");
                Console.WriteLine("1. Create New Transaction (Enter Purchase & Selling Details)");
                Console.WriteLine("2. View Last Transaction");
                Console.WriteLine("3. Calculate Profit/Loss (Recompute & Print)");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your option: ");

                #endregion

                string option = Console.ReadLine();

                #region Menu Handling

                switch (option)
                {
                    case "1":
                        SaleTransaction.CreateTransaction();
                        break;

                    case "2":
                        SaleTransaction.ViewLastTransaction();
                        break;

                    case "3":
                        SaleTransaction.CalculateProfitOrLoss();
                        break;

                    case "4":
                        isRunning = false;
                        Console.WriteLine("Thank you. Application closed normally.");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please select between 1 and 4.");
                        break;
                }

                #endregion
            }
        }
    }
}
