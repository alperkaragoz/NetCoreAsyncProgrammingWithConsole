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

namespace WinFormTaskCancellationToken
{
    public partial class Form1 : Form
    {
        //CancellationToken üreteceğimiz source oluşturuyoruz.
        CancellationTokenSource ct = new CancellationTokenSource();

        public Form1()
        {
            InitializeComponent();
        }

        // Asenkron metodun token alabilmesi için mutlaka ctor'unda overload'un olması gerekiyor.Her asenkron metodun token parametresi olmayabilir.
        private async void btnStart_Click(object sender, EventArgs e)
        {
            Task<HttpResponseMessage> responseTask;
            try
            {

                // GetAsync metodunun overloadlarında CancellationToken alan bir overload var.GetStringAsyc overloadlarında CancellationToken bulunmuyor.
                // Debug yaparken Watch alanında var task i gözlemlediğimizdeki propertylerinde Status=Canceled olduğunu görüyoruz.Diğer propertylerinde IsCanceled=true olarak gözüküyor.
                var task = new HttpClient().GetAsync("https://localhost:7215/api/home", ct.Token);

                await task;

                var content = await task.Result.Content.ReadAsStringAsync();

                rtbShow.Text = content;
            }
            catch (TaskCanceledException tex)
            {
                MessageBox.Show(tex.Message);
            }

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            // Async işlemi iptal eder ve hata fırlatır.(TaskCanceledException exception tipinden)
            ct.Cancel();
        }
    }
}
