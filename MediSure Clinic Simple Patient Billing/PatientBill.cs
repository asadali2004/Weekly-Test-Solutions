using System;

namespace MediSure_Clinic_Simple_Patient_Billing
{
    public class PatientBill
    {

        /// <summary>
        /// Gets or sets the unique identifier for the bill.
        /// </summary>
        #region Entity Properties 
        public string BillId { get; set; }
        public string PatientName { get; set; }
        public bool HasInsurance { get; set; }
        public decimal ConsultationFee { get; set; }
        public decimal LabCharges { get; set; }
        public decimal MedicineCharges { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalPayable { get; set; }
        #endregion

        #region Static Storage
        public static PatientBill LastBill; // Store the last created bill
        public static bool HasLastBill; // Flag to indicate if a last bill exists
        #endregion

        /// <summary>
        /// Creates a new patient bill by prompting the user for billing details and displays the calculated amounts.
        /// </summary>
        /// <remarks>This method interacts with the user via the console to collect billing information,
        /// including patient details, insurance status, and various charges. It calculates the gross amount, applies a
        /// discount if the patient is insured, and displays the final payable amount. The most recently created bill is
        /// stored for later retrieval. This method does not return a value and is intended for use in interactive
        /// console applications.</remarks>
        
        public static void CreateBill()
        {
            #region Create Bill
            // Read BillId from console
            Console.Write("Enter Bill Id: ");
            string BillId = Console.ReadLine();
            if (string.IsNullOrEmpty(BillId))
            {
                Console.WriteLine("Bill Id cannot be empty.");
                return;
            }

            // Read PatientName from console
            Console.Write("Enter Patient Name: ");
            string PatientName = Console.ReadLine();

            // Read HasInsurance from console
            Console.Write("Is the patient insured? (Y/N): ");
            string insuranceInput = Console.ReadLine();
            bool HasInsurance = insuranceInput.Equals("Y", StringComparison.OrdinalIgnoreCase);

            // Read ConsultationFee, LabCharges, MedicineCharges from console
            decimal ConsultationFee = ReadDecimal("Enter Consultation Fee: ", true);
            decimal LabCharges = ReadDecimal("Enter Lab Charges: ", false);
            decimal MedicineCharges = ReadDecimal("Enter Medicine Charges: ", false);

            // Create PatientBill instance and compute amounts
            PatientBill bill = new PatientBill();
            bill.BillId = BillId.Trim();
            bill.PatientName = PatientName.Trim();
            bill.HasInsurance = HasInsurance;
            bill.ConsultationFee = ConsultationFee;
            bill.LabCharges = LabCharges;
            bill.MedicineCharges = MedicineCharges;

            // Compute GrossAmount, DiscountAmount, FinalPayable
            bill.GrossAmount = ConsultationFee + LabCharges + MedicineCharges;
            bill.DiscountAmount = HasInsurance ? bill.GrossAmount * 0.10m : 0m;
            bill.FinalPayable = bill.GrossAmount - bill.DiscountAmount;

            // Store the created bill as the last bill
            LastBill = bill;
            HasLastBill = true;

            // Display the bill details
            Console.WriteLine("\nBill created successfully.");
            Console.WriteLine($"Gross Amount: {bill.GrossAmount:F2}");
            Console.WriteLine($"Discount Amount: {bill.DiscountAmount:F2}");
            Console.WriteLine($"Final Payable: {bill.FinalPayable:F2}");
            Console.WriteLine("------------------------------------------------------------\n");

            #endregion
        }


        #region View Bill
        /// <summary>
        /// Displays the details of the most recently created bill in the console output.
        /// </summary>
        /// <remarks>If no bill has been created, a message is displayed indicating that no bill is
        /// available. This method does not return a value and is intended for use in console applications to review the
        /// last bill's information.</remarks>
        public static void ViewLastBill()
        {
            if (!HasLastBill || LastBill == null)
            {
                Console.WriteLine("No bill available. Please create a new bill first.");
                return;
            }

            Console.WriteLine("\n----------- Last Bill -----------");
            Console.WriteLine($"BillId: {LastBill.BillId}");
            Console.WriteLine($"Patient: {LastBill.PatientName}");
            Console.WriteLine($"Insured: {(LastBill.HasInsurance ? "Yes" : "No")}");
            Console.WriteLine($"Consultation Fee: {LastBill.ConsultationFee:F2}");
            Console.WriteLine($"Lab Charges: {LastBill.LabCharges:F2}");
            Console.WriteLine($"Medicine Charges: {LastBill.MedicineCharges:F2}");
            Console.WriteLine($"Gross Amount: {LastBill.GrossAmount:F2}");
            Console.WriteLine($"Discount Amount: {LastBill.DiscountAmount:F2}");
            Console.WriteLine($"Final Payable: {LastBill.FinalPayable:F2}");
            Console.WriteLine("--------------------------------\n");
        }
        #endregion

        #region Clear Bill
        /// <summary>
        /// Clears the information about the last processed bill.
        /// </summary>
        /// <remarks>After calling this method, the application will no longer retain any data related to
        /// the previous bill. Use this method to reset the bill state before processing a new bill.</remarks>
        public static void ClearLastBill()
        {
            LastBill = null;
            HasLastBill = false;
            Console.WriteLine("Last bill cleared.\n");
        }
        #endregion

        #region Input Validation
        /// <summary>
        /// Reads a decimal value from the console after displaying the specified prompt message, with optional
        /// validation for positivity.
        /// </summary>
        /// <remarks>If the user enters an invalid number or a value that does not meet the positivity
        /// constraint, an error message is displayed and 0 is returned.</remarks>
        /// <param name="message">The message to display to the user as a prompt before reading input.</param>
        /// <param name="mustBePositive">true to require the entered value to be greater than zero; false to allow zero and negative values.</param>
        /// <returns>The decimal value entered by the user if valid; otherwise, 0 if the input is invalid or does not meet the
        /// positivity requirement.</returns>
        private static decimal ReadDecimal(string message, bool mustBePositive)
        {
            decimal value;
            Console.Write(message);

            if (!decimal.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("Invalid number entered.");
                return 0;
            }

            if (mustBePositive && value <= 0)
            {
                Console.WriteLine("Value must be greater than zero.");
                return 0;
            }

            if (!mustBePositive && value < 0)
            {
                Console.WriteLine("Value cannot be negative.");
                return 0;
            }

            return value;
        }
        #endregion
    }
}
