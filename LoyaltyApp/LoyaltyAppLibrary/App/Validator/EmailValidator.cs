using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LoyaltyAppLibrary.App.Validator
{
    public class EmailValidator : InputValidator<string, EmailValidationResult>
    {
        public override EmailValidationResult Validate(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return new EmailValidationResult(false, EmailValidationResult.ErrorCode.Empty);
            }
            try
            {
                var result = Regex.IsMatch(input,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                return new EmailValidationResult(result, EmailValidationResult.ErrorCode.BadSyntax);
            }
            catch (RegexMatchTimeoutException)
            {
                return new EmailValidationResult(false,EmailValidationResult.ErrorCode.BadSyntax);
            }

        }
    }
    public class EmailValidationResult : ValidationResult
    {
        public ErrorCode Error { get; private set; }
        public EmailValidationResult(bool result, ErrorCode error)
        {
            Result = result;
            Error = error;
        }
        public enum ErrorCode
        {
            Empty,
            BadSyntax
        }
    }
}
