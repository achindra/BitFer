using Microsoft.Azure.Mobile.Server;

namespace BitFerService.DataObjects
{
   public class BitFerUsers : EntityData
   {
      [Newtonsoft.Json.JsonIgnore]
      public string UserId { get; set; }

      public int Otp {get; set;}

      public string Phone { get; set; }
      public string Email { get; set; }
      public double Balance { get; set; }
   }
}