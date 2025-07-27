using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

namespace Dögüler_Timer_Progress_bar
{
    public partial class Form1 : Form
    {
        #region Degişkenşler
        int pandisyanyaTime = 0; // Pandistanya saniye cinsinden geçen süre
        int cremaTime = 0; // Krema saniye cinsinden geçen süre
        bool crematimestart = false, pandispanyastrat = false;
        int coverTime = 0;
        bool covertimestar = false;
        bool moneystatus;
        bool ready = false;
        int money, value, colortime;
        bool[] pandispanyaAllmaterials = new bool[7];
        bool[] cremaAllMaterials = new bool[6];
        bool[] coverAllMaterials = new bool[3];
        private string originalText;
        private Label targetLabel;
        #endregion
        public Form1()
        {
            InitializeComponent();
        }

        #region Label_Clikler
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
        #endregion



        #region Timerler&Form_1Load
        private Timer moneyWatcher;
        private Timer materialsWatcher;
        private Timer colourtimer;
        private void Form1_Load(object sender, EventArgs e)
        {
            moneyWatcher = new Timer();
            moneyWatcher.Interval = 100;
            moneyWatcher.Tick += MoneyWatcher_Tick;
            moneyWatcher.Start();
            colourtimer = new Timer();
            colourtimer.Interval = 1000;
            colourtimer.Tick += ColourTimer_Tick;
        }
        private void ColourTimer_Tick(Object sender, EventArgs e)
        {
            if (ready)
            {
                colortime++;
                if (colortime % 2 == 0 && colortime != 10)
                {
                    targetLabel.BackColor = Color.Red;
                }
                else if (colortime % 2 == 1)
                {
                    targetLabel.BackColor = Color.White;
                }

                if (colortime == 10)
                {
                    targetLabel.BackColor = Color.Brown;
                    targetLabel.Text = originalText;
                    colourtimer.Stop();
                }
            }
        }
        private void MoneyWatcher_Tick(object sender, EventArgs e)
        {
            if (int.TryParse(label5.Text, out money))
            {
                bool hasMoney = money > 0;

                moneystatus = hasMoney;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pandispanyastrat)
            {
                pandisyanyaTime++;
                progressBar2.Value += 10;
                if (pandisyanyaTime >= 10)
                {
                    timer1.Stop();
                    ButtonVisibleEnable(label18, label43, label44, button1, button2, button16, button17);
                    button2.Enabled = true;
                    MessageBox.Show("Pandispanya Bitti.");
                    progressBar1.Value += 40;
                    pandispanyastrat = false;
                    progressBar2.Value = 0;
                }
            }
            if (crematimestart)
            {
                cremaTime++;
                progressBar2.Value += 10;
                if (cremaTime >= 10)
                {
                    timer1.Stop();
                    ButtonVisibleEnable(label2, label43, label44, button1, button2, button16, button17);
                    button16.Enabled = true;
                    MessageBox.Show("Kreama Bitti.");
                    progressBar1.Value += 40;
                    crematimestart = false;
                    progressBar2.Value = 0;
                }

            }
            if (covertimestar)
            {
                coverTime++;
                progressBar2.Value += 10;
                if (coverTime >= 10)
                {
                    timer1.Stop();
                    ButtonVisibleEnable(label2, label18, label44, button1, button2, button16, button17);
                    button17.Enabled = true;
                    MessageBox.Show("Dış Kaplama Bitti.");
                    progressBar1.Value += 20;
                    covertimestar = false;
                    progressBar2.Value = 0;
                }

            }
        }
        #endregion



        #region Metotlar
        private void ColorChange (Label label, string write)
        {
            if (ready)
            {
                targetLabel = label;
                originalText = label.Text;
                targetLabel.BackColor = Color.Red;
                colortime = 0;

                label.Text = write;
                colourtimer.Start();
            }

        }
        private void MaterialsWatcher(Label label, bool[] series ,int amount, int number)
        {
            int currentvalue = Convert.ToInt32(label.Text);
            if(currentvalue < amount)
            {
                series[number] = false;
            }
            else if(currentvalue >= amount)
            {
                series[number] = true;
            }
        }
        private void SeriesClenar(bool[] series)
        {
            for (int i = 0; i < series.Length; i++)
            {
                series[i] = false;
            }
        }
        private void DecreaseMoneyValue(Label label, int amount)
        {
            int currentvalue = Convert.ToInt32(label.Text);
            currentvalue -= amount;
            label.Text = currentvalue.ToString();
        }
        private void İncreaseMoneyValue(Label label, int amount)
        {
            int currentvalue = Convert.ToInt32(label.Text);
            currentvalue += amount;
            label.Text = currentvalue.ToString();
        }
        private void DecreaseLabelValue(Label label, int amount)
        {
            int currentValue = Convert.ToInt32(label.Text);
            currentValue -= amount;
            label.Text = currentValue.ToString();
        }
        private void İncreaseLabelValue(Label label, int amount)
        {
            int currentValue = Convert.ToInt32(label.Text);
            currentValue += amount;
            label.Text = currentValue.ToString();
        }
        private void ButtonVisibleDisable(Label label, Label label1, Label label2,Button button, Button button1, Button button2, Button button3)
        {
            label.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            button.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
        }
        private void ButtonVisibleEnable(Label label, Label label1, Label label2, Button button, Button button1, Button button2, Button button3)
        {
            label.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            button.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
        }



        #endregion



        #region Ana Butonlar
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Kullanılanlar\n5 Yumurta\n1 Su Bardağı Şeker\n1 Su Bardağı Un\n1 Tutam Tuz \n1 Paket Vanilya\n1 Paket Kabartma Tozu\n1 Su Bardağı Su.");
            MaterialsWatcher(label8,pandispanyaAllmaterials, 5 , 0);
            MaterialsWatcher(label13, pandispanyaAllmaterials, 1, 1);
            MaterialsWatcher(label14,pandispanyaAllmaterials, 1, 2);
            MaterialsWatcher(label15,pandispanyaAllmaterials, 1, 3);
            MaterialsWatcher(label17,pandispanyaAllmaterials, 1, 4);
            MaterialsWatcher(label25,pandispanyaAllmaterials, 1, 5);
            MaterialsWatcher(label16, pandispanyaAllmaterials, 1, 6);
            ready = pandispanyaAllmaterials.All(f => f);
            SeriesClenar(pandispanyaAllmaterials);
            ColorChange(label2, "PandisPanya Hazırlanıyor...");
            if (ready)
            {
                timer1.Interval = 1000;
                pandisyanyaTime = 0;
                pandispanyastrat = true;
                timer1.Start();
                button1.Enabled = false;
                ButtonVisibleDisable(label18, label43, label44, button1, button2, button16, button17);
                DecreaseLabelValue(label8, 5);    // Yumurtalar
                DecreaseLabelValue(label13, 1);   // Şeker
                DecreaseLabelValue(label14, 1);   // Tuz
                DecreaseLabelValue(label15, 1);   // Vanilya
                DecreaseLabelValue(label17, 1);   // Su
                DecreaseLabelValue(label25, 1);   // Un
                DecreaseLabelValue(label16, 1);   // Kabartma Tozu
            }
            else
            {
                MessageBox.Show("Yeterli Malzeme Yok");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Kullanılanlar\n4 Bardak Süt\n3 Yemek Kaşıgı Nişasta\n2 Yemek Kaşıgı Un\n1 Su Bardagı Şeker\n1 Yemek Kaşıgı Tereyagı\n1 Paket Vanilya.");
            MaterialsWatcher(label23, cremaAllMaterials, 4, 0);
            MaterialsWatcher(label24, cremaAllMaterials, 3, 1);
            MaterialsWatcher(label25, cremaAllMaterials, 2, 2);
            MaterialsWatcher(label13, cremaAllMaterials, 1, 3);
            MaterialsWatcher(label26, cremaAllMaterials, 1, 4);
            MaterialsWatcher(label15, cremaAllMaterials, 1, 5);
            ready = cremaAllMaterials.All(f => f);
            ColorChange(label18, "Krema Hazırlanıyor...");
            SeriesClenar(cremaAllMaterials);
            
            if (ready)
            {
                timer1.Interval = 1000;
                cremaTime = 0;
                crematimestart = true;
                timer1.Start();
                button2.Enabled = false;
                ButtonVisibleDisable(label2, label43, label44, button1, button2, button16, button17);
                DecreaseLabelValue(label23, 4); //Süt
                DecreaseLabelValue(label24, 3); //Nişasta
                DecreaseLabelValue(label25, 2); //Un
                DecreaseLabelValue(label13, 1); //Şeker
                DecreaseLabelValue(label26, 1); //Tereyagı
                DecreaseLabelValue(label15, 1); //Vanilya
            }
            else
            {
                MessageBox.Show("Yeterli Materyal Yok");
            }
        }
        
        private void button16_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Kullanılanlar\n1 Paket Krem Şanti\n1 Çay Bardağı Süt\n1 Adet Kırmızı Gıda Boyası.");
            MaterialsWatcher(label39, coverAllMaterials, 1, 0);
            MaterialsWatcher(label40, coverAllMaterials, 1, 1);
            MaterialsWatcher(label23, coverAllMaterials, 1, 2);
            ready = coverAllMaterials.All(f => f);
            ColorChange(label43, "Dış Kaplama Hazırlanıyor...");
            SeriesClenar(coverAllMaterials);
            if (ready)
            {
                timer1.Interval = 1000;
                coverTime = 0;
                covertimestar = true;
                timer1.Start();
                ButtonVisibleDisable(label2, label18, label44, button1, button2, button16, button17);
                button16.Enabled = false;
                DecreaseLabelValue(label39, 1);
                DecreaseLabelValue(label40, 1);
                DecreaseLabelValue(label23, 1);
            }
            else
            {
                MessageBox.Show("Yeterli Malzeme Yok");
            }
        }
        private void button17_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Pasta Bitmiştir");
            İncreaseLabelValue(label46, 1);
            progressBar1.Value = 0;
            button17.Enabled = false;
            button1.Enabled = true;
        }
        #endregion



        #region Satın Alım Butonları


        private void button3_Click(object sender, EventArgs e)
        {
            while (moneystatus)
            {
                İncreaseLabelValue(label8, 1);
                DecreaseMoneyValue(label5, 5);
                break;
            }
            if(money <5 && money >0)
            {
                DecreaseLabelValue(label8, 1);
                İncreaseMoneyValue(label5, 5);
                MessageBox.Show("Yeterli Para Yok");
            }
            else if(money == 0)
            {
                MessageBox.Show("Yeterli Para Yok");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            while (moneystatus)
            {
                İncreaseLabelValue(label13, 1);
                DecreaseMoneyValue(label5, 3);
                break;
            }
            if (money < 3 && money >0)
            {
                DecreaseLabelValue(label13, 1);
                İncreaseMoneyValue(label5, 3);
                MessageBox.Show("Yeterli Para Yok");
            }
            else if (money == 0)
            {
                MessageBox.Show("Yeterli Para Yok");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            while (moneystatus)
            {
                İncreaseLabelValue(label14, 1);
                DecreaseMoneyValue(label5, 1);
                break;
            }
            if (money < 1 && money >0)
            {
                DecreaseLabelValue(label14, 1);
                İncreaseMoneyValue(label5, 1);
                MessageBox.Show("Yeterli Para Yok");
            }
            else if (money == 0)
            {
                MessageBox.Show("Yeterli Para Yok");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            while (moneystatus)
            {
                İncreaseLabelValue(label15, 1);
                DecreaseMoneyValue(label5, 4);
                break;
            }
            if (money < 4 && money >0)
            {
                DecreaseLabelValue(label15, 1);
                İncreaseMoneyValue(label5, 4);
                MessageBox.Show("Yeterli Para Yok");
            }
            else if (money == 0)
            {
                MessageBox.Show("Yeterli Para Yok");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            while (moneystatus)
            {
                İncreaseLabelValue(label16, 1);
                DecreaseMoneyValue(label5, 4);
                break;
            }
            if (money < 4 && money >0)
            {
                DecreaseLabelValue(label16, 1);
                İncreaseMoneyValue(label5, 4);
                MessageBox.Show("Yeterli Para Yok");
            }
            else if (money == 0)
            {
                MessageBox.Show("Yeterli Para Yok");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            while (moneystatus)
            {
                İncreaseLabelValue(label17, 1);
                DecreaseMoneyValue(label5, 1);
                break;
            }
            if (money < 1 && money >0 )
            {
                DecreaseLabelValue(label17, 1);
                İncreaseMoneyValue(label5, 1);
                MessageBox.Show("Yeterli Para Yok");
            }
            else if (money == 0)
            {
                MessageBox.Show("Yeterli Para Yok");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            while (moneystatus)
            {
                İncreaseLabelValue(label23, 1);
                DecreaseMoneyValue(label5, 3);
                break;
            }
            if (money < 3 && money >0 )
            {
                DecreaseLabelValue(label23, 1);
                İncreaseMoneyValue(label5, 3);
                MessageBox.Show("Yeterli Para Yok");
            }
            else if (money == 0)
            {
                MessageBox.Show("Yeterli Para Yok");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            while (moneystatus)
            {
                İncreaseLabelValue(label24, 1);
                DecreaseMoneyValue(label5, 2);
                break;
            }
            if (money < 2 && money >0 )
            {
                DecreaseLabelValue(label24, 1);
                İncreaseMoneyValue(label5, 2);
                MessageBox.Show("Yeterli Para Yok");
            }
            else if (money == 0)
            {
                MessageBox.Show("Yeterli Para Yok");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            while (moneystatus)
            {
                İncreaseLabelValue(label25, 1);
                DecreaseMoneyValue(label5, 1);
                break;
            }
            if (money < 1 && money >0 )
            {
                DecreaseLabelValue(label25, 1);
                İncreaseMoneyValue(label5, 1);
                MessageBox.Show("Yeterli Para Yok");
            }
            else if (money == 0)
            {
                MessageBox.Show("Yeterli Para Yok");
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Gerekli Malzemeler\n5 Yumurta\n1 Su Bardağı Şeker\n1 Su Bardağı Un\n1 Tutam Tuz \n1 Paket Vanilya\n1 Paket Kabartma Tozu\n1 Su Bardağı Su.");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Gerekli Malzemeler\n4 Bardak Süt\n3 Yemek Kaşıgı Nişasta\n2 Yemek Kaşıgı Un\n1 Su Bardagı Şeker\n1 Yemek Kaşıgı Tereyagı\n1 Paket Vanilya.");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Gerekli Malzemeler\n1 Paket Krem Şanti\n1 Çay Bardağı Süt\n1 Adet Kırmızı Gıda Boyası.");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            while (moneystatus)
            {
                İncreaseLabelValue(label26, 1);
                DecreaseMoneyValue(label5, 1);
                break;
            }
            if (money < 1 && money >0)
            {
                DecreaseLabelValue(label26, 1);
                İncreaseMoneyValue(label5, 1);
                MessageBox.Show("Yeterli Para Yok");
            }
            else if (money == 0)
            {
                MessageBox.Show("Yeterli Para Yok");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            while (moneystatus)
            {
                İncreaseLabelValue(label39, 1);
                DecreaseMoneyValue(label5, 5);
                break;
            }
            if (money < 5 && money >0)
            {
                DecreaseLabelValue(label39, 1);
                İncreaseMoneyValue(label5, 5);
                MessageBox.Show("Yeterli Para Yok");
            }
            else if (money == 0)
            {
                MessageBox.Show("Yeterli Para Yok");
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            while (moneystatus)
            {
                İncreaseLabelValue(label40, 1);
                DecreaseMoneyValue(label5, 3);
                break;
            }
            if (money < 3 && money >0)
            {
                DecreaseLabelValue(label40, 1);
                İncreaseMoneyValue(label5, 3);
                MessageBox.Show("Yeterli Para Yok");
            }
            else if (money == 0)
            {
                MessageBox.Show("Yeterli Para Yok");
            }
        }
        #endregion
    }
}
