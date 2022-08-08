using Client.Infastructure.Commands;
using Client.Infastructure.Commands.Base;
using Client.Models;
using Client.ViewModels.Base;
using Client.Views;
using Microsoft.Win32;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Client.ViewModels.UserControls
{
    internal class CatalogViewModel : ViewModel
    {
        private CardService service;

        #region Cards

        private List<Card> cards;

        public List<Card> Cards
        {
            get { return cards; }
            set { Set<List<Card>>(ref cards, value); }
        }

        #endregion

        #region SelectedItem

        private Card selectedCard;

        public Card SelectedCard
        {
            get { return selectedCard; }
            set { selectedCard = value;
                OnPropertyChanged(nameof(SelectedCard));
            }
        }

        #endregion

        #region ChangedCard

        private Card changedCard;

        public Card ChangedCard
        {
            get { return changedCard; }
            set { changedCard = value;
                OnPropertyChanged(nameof(changedCard));
            }
        }

        #endregion

        #region RefreshCommand
        public IAsyncCommand Refresh { get; private set; }

        private async Task ExecuteRefreshAsync()
        {
            await Task.Run(() =>
            {
                Cards=service.GetCards();
            });
        }

        private bool CanExecuteRefresh() => true;

        #endregion


        #region DeleteCommand
        public IAsyncCommand Delete { get; private set; }

        private async Task ExecuteDeleteAsync()
        {
            await Task.Run(() =>
            {
                if (service.TryDeleteCard(SelectedCard))
                {
                   Cards.Remove(SelectedCard);
                }
            });
        }

        private bool CanExecuteDelete() => true;

        #endregion

        #region UpdateCommand
        public IAsyncCommand Update { get; private set; }

        private async Task ExecuteUpdateAsync()
        {
            await Task.Run(() =>
            {
                if (service.TryChangeCard(SelectedCard))
                {
                    int index = Cards.FindIndex(c => c.Id == SelectedCard.Id);
                    Cards[index]=SelectedCard;
                    OnPropertyChanged(nameof(Cards));
                }
            });
        }

        private bool CanExecuteUpdate() => true;

        #endregion


        #region ChooseImageCommand

        public ICommand ChooseImageCommand { get; }

        private bool CanChooseImageCommandExecute(object p) => true;

        private void OnChooseImageCommandExecuted(object p)
        {
            OpenFileDialog FileDialog = new OpenFileDialog();
            FileDialog.Filter = "Files | *.jpg; *.jpeg; *.png";

            Nullable<bool> result = FileDialog.ShowDialog();

            if (result == true)
            {
                var converter = new ImageSourceConverter();
                string filename = FileDialog.FileName;
                try
                {
                    byte[] img = File.ReadAllBytes(filename);
                    SelectedCard.Image = img;
                    OnPropertyChanged(nameof(SelectedCard));
                }
                catch (Exception ex)
                {
                }

            }
        }

        #endregion








        public CatalogViewModel()
        {
            service = new CardService();

            Refresh = new AsyncCommand(ExecuteRefreshAsync, CanExecuteRefresh);
            Delete=new AsyncCommand(ExecuteDeleteAsync, CanExecuteDelete);
            Update = new AsyncCommand(ExecuteUpdateAsync, CanExecuteUpdate);
            ChooseImageCommand = new LambdaCommand(OnChooseImageCommandExecuted, CanChooseImageCommandExecute);
        }
    }
    
}
