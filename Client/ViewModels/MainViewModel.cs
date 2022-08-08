using Client.Infastructure.Commands;
using Client.Infastructure.Commands.Base;
using Client.Models;
using Client.ViewModels.Base;
using Client.ViewModels.UserControls;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Client.ViewModels
{
    internal class MainViewModel : ViewModel
    {
        #region UserControl object
        public AddViewModel AddVM { get; set; }
        public CatalogViewModel CatalogVM { get; set; }


        #endregion

        #region Cards

        private List<Card> _cards;

        /// <summary>Заголовок окна</summary>
        public List<Card> Cards
        {
            get => _cards;
            set
            {
                _cards = value;
                OnPropertyChanged(nameof(Cards));
            }
        }

        #endregion

        #region CurrentView

        /// <summary>Текущий вид</summary>
        private object _currentView=new CatalogViewModel();
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }


        #endregion

        #region header

        private string header="123";

        /// <summary>Заголовок окна</summary>
        public string Header
        {
            get => header;
            set
            {
                header = value;
                OnPropertyChanged(nameof(Header));
            }
        }

        #endregion

        private CardService service;

        #region LoadCardsCommand


        public IAsyncCommand Load { get; private set; }

        private async Task ExecuteLoadAsync()
        {
            await Task.Run(() => {
                Cards = service.GetCards();
            });
            
        }

        private bool CanExecuteLoad()
        {
            return true;
        }

        #endregion

        #region AddCardsCommand


        public IAsyncCommand Add { get; private set; }

        private async Task ExecuteAddAsync()
        {
            await Task.Run(() =>
            {
                
                    service.TryAddCard(new Card { Id = Guid.NewGuid(), Name = "custom"});
                });
                
        }

        private bool CanExecuteAdd()
        {
            return true;
        }

        #endregion

        #region ChangeViewCommand

        public ICommand ChangeViewCommand { get; }

        private bool CanChangeViewCommandExecute(object p) => true;

        private void OnChangeViewCommandExecuted(object p)
        {
            CurrentView = p;
        }

        #endregion









        public MainViewModel()
        {
            AddVM = new AddViewModel();
            CatalogVM = new CatalogViewModel();


            service = new CardService();


            Load = new AsyncCommand(ExecuteLoadAsync, CanExecuteLoad);
            Add=new AsyncCommand(ExecuteAddAsync, CanExecuteAdd);
            ChangeViewCommand = new LambdaCommand(OnChangeViewCommandExecuted, CanChangeViewCommandExecute);

        }
    }
}
