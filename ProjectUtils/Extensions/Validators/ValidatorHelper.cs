using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectUtils.Extensions.Validators
{
    public static class ValidatorHelper
    {
        public static void ThrowIfException(this FluentValidation.Results.ValidationResult validationResult)
        {
            if (validationResult.IsValid)
                return;

            var message = string.Join(',', validationResult.Errors.Select(x => x.ErrorMessage));
            throw new ValidationException(message);
        }
    }
}
