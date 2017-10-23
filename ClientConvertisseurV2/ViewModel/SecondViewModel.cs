using ClientConvertisseurV2.Models;
using ClientConvertisseurV2.Service;
using ClientConvertisseurV2.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Popups;

namespace ClientConvertisseurV2.ViewModel
{
    public class SecondViewModel : ViewModelBase
    {
        private ObservableCollection<Devise> _comboBoxDevises;
        private string _montantEuros;
        private string _valDevise;
        private Devise _montantDevise;
        public ICommand BtnSetConversion { get; private set; }
        public ICommand BtnChangeConverter { get; private set; }

        public ObservableCollection<Devise> ComboBoxDevises
        {
            get { return _comboBoxDevises; }
            set
            {
                _comboBoxDevises = value;
                RaisePropertyChanged();// Pour notifier de la modification de ses données
            }
        }

        public SecondViewModel()
        {
            ActionGetData();
            BtnSetConversion = new RelayCommand(ActionSetConversion);

            BtnChangeConverter = new RelayCommand(ActionChangeConvertisseur);
        }
        private async void ActionGetData()
        {
            var result = await WSService.getAllDevisesAsync();
            ComboBoxDevises = new ObservableCollection<Devise>(result);
        }

        private void ActionSetConversion()
        {
            Boolean error = false;
            String errorMessage = "";
            Regex regex = new Regex(@"[\d]");
            if (_valDevise == null || _montantDevise == null)
            {
                error = true;
                errorMessage = "Veuillez remplir tout les champs.";
            }
            else if(!regex.IsMatch(_valDevise)){
                error = true;
                errorMessage = "La valeur doit etre un chiffre.";
            }
            else
            {
                this.MontantEuros = Convert.ToString(Double.Parse(_valDevise) / _montantDevise.Taux); 
            }

            if (error)
            {
                this.MontantEuros = "";
                AfficherPopUp(errorMessage);
            }
        }

        private async void AfficherPopUp(string errorMessage)
        {
            MessageDialog message = new MessageDialog(errorMessage);
            await message.ShowAsync();
        }

        public string ValDevise
        {
            get { return _valDevise; }
            set
            {
                _valDevise = value;
                RaisePropertyChanged();
            }
        }
        public string MontantEuros
        {
            get { return _montantEuros; }
            set
            {
                _montantEuros = value;
                RaisePropertyChanged();
            }
        }

        public Devise ComboBoxDeviseItem
        {
            get { return _montantDevise; }
            set
            {
                _montantDevise = value;
                RaisePropertyChanged();
            }
        }

        private void ActionChangeConvertisseur()
        {
            RootPage r = (RootPage)Window.Current.Content;
            SplitView sv = (SplitView)(r.Content);
            (sv.Content as Frame).Navigate(typeof(MainPage));
        }

    }
}
