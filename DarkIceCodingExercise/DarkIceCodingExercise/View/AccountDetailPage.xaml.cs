using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DarkIceCodingExercise.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountDetailPage : ContentPage
    {
        public AccountDetailPage(string picture, string fullName)
        {
            InitializeComponent();
            Picture.Source = picture;
            Name.Text = fullName;
        }
    }
}