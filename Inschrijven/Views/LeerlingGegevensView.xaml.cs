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
    /// Interaction logic for LeerlingGegevensView.xaml
    /// </summary>
    public partial class LeerlingGegevensView : Page
    {
        public LeerlingGegevensView(IGegevensService dataService, Frame frame, Page page, Inschrijving inschrijving)
        {
            DataContext = new LeerlingGegevensViewModel(dataService, frame, page, inschrijving);

            InitializeComponent();
        }
    }
}
