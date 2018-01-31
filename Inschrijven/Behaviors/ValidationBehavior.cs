using Inschrijven.Services;
using Inschrijven.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace Inschrijven.Behaviors
{
    public class ValidationBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty ValidationRuleProperty = DependencyProperty.Register(
            "ValidationRule", typeof(string), typeof(ValidationBehavior));

        public string ValidationCondition
        {
            get { return (string)GetValue(ValidationRuleProperty); }
            set { SetValue(ValidationRuleProperty, value); }
        }

        protected override void OnAttached()
        {
            Window parent = Application.Current.MainWindow;
            ValidationService validationService = (AssociatedObject.DataContext as BaseViewModel).ValidationService;

            validationService.RegisterRule(AssociatedObject, x => !String.IsNullOrWhiteSpace(x));

        }
    }
}
