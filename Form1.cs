using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tsp;
using System.IO;
using GezginAliciProblem.Properties;
using System.Xml;
using System.Xml.Serialization;

namespace GezginAliciProblem
{
    public partial class Form1 : Form
    {
        public int mouseX, mouseY;
        public int p;
        public Form1()
        {
            InitializeComponent();
        }

        /* private void button1_Click(object sender, EventArgs e)
         {
             int x = checkBox1.Location.X;
             int y = checkBox1.Location.Y;
             int a = checkBox2.Location.X;
             int b = checkBox2.Location.Y;
             double uz1 = Math.Sqrt((x ^ 2 + y ^ 2));
             double uz2 = Math.Sqrt((a ^ 2 + b ^ 2));
             double sonuc = Math.Abs((uz1 - uz2));


             label1.Text = sonuc.ToString();
             double s = Math.Abs(x - a);
             double s1 = Math.Abs(y - b);
             double sonuc1 = Math.Sqrt(Math.Pow(s, 2) + Math.Pow(s1, 2));
             s.ToString();
             s1.ToString();

             label2.Text = s + "   " + s1 + "  " + sonuc1;
         }*/

        
        private void Form1_Load(object sender, EventArgs e)
        {
            Location = new Point(250,50);
            int A = this.Size.Height;
            int B = this.Size.Width;

            Harita.Height = this.Size.Height-40;
            Harita.Width = this.Size.Width - 300;

            
           
           
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            DataSet cityDS = new DataSet();
            cityDS.ReadXml("C:\\Users\\Kadir\\Documents\\Visual Studio 2017\\Project\\GezginAliciProblem.UI\\GezginAliciProblem\\MarketList.xml"); // filename ile alınan xml dosyası okunur.
            DataRowCollection cities = cityDS.Tables[0].Rows;
            Rectangle recteski = new Rectangle(160,370,10,10);
            foreach (DataRow city in cities)
            {
                listBox1.Items.Add(city["X"]);
                listBox1.Items.Add(city["Y"]);
                mouseX = Convert.ToInt16(city["X"]);
                mouseY = Convert.ToInt16(city["Y"]);
                Graphics g;
                Pen whitePen = new Pen(Color.Blue, 2);
                
                Rectangle rect = new Rectangle(mouseX, mouseY, 10, 10);
                g = Harita.CreateGraphics();
                g.DrawEllipse(whitePen, rect);
                

                Harita.CreateGraphics();
                
                g.DrawLine(Pens.DarkRed, rect.Location,recteski.Location);

                int x1 = rect.X;
                int y1 = rect.Y;
                int x2 = recteski.X;
                int y2 = recteski.Y;
                double s = (x2 - x1);
                double s1 = (y2 - y1);
                double sonuc1 = Math.Sqrt(Math.Pow(s, 2) + Math.Pow(s1, 2));
                listBox2.Items.Add(sonuc1);
                recteski = rect;

            }

            

        }

    

        public void OpenCityList(string fileName) // XML dosyasını açma metodu
        {
            DataSet cityDS = new DataSet(); // dataset nesnemizi oluşturuyoruz.
            try
            {   
               
                cityDS.ReadXml(fileName); // filename ile alınan xml dosyası okunur.
                DataRowCollection MarketList = cityDS.Tables[0].Rows;
                foreach (DataRow city in MarketList)
                {
                    MessageBox.Show(city.ToString());

                    //this.Add(new City(Convert.ToInt32(city["X"], CultureInfo.CurrentCulture), Convert.ToInt32(city["Y"], CultureInfo.CurrentCulture)));
                } //datarowcollections nesnesi ile picture nesnesi içerisine şehirler yerleştirilir.
            }
            finally
            {
                cityDS.Dispose();
            }
        }
            private void Harita_MouseClick(object sender, MouseEventArgs e)
        {
           
            if (checkBox3.Checked == true)
            {
                mouseX = e.X;
                mouseY = e.Y;
                Graphics g;
                Pen whitePen = new Pen(Color.Blue, 2);

                Rectangle rect = new Rectangle(mouseX, mouseY, 10, 10);
                g = Harita.CreateGraphics();
                g.DrawEllipse(whitePen, rect);

                Harita.CreateGraphics();

                DosyaYaz(mouseX, mouseY);
               g.DrawLine(Pens.DarkBlue, checkBox1.Location, checkBox2.Location);
            }
            //else MessageBox.Show("checbox'a  tıkla");
           
            

            Point point = this.PointToClient(Cursor.Position);
            if (point.X >= 100 && point.X <= 110 && point.Y >= 100 && point.Y <= 110)
            {
            
            MessageBox.Show(point.ToString()+"  market 1");
            }
            //p++;//dire saymak için
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {

            /*System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();
            formGraphics.FillRectangle(myBrush, new Rectangle(e.X, e.Y, 200, 300));
            myBrush.Dispose();
            formGraphics.Dispose();*/
            /* CheckBox c = new CheckBox();

             c.Location = new Point(250,250);
             c.Location = new Point(e.X, e.Y);
             Controls.Add(c);*/
            //MessageBox.Show(p.ToString());   //daire saymak için

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //CheckBox c = new CheckBox();
            Graphics g;
            Pen whitePen = new Pen(Color.Blue, 2);

            Rectangle rect = new Rectangle(100, 100, 10, 10);
            g = Harita.CreateGraphics();
            //c.Location = new Point(100,100);
            //Harita.Controls.Add(c);
            
            g.DrawEllipse(whitePen, rect);


            Harita.CreateGraphics();
        }

        private static void DosyaYaz(int a,int b) {
            StreamWriter sw = File.AppendText("C:\\Users\\Kadir\\Desktop\\metinbelgesi1.txt");
            sw.Write(a + ";" + b+",");
            sw.Close();
            
            
            /*String Yol= @"C:\Users\Kadir\Desktop\metinbelgesi.txt";
            FileStream fs = new FileStream(Yol, FileMode.Open, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            //Yazma işlemi için bir StreamWriter nesnesi oluşturduk.
            sw.WriteLine(a+";"+b);
           // sw.WriteLine(b);
            //Dosyaya ekleyeceğimiz iki satırlık yazıyı WriteLine() metodu ile yazacağız.
            sw.Flush();
            //Veriyi tampon bölgeden dosyaya aktardık.
            sw.Close();
            fs.Close();*/

        }
        
    }
}
