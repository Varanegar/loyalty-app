using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyAppLibrary.FrameWork
{
    public class BaseModel
    {
         public BaseModel()
        {

        }
        public Guid UniqueId { get; set; }
        public bool IsSaveRequired { get; set; }
        public bool ReadOnly { get { return false; } }
        public bool IsRemoved { get; set; }
        public bool IsValid { get { return (String.IsNullOrEmpty(message)) ? true : false; } private set { IsValid = value; } }
        public string message { get; set; }
        public Dictionary<string, string[]> ModelState { get; set; }

        public string PrivateOwnerId { get { return "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C"; } }
    }
}
