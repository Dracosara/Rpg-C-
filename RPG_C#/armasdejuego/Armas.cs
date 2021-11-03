using System;
using System.Collections.Generic;
using System.Text;


namespace armasdejuego
{
    class Armas : Objetos
    {
        // declaración variables
        private int daño_maximo;
        private int daño_minimo;
        public int getdMaximo { get { return daño_maximo; } }
        public int getdMinimo { get { return daño_minimo; } }
        //constructor
        public Armas(int daño_minimo, int daño_maximo, string descripcion, int ID, int precio, string nombre)
            : base(ID, precio, nombre, descripcion)
        {
            this.daño_maximo = daño_maximo;
            this.daño_minimo = daño_minimo;
        }
        // define la cantidad de daño que se hará
        public int damage(int lv)
        {
            return new Random().Next(this.daño_minimo, this.daño_maximo + 1);
        }
        // usa el arma, hace daño al enemigo y lo escribe por pantalla
        public override void usar(ref Jugador j, ref Enemigos e)
        {
            int dmg = damage(j.getnivel);
            e.getvida = e.getvida - dmg;
            Console.Write("¡Has hecho " + dmg + "  de daño!");
            if (ID == 10)
            {
                j.getvida = j.getvida + dmg;
                Console.Write("Y te has curado " + dmg + " ps");
            }
            Console.WriteLine();
        }
    }
}