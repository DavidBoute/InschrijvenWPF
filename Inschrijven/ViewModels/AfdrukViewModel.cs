using Inschrijven.Helpers;
using Inschrijven.Model;
using Inschrijven.Services.Abstract;
using Inschrijven.ViewModels.Abstract;
using Inschrijven.Views;
using SimpleWPFReporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Inschrijven.ViewModels
{

    public class AfdrukViewModel : BaseViewModel
    {
        const double A4_HEIGHT_CM = 29.7;
        const double A4_WIDTH_CM = 21;

        private Inschrijving _inschrijving;

        // Commands
        #region Commands

        public ICommand AfdrukCommand
        {
            get
            {
                return new RelayCommand(
                   async (object obj) =>
                   {
                       ReportView view = new ReportView(_inschrijving, A4_HEIGHT_CM, A4_WIDTH_CM);

                       StackPanel reportPanel = ExtractReportStackPanel(view);
                       ResourceDictionary resourceDictionary = ExtractReportResources(view);
                       DataTemplate footerTemplate = ExtractReportFooter(view);

                       bool isDoneExporting = false;

                       while (!isDoneExporting)
                       {
                           try
                           {
                               Report.ExportReportAsPdf(reportPanel, view.DataContext, ReportOrientation.Portrait,
                                                         resourceDictionary: resourceDictionary,
                                                         reportFooterDataTemplate: footerTemplate);
                               isDoneExporting = true;
                           }
                           catch (System.IO.IOException)
                           {
                               MessageBoxResult messageBoxResult =  MessageBox.Show("Het bestand kan niet geschreven worden. Het is misschien in gebruik. Opnieuw proberen?",
                                                                                    "Fout",
                                                                                    MessageBoxButton.YesNo,
                                                                                    MessageBoxImage.Error);
                               if (messageBoxResult == MessageBoxResult.No)
                               {
                                   isDoneExporting = true;
                               }
                           }
                       }
                   });
            }
        }

        public ICommand ToonCommand
        {
            get
            {
                return new RelayCommand(
                   async (object obj) =>
                   {
                       ReportView view = new ReportView(_inschrijving, A4_HEIGHT_CM, A4_WIDTH_CM);

                       frame.Content = view;

                   });
            }
        }

        public StackPanel ExtractReportStackPanel(ReportView reportView)
        {
            StackPanel reportStackPanel = reportView.ReportStackPanel;
            reportStackPanel.Orientation = Orientation.Vertical;

            return reportStackPanel;
        }

        public ResourceDictionary ExtractReportResources(ReportView reportView)
        {
            ResourceDictionary resourceDictionary = reportView.Resources;
            return resourceDictionary;
        }

        public DataTemplate ExtractReportFooter(ReportView reportView)
        {
            ResourceDictionary resourceDictionary = reportView.Resources;
            DataTemplate footerTemplate = resourceDictionary["ReportFooterDataTemplate"] as DataTemplate;
            return footerTemplate;
        }

        #endregion



        public AfdrukViewModel(IGegevensService dataService, Frame frame, Inschrijving inschrijving)
            : base(dataService, frame)
        {
            _inschrijving = inschrijving;
        }
    }
}
