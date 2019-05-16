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
using System.Resources;
using System.Reflection;


namespace CodeAnalyzeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public class CrossClassExchanger
    {
        public void UnderConstruction()
        {
            MessageBox.Show("OwO", "owo", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    public class SettingsBone
    {
        //
    }

    public class Backbone
    {
        public static Int32 ttal = 12;
        public struct ElementLog
        {
            public UInt32 indexCode;    //Index in list
            public Int64 elementHash;   //List Grid Hash
            public string title;        //Data Title
            public DateTime modifyTime; //Time last modified
            public string content;      //Data Content
            public UInt32 imgId;        //Image ID
            public bool analyzed;       //Analyzed mark
        };

        public string EnumCodingToString(UInt32 code, Int32 digitLength)
        {
            string stringcode = Convert.ToString(code);
            Int32 deltaLgth = digitLength - stringcode.Length;
            if (deltaLgth > 0)
                for (; deltaLgth > 0; deltaLgth--)
                    stringcode = "0" + stringcode;
            return stringcode;
        }

        public void BackboneInit(ElementLog[] itemlist)
        {
            UInt32 i;
            for (i = 0; i < Backbone.ttal; i++)
            {
                itemlist[i].indexCode = 0xFFFFFFFF;
                itemlist[i].elementHash = -1;
                itemlist[i].title = "";
                itemlist[i].modifyTime = DateTime.ParseExact("1900-01-01 00:00:00", "yyyy-MM-dd HH:mm:ss", null);
                itemlist[i].imgId = 0xFFFFFFFF;
                itemlist[i].analyzed = false;
            }
        }

        public void ListUpdate(ElementLog[] itemlist)
        {
            UInt32 i, j;
            DateTime[] tmplssu = new DateTime[Backbone.ttal];
            UInt32[] rank = new UInt32[Backbone.ttal];
            for (i = 0; i < Backbone.ttal; i++)
            {
                tmplssu[i] = itemlist[i].modifyTime;
                if (itemlist[i].indexCode == 0xFFFFFFFF)
                    rank[i] = 0xFFFFFFFF;
                else
                    rank[i] = 0;
            }
            for (i = 0; i < Backbone.ttal; i++)
            {
                UInt32 maxPhysicsIndex = Convert.ToUInt32(Backbone.ttal);
                UInt32 maxLogicalIndex = i;
                for (j = 0; j < Backbone.ttal; j++)
                    if (rank[j] == 0)
                    {
                        maxPhysicsIndex = j;
                        break;
                    }
                for (j = maxPhysicsIndex; j < Backbone.ttal; j++)
                    if ((DateTime.Compare(tmplssu[j], tmplssu[maxPhysicsIndex]) > 0) && (rank[j] == 0))
                        maxPhysicsIndex = j;
                if (maxPhysicsIndex < Backbone.ttal)
                    rank[maxPhysicsIndex] = maxLogicalIndex + 1;
            }
            for (i = 0; i < Backbone.ttal; i++)
                if (rank[i] < Backbone.ttal)
                    itemlist[i].indexCode = rank[i];
                else
                    itemlist[i].indexCode = 0xFFFFFFFF;
        }

        public void DemoContentPrefill(ElementLog[] itemlist)
        {
            UInt32 i;
            ResourceManager preloadData = new ResourceManager("CodeAnalyzeWPF.Properties.Resources", Assembly.GetExecutingAssembly());
            for (i = 0; i < Backbone.ttal - 1; i++)
            {
                string lognum = EnumCodingToString(i + 1, 2);
                itemlist[i].indexCode = i + 1;
                itemlist[i].title = preloadData.GetString("PreloadTitle" + lognum);
                itemlist[i].modifyTime = DateTime.ParseExact(preloadData.GetString("PreloadTime" + lognum), "yyyy-MM-dd HH:mm:ss", null);
                itemlist[i].imgId = Convert.ToUInt32((new Random()).Next(1, 5));
                itemlist[i].analyzed = false;
            }
        }

        public int ReadItemIndexInStorage(ElementLog[] list, Int64 itemhash)
        {
            int physicsindex;
            for (physicsindex = 0; physicsindex < ttal; physicsindex++)
                if (list[physicsindex].elementHash == itemhash)
                    break;
            if (physicsindex == ttal)
                physicsindex = -1;
            return physicsindex;
        }

        public void SaveItem(ElementLog[] list, string itemtitle, string itemcontent, DateTime mdt, Int64 hash)
        {
            int i;
            for (i = 0; i < ttal; i++)
                if (list[i].indexCode == 0xFFFFFFFF)
                {
                    list[i].title = itemtitle;
                    list[i].content = itemcontent;
                    list[i].modifyTime = mdt;
                    list[i].indexCode = 0;
                    if (hash != 0)
                        list[ReadItemIndexInStorage(list, hash)].indexCode = 0xFFFFFFFF;
                    break;
                }
        }
    }

    public class MISCBone
    {
        public void DemoStartup()
        {
            Properties.Settings.Default.Demo = true;
        }
    }

    public partial class MainWindow : Window
    {
        public CrossClassExchanger XCE = new CrossClassExchanger();
        private Backbone Bone = new Backbone();
        private MISCBone MISC = new MISCBone();
        private SolidColorBrush hovercolour = new SolidColorBrush(Color.FromArgb(0xFF, 0xDB, 0xD9, 0xD8));   //Hovering Colour
        private SolidColorBrush selectedcolour = new SolidColorBrush(Color.FromArgb(0xFF, 0xC3, 0xC3, 0xC4));//Selected Colour
        private SolidColorBrush unselectcolour = new SolidColorBrush(Color.FromArgb(0x00, 0xFF, 0xFF, 0xFF));//Unselected Colour
        public Backbone.ElementLog[] itemlist = new Backbone.ElementLog[Backbone.ttal];
        private Int64 currentListHash = -1;
        private bool edited = false;
        
        public MainWindow()
        {
            // Special Startup Content
            MISC.DemoStartup();
            // Normal Startup Contents
            StartupDataPrepare();
            InitializeComponent();
            ButtTest(null, null);
            EditPage.Visibility = Visibility.Collapsed;
            ResultPage.Visibility = Visibility.Collapsed;
            VerCue.Text = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetEntryAssembly().Location).ProductVersion;
            //UPDINF.Document = "";
            ProjectListFlush();
        }

        private void StartupDataPrepare()
        {
            Bone.BackboneInit(itemlist);
            if (Properties.Settings.Default.Demo)
            {
                Bone.DemoContentPrefill(itemlist);
            }
            Bone.ListUpdate(itemlist);
        }

        private void InitUPD()
        {
            UPDINF.Document.Blocks.Clear();
            Paragraph Upd01 = new Paragraph();
            Paragraph Upd02 = new Paragraph();
            Paragraph Upd03 = new Paragraph();
            //Upd01. = Properties.Resources.UPD01;

        }

        private void ProjectListFlush()
        {
            UInt32 i, loggedcounter = 0;
            for (i = 0; i < Backbone.ttal; i++)
                if (itemlist[i].indexCode != 0xFFFFFFFF)
                {
                    string lognum = Bone.EnumCodingToString(itemlist[i].indexCode, 2);
                    (FindName("title" + lognum) as TextBlock).Text = itemlist[i].title;
                    (FindName("time" + lognum) as TextBlock).Text = Convert.ToString(itemlist[i].modifyTime);
                    itemlist[i].elementHash = FindName("tem" + lognum).GetHashCode();
                    loggedcounter += 1;
                }
            for (i = loggedcounter + 1; i < Backbone.ttal; i++)
            {
                string lognum = Bone.EnumCodingToString(i, 2);
                (FindName("title" + lognum) as TextBlock).Text = "";
                (FindName("time" + lognum) as TextBlock).Text = "";
            }
        }

        private bool PageTurner(Int64 itemhash)
        {
            bool pageswitchnotice = true;
            string pjtitle = "", pjcontent = "";
            if (itemhash != 0)
            {
                int phyx = Bone.ReadItemIndexInStorage(itemlist, itemhash);
                pjtitle = itemlist[phyx].title;
                pjcontent = itemlist[phyx].content;
            }
            if (edited)
            {
                string NoticeMsg = "未保存的内容将会丢失，是否放弃当前内容？\n是：放弃并打开新页面\n否：保存并打开新页面\n取消：留在原页面";
                MessageBoxResult notsavesel;
                notsavesel = MessageBox.Show(NoticeMsg, "内容未保存", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (notsavesel != MessageBoxResult.Cancel)
                {
                    if (notsavesel == MessageBoxResult.No)
                    {
                        Bone.SaveItem(itemlist, BlockTitle.Text, CodeContent.Text, DateTime.UtcNow, currentListHash);
                        Bone.ListUpdate(itemlist);
                        ProjectListFlush();
                    }
                }
                else
                    pageswitchnotice = false;
            }
            BlockTitleEdit.Visibility = Visibility.Collapsed;
            if (pageswitchnotice)
            {
                BlockTitle.Text = pjtitle;
                CodeContent.Text = pjcontent;
                if (!EditPage.IsVisible)
                {
                    ResultPage.Visibility = Visibility.Collapsed;
                    EditPage.Visibility = Visibility.Visible;
                }
                if (BlockTitle.Text != "")
                    BlockTitle_Hint.Visibility = Visibility.Collapsed;
                else
                    BlockTitle_Hint.Visibility = Visibility.Visible;
                edited = false;
            }
            return pageswitchnotice;
        }

        private void NewItemClick(object sender, MouseButtonEventArgs e)
        {
            int scitemidx;
            UInt32 listindex = 0xFFFFFFFF;
            if ((currentListHash != -1) && (currentListHash != 0))
            {
                scitemidx = Bone.ReadItemIndexInStorage(itemlist, currentListHash);
                listindex = itemlist[scitemidx].indexCode;
            }
            if (PageTurner(0))
            {
                if (listindex != 0xFFFFFFFF)
                    (FindName("tem" + Bone.EnumCodingToString(listindex, 2)) as Grid).Background = unselectcolour;
                newitem.Background = selectedcolour;
                BlockTitleEdit.Visibility = Visibility.Visible;
                currentListHash = 0;
            }
        }

        private void ReadItem(object sender, MouseButtonEventArgs e)
        {
            int scitemidx;
            UInt32 listindex = 0xFFFFFFFF;
            if ((currentListHash != -1) && (currentListHash != 0))
            {
                scitemidx = Bone.ReadItemIndexInStorage(itemlist, currentListHash);
                listindex = itemlist[scitemidx].indexCode;
            }
            scitemidx = Bone.ReadItemIndexInStorage(itemlist, sender.GetHashCode());
            if (PageTurner(sender.GetHashCode()))
            {
                if (listindex != 0xFFFFFFFF)
                    (FindName("tem" + Bone.EnumCodingToString(listindex, 2)) as Grid).Background = unselectcolour;
                if (currentListHash == 0)
                    newitem.Background = unselectcolour;
                listindex = itemlist[scitemidx].indexCode;
                (FindName("tem" + Bone.EnumCodingToString(listindex, 2)) as Grid).Background = selectedcolour;
                currentListHash = itemlist[scitemidx].elementHash;
            }
        }

        private void MouseHovering(object sender, MouseButtonEventArgs e)
        {
            (sender as Grid).Background = hovercolour;
        }

        private void MergeChange(object sender, MouseButtonEventArgs e)
        {
            if (edited)
            {
                if (currentListHash == 0)
                    newitem.Background = unselectcolour;
                else
                    (FindName("tem" + Bone.EnumCodingToString(Convert.ToUInt32(Bone.ReadItemIndexInStorage(itemlist, currentListHash)) + 1, 2)) as Grid).Background = unselectcolour;
                Bone.SaveItem(itemlist, BlockTitleEdit.Text, CodeContent.Text, DateTime.UtcNow, currentListHash);
                if (currentListHash != 0)
                    itemlist[Bone.ReadItemIndexInStorage(itemlist, currentListHash)].indexCode = 0xFFFFFFFF;
                BlockTitleEdit.Visibility = Visibility.Collapsed;
                Bone.ListUpdate(itemlist);
                ProjectListFlush();
                tem01.Background = selectedcolour;
                edited = false;
                currentListHash = tem01.GetHashCode();
            }
        }

        private void TitleChanging(object sender, TextChangedEventArgs e)
        {
            edited = true;
            if (BlockTitleEdit.Text != "")
                BlockTitle_Hint.Visibility = Visibility.Collapsed;
            else
                BlockTitle_Hint.Visibility = Visibility.Visible;
        }

        private void ContentChanging(object sender, TextChangedEventArgs e)
        {
            edited = true;
            if (CodeContent.Text != "")
                CodeContent_Hint.Visibility = Visibility.Collapsed;
            else
                CodeContent_Hint.Visibility = Visibility.Visible;
        }

        private void ButtUpload_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("是否要覆盖当前内容？", "覆盖确认", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.No)
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
                    BlockTitle.Text = (fName.Split('\\'))[fName.Split('\\').Length - 1];
                }
            }
        }

        private void ButtAnalyze(object sender, RoutedEventArgs e)
        {
            int cursor = Bone.ReadItemIndexInStorage(itemlist, currentListHash);
            switch(itemlist[cursor].imgId)
            {
                case 1:
                    {
                        CodePlay.Source = new BitmapImage(new Uri("Resources/RZRC/1_a.jpg",UriKind.Relative));
                        AnalyzeRzlt.Source = new BitmapImage(new Uri("Resources/RZRC/1_b.png", UriKind.Relative));
                    };
                    break;
                case 2:
                    {
                        CodePlay.Source = new BitmapImage(new Uri("Resources/RZRC/2_a.jpg", UriKind.Relative));
                        AnalyzeRzlt.Source = new BitmapImage(new Uri("Resources/RZRC/2_b.png", UriKind.Relative));
                    };
                    break;
                case 3:
                    {
                        CodePlay.Source = new BitmapImage(new Uri("Resources/RZRC/3_a.jpg", UriKind.Relative));
                        AnalyzeRzlt.Source = new BitmapImage(new Uri("Resources/RZRC/3_b.png", UriKind.Relative));
                    };
                    break;
                case 4:
                    {
                        CodePlay.Source = new BitmapImage(new Uri("Resources/RZRC/4_a.jpg", UriKind.Relative));
                        AnalyzeRzlt.Source = new BitmapImage(new Uri("Resources/RZRC/4_b.png", UriKind.Relative));
                    };
                    break;
                case 5:
                    {
                        CodePlay.Source = new BitmapImage(new Uri("Resources/RZRC/5_a.jpg", UriKind.Relative));
                        AnalyzeRzlt.Source = new BitmapImage(new Uri("Resources/RZRC/5_b.png", UriKind.Relative));
                    };
                    break;
                default:
                    {
                        XCE.UnderConstruction();
                    };
                    break;
            }
            EditPage.Visibility = Visibility.Collapsed;
            ResultPage.Visibility = Visibility.Visible;
        }

        private void StartTitleEdit(object sender, MouseButtonEventArgs e)
        {
            BlockTitleEdit.Visibility = Visibility.Visible;
        }

        private void ButtBack_Click(object sender, RoutedEventArgs e)
        {
            EditPage.Visibility = Visibility.Visible;
            ResultPage.Visibility = Visibility.Collapsed;
        }

        private void ButtDown_Click(object sender, RoutedEventArgs e)
        {
            XCE.UnderConstruction();
            Microsoft.Win32.SaveFileDialog savediag = new Microsoft.Win32.SaveFileDialog();

        }

        private void ButtExit_Click(object sender, RoutedEventArgs e)
        {
            if (!edited)
                Environment.Exit(0);
            else
            {
                MessageBoxResult exitwarn = MessageBox.Show("丢弃未保存内容然后退出？", "咳咳", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (exitwarn != MessageBoxResult.Cancel)
                {
                    if (exitwarn == MessageBoxResult.No)
                        Bone.SaveItem(itemlist, BlockTitleEdit.Text, CodeContent.Text, DateTime.UtcNow, currentListHash);
                    Environment.Exit(0);
                }
            }
        }

        private void ButtCkUp_Click(object sender, RoutedEventArgs e)
        {
            XCE.UnderConstruction();
        }

        private void ButtFeed_Click(object sender, RoutedEventArgs e)
        {
            XCE.UnderConstruction();
        }

        private void ButtTest(object sender, MouseButtonEventArgs e)
        {
            CodeItc.Visibility = Visibility.Visible;
            LoginManager.Visibility = Visibility.Collapsed;
            SettingManager.Visibility = Visibility.Collapsed;
            buttTest.Visibility = Visibility.Collapsed;
            buttTestMask.Visibility = Visibility.Visible;
            buttUser.Visibility = Visibility.Visible;
            buttUserMask.Visibility = Visibility.Collapsed;
            buttSetting.Visibility = Visibility.Visible;
            buttSettingMask.Visibility = Visibility.Collapsed;
        }

        private void ButtUser(object sender, MouseButtonEventArgs e)
        {
            CodeItc.Visibility = Visibility.Collapsed;
            LoginManager.Visibility = Visibility.Visible;
            SettingManager.Visibility = Visibility.Collapsed;
            buttTest.Visibility = Visibility.Visible;
            buttTestMask.Visibility = Visibility.Collapsed;
            buttUser.Visibility = Visibility.Collapsed;
            buttUserMask.Visibility = Visibility.Visible;
            buttSetting.Visibility = Visibility.Visible;
            buttSettingMask.Visibility = Visibility.Collapsed;
        }

        private void ButtSett(object sender, MouseButtonEventArgs e)
        {
            CodeItc.Visibility = Visibility.Collapsed;
            LoginManager.Visibility = Visibility.Collapsed;
            SettingManager.Visibility = Visibility.Visible;
            buttTest.Visibility = Visibility.Visible;
            buttTestMask.Visibility = Visibility.Collapsed;
            buttUser.Visibility = Visibility.Visible;
            buttUserMask.Visibility = Visibility.Collapsed;
            buttSetting.Visibility = Visibility.Collapsed;
            buttSettingMask.Visibility = Visibility.Visible;
        }

        private void MainWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ButtExit_Click(sender, null);
        }
    }
}
