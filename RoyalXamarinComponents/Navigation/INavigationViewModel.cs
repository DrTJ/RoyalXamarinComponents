namespace RoyalXamarinComponents.Navigation {
    public interface INavigationViewModel {
        void OnNavigatedTo(INavigationParameters navigationParameters);
        void OnBackwardNavigated();
    }
}