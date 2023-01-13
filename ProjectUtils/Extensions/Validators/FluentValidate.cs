using Data.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectUtils.Extensions.Validators
{
    public class FluentValidate : AbstractValidator<User>
    {
        public FluentValidate()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Address).MinimumLength(15);
            RuleFor(x => x.Phone).NotEmpty();
        }

    }
}
