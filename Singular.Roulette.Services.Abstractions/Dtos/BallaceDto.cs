using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Services.Abstractions.Dtos
{
    public class BallaceDto
    {
        public decimal Balance { get; set; }
        public string Currency { get; set; }    
        public string Dimention { get; set; }

        public BallaceDto(decimal balance)
        {

            Currency = "USD";
            Dimention = "¢";
            Balance = balance;


        }
    }
}
