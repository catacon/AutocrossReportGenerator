using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace autocrossreportgenerator.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {

        public HomeViewModel(string pageTitle) : base(pageTitle)
        {

        }

        public int SeasonYear { get; }

        public ICommand Import
        {
            get
            {
                return SetNextViewModel("EventImportViewModel");
            }
        }

        public ICommand EventReport
        {
            get
            {
                return SetNextViewModel("EventReportViewModel");
            }
        }

        public ICommand ChampionshipReport
        {
            get
            {
                return SetNextViewModel("ChampionshipReportViewModel");
            }
        }

        public ICommand ViewDrivers
        {
            get
            {
                return SetNextViewModel("DriversViewModel");
            }
        }

        public ICommand EditSeason
        {
            get
            {
                return SetNextViewModel("EditSeasonViewModel");
            }
        }

        public ICommand NewSeason
        {
            get
            {
                return SetNextViewModel("NewSeasonViewModel");
            }
        }

        public override void Update()
        {
            
        }
    }
}
