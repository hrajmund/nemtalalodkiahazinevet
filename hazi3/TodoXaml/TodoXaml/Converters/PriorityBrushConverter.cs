using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoXaml.Models;

namespace TodoXaml.Converters
{
    //Először itt írtam meg a Convert függvényt, végül nem ezt használtam, mivel a Visual Studio win10-x64.pubxml hiányára hivatkozott
    //Végül a MainPage.xaml.cs-ben írtam meg, mint GetForeground()
    public class PriorityBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        { 
            if (value is Priority priority)
            {
                switch (priority)
                {
                    case Priority.High:
                        return new SolidColorBrush(Colors.Red);
                    case Priority.Normal:
                        return (SolidColorBrush)App.Current.Resources["ApplicationForegroundThemeBrush"];
                    case Priority.Low:
                        return new SolidColorBrush(Colors.Yellow);
                    default:
                        return (SolidColorBrush)App.Current.Resources["ApplicationForegroundThemeBrush"];
                }
            }

            return (SolidColorBrush)App.Current.Resources["ApplicationForegroundThemeBrush"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

    }
}
