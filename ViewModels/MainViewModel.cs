using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System.Collections.Generic;
using WPF_StylesDemo.Commands;
using WPF_StylesDemo.Models;
using WPF_StylesDemo.Themes;

namespace WPF_StylesDemo.ViewModels
{
    /// <summary>
    /// 主窗口的ViewModel，包含样式演示所需的数据和命令
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region 私有字段

        private string _currentTheme = "Blue"; // 默认主题
        private ObservableCollection<StyleItem> _items;
        private ObservableCollection<StyleItem> _filteredItems;
        private StyleItem _selectedItem;
        private string _searchText;
        private bool _isEditing;
        private List<StyleItem> _allItems; // 存储所有项目的备份

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数，初始化数据和命令
        /// </summary>
        public MainViewModel()
        {
            // 初始化样式演示项目集合
            _allItems = new List<StyleItem>
            {
                new StyleItem
                {
                    Id = 1,
                    Name = "基本按钮样式",
                    Description = "演示基本的按钮样式定义与应用",
                    Status = "Success",
                    CreatedAt = DateTime.Now.AddDays(-5),
                    IsActive = true
                },
                new StyleItem
                {
                    Id = 2,
                    Name = "触发器样式",
                    Description = "演示各种触发器的使用方法",
                    Status = "Warning",
                    CreatedAt = DateTime.Now.AddDays(-3),
                    IsActive = true
                },
                new StyleItem
                {
                    Id = 3,
                    Name = "样式继承",
                    Description = "演示WPF样式继承机制",
                    Status = "Error",
                    CreatedAt = DateTime.Now.AddDays(-1),
                    IsActive = false
                },
                new StyleItem
                {
                    Id = 4,
                    Name = "动态资源",
                    Description = "演示动态资源的使用方法",
                    Status = "Success",
                    CreatedAt = DateTime.Now,
                    IsActive = true
                }
            };

            // 初始化过滤后的集合
            _filteredItems = new ObservableCollection<StyleItem>(_allItems);
            Items = _filteredItems;

            // 初始化命令
            SwitchThemeCommand = new RelayCommand(ExecuteSwitchTheme);
            AddItemCommand = new RelayCommand(ExecuteAddItem);
            EditItemCommand = new RelayCommand(ExecuteEditItem, CanExecuteEditItem);
            DeleteItemCommand = new RelayCommand(ExecuteDeleteItem, CanExecuteEditItem);
            SaveItemCommand = new RelayCommand(ExecuteSaveItem);
            CancelEditCommand = new RelayCommand(ExecuteCancelEdit);
        }

        #endregion

        #region 公共属性

        /// <summary>
        /// 当前主题名称
        /// </summary>
        public string CurrentTheme
        {
            get => _currentTheme;
            set
            {
                if (SetProperty(ref _currentTheme, value))
                {
                    // 当CurrentTheme改变时，应用新主题
                    ThemeManager.ApplyTheme(value);
                }
            }
        }

        /// <summary>
        /// 样式演示项目集合
        /// </summary>
        public ObservableCollection<StyleItem> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        /// <summary>
        /// 当前选中的项目
        /// </summary>
        public StyleItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (SetProperty(ref _selectedItem, value))
                {
                    // 属性更改后，通知可编辑命令更新可执行状态
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        /// <summary>
        /// 搜索文本
        /// </summary>
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    FilterItems();
                }
            }
        }

        /// <summary>
        /// 是否处于编辑状态
        /// </summary>
        public bool IsEditing
        {
            get => _isEditing;
            set => SetProperty(ref _isEditing, value);
        }

        #endregion

        #region 命令

        /// <summary>
        /// 切换主题命令
        /// </summary>
        public ICommand SwitchThemeCommand { get; }

        /// <summary>
        /// 添加项目命令
        /// </summary>
        public ICommand AddItemCommand { get; }

        /// <summary>
        /// 编辑项目命令
        /// </summary>
        public ICommand EditItemCommand { get; }

        /// <summary>
        /// 删除项目命令
        /// </summary>
        public ICommand DeleteItemCommand { get; }

        /// <summary>
        /// 保存项目命令
        /// </summary>
        public ICommand SaveItemCommand { get; }

        /// <summary>
        /// 取消编辑命令
        /// </summary>
        public ICommand CancelEditCommand { get; }

        #endregion

        #region 命令执行方法

        /// <summary>
        /// 执行切换主题命令
        /// </summary>
        /// <param name="parameter">主题名称</param>
        private void ExecuteSwitchTheme(object parameter)
        {
            if (parameter is string themeName)
            {
                CurrentTheme = themeName;
                // 注意：在CurrentTheme属性的setter中已经调用了ThemeManager.ApplyTheme
            }
        }

        /// <summary>
        /// 执行添加项目命令
        /// </summary>
        private void ExecuteAddItem(object parameter)
        {
            var newItem = new StyleItem
            {
                Id = _allItems.Count > 0 ? _allItems.Max(i => i.Id) + 1 : 1,
                Name = "新样式项目",
                Description = "请输入描述",
                Status = "Success",
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            _allItems.Add(newItem);
            FilterItems(); // 重新应用过滤
            SelectedItem = newItem;
            IsEditing = true;
        }

        /// <summary>
        /// 执行编辑项目命令
        /// </summary>
        private void ExecuteEditItem(object parameter)
        {
            IsEditing = true;
        }

        /// <summary>
        /// 执行删除项目命令
        /// </summary>
        private void ExecuteDeleteItem(object parameter)
        {
            if (SelectedItem != null)
            {
                _allItems.Remove(SelectedItem);
                FilterItems(); // 重新应用过滤
                SelectedItem = null;
            }
        }

        /// <summary>
        /// 执行保存项目命令
        /// </summary>
        private void ExecuteSaveItem(object parameter)
        {
            // 在实际应用中，这里会保存数据到数据库
            IsEditing = false;
        }

        /// <summary>
        /// 执行取消编辑命令
        /// </summary>
        private void ExecuteCancelEdit(object parameter)
        {
            IsEditing = false;
        }

        /// <summary>
        /// 判断是否可以执行编辑相关命令
        /// </summary>
        /// <returns>如果有选中项目则返回true</returns>
        private bool CanExecuteEditItem(object parameter)
        {
            return SelectedItem != null && !IsEditing;
        }

        /// <summary>
        /// 根据搜索文本过滤项目
        /// </summary>
        private void FilterItems()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                // 如果搜索文本为空，显示所有项目
                _filteredItems.Clear();
                foreach (var item in _allItems)
                {
                    _filteredItems.Add(item);
                }
            }
            else
            {
                // 否则，根据搜索文本过滤项目
                var searchTerm = SearchText.ToLower();
                _filteredItems.Clear();
                
                // 检查是否是搜索激活状态的关键词
                bool? searchIsActive = null;
                if (searchTerm.Contains("激活") || searchTerm.Contains("是") || 
                    searchTerm.Contains("yes") || searchTerm.Contains("true") || 
                    searchTerm.Contains("已"))
                {
                    searchIsActive = true;
                }
                else if (searchTerm.Contains("未激活") || searchTerm.Contains("否") || 
                         searchTerm.Contains("no") || searchTerm.Contains("false") || 
                         searchTerm.Contains("禁用") || searchTerm.Contains("未"))
                {
                    searchIsActive = false;
                }
                
                foreach (var item in _allItems)
                {
                    // 先检查是否匹配激活状态
                    if (searchIsActive.HasValue)
                    {
                        if (item.IsActive == searchIsActive.Value)
                        {
                            _filteredItems.Add(item);
                        }
                    }
                    // 如果不是搜索激活状态，则检查其他属性
                    else if (item.Name.ToLower().Contains(searchTerm) || 
                             item.Description.ToLower().Contains(searchTerm) ||
                             item.Status.ToLower().Contains(searchTerm))
                    {
                        _filteredItems.Add(item);
                    }
                }
            }
        }

        #endregion
    }
} 