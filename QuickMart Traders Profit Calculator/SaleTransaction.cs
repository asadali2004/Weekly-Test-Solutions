using System;

namespace QuickMart_Traders_Profit_Calculator
{
    /// <summary>
    /// Represents a sales transaction, including details such as invoice number, customer, item, quantities, amounts,
    /// and calculated profit or loss information.
    /// </summary>
    /// <remarks>The SaleTransaction class provides properties for storing transaction data and static methods
    /// for creating, viewing, and calculating profit or loss for the most recent transaction. Only one transaction is
    /// stored at a time; each new transaction overwrites the previous one. This class is intended for simple,
    /// single-transaction scenarios and is not thread-safe.</remarks>
    public class SaleTransaction
    {
        // Plan (pseudocode):
        // - Create/Register Method:
        //   - Read inputs from console: invoiceNo, customerName, itemName, quantity, purchaseAmount, sellingAmount
        //   - Validate: invoiceNo not empty; quantity > 0; purchaseAmount > 0; sellingAmount >= 0
        //   - Create SaleTransaction instance
        //   - Compute:
        //     - if selling > purchase: status=PROFIT, amount=selling - purchase
        //     - else if selling < purchase: status=LOSS, amount=purchase - selling
        //     - else: status=BREAK-EVEN, amount=0
        //     - marginPercent = (amount / purchase) * 100
        //   - Store to LastTransaction and set HasLastTransaction = true
        //   - Print formatted transaction with values rounded to 2 decimals
        //
        // - View Method:
        //   - If HasLastTransaction false -> print message and return
        //   - Print LastTransaction in formatted output, rounding to 2 decimals
        //
        // - Calculation Method:
        //   - If HasLastTransaction false -> print message and return
        //   - Recompute profit/loss and margin on LastTransaction (same rules as above)
        //   - Print formatted transaction with rounded values

        #region Properties

        public string InvoiceNo { get; set; }
        public string CustomerName { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal PurchaseAmount { get; set; }
        public decimal SellingAmount { get; set; }
        public string ProfitOrLossStatus { get; set; }
        public decimal ProfitOrLossAmount { get; set; }
        public decimal ProfitMarginPercent { get; set; }

        #endregion

        #region Static Storage

        public static SaleTransaction LastTransaction;
        public static bool HasLastTransaction = false;

        #endregion

        #region Create/Register Method

        public static void CreateTransaction()
        {
            Console.Write("Enter Invoice No: ");
            string invoiceNo = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(invoiceNo))
            {
                Console.WriteLine("Invoice No cannot be empty.");
                return;
            }

            Console.Write("Enter Customer Name: ");
            string customerName = Console.ReadLine();

            Console.Write("Enter Item Name: ");
            string itemName = Console.ReadLine();

            Console.Write("Enter Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
            {
                Console.WriteLine("Quantity must be greater than 0.");
                return;
            }

            Console.Write("Enter Purchase Amount (total): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal purchaseAmount) || purchaseAmount <= 0)
            {
                Console.WriteLine("Purchase Amount must be greater than 0.");
                return;
            }

            Console.Write("Enter Selling Amount (total): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal sellingAmount) || sellingAmount < 0)
            {
                Console.WriteLine("Selling Amount must be >= 0.");
                return;
            }

            var transaction = new SaleTransaction
            {
                InvoiceNo = invoiceNo.Trim(),
                CustomerName = (customerName ?? string.Empty).Trim(),
                ItemName = (itemName ?? string.Empty).Trim(),
                Quantity = quantity,
                PurchaseAmount = purchaseAmount,
                SellingAmount = sellingAmount
            };

            // Compute profit/loss
            if (transaction.SellingAmount > transaction.PurchaseAmount)
            {
                transaction.ProfitOrLossStatus = "PROFIT";
                transaction.ProfitOrLossAmount = transaction.SellingAmount - transaction.PurchaseAmount;
            }
            else if (transaction.SellingAmount < transaction.PurchaseAmount)
            {
                transaction.ProfitOrLossStatus = "LOSS";
                transaction.ProfitOrLossAmount = transaction.PurchaseAmount - transaction.SellingAmount;
            }
            else
            {
                transaction.ProfitOrLossStatus = "BREAK-EVEN";
                transaction.ProfitOrLossAmount = 0m;
            }

            transaction.ProfitMarginPercent = (transaction.PurchaseAmount == 0m)
                ? 0m
                : (transaction.ProfitOrLossAmount / transaction.PurchaseAmount) * 100m;

            LastTransaction = transaction;
            HasLastTransaction = true;

            Console.WriteLine("\n-------------- Last Transaction --------------");
            Console.WriteLine($"Invoice No          : {transaction.InvoiceNo}");
            Console.WriteLine($"Customer            : {transaction.CustomerName}");
            Console.WriteLine($"Item                : {transaction.ItemName}");
            Console.WriteLine($"Quantity            : {transaction.Quantity}");
            Console.WriteLine($"Purchase Amount     : {transaction.PurchaseAmount:F2}");
            Console.WriteLine($"Selling Amount      : {transaction.SellingAmount:F2}");
            Console.WriteLine($"Status              : {transaction.ProfitOrLossStatus}");
            Console.WriteLine($"Profit/Loss Amount  : {transaction.ProfitOrLossAmount:F2}");
            Console.WriteLine($"Profit Margin (%)   : {transaction.ProfitMarginPercent:F2}");
            Console.WriteLine("----------------------------------------------\n");
        }

        #endregion

        #region View Transaction Method

        public static void ViewLastTransaction()
        {
            if (!HasLastTransaction)
            {
                Console.WriteLine("No transaction available. Please create a new transaction first.");
                return;
            }

            var t = LastTransaction;

            Console.WriteLine("\n-------------- Last Transaction --------------");
            Console.WriteLine($"Invoice No          : {t.InvoiceNo}");
            Console.WriteLine($"Customer            : {t.CustomerName}");
            Console.WriteLine($"Item                : {t.ItemName}");
            Console.WriteLine($"Quantity            : {t.Quantity}");
            Console.WriteLine($"Purchase Amount     : {t.PurchaseAmount:F2}");
            Console.WriteLine($"Selling Amount      : {t.SellingAmount:F2}");
            Console.WriteLine($"Status              : {t.ProfitOrLossStatus}");
            Console.WriteLine($"Profit/Loss Amount  : {t.ProfitOrLossAmount:F2}");
            Console.WriteLine($"Profit Margin (%)   : {t.ProfitMarginPercent:F2}");
            Console.WriteLine("----------------------------------------------\n");
        }

        #endregion

        #region Calculation Method

        public static void CalculateProfitOrLoss()
        {
            if (!HasLastTransaction)
            {
                Console.WriteLine("No transaction available. Please create a new transaction first.");
                return;
            }

            var t = LastTransaction;

            if (t.SellingAmount > t.PurchaseAmount)
            {
                t.ProfitOrLossStatus = "PROFIT";
                t.ProfitOrLossAmount = t.SellingAmount - t.PurchaseAmount;
            }
            else if (t.SellingAmount < t.PurchaseAmount)
            {
                t.ProfitOrLossStatus = "LOSS";
                t.ProfitOrLossAmount = t.PurchaseAmount - t.SellingAmount;
            }
            else
            {
                t.ProfitOrLossStatus = "BREAK-EVEN";
                t.ProfitOrLossAmount = 0m;
            }

            t.ProfitMarginPercent = (t.PurchaseAmount == 0m)
                ? 0m
                : (t.ProfitOrLossAmount / t.PurchaseAmount) * 100m;

            Console.WriteLine("\n-------------- Last Transaction --------------");
            Console.WriteLine($"Invoice No          : {t.InvoiceNo}");
            Console.WriteLine($"Customer            : {t.CustomerName}");
            Console.WriteLine($"Item                : {t.ItemName}");
            Console.WriteLine($"Quantity            : {t.Quantity}");
            Console.WriteLine($"Purchase Amount     : {t.PurchaseAmount:F2}");
            Console.WriteLine($"Selling Amount      : {t.SellingAmount:F2}");
            Console.WriteLine($"Status              : {t.ProfitOrLossStatus}");
            Console.WriteLine($"Profit/Loss Amount  : {t.ProfitOrLossAmount:F2}");
            Console.WriteLine($"Profit Margin (%)   : {t.ProfitMarginPercent:F2}");
            Console.WriteLine("----------------------------------------------\n");
        }

        #endregion
    }
}
