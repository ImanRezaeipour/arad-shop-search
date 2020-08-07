using Common.Domain.Model.Common;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Persistance.EF.Validation
{
    public class ProductValidator : AbstractValidator<ShopperProduct>
    {
        public ProductValidator()
        {
            //RuleFor(pos => pos.Title).NotEmpty();
            //RuleFor(pos => pos.Title).NotEmpty().Length(5, 250).WithMessage("Pos Is Required a Model");
        }

    }
}
