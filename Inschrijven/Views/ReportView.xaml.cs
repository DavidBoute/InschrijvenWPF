using Inschrijven.Model;
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
    /// Interaction logic for ReportView.xaml
    /// </summary>
    public partial class ReportView : Page
    {
        public ReportView(Inschrijving inschrijving)
        {
            DataContext = new ReportViewModel(inschrijving);

            InitializeComponent();
        }

        public ReportView(Inschrijving inschrijving, double heightInCM, double widthInCM)
        {
            DataContext = new ReportViewModel(inschrijving);

            InitializeComponent();

            this.Height = heightInCM / 2.54 * 96; // px in WPF = 1/96 inch
            this.Width = widthInCM / 2.54 * 96;

            Measure(new Size(this.Width-50, this.Height-50));
            Arrange(new Rect(0, 0, DesiredSize.Width, DesiredSize.Height));
        }
    }
}
