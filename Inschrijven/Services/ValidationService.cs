using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inschrijven.Services
{
    public class ValidationService
    {
        private List<ValidationRule> _validationDictionary;

        public ValidationService RegisterRule(FrameworkElement element, Predicate<string> rule, 
                                                string successMessage = "OK", string errorMessage = "Error")
        {
            _validationDictionary.Add(new ValidationRule(element, rule, successMessage, errorMessage));

            return this;
        }

        internal void ValidateRule(ValidationRule validationRule)
        {
            //validationRule.ValidatedElement
        }

        public ValidationService ValidateAll()
        {
            foreach (ValidationRule validationRule in _validationDictionary)
            {
                ValidateRule(validationRule);
            }

            return this;
        }

        public ValidationService ValidateElement(UIElement element)
        {
            foreach (ValidationRule validationRule 
                in _validationDictionary.Where(x=> x.ValidatedElement == element))
            {
                ValidateRule(validationRule);
            }

            return this;
        }

        public List<ValidationResult> ValidationResultAll()
        {
            List<ValidationResult> list = new List<ValidationResult>();
            foreach (ValidationRule validationRule in _validationDictionary)
            {
                list.Add(validationRule.Result);
            }

            return list;
        }

        public List<ValidationResult> ValidationResultElement(UIElement element)
        {
            List<ValidationResult> list = new List<ValidationResult>();
            foreach (ValidationRule validationRule 
                in _validationDictionary.Where(x => x.ValidatedElement == element))
            {
                list.Add(validationRule.Result);
            }

            return list;
        }


        // Constructor
        public ValidationService()
        {
            _validationDictionary = new List<ValidationRule>();
        }
    }

    internal class ValidationRule
    {
        internal FrameworkElement ValidatedElement;
        internal Predicate<string> Rule;
        internal string ValidationErrorMessage;
        internal string ValidationSuccessMessage;
        internal ValidationResult Result;

        public ValidationRule(FrameworkElement element, Predicate<string> rule,
                                string successMessage, string errorMessage)
        {
            ValidatedElement = element;
            Rule = rule;
            ValidationSuccessMessage = successMessage;
            ValidationErrorMessage = errorMessage;

            // Standaard niet gevalideerd
            Result = new ValidationResult { IsValidated = false, ValidationMessage = "Not validated yet" };
        }
    }

    public class ValidationResult
    {
        public bool IsValidated;
        public string ValidationMessage;
    }
}
