using CefSharp;
using CefSharp.WinForms;
using CefSharp.BrowserSubprocess;
using System;
using System.Windows.Forms;
using System.Threading;

namespace GoogleChromeDemo
{
    public partial class Form2 : Form
    {
        ChromiumWebBrowser WebBrowser;
        public Form2()
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
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;//新cefsharp绑定需要优先申明
            WebBrowser.RegisterJsObject("jsObj", new JsEvent(), new CefSharp.BindingOptions() { CamelCaseJavascriptNames = false }); //交互数据                                           

            WebBrowser.Dock = DockStyle.Fill;//铺满
            this.Controls.Add(WebBrowser);//加入窗体
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
        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
