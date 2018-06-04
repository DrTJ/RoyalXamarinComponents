using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RoyalXamarinComponents
{
    public class RepeaterLayout: StackLayout
    {
        #region Fields

        #endregion

        #region Bindable properties

        public static BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource),
            typeof(ObservableCollection<object>),
            typeof(RepeaterLayout),
            new ObservableCollection<object>(),
            BindingMode.TwoWay,
            propertyChanging: (BindableObject bindable, object oldvalue, object newvalue) => {
                RepeaterLayout layout = (RepeaterLayout)bindable;
                layout.ItemsSource = (ObservableCollection<object>)newvalue;
                layout.RefreshLayouts();
            },
            propertyChanged: (BindableObject bindable, object oldvalue, object newvalue) => {
                RepeaterLayout layout = (RepeaterLayout)bindable;
                layout.ItemsSource = (ObservableCollection<object>)newvalue;
                layout.RefreshLayouts();
            });

        public ObservableCollection<object> ItemsSource
        {
            get { return (ObservableCollection<object>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        #endregion

        #region Constructor

        public RepeaterLayout()
        {
        }

        #endregion

        #region Properties

        public DataTemplate ItemTemplate { get; set; }

        #endregion

        #region Methods

        private void RefreshLayouts()
        {
            this.Children.Clear();

            if (this.ItemsSource?.Any() ?? false)
            {
                foreach (object item in this.ItemsSource)
                {
                    View view = (View)this.ItemTemplate.CreateContent();
                    view.BindingContext = item;
                }
            }
        }

        #endregion
    }
}
