# WPF样式详解示例程序

这是一个基于.NET Core 8.0的WPF MVVM模式示例程序，专注于展示WPF样式系统的功能和用法。

## 项目结构

- `/Commands` - 命令类，实现MVVM模式中的命令绑定
- `/Converters` - 值转换器，用于UI绑定中的数据转换
- `/Models` - 数据模型类
- `/Styles` - 样式资源文件
- `/Themes` - 主题资源文件
- `/ViewModels` - 视图模型类
- `/Views` - 视图组件

## 功能特性

此示例程序展示了以下WPF样式相关技术：

1. **样式定义与应用**
   - 基础样式定义
   - 显式样式引用
   - 隐式样式应用

2. **样式继承**
   - 使用BasedOn属性实现样式继承
   - 构建样式层次结构

3. **触发器使用**
   - 属性触发器 (Trigger)
   - 数据触发器 (DataTrigger)
   - 多条件触发器 (MultiTrigger)
   - 事件触发器 (EventTrigger)

4. **资源字典**
   - 资源字典的创建
   - 资源引用
   - 资源字典合并

5. **主题切换**
   - 动态资源
   - 运行时主题切换

6. **MVVM模式实现**
   - 数据绑定
   - 命令绑定
   - 状态管理

## 如何使用

1. 启动程序，默认使用蓝色主题
2. 使用顶部的主题切换按钮更改应用程序主题
3. 左侧列表展示不同样式项目，点击查看详情
4. 可以添加、编辑、删除项目

## 学习资源

本项目主要基于以下资源：

- [WPF之样式详解.md](WPF之样式详解.md) - 详细的WPF样式系统技术文档
- [Microsoft WPF Documentation](https://learn.microsoft.com/zh-cn/dotnet/desktop/wpf/controls/styles-templates-overview)
- [WPF Tutorial](https://wpf-tutorial.com/zh/35/样式/样式简介/) 