using Inschrijven.Extensions;
using Inschrijven.Helpers;
using Inschrijven.Model;
using Inschrijven.Services.Abstract;
using Inschrijven.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Inschrijven.ViewModels
{
    public class LeerlingGegevensViewModel : BaseViewModel
    {
        // Properties
        #region Properties

        //public static int Errors { get; set; }
        private Inschrijving _inschrijving;

        [Required(ErrorMessage = "Typ de voornaam in")]
        public string Voornaam
        {
            get { return GetValue(() => Voornaam); }
            set { SetValue(() => Voornaam, value); }
        }

        [Required(ErrorMessage = "Typ de familienaam in")]
        public string Naam
        {
            get { return GetValue(() => Naam); }
            set { SetValue(() => Naam, value); }
        }


        #endregion

        // Commands
        #region Commands

        public ICommand VolgendeCommand
        {
            get
            {
                return new RelayCommand(
                   async (object obj) =>
                   {
                       await _dataService.SaveChangesAsync(_inschrijving);
                   });
            }
        }

        #endregion

        // Custom Validation Rules
        // https://stackoverflow.com/questions/16100300/asp-net-mvc-custom-validation-by-dataannotation
        public class IsOptieRequiredAttribute : ValidationAttribute
        {
            protected override System.ComponentModel.DataAnnotations.ValidationResult
                IsValid(object value, ValidationContext validationContext)
            {
                var viewModel = validationContext.ObjectInstance as StartInschrijvingViewModel;

                int aantalOpties = 0;
                if (viewModel.OptiesGefilterd != null)
                {
                    aantalOpties = viewModel.OptiesGefilterd.Count();
                }

                if (aantalOpties != 0 && value == null)
                {
                    return new System.ComponentModel.DataAnnotations.ValidationResult
                        (this.FormatErrorMessage(validationContext.DisplayName));
                }
                return null;
            }
        }

        // Constructors
        #region Constructors

        public LeerlingGegevensViewModel(IGegevensService dataService, Frame frame, Page page, Inschrijving inschrijving)
            : base(dataService, frame, page)
        {
            _inschrijving = inschrijving;
        }

        #endregion
    }
}
