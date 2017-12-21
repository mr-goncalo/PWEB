using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace tp_escolas.ValidationAttributes
{
    public class MinPalavrasAttribute : ValidationAttribute 
    {
        private readonly int _minPalavras;

        public MinPalavrasAttribute(int numPalavras) : base("{0} tem poucas palavras!")
		{
            _minPalavras = numPalavras;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var valorStr = value.ToString();
                if (valorStr.Split(' ').Length < _minPalavras)
                {
                    var msgErro = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(msgErro);
                }
            }
            return ValidationResult.Success;
        }
    }
}