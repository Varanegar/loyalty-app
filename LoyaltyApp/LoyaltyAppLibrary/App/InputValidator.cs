using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyAppLibrary.App
{
    public class InputValidator
    {
        public static ValidationResult Validate(string input, Filter filter)
        {
            if (filter == Filter.AbsoluteUri)
            {
                if (string.IsNullOrEmpty(input))
                {
                    return new ValidationResult() { Error = ValidationError.Empty };
                }
                Uri myUri;
                if (!Uri.TryCreate(input, UriKind.Absolute, out myUri))
                {
                    return new ValidationResult() { Error = ValidationError.InvalidUri };
                }
                return new ValidationResult();
            }
            else if (filter == Filter.RelativeUri)
            {
                if (string.IsNullOrEmpty(input))
                {
                    return new ValidationResult() { Error = ValidationError.Empty };
                }
                Uri myUri;
                if (!Uri.TryCreate(input, UriKind.Relative, out myUri))
                {
                    return new ValidationResult() { Error = ValidationError.InvalidUri };
                }
                return new ValidationResult();
            }
            else if (filter == Filter.UserName)
            {
                if (string.IsNullOrEmpty(input))
                {
                    return new ValidationResult() { Error = ValidationError. };
                }
                Uri myUri;
                if (!Uri.TryCreate(input, UriKind.Relative, out myUri))
                {
                    return new ValidationResult() { Error = ValidationError.InvalidUri };
                }
                return new ValidationResult();
            }
            else return new ValidationResult();
        }

        public enum Filter
        {
            AbsoluteUri = 0,
            RelativeUri,
            Email,
            UserName,
            PassWord
        }
        public enum ValidationError
        {
            Null = 0,
            InvalidUri,
            Empty
        }
    }
    public class ValidationResult
    {
        public InputValidator.ValidationError Error { get; set; }
        public bool Result
        {
            get
            {
                return Error == InputValidator.ValidationError.Null ? true : false;
            }
        }
    }
}
