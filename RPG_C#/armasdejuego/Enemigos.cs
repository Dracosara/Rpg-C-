using System;
using System.Collections.Generic;
using System.Text;

namespace armasdejuego
{
    class Enemigos : Personaje
    {
        // declaración variables
        public string getnombre { get { return nombre; } }
        private int id;
        private int daño;
        public int getdaño { set { daño = value; } get { return daño; } }

        //constructor
        public Enemigos(int Id, int vida, int Daño, string nombre, int xp, int dinero) : base(nombre, dinero, xp, vida)
        {

            this.id = Id;
            this.daño = Daño;

        }
        // quita al jugador tanta vida como daño haga al atacar y lo saca por pantalla
        public void atacaJugador(ref Jugador j){
            j.getvida = j.getvida - this.daño;
            Console.WriteLine(" AUCH!!! Te han hecho " + this.daño + " de daño");
            Console.WriteLine("");
        }
    }
}
