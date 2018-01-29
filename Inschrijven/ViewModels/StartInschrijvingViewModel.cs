using Inschrijven.DAL;
using Inschrijven.Model;
using Inschrijven.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
                RichtingenGefilterd = new ObservableCollection<Richting>(
                    _alleRichtingen.Where(x => x.Jaar == value));

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
                OptiesGefilterd = new ObservableCollection<Optie>(
                    _alleOpties.Where(x => x.Richtingen.Contains(Richting)));

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

        // Constructors
        #region Constructors

        public StartInschrijvingViewModel(InschrijvingContext db, Frame frame, Page page, Leerkracht inschrijvendeLeerkracht, Inschrijving inschrijving = null)
            : base(db, frame, page)
        {
            SchooljarenLijst = new ObservableCollection<Schooljaar>(db.Schooljaren);
            Jaren = new ObservableCollection<int>(new int[] { 1, 2, 3, 4, 5, 6 });
            _alleRichtingen = new List<Richting>(db.Richtingen.Include("Opties"));
            _alleOpties = new List<Optie>(db.Opties);

            if (inschrijving != null)
            {
                Schooljaar = inschrijving.Schooljaar;
                _inschrijving = inschrijving;
            }
            else
            {
                DateTime vandaag = DateTime.Today;

                int jaarAanpassing = 0;
                if (vandaag.Month <= 3) jaarAanpassing = -1;

                // Indien schooljaar nog niet bestaat, aanvullen
                if (!db.Schooljaren
                        .Any(x => x.SchooljaarStartDatum.Year == vandaag.Year + jaarAanpassing))
                {
                    try
                    {
                        db.Schooljaren.Add(new Schooljaar
                        {
                            SchooljaarStartDatum = new DateTime(vandaag.Year + jaarAanpassing, 9, 1),
                            SchooljaarNaam = $"{vandaag.Year + jaarAanpassing}-{vandaag.Year + jaarAanpassing + 1}"
                        });

                        db.SaveChanges();

                        SchooljarenLijst = new ObservableCollection<Schooljaar>(db.Schooljaren);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }

                Schooljaar = db.Schooljaren
                        .SingleOrDefault(x => x.SchooljaarStartDatum.Year == vandaag.Year + jaarAanpassing);

                _inschrijving = new Inschrijving()
                {
                    Leerkracht = inschrijvendeLeerkracht
                };
            }



        }

        # endregion
    }
}
