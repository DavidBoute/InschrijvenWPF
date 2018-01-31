using Inschrijven.DAL;
using Inschrijven.Extensions;
using Inschrijven.Helpers;
using Inschrijven.Model;
using Inschrijven.Services;
using Inschrijven.Services.Abstract;
using Inschrijven.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Inschrijven.ViewModels
{
    public class StartInschrijvingViewModel : BaseViewModel
    {
        // Properties
        #region Properties

        private Inschrijving _inschrijving;
        private List<Richting> _alleRichtingen;
        private List<Optie> _alleOpties;

        private ObservableCollection<int> _jaren;
        public ObservableCollection<int> Jaren
        {
            get { return _jaren; }
            private set { _jaren = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Schooljaar> _schooljarenLijst;
        public ObservableCollection<Schooljaar> SchooljarenLijst
        {
            get { return _schooljarenLijst; }
            set { _schooljarenLijst = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Richting> _richtingenGefilterd;
        public ObservableCollection<Richting> RichtingenGefilterd
        {
            get { return _richtingenGefilterd; }
            set { _richtingenGefilterd = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Optie> _optiesGefilterd;
        public ObservableCollection<Optie> OptiesGefilterd
        {
            get { return _optiesGefilterd; }
            set { _optiesGefilterd = value; OnPropertyChanged(); }
        }

        private Schooljaar _schooljaar;
        public Schooljaar Schooljaar
        {
            get { return _schooljaar; }
            set { _schooljaar = value; OnPropertyChanged(); }
        }

        private int _jaar;
        public int Jaar
        {
            get { return _jaar; }
            set
            {
                _jaar = value;
                RichtingenGefilterd = _alleRichtingen.Where(x => x.Jaar == value)
                                                    .ToObservableCollection();

                OnPropertyChanged();
            }
        }

        private Richting _richting;
        public Richting Richting
        {
            get { return _richting; }
            set
            {
                _richting = value;
                OptiesGefilterd = _alleOpties.Where(x => x.Richtingen.Contains(Richting))
                                            .ToObservableCollection();

                OnPropertyChanged();
            }
        }

        private Optie _optie;
        public Optie Optie
        {
            get { return _optie; }
            set { _optie = value; OnPropertyChanged(); }
        }

        #endregion

        // Commands
        #region Commands

        public ICommand StartInschrijvingCommand
        {
            get
            {
                return new RelayCommand(
                   (object obj) =>
                   {
                       ValidationService.ValidateAll();

                       //frame.Content = new StartInschrijvingView(_dataService, frame, HuidigeLeerkracht);
                   });
            }
        }

        #endregion

        //// Validation
        //#region Validation

        //private void AddValidation()
        //{
        //    var validationService = new ValidationService();

        //    validationService.RegisterRule(page.FindName("cboRichting") as UIElement, 
        //                                    x => !String.IsNullOrWhiteSpace(x));
        //}

        //#endregion

        // Constructors
        #region Constructors

        public StartInschrijvingViewModel(IGegevensService dataService, Frame frame, Page page, Leerkracht inschrijvendeLeerkracht, Inschrijving inschrijving = null)
            : base(dataService, frame, page)
        {
            if (inschrijving != null)
            {
                Schooljaar = inschrijving.Schooljaar;
                _inschrijving = inschrijving;
            }
            else
            {
                Schooljaar = dataService.GetStandaardSchooljaar();
                _inschrijving = new Inschrijving()
                {
                    Leerkracht = inschrijvendeLeerkracht
                };
            }

            SchooljarenLijst = dataService.GetAlleSchooljaren().ToObservableCollection();
            Jaren = new int[] { 1, 2, 3, 4, 5, 6 }.ToObservableCollection();
            _alleRichtingen = dataService.GetAlleRichtingen();
            _alleOpties = dataService.GetAlleOpties();

            ValidationService = new ValidationService();
        }

        # endregion
    }
}
