using InventaireStock.Models;

namespace InventaireStock.ViewModels
{
    class InventaireViewModel : BaseViewModel
    {
        private string CodeImmo_;
        private string Description_;
        private string FAMILLE_;
        private string SFAMILLE_;
        private string MARQUE_;
        private string MODELE_;
        private string SelectedEtat_;

        public string CodeImmo
        {
            get => CodeImmo_;
            set
            {
                SetProperty(ref CodeImmo_, value);
            }
        }

        public string Description
        {
            get => Description_;
            set
            {
                SetProperty(ref Description_, value);
            }
        }
        public string FAMILLE
        {
            get => FAMILLE_;
            set
            {
                SetProperty(ref FAMILLE_, value);
            }
        }
        public string SFAMILLE
        {
            get => SFAMILLE_;
            set
            {
                SetProperty(ref SFAMILLE_, value);
            }
        }
        public string MARQUE
        {
            get => MARQUE_;
            set
            {
                SetProperty(ref MARQUE_, value);
            }
        }
        public string MODELE
        {
            get => MODELE_;
            set
            {
                SetProperty(ref MODELE_, value);
            }
        }

        public string SelectedEtat
        {
            get => SelectedEtat_;
            set
            {

                SetProperty(ref SelectedEtat_, value);
            }
        }

        public InventaireViewModel()
        {
            SaveCommand = new Command(OnSaveClicked, ValidateString);
            this.PropertyChanged +=
                    (_, __) => SaveCommand.ChangeCanExecute();
            //FilterSFamilleCommand = new Command(OnFilterSFamilleClicked, ValidateStringSFamille);
            //this.PropertyChanged +=
            //        (_, __) => FilterSFamilleCommand.ChangeCanExecute();
        }

        private void OnSaveClicked()
        {

        }
        private bool ValidateString()
        {
            return !String.IsNullOrWhiteSpace(CodeImmo);

            //return !String.IsNullOrWhiteSpace(CodeImmo) && !String.IsNullOrWhiteSpace(Description) && !String.IsNullOrWhiteSpace(FAMILLE) && !String.IsNullOrWhiteSpace(SFAMILLE) && !String.IsNullOrWhiteSpace(MARQUE) && !String.IsNullOrWhiteSpace(MODELE) && !String.IsNullOrWhiteSpace(SelectedEtat);
        }
        private void OnFilterSFamilleClicked()
        {
        }
        private bool ValidateStringSFamille()
        {
            bool veriffamille = !String.IsNullOrWhiteSpace(FAMILLE);
            if (!veriffamille)
            {
                SFAMILLE = string.Empty;
            }
            return veriffamille;
        }

        public Command SaveCommand { get; }
        public Command FilterSFamilleCommand { get; }


    }
}
