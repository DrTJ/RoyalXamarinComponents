using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RoyalXamarinComponents.Navigation {
    public static class Navigator {
        public static void Push(Page pageToNavigate, INavigationParameters navigationParameters = null) {
            var viewModel = pageToNavigate.BindingContext as INavigationViewModel;
            viewModel?.OnNavigatedTo(navigationParameters);

            if (viewModel != null) {
                CurrentPage.Appearing += PreviousPage_Appearing;
            }


            if (CurrentNavigationPage == null) {
                Application.Current.MainPage = new NavigationPage(pageToNavigate);
                return;
            }

            CurrentNavigationPage.Navigation.PushAsync(pageToNavigate);
        }

        private static void PreviousPage_Appearing(object sender, EventArgs e) {
            var viewModel = (sender as Page).BindingContext as INavigationViewModel;
            viewModel?.OnBackwardNavigated();
        }

        public static void PushModal(Page pageToNavigate) {
            CurrentPage.Navigation.PushModalAsync(pageToNavigate);
        }

        public static async Task Pop() {
            await CurrentPage.Navigation.PopAsync();
        }

        public static async Task PopModal() {
            await CurrentPage.Navigation.PopModalAsync();
        }

        public static void SetMasterPage(MasterDetailPage masterDetailPage, Page detailPage) {
            masterDetailPage.Detail = new NavigationPage(detailPage);
            Application.Current.MainPage = masterDetailPage;
        }

        public static void SetPage(Page page) {
            Application.Current.MainPage = page;
        }

        public static Page CurrentPage {
            get {
                var currentPage = Application.Current.MainPage is MasterDetailPage
                    ? ((MasterDetailPage)Application.Current.MainPage).Detail
                    : Application.Current.MainPage;

                return currentPage is NavigationPage ? ((NavigationPage)currentPage).CurrentPage : currentPage;
            }
        }

        public static Page CurrentNavigationPage {
            get {
                var navPage = Application.Current.MainPage as NavigationPage;
                if (navPage != null)
                    return navPage;

                var resPage = Application.Current.MainPage as MasterDetailPage;
                if (resPage != null)
                    return resPage.Detail;

                return Application.Current.MainPage;
            }
        }

        public static MasterDetailPage MasterPage => Application.Current.MainPage as MasterDetailPage;

        public static bool IsShowingAnyPage => Application.Current.MainPage != null;
    }
}