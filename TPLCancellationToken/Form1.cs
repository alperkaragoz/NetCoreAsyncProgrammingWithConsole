using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPLCancellationToken
{
    public partial class Form1 : Form
    {
        CancellationTokenSource cts;
        public int Counter { get; set; } = 0;

        public Form1()
        {
            InitializeComponent();
            cts = new CancellationTokenSource();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            // Get ettiğimizde ve sonrasında Cancel'a bastığımızda iptal edildi uyarısı alıyoruz.Sonrasında tekrar Get'e bastığımızda iptal edildi uyarısını almaya devam ediyoruz.Tekrar Get'e bastığımızda datayı get edebilmemiz için cts'i tekrar new'liyoruz.
            cts = new CancellationTokenSource();

            List<string> urls = new List<string>()
            {
                "https://www.google.com",
                "https://www.microsoft.com",
                "https://www.amazon.com",
                "https://www.google.com",
                "https://www.microsoft.com",
                "https://www.amazon.com",
                "https://www.google.com",
                "https://www.microsoft.com",
                "https://www.amazon.com",
                "https://www.google.com",
                "https://www.microsoft.com",
                "https://www.amazon.com",
                "https://www.google.com",
                "https://www.microsoft.com",
                "https://www.amazon.com"
            };

            HttpClient client = new HttpClient();

            // Bir for veya foreach içerisine CancellationToken yerleştirebilmemiz için ParallelOptions classını kullanıyoruz.
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.CancellationToken = cts.Token;

            Task.Run(() =>
            {
                try
                {
                    Parallel.ForEach<string>(urls, parallelOptions, (url) =>
                    {
                        // Result bloklayan bir property'dir.
                        string content = client.GetStringAsync(url).Result;

                        // Farklı bir threadteyiz.Farklı thread'ten UI'a erişebilmemiz için Invoke metodunu kullanıyoruz.

                        string data = $"{url}: {content.Length}";

                        cts.Token.ThrowIfCancellationRequested();
                        listBox1.Invoke((MethodInvoker)delegate { listBox1.Items.Add(data); });
                    });
                }
                catch (OperationCanceledException oce)
                {
                    MessageBox.Show("Process Cancelled:" + oce.Message);
                }
                catch (Exception )
                {
                    MessageBox.Show("An error occured:");
                }
            });


        }

        private void btnIncrease_Click(object sender, EventArgs e)
        {
            btnIncrease.Text = Counter++.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cts.Cancel();
        }
    }
}
