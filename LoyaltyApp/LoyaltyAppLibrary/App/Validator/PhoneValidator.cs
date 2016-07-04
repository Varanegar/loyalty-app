using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyAppLibrary.App.Validator
{
    public class PhoneValidator : InputValidator<string, PhoneValidationResult>
    {
        public override PhoneValidationResult Validate(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return new PhoneValidationResult(false, PhoneValidationResult.ErrorCode.empty);
            }
            if (input.Length < 11)
            {
                return new PhoneValidationResult(false, PhoneValidationResult.ErrorCode.BadSyntax);
            }
            try
            {
                long.Parse(input);
                return new PhoneValidationResult(true);
            }
            catch (Exception)
            {
                return new PhoneValidationResult(false, PhoneValidationResult.ErrorCode.BadSyntax);
            }
        }
    }
    public class PhoneValidationResult : ValidationResult
    {
        public ErrorCode Error;
        public PhoneValidationResult(bool result) 
        {
            Result = result;
        }
        public PhoneValidationResult(bool result, ErrorCode error)
        {
            Result = result;
            Error = error;
        }
        public enum ErrorCode
        {
            empty,
            BadSyntax
        }
    }
}
