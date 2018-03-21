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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

        public static int Errors { get; set; }

        private Inschrijving _inschrijving;
        private List<Richting> _alleRichtingen;
        private List<Optie> _alleOpties;

        public ObservableCollection<int> Jaren
        {
            get { return GetValue(() => Jaren); }
            set { SetValue(() => Jaren, value); }
        }

        public ObservableCollection<Schooljaar> SchooljarenLijst
        {
            get { return GetValue(() => SchooljarenLijst); }
            set { SetValue(() => SchooljarenLijst, value); }
        }

        public ObservableCollection<Richting> RichtingenGefilterd
        {
            get { return GetValue(() => RichtingenGefilterd); }
            set { SetValue(() => RichtingenGefilterd, value); }
        }

        public ObservableCollection<Optie> OptiesGefilterd
        {
            get { return GetValue(() => OptiesGefilterd); }
            set { SetValue(() => OptiesGefilterd, value); }
        }

        [Required(ErrorMessage = "Selecteer een schooljaar.")]
        public Schooljaar Schooljaar
        {
            get { return GetValue(() => Schooljaar); }
            set { SetValue(() => Schooljaar, value); }
        }

        [Range(1, 7, ErrorMessage = "Selecteer een jaar.")]
        public int Jaar
        {
            get { return GetValue(() => Jaar); }
            set
            {
                SetValue(() => Jaar, value);
                RichtingenGefilterd = _alleRichtingen.Where(x => x.Jaar == value)
                                                    .ToObservableCollection();
            }
        }

        [Required(ErrorMessage = "Selecteer een richting.")]
        public Richting Richting
        {
            get { return GetValue(() => Richting); }
            set
            {
                SetValue(() => Richting, value);
                OptiesGefilterd = _alleOpties.Where(x => x.Richtingen.Contains(Richting))
                                                .ToObservableCollection();
            }
        }

        public Optie Optie
        {
            get { return GetValue(() => Optie); }
            set { SetValue(() => Optie, value); }
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
                       //ValidationService.ValidateAll();

                       //frame.Content = new StartInschrijvingView(_dataService, frame, HuidigeLeerkracht);
                   });
            }
        }

        #endregion

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
                //Schooljaar = dataService.GetStandaardSchooljaar();
                _inschrijving = new Inschrijving()
                {
                    Leerkracht = inschrijvendeLeerkracht
                };
            }

            SchooljarenLijst = dataService.GetAlleSchooljaren().ToObservableCollection();
            Jaren = new int[] { 1, 2, 3, 4, 5, 6, 7 }.ToObservableCollection();
            _alleRichtingen = dataService.GetAlleRichtingen();
            _alleOpties = dataService.GetAlleOpties();
        }

        #endregion
    }
}
