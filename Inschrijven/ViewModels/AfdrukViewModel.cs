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
                       ReportView view = new ReportView(_inschrijving);

                       StackPanel reportPanel = ExtractReportStackPanel(view);
                       ResourceDictionary resourceDictionary = ExtractReportResources(view);

                       Report.PrintReport(reportPanel, _inschrijving,ReportOrientation.Portrait,
                           resourceDictionary: resourceDictionary);

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

        #endregion



        public AfdrukViewModel(IGegevensService dataService, Frame frame, Inschrijving inschrijving)
            : base(dataService, frame)
        {
            _inschrijving = inschrijving;
        }
    }
}
