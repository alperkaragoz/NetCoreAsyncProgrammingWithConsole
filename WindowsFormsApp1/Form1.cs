using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public static int Counter { get; set; } = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            var firstTask = Go(progressBar1);
            var secondTask = Go(progressBar2);

            await Task.WhenAll(firstTask, secondTask);
        }

        public async Task Go(ProgressBar pb)
        {
            // Ayrı threadler üzerinden çalışacaktır.
            await Task.Run(() =>
            {
                Enumerable.Range(1, 100).ToList().ForEach(i =>
                {
                    Thread.Sleep(100);
                    // Oluşturulmuş olduğu başka bir threadten bu ui elementine erişmeye çalıştığı için aşağıdaki kod hata verecektir.ui threadten erişilebilir fakat farklı bir threadten erişilemez.
                    //pb.Value = i;

                    // Invoke metodunu çağırdıktan sonra, ayrı bir thread üzerinden bu ui elementine erişim sağlayabiliriz.
                    pb.Invoke((MethodInvoker)delegate { pb.Value = i; });
                });
            });
        }

        private void btnCounter_Click(object sender, EventArgs e)
        {
            btnCounter.Text = Counter++.ToString();
        }
    }
}
