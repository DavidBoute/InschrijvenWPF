using Inschrijven.Helpers;
using Inschrijven.Model;
using Inschrijven.Services.Abstract;
using Inschrijven.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Inschrijven.ViewModels
{
    public class AkkoordToestemmingenViewModel:BaseViewModel
    {
        // Properties
        #region Properties

        private Inschrijving _inschrijving;

        public ObservableCollection<Toestemming> LijstToestemmingen
        {
            get { return GetValue(() => LijstToestemmingen); }
            set { SetValue(() => LijstToestemmingen, value); }
        }

        public bool IsAkkoordSchoolreglement
        {
            get { return GetValue(() => IsAkkoordSchoolreglement); }
            set { SetValue(() => IsAkkoordSchoolreglement, value); }
        }


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
 
                   });
            }
        }



        #endregion


        // Custom Validation Rules
        #region Custom Validation Rules

       

        #endregion

        // Constructors
        #region Constructors

        public AkkoordToestemmingenViewModel(IGegevensService dataService, Frame frame, Inschrijving inschrijving)
            : base(dataService, frame)
        {
            if (inschrijving.Toestemmingen is null)
            {
                inschrijving.Toestemmingen = new List<Toestemming>();

                foreach (var toestemmingSoort in dataService.GetAlleToestemmingSoorten(inschrijving.Richting.Jaar))
                {
                    inschrijving.Toestemmingen.Add(new Toestemming { ToestemmingSoort = toestemmingSoort });
                }
            }

            LijstToestemmingen = new ObservableCollection<Toestemming>(inschrijving.Toestemmingen);

            _inschrijving = inschrijving;

            IsAkkoordSchoolreglement = inschrijving.IsAkkoordSchoolreglement; 
        }

        #endregion
    }
}
