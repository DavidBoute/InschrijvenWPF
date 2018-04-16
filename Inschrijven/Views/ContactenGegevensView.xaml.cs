using Inschrijven.Model;
using Inschrijven.Services.Abstract;
using Inschrijven.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Inschrijven.Views
{
    /// <summary>
    /// Interaction logic for ContactenGegevensView.xaml
    /// </summary>
    public partial class ContactenGegevensView : Page
    {
        public ContactenGegevensView(IGegevensService dataService, Frame frame, Inschrijving inschrijving)
        {
            DataContext = new ContactenGegevensViewModel(dataService, frame, inschrijving);

            InitializeComponent();
        }
    }
}
