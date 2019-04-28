using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


/*
// Dusted
public struct DataBackbone
{
    DataBackbone *prev;
    String name;
    DateTime timeset;
    UInt32 indexcode;
    DataBackbone* next;
};
*/

namespace CodeAnalyzeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (BlockTitle.Text != "")
                BlockTitle_Hint.Visibility = Visibility.Hidden;
            else
                BlockTitle_Hint.Visibility = Visibility.Visible;
            //EditPage.Visibility = Visibility.Visible;
            //ResultPage.Visibility = Visibility.Hidden;
            EditPage.Visibility = Visibility.Hidden;
            ResultPage.Visibility = Visibility.Visible;
            CodePlay.Source = new BitmapImage(new Uri(@"Resources/RZRC/1_a.jpg", UriKind.Relative));
        }

        private void ButtLoad_Click(object sender, RoutedEventArgs e)
        {
            if (CodeContent.Text != null)
            {
                if(MessageBox.Show("是否要覆盖当前内容？", "覆盖确认", MessageBoxButton.YesNo, MessageBoxImage.Question)!=MessageBoxResult.No)
                {
                    Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                    openFileDialog.InitialDirectory = "%userprofile%";
                    openFileDialog.Filter = "文本文件|*.txt|所有文件|*.*";
                    openFileDialog.RestoreDirectory = true;
                    openFileDialog.FilterIndex = 1;
                    if (openFileDialog.ShowDialog() == true)
                    {
                        string fName = openFileDialog.FileName;
                        string text = System.IO.File.ReadAllText(fName);
                        CodeContent.Text = text;
                    }
                }
            }
        }

        private void EditTitle(object sender, MouseButtonEventArgs e)
        {
            BlockTitleEdit.Text = BlockTitle.Text;
            BlockTitleEdit.Visibility = Visibility.Visible;
            BlockTitle_Hint.Visibility = Visibility.Hidden;
            BlockTitleEdit.Focus();
        }

        private void TitleEditCompleted(object sender, RoutedEventArgs e)
        {
            BlockTitle.Text = BlockTitleEdit.Text;
            BlockTitleEdit.Visibility = Visibility.Hidden;
            DataTitle.Text = BlockTitle.Text;
            DataUpd.Text = DateTime.Now.ToString();
            if (BlockTitle.Text != "")
                BlockTitle_Hint.Visibility = Visibility.Hidden;
            else
                BlockTitle_Hint.Visibility = Visibility.Visible;
        }

        private void BlockTitleEdit_TextChanged(object sender, TextChangedEventArgs e)
       {
            BlockTitle.Text = BlockTitleEdit.Text;
        }

        private void CodeContent_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CodeContent.Text != "")
                CodeContent_Hint.Visibility = Visibility.Hidden;
            else
                CodeContent_Hint.Visibility = Visibility.Visible;
        }

        private void EatenUp(object sender, RoutedEventArgs e)
        {
            BlockTitle.Text = BlockTitleEdit.Text;
            BlockTitleEdit.Visibility = Visibility.Hidden;
            DataTitle.Text = BlockTitle.Text;
            DataUpd.Text = DateTime.Now.ToString();
        }

        private void KeyChecker(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == Key.Enter.ToString())
                TitleEditCompleted(sender, e);
        }

        private void ButtAnalyze_Click(object sender, RoutedEventArgs e)
        {
            EditPage.Visibility = Visibility.Hidden;
            ResultPage.Visibility = Visibility.Visible;
        }

        private void ButtBack_Click(object sender, RoutedEventArgs e)
        {
            ResultPage.Visibility = Visibility.Hidden;
            EditPage.Visibility = Visibility.Visible;
        }

        private void ButtDownload_Click(object sender, RoutedEventArgs e)
        {
            // Unknown Seq
        }

        // WARNING
        // Codes below is not a good sample for a REAL program!
    }
}
