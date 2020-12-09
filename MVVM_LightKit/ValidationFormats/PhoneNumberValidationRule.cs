using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MVVM_LightKit.ValidationFormats
{
    class PhoneNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (Regex.Match((string)value, @"^\+?3?8?(0\d{9})$").Success)
                return new ValidationResult(true, null);
            return new ValidationResult(false, "Incorrect phone number format");
        }
    }
}
