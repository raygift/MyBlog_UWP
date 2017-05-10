using System;
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
using Windows.Storage;
using System.Text;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace MyBlog
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class BlogWriting : Page
    {
        public BlogWriting()
        {
            this.InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var date = publicDate.Date;
            var type = ((ComboBoxItem)typeSelectBox.SelectedItem).Content.ToString();
            var description = blogDescription;
            var tags = blogTags;
            var content = blogContent;
            

            var filename= date.Year.ToString() + "-" + date.Month.ToString() + "-" + date.Day.ToString() +"-"+ blogTitle.Text.Replace(" ", "-");
            var filecontent = "---\n"+"layout: post\n"+"title: " + blogTitle.Text+"\n"+"category: "+ type+"\n"+"tags: "+blogTags.Text+"\n"+"description: "+blogDescription.Text+"\n"+"---\n"+"\n\n\n"+blogContent.Text;
            SaveFile(filename, filecontent);
        }

        //private async void GetFile() {
            //StorageFolder picutureFolder = KnownFolders.PicturesLibrary;
            //StringBuilder pictureFolderInfo = new StringBuilder();
            //IReadOnlyList<IStorageItem> pictureFileItem = await picutureFolder.GetItemsAsync();
            //foreach (var i in pictureFileItem)
            //{
            //    if (i is StorageFolder)
            //        pictureFolderInfo.Append(i.Name + "\n");
            //    else
            //        pictureFolderInfo.Append(i.Name + "\n");
            //}
            //textBlockFileName.Text = pictureFolderInfo.ToString();
        //}

        private async void SaveFile(string FileName,string FileContent)
        {
            FileSavePicker saveFile = new FileSavePicker();
            saveFile.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;

            // 显示在下拉列表的文件类型
            saveFile.FileTypeChoices.Add("markdown文件", new List<string>() { ".md" });

            // 默认的文件名
            saveFile.SuggestedFileName = FileName;

            StorageFile file = await saveFile.PickSaveFileAsync();

            if (file != null)
            {
                // 在用户完成更改并调用CompleteUpdatesAsync之前，阻止对文件的更新
                CachedFileManager.DeferUpdates(file);
                string fileContent = FileContent;
                await FileIO.WriteTextAsync(file, fileContent);
                // 当完成更改时，其他应用程序才可以对该文件进行更改。
                FileUpdateStatus updateStatus = await CachedFileManager.CompleteUpdatesAsync(file);
                if (updateStatus == FileUpdateStatus.Complete)
                {
                    textBlockFileName.Text = file.Name + " 已经保存好了。";
                }
                else
                {
                    textBlockFileName.Text = file.Name + " 保存失败了。";
                }
            }
            else
            {
                textBlockFileName.Text = "保存操作被取消。";
            }
        }
    }
}
