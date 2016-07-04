using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyAppLibrary.App.Validator
{
    public abstract class InputValidator<InputType, ValidationResultT> where ValidationResultT : ValidationResult
    {
        public abstract ValidationResultT Validate(InputType input);
    }

    public abstract class ValidationResult
    {
        public string ErrorString { get; protected set; }
        public bool Result { get; protected set; }
    }

}

