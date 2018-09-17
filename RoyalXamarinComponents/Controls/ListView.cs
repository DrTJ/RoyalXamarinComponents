using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

        private DataTemplate primaryTemplate;
        private DataTemplate secondaryTemplate;

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

        public static BindableProperty UseAlternativeTemplateProperty = BindableProperty.Create(
            nameof(UseAlternativeTemplate),
            typeof(bool),
            typeof(ListView), 
            false);

        public bool UseAlternativeTemplate
        {
            get { return (bool)GetValue(UseAlternativeTemplateProperty); }
            set { SetValue(UseAlternativeTemplateProperty, value); }
        }

        public DataTemplate PrimaryTemplate
        {
            get => primaryTemplate;
            set
            {
                if (Equals(primaryTemplate, value))
                {
                    return;
                }

                primaryTemplate = value;

                // Generate Selector
                if(primaryTemplate != null && secondaryTemplate != null)
                {
                    ResetTemplateSelectorSelector();
                }
            }
        }

        public DataTemplate SecondaryTemplate
        {
            get => secondaryTemplate;
            set
            {
                if (Equals(secondaryTemplate, value))
                {
                    return;
                }

                secondaryTemplate = value;

                // Generate Selector
                if (primaryTemplate != null && secondaryTemplate != null)
                {
                    ResetTemplateSelectorSelector();
                }
            }
        }

        private void ResetTemplateSelectorSelector()
        {
            ItemTemplate = new AlternativeTemplateSelector(PrimaryTemplate, SecondaryTemplate);
        }

        #endregion

        #region Commands

        #endregion

        #region Methods

        #endregion
    }

    public class AlternativeTemplateSelector : DataTemplateSelector
    {
        private readonly DataTemplate primaryDataTemplate;
        private readonly DataTemplate secondaryDataTemplate;

        public AlternativeTemplateSelector(DataTemplate primaryDataTemplate, DataTemplate secondaryDataTemplate)
        {
            this.primaryDataTemplate = primaryDataTemplate;
            this.secondaryDataTemplate = secondaryDataTemplate;
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var listView = container as ListView;
            if (listView == null)
                return null;
            
            double index = 0;
            foreach (var itemInSource in listView.ItemsSource)
            {
                if(Equals(itemInSource, item))
                {
                    break;
                }

                index++;
            }

            return index % 2.0 == 0 ? primaryDataTemplate : secondaryDataTemplate;
        }
    }
}
