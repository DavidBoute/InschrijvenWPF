using Inschrijven.Helpers;
using Inschrijven.Model;
using Inschrijven.Services.Abstract;
using Inschrijven.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel.DataAnnotations;

namespace Inschrijven.ViewModels
{
    public class ContactenGegevensViewModel: BaseViewModel
    {
        // Properties
        #region Properties

        private Inschrijving _inschrijving;

        public List<Contact> LijstContacten
        {
            get { return GetValue(() => LijstContacten); }
            private set { SetValue(() => LijstContacten, value); }
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

                   });
            }
        }

        #endregion

        // Custom Validation Rules
        #region Custom Validation Rules
      
       

        #endregion

        // Constructors
        #region Constructors

        public ContactenGegevensViewModel(IGegevensService dataService, Frame frame, Inschrijving inschrijving)
            : base(dataService, frame)
        {
            _inschrijving = inschrijving;

            LijstContacten = inschrijving.Leerling.Contacten.ToList();


        }

        #endregion
    }
}
