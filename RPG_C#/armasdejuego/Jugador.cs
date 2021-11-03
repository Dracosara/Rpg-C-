using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace armasdejuego
{
    class Jugador : Personaje
    {
        private int nivel;
        public int getnivel { set { nivel = value; } get { return nivel; } }
        private Pociones[] pociones = new Pociones[5];
        private Queue Potis = new Queue();
        public Pociones[] getpociones { set { pociones = value; } get { return pociones; } }

        private Armas arma1;
        public Armas getarma1 { set { arma1 = value; } get { return arma1; } }


        public Jugador(string nombre) : base(nombre, 1000, 0, 150)
        {
            this.nivel = 1;
            this.arma1 = new Armas(1, 2, "Básica, pero puede valer", 0, 10, " Bastón ");


            pociones[0] = new Pociones(" Te cura 20 ps", 13, 75, "Poción de vida mediana"); ;
            pociones[1] = new Pociones(" Hace 20 de daño al enemigo  ", 15, 150, "Poción de daño mediana");
            pociones[2] = new Pociones(" No tienes nada ", 11, 0, "Hueco vacío");
            pociones[3] = new Pociones(" No tienes nada ", 11, 0, "Hueco vacío");
            pociones[4] = new Pociones(" No tienes nada ", 11, 0, "Hueco vacío");

        }
        public void writePociones()
        {
            for (int k = 0; k < 5; k++)
            {
                Console.WriteLine((k + 1) + ".- " + pociones[k].getDescription);
            }
            Console.WriteLine("6.- Atras");
        }
        public void InsertarPoti(Pociones Poti)
        {
            this.Potis.Enqueue(Poti);
        }

        public Pociones descartarPoti(Pociones poti)
        {
            try
            {
                poti = (Pociones)Potis.Dequeue();
                Console.WriteLine("Has usado " + poti.getnombre);
                return poti;
            }
            catch 
            {
                Console.WriteLine("No tienes objetos");
                Pociones potis = new Pociones("",0,0,"");
                return potis;
            }
        }
    }
}
