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

namespace PLINQCancellationTokenSamples
{
    public partial class Form1 : Form
    {
        CancellationTokenSource cts;
        public Form1()
        {
            InitializeComponent();
            cts = new CancellationTokenSource();
        }

        private bool Calculate(int x)
        {
            // CPU'da verilen iterasyon kadar kod döndürür.(500 versek 500 kere dönen for döngüsü gibi düşünülebilir.)
            Thread.SpinWait(500);
            return x % 12 == 0;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // UI thread'te çalıştığı için UI threadi bloklanır.O yüzden ayrı bir threadte çalıştırmak için Run metodunu kullanıyoruz.
            Task.Run(() =>
            {
                try
                {
                    // Gelen her bir değerin 12'ye bölünüp bölünmediğini kontrol ediyoruz.
                    // Token iptal gördüğü anda eğer hesaplama bitmemişse duracak.
                    Enumerable.Range(1, 100000).AsParallel().WithCancellation(cts.Token).Where(x => Calculate(x)).ToList().ForEach(x =>
                    {
                        Thread.Sleep(100);
                        cts.Token.ThrowIfCancellationRequested();
                        //  list' e UI threadten erişebiliyoruz.Buradan erişebilmemiz için Invoke metodunu kullanıyoruz.
                        listBox1.Invoke((MethodInvoker)delegate { listBox1.Items.Add(x); });
                    });
                }
                catch(OperationCanceledException oex)
                {
                    MessageBox.Show($"Process cancelled!{ oex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"General Error: { ex.Message}");
                }
            });
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cts.Cancel();
        }
    }
}
