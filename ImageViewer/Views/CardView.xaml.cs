using ImageViewer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImageViewer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardView : ContentPage
    {
        public CardView()
        {
            InitializeComponent();

            BindingContext = new CardsViewModel();

        }
    }
}