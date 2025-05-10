using System;
using System.Configuration;
using System.Data;
using System.Windows;
using WPF_StylesDemo.Themes;

namespace WPF_StylesDemo;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// 应用程序启动事件
    /// </summary>
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        // 初始化默认主题
        ThemeManager.ApplyTheme("Blue");
    }
}

