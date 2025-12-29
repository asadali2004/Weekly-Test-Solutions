using System;


namespace MediSure_Clinic_Simple_Patient_Billing
{

    /// <summary>
    /// Provides the entry point for the MediSure Clinic Billing application.
    /// </summary>
    /// <remarks>This class contains the application's main loop, presenting a console-based menu for
    /// creating, viewing, and clearing patient bills. The application remains active until the user chooses to
    /// exit.</remarks>
    class Program
    {

        /// <summary>
        /// Serves as the entry point for the MediSure Clinic Billing application.
        /// </summary>
        /// <remarks>This method displays a console-based menu for creating, viewing, and clearing patient
        /// bills. The application continues running until the user selects the exit option.</remarks>
        /// <param name="args">An array of command-line arguments supplied to the application. This parameter is not used.</param>
        static void Main(string[] args)
        {
            
            bool running = true; 

            while (running) // Keep asking User input until user enter option Exit
            {
                #region Menu Display
                Console.WriteLine("================== MediSure Clinic Billing ==================");
                Console.WriteLine("1. Create New Bill (Enter Patient Details)");
                Console.WriteLine("2. View Last Bill");
                Console.WriteLine("3. Clear Last Bill");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your option: ");
                #endregion

                string input = Console.ReadLine();
                Console.WriteLine();
                
                #region Menu Handling
                switch (input) 
                {
                    case "1":
                        PatientBill.CreateBill();
                        break;

                    case "2":
                        PatientBill.ViewLastBill();
                        break;

                    case "3":
                        PatientBill.ClearLastBill();
                        break;

                    case "4":
                        running = false;
                        Console.WriteLine("Thank you. Application closed normally.");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please select between 1 and 4.\n");
                        break;
                }
                #endregion
            }
        }
    }
}
