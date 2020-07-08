using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;


namespace Laboratoria.Views.Controls
{
    public partial class Details : StackLayout
    {
        public Details()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
    propertyName: nameof(Title),
    returnType: typeof(string),
    declaringType: typeof(Details),
    defaultValue: null);

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly BindableProperty ControlContentProperty = BindableProperty.Create(
    propertyName: nameof(ControlContent),
    returnType: typeof(View),
    declaringType: typeof(Details),
    defaultValue: null);
        public View ControlContent
        {
            get { return (View)GetValue(ControlContentProperty); }
            set { SetValue(ControlContentProperty, value); }
        }
    }
}