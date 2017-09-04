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
using System.Web;
using System.Net;
using System.IO;

namespace PortalApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if(textBox.Text == "" || textBox1.Text == "")
            {
                MessageBox.Show("用户名或密码不能为空");
            }
            else
            {
                //TODO
                doLogin(textBox.Text, textBox1.Text);
            }
        }

        /**
         * 用户登录接口模型的构建
         * @param string username 用户名
         * @param string password 用户密码
         * @return mixed 登录结果
         */
        public void doLogin(String username, String password)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://passport.dingstudio.cn/sso/api?format=ajaxlogin");
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = Encoding.UTF8.GetByteCount("username=" + username + "&userpwd=" + password + "&cors_domain=localapp");
            request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version10;
            String post_data = "username=" + username + "&userpwd=" + password + "&cors_domain=localapp";
            MessageBox.Show(post_data);
            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(post_data);
                dataStream.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            MessageBox.Show(retString);
            //解析josn
        }
    }
}
