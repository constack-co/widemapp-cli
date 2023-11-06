using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constack.Widemapp.Application.Services.Entities.Commands.Add
{
    public class AddEntityCommandValidator : AbstractValidator<AddEntityCommand>
    {
        public AddEntityCommandValidator()
        {
            Validations();
        }

        private void Validations()
        {

        }
    }
}
