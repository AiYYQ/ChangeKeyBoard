using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;


namespace 修改键盘重复延迟
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int keyBoardDelay = SystemInformation.KeyboardDelay;
            int keyBoardSpeed = SystemInformation.KeyboardSpeed;

            StringBuilder sbuilder = new StringBuilder();
            sbuilder.Append("当前键盘重复延迟为");
            sbuilder.Append(keyBoardDelay);
            sbuilder.AppendLine();
            sbuilder.Append("当前键盘重复速度为");
            sbuilder.Append(keyBoardSpeed);
            MessageBox.Show(sbuilder.ToString(),"属性");
        }

        
        protected void changeRegistry()
        {
            RegistryKey registryKey = Registry.CurrentUser;
            RegistryKey target = registryKey.OpenSubKey(@"Control Panel\Keyboard\", true);
            if (target == null)
            {
                this.label1.Text = "查询注册表失败,注册表中不存在该项";
                return;
            }
            // 获取注册表源值
            string a = target.GetValue("KeyboardSpeed").ToString();
            if ("48".Equals(a))
            {
                this.label1.Text = "修复成功";
                return;
            }
            else
            {
                this.label1.Text = "当前键盘重复速度为" + a;
            }
            // 修改注册表
            target.SetValue("KeyboardSpeed", "48");
            a = target.GetValue("KeyboardSpeed").ToString();
            this.label1.Text = "当前键盘重复速度为" + a + "修改成功!";
            registryKey.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            changeRegistry();
        }

        
    }

}
