using System;
using System.Collections.Generic;
using System.Text;

namespace armasdejuego
{
    class Controlador
    {

        public Controlador() { }

        public static void Lootear( Jugador jugador, Armas[] misarmas, Pociones[] mispotis) {
            Random rnd = new Random();
            Random rnd2 = new Random();
            int numeroa = rnd.Next(0, 2);
            int numeroa2 = rnd2.Next(0,10);
            string eleccion;
            int elecionpotis;
            //Si da 1 dropea un arma
            if (numeroa == 0)
            {
                Console.WriteLine("Has encontrado algo! :  " + misarmas[numeroa2].getnombre + " - " + misarmas[numeroa2].getDescription + "   Hace de " + misarmas[numeroa2].getdMinimo + " a " + misarmas[numeroa2].getdMaximo + "  Cambiar por el arma actual???  Y/N ");
                eleccion = Console.ReadLine();
                //Si eliges quearte con el arma sustituye la que ya tienes
                if (eleccion == "y" || eleccion == "Y") {
                    jugador.getarma1 = misarmas[numeroa2];
                }
            }
            //Si da 2 dropea una poti
            else if (numeroa == 1)
            {
                Console.WriteLine("Has encontrado algo! :  "+mispotis[numeroa2].getnombre + " - " + mispotis[numeroa2].getDescription + "  ¿¿Cojer el Objeto?? Y/N ");
                eleccion = Console.ReadLine();
                //La sustituyes por otra de tu inventario
                if (eleccion == "y" || eleccion == "Y")
                {
                    /*Console.WriteLine("¿¿Por que otro lo cambias?? \n 1.-" + jugador.getpociones[0].getDescription+ "\n 2.-" +jugador.getpociones[1].getDescription+ "\n 3.-" + jugador.getpociones[2].getDescription+ "\n 4.-" + jugador.getpociones[3].getDescription+ "\n 5.-" + jugador.getpociones[4].getDescription);
                    elecionpotis = Convert.ToInt32(Console.ReadLine());

                    jugador.getpociones[elecionpotis-1] = mispotis[numeroa2];*/
                    jugador.InsertarPoti(mispotis[numeroa2]);
                }
            }
          

        }


    }
}
