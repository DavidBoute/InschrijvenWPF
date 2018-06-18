using Inschrijven.Helpers;
using Inschrijven.Model;
using Inschrijven.Services.Abstract;
using Inschrijven.ViewModels.Abstract;
using Inschrijven.Views;
using Inschrijven.Views.Window;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Inschrijven.ViewModels
{
    public class ZoekSchoolViewModel : BaseViewModel
    {
        private List<School> LijstScholen { get; set; }

        [Required(ErrorMessage ="Selecteer een school")]
        public School School
        {
            get { return GetValue(() => School); }
            set { SetValue(() => School, value); }
        }

        public string ZoektermGemeente
        {
            get { return GetValue(() => ZoektermGemeente); }
            set
            {
                SetValue(() => ZoektermGemeente, value);
                GefilterdeLijstScholen.Refresh();
            }
        }

        public string ZoektermNaam
        {
            get { return GetValue(() => ZoektermNaam); }
            set
            {
                SetValue(() => ZoektermNaam, value);
                GefilterdeLijstScholen.Refresh();
            }
        }

        public ICollectionView GefilterdeLijstScholen { get; set; }


        private bool ScholenFilter(object item)
        {
            School school = item as School;

            bool IsGefilterdeWaarde = school.Naam.ToLower().Contains((ZoektermNaam ?? String.Empty).ToLower())
                                    && school.Gemeente.ToLower().Contains((ZoektermGemeente ?? String.Empty).ToLower());
            return IsGefilterdeWaarde;
        }


        // Commands
        #region Commands

        public ICommand SelecteerCommand
        {
            get
            {
                return new RelayCommand(
                   async (object obj) =>
                   {
                       Window window = frame.Parent as Window;
                       window.DialogResult = true;
                       window.Close();
                   });
            }
        }


        #endregion

        // Constructors
        #region Constructors

        public ZoekSchoolViewModel(IGegevensService dataService, Frame frame)
            : base(dataService, frame)
        {
            LijstScholen = dataService.GetAlleScholen();

            GefilterdeLijstScholen = CollectionViewSource.GetDefaultView(LijstScholen);
            GefilterdeLijstScholen.Filter = ScholenFilter;
        }

        #endregion
    }
}
