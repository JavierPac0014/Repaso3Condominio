using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Repaso3Condominio
{
    public partial class Form1 : Form
    {
        List<Propietario> propietarios = new List<Propietario>();
        List<Propiedad> propiedads = new List<Propiedad>();
        List<Datosdata> datosdatas = new List<Datosdata>(); 
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Propietario protemp = new Propietario();
            protemp.Dpi = Convert.ToInt32(txtDPI.Text);
            protemp.Nombre = txtNombre.Text;
            protemp.Apellido = txtApellido.Text;

            propietarios.Add(protemp);

            txtDPI.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDPI.Focus();

            if (File.Exists("propietarios.txt"))
            {
                FileStream stream = new FileStream("propietarios.txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter writer = new StreamWriter(stream);
                foreach (var p in propietarios)
                {
                    writer.WriteLine(p.Dpi);
                    writer.WriteLine(p.Nombre);
                    writer.WriteLine(p.Apellido);
                }
                writer.Close();
            }
        }
        private void leer()
        {
            { 
                FileStream stream = new FileStream("propietarios.txt", FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);
                while (reader.Peek() > -1)
                {
                    Propietario prtemp = new Propietario();
                    prtemp.Dpi = Convert.ToInt32(reader.ReadLine());
                    prtemp.Nombre = reader.ReadLine();
                    prtemp.Apellido = reader.ReadLine();
                    propietarios.Add(prtemp);
                }
                reader.Close();
            }

            {
                FileStream stream2 = new FileStream("propiedades.txt", FileMode.Open, FileAccess.Read);
                StreamReader reader2 = new StreamReader(stream2);

                while (reader2.Peek() > -1)
                {
                    Propiedad tempro = new Propiedad();
                    tempro.Nodecasa = Convert.ToInt32(reader2.ReadLine());
                    tempro.Dpi = Convert.ToInt32(reader2.ReadLine());
                    tempro.Cuota = Convert.ToInt32(reader2.ReadLine());
                    propiedads.Add(tempro);
                }
                reader2.Close();
            }
        }
       
        private void mostrar (bool ordenar = false)
        {
            datosdatas.Clear();
            /*for (int i = 0; i< propiedads.Count; i ++)
            {
                for(int j =0; j< propietarios.Count; j++)
                {
                    if (propiedads[i].Dpi == propietarios[j].Dpi)
                    {
                        Datosdata datosdata = new Datosdata();
                        datosdata.Nombre = propietarios[j].Nombre;
                        datosdata.Apellido = propietarios[j].Apellido;
                        datosdata.Nodecasa = propiedads[i].Nodecasa;
                        datosdata.Cuota = propiedads[i].Cuota;

                        datosdatas.Add(datosdata);
                    }
                }
            }
            if (ordenar)
                datosdatas = datosdatas.OrderBy(o => o.Cuota).ToList();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = datosdatas;
            dataGridView1.Refresh();*/

              foreach (var propiedad in propiedads)
              {
                  Propietario propietario = propietarios.Find(po => po.Dpi == propiedad.Dpi);

                  Datosdata datosdata = new Datosdata();

                  datosdata.Dpi = propietario.Dpi;
                  datosdata.Nombre = propietario.Nombre;
                  datosdata.Apellido = propietario.Apellido;
                  datosdata.Nodecasa = propiedad.Nodecasa;
                  datosdata.Cuota = propiedad.Cuota;
                  datosdatas.Add(datosdata);
              }
               if (ordenar)
                datosdatas = datosdatas.OrderBy(o => o.Cuota).ToList();

              dataGridView1.DataSource = null;
              dataGridView1.DataSource = datosdatas;
              dataGridView1.Refresh();

        }
        private void button3_Click(object sender, EventArgs e)
        {
    
            mostrar();
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Propiedad tempro = new Propiedad();
            tempro.Nodecasa = Convert.ToInt32(txtNoCasa.Text);
            tempro.Dpi = Convert.ToInt32(txtDPIpropiedad.Text);
            tempro.Cuota = Convert.ToInt32(txtCuota.Text);

            propiedads.Add(tempro);

            txtNoCasa.Text = "";
            txtDPIpropiedad.Text = "";
            txtCuota.Text = "";
            txtNoCasa.Focus();

            if (File.Exists("propiedades.txt"))
            {
                FileStream stream = new FileStream("propiedades.txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter writer = new StreamWriter(stream);
                foreach (var p in propiedads)
                {
                    writer.WriteLine(p.Nodecasa);
                    writer.WriteLine(p.Dpi);
                    writer.WriteLine(p.Cuota);
                }
                writer.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            leer();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //leer();
            mostrar(true);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var pro = datosdatas.GroupBy(d => d.Dpi);

            int maxi = 0;
            int posi = 0;

            for (int i = 0; i< pro.Count(); i++)
            {
                if(pro.ToList()[i].Count() > maxi)
                {
                    maxi = pro.ToList()[i].Count();
                    posi = i;
                }
            }
            lbDPIpropietario.Text = "DPI: " + pro.ToList()[posi].Key;
            lbMontoPropietario.Text = "Tiene: " + maxi.ToString() + " Propiedades";

        }

        private void button6_Click(object sender, EventArgs e)
        {
            mostrar(true);
            int cont = datosdatas.Count();
            lbBajas.Text = "mas bajas " + datosdatas[0].Cuota.ToString() + "," + datosdatas[1].Cuota.ToString() + "," + datosdatas[2].Cuota.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var ag = datosdatas.GroupBy(d => d.Dpi);

            double maxicuota = 0;
            string mdpi = "";

            foreach(var grupo in ag)
            {
                double sumacuota = 0;
                string dpi = "";
                foreach(var p in grupo)
                {
                    sumacuota += p.Cuota;
                    dpi = p.Dpi;
                }
                if(sumacuota > maxicuota)
                {
                    maxicuota = sumacuota;
                    mdpi = dpi; 
                }
            }
            lbDPIpropietario.Text = mdpi;
            lbCuota.Text = maxicuota.ToString();
        }
    }
}
