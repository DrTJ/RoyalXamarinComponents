using System;
using Xamarin.Forms;

namespace RoyalXamarinComponents.Controls {
    public class FloatActionButton : Button {
        public FloatActionButton() {
            FontSize = 25;
            FontAttributes = FontAttributes.Bold;
            TextColor = Color.White;
            BackgroundColor = Color.DeepSkyBlue;
            WidthRequest = Size;
            HeightRequest = Size;
            CornerRadius = Size / 2;
        }


        public BindableProperty SizeProperty = BindableProperty.Create(
            nameof(Size),
            typeof(int),
            typeof(FloatActionButton),
            60,
            propertyChanged: (bindable, oldValue, newValue) => {
                var control = (FloatActionButton)bindable;
                control.WidthRequest = (int)newValue;
                control.HeightRequest = (int)newValue;
                control.CornerRadius = (int)newValue / 2;
            });

        public int Size {
            get => (int)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        /*

        public BindableProperty SizeProperty = BindableProperty.Create(
            nameof(Size),
            typeof(int),
            typeof(FloatActionButton),
            60,
            propertyChanged: (bindable, oldValue, newValue) => {
                var control = (FloatActionButton)bindable;
                // your code goes here!
            });

        public int Size {
            get => (int)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }
         */


    }
}
