﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace MyBlog
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            MyFrame.Navigate(typeof(BlogWriting));
            PageTitle.Text = "Write your blog!";
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySV.IsPaneOpen = !MySV.IsPaneOpen;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Home.IsSelected)
            {
                MyFrame.Navigate(typeof(BlogWriting));
                PageTitle.Text = "Write your blog!";

            }
            else
            {
                MyFrame.Navigate(typeof(SettingPage));
                PageTitle.Text = "Setting your style!";

            }
        }
    }
}
