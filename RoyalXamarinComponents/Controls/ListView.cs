using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RoyalXamarinComponents.Controls
{
    public class ListView : Xamarin.Forms.ListView
    {
        #region Fields

        #endregion

        #region Constructors

        public ListView()
        {
            ItemTapped += (sender, e) => 
            {
                if (ItemTappedCommand?.CanExecute(e.Item) ?? false)
                {
                    ItemTappedCommand?.Execute(e.Item);
                }

                SelectedItem = null;
            };
        }

        #endregion

        #region Properties

        public static BindableProperty ItemTappedCommandProperty = BindableProperty.Create(
            nameof(ItemTappedCommand),
            typeof(ICommand),
            typeof(ListView),
            propertyChanged: (bindable, oldValue, newValue) => {
                var control = (ListView)bindable;
                control.ItemTappedCommand = (ICommand)newValue;
            });

        public ICommand ItemTappedCommand
        {
            get { return (ICommand)GetValue(ItemTappedCommandProperty); }
            set { SetValue(ItemTappedCommandProperty, value); }
        }

        #endregion

        #region Commands

        #endregion

        #region Methods

        #endregion
    }
}
