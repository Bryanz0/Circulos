using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    //NOTA: como el la funcion refresh no logra captar de manera rapida el movimiento se debe mover despacio el circulo interno

    public partial class Form1 : Form
    {

        static int radioInt = 25;
        int radioExt = 100;
        //coordenadas de donde se dibujara el circulo interno x=375,y=225
        //En este caso debemos poner el diametro de nuestro circulo, dado que la figura sera tratada como si estuviera dibujada dentro de un rectangulo
        Rectangle rect = new Rectangle(375, 225, radioInt*2, radioInt*2);
        //variable que nos indicara si se hace clic 
        bool isMouseDown = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //lapiz para dibujar los circulos
            Pen lapiz1 = new Pen(Color.Blue, 5);
           
            
            //coordenadas del circulo externo x=300,y=150
            Graphics circulo1 = this.CreateGraphics();
            //debemos especificar el diametro, no el radio
            circulo1.DrawEllipse(lapiz1, 300, 150, radioExt*2, radioExt*2);
            e.Graphics.FillEllipse(new SolidBrush(Color.RoyalBlue), rect);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //789, 536
           

            if (isMouseDown == true)
            {
                //obtenemos las coordenadas del mouse y las seteamos al rectangulo
                rect.Location = e.Location;
                // al tratarse la figura como un rectangulo, el cursor la movera desde la esquina superior izquierda
                // por lo que deberemos transformar las coordenadas para que el circulo se mueva desde el centro
                // le restaremos el radio interno a cada una de las coordenadas X y Y
           
                rect.X = e.Location.X - radioInt;
                rect.Y = e.Location.Y - radioInt;

                //Debido a que estamos trabajando con las coordenadas del sistema, vamos a transformar a coordenadas
                //que inicien desde el centro de los circulos para que sea mas facil de entender
                //Transformacion de coordenadas
                int xExt = 0;
                int yExt = 0;
                int xInt = 0;
                int yInt = 0;
                xInt = Math.Abs(rect.X - 375);
                yInt = Math.Abs(rect.Y - 225);

                //para saber si esta colisionando con el circulo exterior vamos a trabajar con la distancia
                double distancia = Math.Sqrt(Math.Pow(yExt - yInt, 2) + Math.Pow(xInt - xExt, 2));
                int d = Convert.ToInt32(distancia);
                
                if ((radioExt - radioInt) == d)
                    isMouseDown = false;
                
                //Esta sera la funcion que nos permitira captar cada momento que se mueva el puntero sus coordenadas
                //para que el ciruculo puede moverse junto con el cursor
                Refresh();
            }
        }


    }
//Autor: Bryan Zambrano
}
