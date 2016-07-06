using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyAppLibrary.Model
{
    public class FinancialActivityModel : BaseModel
    {
        public DateTime Date { get; set; }
        public string Activity { get; set; }
        public double Price { get; set; }
        public string Location { get; set; }
    }
}
