using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LoyaltyAppLibrary.App.Validator
{
    public class UsernameValidator : InputValidator<string, UsernameValidationResult>
    {
        public override UsernameValidationResult Validate(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return new UsernameValidationResult(false, UsernameValidationResult.ErrorCode.Empty);
            }
            if (input.Length < 6)
            {
                return new UsernameValidationResult(false, UsernameValidationResult.ErrorCode.Length);
            }
            try
            {
                var result = Regex.IsMatch(input,@"^(?=[a-zA-Z])[-\w.]{0,23}([a-zA-Z\d]|(?<![-.])_)$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                return new UsernameValidationResult(result, UsernameValidationResult.ErrorCode.BadSyntax);
            }
            catch (RegexMatchTimeoutException)
            {
                return new UsernameValidationResult(false, UsernameValidationResult.ErrorCode.BadSyntax);
            }
        }
    }
    public class UsernameValidationResult : ValidationResult
    {
        public ErrorCode Error { get; set; }
        public UsernameValidationResult(bool result, ErrorCode error)
        {
            Error = error;
            Result = result;
        }
        public enum ErrorCode
        {
            Empty,
            Length,
            BadSyntax
        }
    }
}
