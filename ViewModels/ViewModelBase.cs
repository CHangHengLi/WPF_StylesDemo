using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF_StylesDemo.ViewModels
{
    /// <summary>
    /// 所有ViewModel的基类，实现属性更改通知
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// 属性变更事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 通知指定属性值已更改
        /// </summary>
        /// <param name="propertyName">属性名称(可选，默认为调用方法的名称)</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 设置属性值并通知属性更改
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="storage">引用属性的字段</param>
        /// <param name="value">属性的新值</param>
        /// <param name="propertyName">属性名称(可选，默认为调用方法的名称)</param>
        /// <returns>如果值已更改，则为true</returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
} 