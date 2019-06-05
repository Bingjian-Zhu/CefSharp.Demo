using CefSharp;
using CefSharp.WinForms;
using CefSharp.BrowserSubprocess;
using System;
using System.Windows.Forms;
using System.Threading;

namespace GoogleChromeDemo
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Chromium浏览器实例
        /// </summary>
        ChromiumWebBrowser WebBrowser;
        public Form1()
        {
            InitializeComponent();
            var setting = new CefSettings();
            setting.Locale = "zh-CN";
            setting.CachePath = "CHBrowser/BrowserCache";//缓存路径
            setting.AcceptLanguageList = "zh-CN,zh;q=0.8";//浏览器引擎的语言
            setting.LocalesDirPath = "CHBrowser/localeDir";//日志
            setting.LogFile = "CHBrowser/LogData";//日志文件
            setting.PersistSessionCookies = true;//
            setting.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";//浏览器内核
            setting.UserDataPath = "CHBrowser/userData";//个人数据
            ///初始化
            CefSharp.Cef.Initialize(setting);
            WebBrowser = new ChromiumWebBrowser("https://www.baidu.com"); //初始页面
            panel1.Controls.Add(WebBrowser);
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;//新cefsharp绑定需要优先申明
            WebBrowser.RegisterJsObject("jsMsg", new JsEvent(), new CefSharp.BindingOptions() { CamelCaseJavascriptNames = false }); //交互数据                                           

            WebBrowser.Dock = DockStyle.Fill;//铺满
            //this.Controls.Add(WebBrowser);//加入窗体
            this.panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public class JsEvent
        {
            public string MessageText { get; set; }

            public void ShowTest()
            {
                MessageBox.Show("this in C#.\n\r" + MessageText);
            }
            public void ShowTestArg(string ss)
            {
                MessageBox.Show("收到JS带参数调用\n\r" + ss);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebBrowser.Load(textBox1.Text);//浏览网址
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await WebBrowser.GetBrowser().MainFrame.EvaluateScriptAsync("test_val=" + new Random().Next().ToString("F")); //设置页面上js的test_val变量为随机数
            await WebBrowser.GetBrowser().MainFrame.EvaluateScriptAsync("test()");//运行页面上js的test方法
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            await WebBrowser.GetBrowser().MainFrame.EvaluateScriptAsync("testArg('123','我是NET' )");//运行页面上js的testArg带参数的方法
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WebBrowser.Load("https://www.baidu.com");//浏览网址
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WebBrowser.Load("http://localhost:50712/HtmlPage1.html");//浏览网址
            while (WebBrowser.IsLoading)
            {
                Thread.Sleep(100);
            }
            //需要延迟1秒，等页面真正加载完才能把完成数据传输
            Thread.Sleep(1000);
            WebBrowser.GetBrowser().MainFrame.EvaluateScriptAsync("testArg('page1','我是NET' )");//运行页面上js的testArg带参数的方法
        }

        private void button6_Click(object sender, EventArgs e)
        {
            WebBrowser.Load("http://localhost:50712/HtmlPage2.html");//浏览网址
            while (WebBrowser.IsLoading)
            {
                Thread.Sleep(100);
            }
            //需要延迟1秒，等页面真正加载完才能把完成数据传输
            Thread.Sleep(1000);
            WebBrowser.GetBrowser().MainFrame.EvaluateScriptAsync("testArg('page2','我是NET' )");//运行页面上js的testArg带参数的方法
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
