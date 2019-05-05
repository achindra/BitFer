using Microsoft.Azure.Mobile.Server;

namespace BitFerService.DataObjects
{
   public class BitFerTransactions : EntityData
   {
      public string From { get; set; }
      public string To { get; set; }
      public double Amount { get; set; }

   }
}