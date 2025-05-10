using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPF_StylesDemo.Converters
{
    /// <summary>
    /// 将字符串比较结果转换为Visibility的转换器
    /// </summary>
    public class StringEqualityToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// 如果值与参数相等，则返回Visible，否则返回Collapsed
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return Visibility.Collapsed;

            var stringValue = value.ToString();
            var stringParameter = parameter.ToString();

            return string.Equals(stringValue, stringParameter, StringComparison.OrdinalIgnoreCase)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        /// <summary>
        /// 反向转换，这里不支持
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 