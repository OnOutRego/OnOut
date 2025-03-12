using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Exceptions
{
    public class BadRequest: Exception
    {
        public BadRequest(string message) : base(message)
        {

        }

        public BadRequest(string message, ValidationResult validationResult) : base(message)
        {
            ValidationErrors = new();
            foreach (var error in validationResult.Errors)
            {
                ValidationErrors.Add(error.ErrorMessage);
            }
        }

        public List<string> ValidationErrors { get; set; }
    }
}
