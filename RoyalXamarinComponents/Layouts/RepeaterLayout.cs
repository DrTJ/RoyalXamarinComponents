using System.Collections;
using Xamarin.Forms;

namespace RoyalXamarinComponents.Layouts {
    public class RepeaterLayout : StackLayout {
        #region Fields

        #endregion

        #region Constructor

        public RepeaterLayout() {
        }

        #endregion

        #region Properties

        public static BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource),
            typeof(ICollection),
            typeof(RepeaterLayout),
            default(ICollection),
            propertyChanged: (BindableObject bindable, object oldvalue, object newvalue) => {
                var layout = (RepeaterLayout)bindable;
                layout.ItemsSource = (ICollection)newvalue;
                layout.RefreshLayouts();
            });

        public ICollection ItemsSource {
            get { return (ICollection)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public DataTemplate ItemTemplate { get; set; }

        #endregion

        #region Methods

        private void RefreshLayouts() {
            Children.Clear();

            if (ItemsSource == null) {
                return;
            }

            foreach (object item in ItemsSource) {
                View view;

                if (ItemTemplate is DataTemplateSelector selector) {
                    var template = selector.SelectTemplate(item, this);
                    var content = template.CreateContent();

                    var contentView = content as View;
                    var contentViewCell = content as ViewCell;
                    view = contentView ?? contentViewCell?.View;
                }
                else {
                    view = (View)ItemTemplate.CreateContent();
                }

                view.BindingContext = item;
                Children.Add(view);
            }
        }

        #endregion
    }
}