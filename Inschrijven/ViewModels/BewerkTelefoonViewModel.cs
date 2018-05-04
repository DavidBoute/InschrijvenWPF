using Inschrijven.Model;
using Inschrijven.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inschrijven.ViewModels.Abstract;
using Inschrijven.Services.Abstract;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Inschrijven.ViewModels
{
    public class BewerkTelefoonViewModel : BaseViewModel
    {
        // Properties
        #region Properties

        private Inschrijving _inschrijving;

        [Required(ErrorMessage = "Vul het telefoonummer in")]
        public string Telefoonnummer
        {
            get { return GetValue(() => Telefoonnummer); }
            set { SetValue(() => Telefoonnummer, value); }
        }

        public string Opmerking
        {
            get { return GetValue(() => Opmerking); }
            set { SetValue(() => Opmerking, value); }
        }

        [Required(ErrorMessage = "Kies het soort telefoonnummer")]
        public TelefoonSoort TelefoonSoort
        {
            get { return GetValue(() => TelefoonSoort); }
            set { SetValue(() => TelefoonSoort, value); }
        }

        public List<TelefoonSoort> TelefoonSoorten { get; private set; }

        public Telefoon Telefoon { get; private set; }

        #endregion

        // Commands
        #region Commands

        public ICommand OpslaanCommand
        {
            get
            {
                return new RelayCommand(
                   async (object obj) =>
                   {
                       Telefoon.TelefoonSoort = TelefoonSoort;
                       Telefoon.Nummer = Telefoonnummer;
                       Telefoon.Opmerking = Opmerking;

                       Window window = frame.Parent as Window;
                       window.DialogResult = true;
                       window.Close();
                   });
            }
        }


        #endregion

        // Custom Validation Rules
        #region Custom Validation Rules



        #endregion

        // Constructors
        #region Constructors

        public BewerkTelefoonViewModel(Telefoon telefoon, IGegevensService dataService, Frame frame, Inschrijving inschrijving)
            : base(dataService, frame)
        {
            _inschrijving = inschrijving;

            Telefoon = telefoon;

            TelefoonSoorten = dataService.GetAlleTelefoonSoorten();
        }

        #endregion
    }
}
