using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        /*
        Form 실행시 label1, label2의 시간을 출력해준다.
        Task.Run을 실행한다
        label1은 현재시간의 매 초가 25초가 될 때마다 refresh 해준다.
        label2는 실시간 시간을 1초마다 refresh 해준다.
        */

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("tt hh:mm:25");
            label2.Text = DateTime.Now.ToLongTimeString();
            Task.Run(() =>
            {
                for (int i = 0; i < 60; i++)
                {
                    textBox1.AppendText("tick " + i.ToString() + ", ");
                    Delay(1000);
                    label2.Text = DateTime.Now.ToLongTimeString();
                    if (DateTime.Now.ToString("ss") == "25")
                        label1.Text = DateTime.Now.ToLongTimeString();
                }
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private static DateTime Delay(int MS)
        {
            // Thread 와 Timer보다 효율적으로 사용
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }
            return DateTime.Now;
        }
    }
}
