using Inschrijven.Helpers;
using Inschrijven.Model;
using Inschrijven.Services.Abstract;
using Inschrijven.ViewModels.Abstract;
using Inschrijven.Views;
using Inschrijven.Views.Window;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Inschrijven.ViewModels
{
    public class MarketingViewModel : BaseViewModel
    {
        // Properties
        #region Properties

        private Inschrijving _inschrijving;

        public bool IsOverrideValidatie
        {
            get { return GetValue(() => IsOverrideValidatie); }
            set
            {
                SetValue(() => IsOverrideValidatie, value);

                if (!IsOverrideValidatie)
                {
                    IsOverrideKnopZichtbaar = true;
                }
                else
                {
                    IsOverrideKnopZichtbaar = false;
                }
            }
        }

        public bool IsOverrideKnopZichtbaar
        {
            get { return GetValue(() => IsOverrideKnopZichtbaar); }
            set
            {
                SetValue(() => IsOverrideKnopZichtbaar, value);

                IsNietOverrideKnopZichtbaar = !IsOverrideKnopZichtbaar;
            }
        }

        public bool IsNietOverrideKnopZichtbaar
        {
            get { return GetValue(() => IsNietOverrideKnopZichtbaar); }
            set { SetValue(() => IsNietOverrideKnopZichtbaar, value); }
        }


        public ObservableCollection<LerenKennen> LerenKennenSchool
        {
            get { return GetValue(() => LerenKennenSchool); }
            set { SetValue(() => LerenKennenSchool, value); }
        }

        public string LerenKennenSchoolVaria
        {
            get { return GetValue(() => LerenKennenSchoolVaria); }
            set
            {
                SetValue(() => LerenKennenSchoolVaria, value);

                IsOverrideKnopZichtbaar = !(LerenKennenSchool.Any(x => x.IsReden)
                            || !String.IsNullOrWhiteSpace(LerenKennenSchoolVaria));
            }
        }

        public string WaaromGekozenSchool
        {
            get { return GetValue(() => WaaromGekozenSchool); }
            set { SetValue(() => WaaromGekozenSchool, value); }
        }

        #endregion

        #region Custom Event Handling

        // Zorgt voor opvangen Changed event items lijst
        void LerenKennen_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (LerenKennen item in e.NewItems)
                    item.PropertyChanged += LerenKennen_PropertyChanged;

            if (e.OldItems != null)
                foreach (LerenKennen item in e.OldItems)
                    item.PropertyChanged -= LerenKennen_PropertyChanged;
        }

        void LerenKennen_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            LerenKennen lerenkennen = sender as LerenKennen;
            if (lerenkennen == null) { return; }

            IsOverrideKnopZichtbaar = !(LerenKennenSchool.Any(x => x.IsReden)
                                        || !String.IsNullOrWhiteSpace(LerenKennenSchoolVaria));
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
                       _inschrijving.Marketing.LerenKennenSchool = LerenKennenSchool;
                       _inschrijving.Marketing.LerenKennenSchoolVaria = LerenKennenSchoolVaria;
                       _inschrijving.Marketing.WaaromGekozenSchool = WaaromGekozenSchool;


                       await _dataService.SaveChangesAsync(_inschrijving);

                       //frame.Content = new AkkoordToestemmingenView(_dataService, frame, _inschrijving);

                   });
            }
        }

        public ICommand OverrideCommand
        {
            get
            {
                return new RelayCommand(
                   async (object obj) =>
                   {
                       IsOverrideValidatie = true;
                   });
            }
        }

        #endregion

        // Custom Validation Rules
        #region Custom Validation Rules


        #endregion

        // Constructors
        #region Constructors

        public MarketingViewModel(IGegevensService dataService, Frame frame, Inschrijving inschrijving)
            : base(dataService, frame)
        {
            if (inschrijving.Marketing is null)
            {
                inschrijving.Marketing = new Marketing() { MarketingId = Guid.NewGuid() };
                inschrijving.Marketing.LerenKennenSchool = new List<LerenKennen>();

                List<LerenKennenSoort> manierenLerenKennen = _dataService.GetAlleLerenKennenSoorten();
                foreach (var manier in manierenLerenKennen)
                {
                    inschrijving.Marketing.LerenKennenSchool.Add(new LerenKennen
                    {
                        LerenKennenSoort = manier
                    });
                }
            }

            LerenKennenSchool = new ObservableCollection<LerenKennen>();
            LerenKennenSchool.CollectionChanged += LerenKennen_CollectionChanged;

            // CollectionChanged triggert niet bij volledig nieuwe lijst
            foreach (var item in inschrijving.Marketing.LerenKennenSchool)
            {
                LerenKennenSchool.Add(item);
            }

            LerenKennenSchoolVaria = inschrijving.Marketing.LerenKennenSchoolVaria;

            WaaromGekozenSchool = inschrijving.Marketing.WaaromGekozenSchool;

            IsOverrideKnopZichtbaar = IsOverrideKnopZichtbaar = !(LerenKennenSchool.Any(x => x.IsReden)
                                        || !String.IsNullOrWhiteSpace(LerenKennenSchoolVaria));

            _inschrijving = inschrijving;
        }

        #endregion
    }
}
