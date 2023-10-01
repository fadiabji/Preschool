using Preschool.Models;
using System.ComponentModel.DataAnnotations;

namespace Preschool.Models
{
    public class Invoice
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Invoice Number field is required.")]
        public string InvoiceNumber { get; set; } = new Random().Next(1000000, 9999999).ToString();


        [DataType(DataType.Date)]
        [Display(Name = "Invoice Date")]
        public DateTime InvoiceDate { get; set; }

        // Optional discount field
        [Display(Name = "Discount")]
        [Range(0, 1, ErrorMessage = "Discount must be between 0 and 1.")]
        public decimal? Discount { get; set; }

        // Navigation property for related invoice items
        public virtual ICollection<SubscriptionType> InvoiceItems { get; set; }

        // Collection of payments made towards this invoice
        public virtual ICollection<Payment> Payments { get; set; }

        [Required]
        public int ChildId { get; set; }
        public virtual Child Child { get; set; }

        public string LogoFileAddress { get; set; }
        public string QRCodeFileAddress { get; set; }
        public decimal CalculateSubtotal()
        {
            decimal subtotal = 0;
            foreach (var item in InvoiceItems)
            {
                subtotal += item.Price;
            }
            return subtotal;
        }

        public decimal CalculateTotal()
        {
            decimal subtotal = CalculateSubtotal();

            // Apply discount if it exists
            if (Discount.HasValue && Discount.Value > 0)
            {
                subtotal -= subtotal * Discount.Value;
            }

            return subtotal;
        }

        public decimal CalculateBalance()
        {
            decimal total = CalculateTotal();

            // Calculate the sum of payments made towards this invoice
            decimal totalPayments = 0;
            if (Payments != null)
            {
                foreach (var payment in Payments)
                {
                    totalPayments += payment.Amount;
                }
            }

            // Calculate the remaining balance
            return total - totalPayments;
        }
    }

}


