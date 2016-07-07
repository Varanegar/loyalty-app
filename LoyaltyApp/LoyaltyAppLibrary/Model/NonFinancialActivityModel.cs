using Anatoli.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyAppLibrary.Model
{
    public class NonFinancialActivityModel : BaseModel
    {
        public DateTime Date { get; set; }
        public string Activity { get; set; }
        public string Location { get; set; }
    }
}
