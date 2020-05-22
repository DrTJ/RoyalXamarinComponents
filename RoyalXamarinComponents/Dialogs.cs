using System;
using System.Threading.Tasks;

namespace RoyalXamarinComponents {
    public class Dialogs {
        public static Task DisplayAlert(string title, string message, string buttonText = "Ok") {
            return Navigation.Navigator.CurrentPage.DisplayAlert(title, message, buttonText);
        }
    }
}
