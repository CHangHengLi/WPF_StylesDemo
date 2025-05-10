using System;
using System.Windows.Input;

namespace WPF_StylesDemo.Commands
{
    /// <summary>
    /// 实现ICommand接口的命令类，用于MVVM模式中的命令绑定
    /// </summary>
    public class RelayCommand : ICommand
    {
        // 命令执行的动作
        private readonly Action<object> _execute;
        // 判断命令是否可执行的方法
        private readonly Predicate<object> _canExecute;

        /// <summary>
        /// 创建一个始终可执行的命令
        /// </summary>
        /// <param name="execute">命令执行的动作</param>
        public RelayCommand(Action<object> execute) : this(execute, null)
        {
        }

        /// <summary>
        /// 创建一个可根据条件执行的命令
        /// </summary>
        /// <param name="execute">命令执行的动作</param>
        /// <param name="canExecute">判断命令是否可执行的方法</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// 命令可执行状态改变时触发的事件
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// 判断命令是否可执行
        /// </summary>
        /// <param name="parameter">命令参数</param>
        /// <returns>命令是否可执行</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="parameter">命令参数</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
} 