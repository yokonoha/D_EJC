using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace D_EJC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.Black;
            this.TransparencyKey = Color.Black;
            this.Opacity = 0;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string myname = Process.GetCurrentProcess().ProcessName;
            //A_Ejector.exeならAドライブを選択。基本的にはexeの名前の最初の文字がドライブレターになるよ。
            if (myname.Length >= 2 &&myname[1] == '_' &&char.IsLetter(myname[0]))
            {
                char driveletter = myname[0];
                ejectdisc(driveletter.ToString().ToUpper());
                
                this.Close();
            }
            else
            {
                MessageBox.Show("このプログラムは、実行ファイルの名前の最初の文字がドライブレターで、次にアンダースコアが続く形式で命名されている必要があります。例: A_Ejector.exe");
                this.Close();
            }

        }

        private void ejectdisc(string drivepath)
        {
            string psCommand = "(New-Object -comObject Shell.Application).Namespace(17).ParseName('" + drivepath+":" + "').InvokeVerb('Eject')";

            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = "powershell.exe",
                Arguments = "-NoProfile -ExecutionPolicy Bypass -WindowStyle Hidden -Command \"" + psCommand + "\"",

                CreateNoWindow = true,
                UseShellExecute = false
            };

            using (Process p = new Process())
            {
                p.StartInfo = psi;
                p.Start();
                p.WaitForExit();
            }
        }
    }
}
