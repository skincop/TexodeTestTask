using Client.Infastructure.Commands;
using Client.Models;
using Client.ViewModels.Base;
using Microsoft.Win32;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using Client.Infastructure.Commands.Base;

namespace Client.ViewModels.UserControls
{
    internal class AddViewModel : ViewModel
    {


        #region Image

        private string _imageSource = "../../Resources/Images/no-image.png";

        /// <summary>Заголовок окна</summary>
        public string ImageSource
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }

        #endregion

        #region Name

        private string _name;

        /// <summary>Заголовок окна</summary>
        public string Name
        {
            get => _name;
            set {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

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
                    var image = p as System.Windows.Controls.Image;
                    image.Source = (ImageSource)converter.ConvertFromString(filename);
                    ImageSource = filename;
                }
                catch (Exception ex)
                {
                }

            }
        }

        #endregion

        #region AddCommand
        public IAsyncCommand Add { get; private set; }

        private async Task ExecuteAddAsync()
        {
            await Task.Run(() =>
            {
                byte[] imageArray = System.IO.File.ReadAllBytes(ImageSource);
                Card card = new Card(Name, imageArray);
                CardService service = new CardService();

                service.TryAddCard(card);
            });
        }

        private bool CanExecuteAdd() => true;

        #endregion


        public AddViewModel()
        {
            ChooseImageCommand = new LambdaCommand(OnChooseImageCommandExecuted, CanChooseImageCommandExecute);
            Add = new AsyncCommand(ExecuteAddAsync, CanExecuteAdd);
        }




    }
}
