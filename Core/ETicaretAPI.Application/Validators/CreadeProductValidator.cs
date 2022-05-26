using ETicaretAPI.Application.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Validators
{
    public class CreadeProductValidator : AbstractValidator<Vm_Create_Product>
    {
        public CreadeProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen ürün adını boş geçmeyiniz.")
                .MaximumLength(150)
                .MinimumLength(3)
                     .WithMessage("Ürün ismi 3 ile 150 karakter arasnda giriniz.");

            RuleFor(x => x.Stock)
                .NotNull()
                .NotEmpty()
                    .WithMessage("Lütfen Stok bilgisi giriniz.")
                .Must(s => s >= 0)
                    .WithMessage("Stok bilgisi 0 den büyük olmalıdır.");

            RuleFor(x => x.Price)
               .NotNull()
               .NotEmpty()
                   .WithMessage("Lütfen Fiyat bilgisi giriniz.")
               .Must(s => s >= 0)
                   .WithMessage("Fiyat bilgisi 0 dan büyük olmalı.");
        }
    }
}
