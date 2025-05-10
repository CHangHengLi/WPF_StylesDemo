using System;
using System.Windows;

namespace WPF_StylesDemo.Themes
{
    /// <summary>
    /// 主题管理器，负责应用程序主题的切换
    /// </summary>
    public class ThemeManager
    {
        /// <summary>
        /// 可用的主题名称
        /// </summary>
        public static string[] ThemeNames = new[] { "Blue", "Green", "Red", "Dark" };

        /// <summary>
        /// 应用主题
        /// </summary>
        /// <param name="themeName">主题名称 (Blue, Green, Red, Dark)</param>
        public static void ApplyTheme(string themeName)
        {
            // 验证主题名称是否有效
            if (string.IsNullOrEmpty(themeName))
            {
                themeName = "Blue"; // 默认主题
            }

            // 构建主题资源URI
            Uri themeUri = new Uri($"/Themes/{themeName}Theme.xaml", UriKind.Relative);

            // 清理并更新Application中的资源字典
            ResourceDictionary currentThemeDict = null;

            // 查找并移除现有主题资源字典
            foreach (ResourceDictionary dict in Application.Current.Resources.MergedDictionaries)
            {
                if (dict.Source != null && dict.Source.ToString().Contains("/Themes/") && dict.Source.ToString().EndsWith("Theme.xaml"))
                {
                    currentThemeDict = dict;
                    break;
                }
            }

            if (currentThemeDict != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(currentThemeDict);
            }

            // 添加新的主题资源字典
            ResourceDictionary newThemeDict = new ResourceDictionary
            {
                Source = themeUri
            };

            Application.Current.Resources.MergedDictionaries.Add(newThemeDict);
        }
    }
} 