## Task Queue 활용 기초 응용프로그램

- Timer 스레드를 사용하지 않고 Task의 Async Await 비동기 처리 방식을 이해하여 프로그램을 작성

![Animation](https://user-images.githubusercontent.com/18677603/124706740-bdc38b80-df32-11eb-96d0-63430357c993.gif)


```c#
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private readonly char[] arr1 = "1234567890".ToCharArray();
        private readonly char[] arr2 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private readonly char[] arr3 = "가나다라마바사아자차카타파하".ToCharArray();

        private Queue<Task> queue = new Queue<Task>();
        private bool isProcess = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void ExecTask()
        {
            Task.Run(async () => {
                while (!isProcess && queue.Count > 0)
                {
                    isProcess = true;
                    Task task = queue.Dequeue();
                    task.Start();
                    task.Wait();
                    isProcess = false;
                }
                await Task.Delay(3000);
            });
        }

        private void PrintText(char[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                this.Invoke(new Action(() => { textBox1.Text += $"{arr[i]} "; }));
                Thread.Sleep(50);
            }
            this.Invoke(new Action(() => { textBox1.Text += Environment.NewLine; }));
        }

        private void button1_Click(object sender, EventArgs e) //숫자 출력 0~10
        {
            Task task = new Task(() => PrintText(arr1));
            queue.Enqueue(task);
            ExecTask();
            textBox2.Text += "숫자 Task Add" + Environment.NewLine;
        }

        private void button2_Click(object sender, EventArgs e) //알파벳 출력 0~26
        {
            Task task = new Task(() => PrintText(arr2));
            queue.Enqueue(task);
            ExecTask();
            textBox2.Text += "영어 Task Add" + Environment.NewLine;
        }

        private void button3_Click(object sender, EventArgs e) //한글 출력 0~14
        {
            Task task = new Task(() => PrintText(arr3));
            queue.Enqueue(task);
            ExecTask();
            textBox2.Text += "한글 Task Add" + Environment.NewLine;
        }
    }
}
```
