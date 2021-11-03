using System;
using System.Collections.Generic;
using System.Text;

namespace armasdejuego
{

    class Mapa
	{
		// declaración variables
		private int anchoMapa;
		private int largoMapa;
		private int[,] mapa;
		private int jugadorX = 0;
		private int jugadorY = 0;
		//constructor
		public Mapa()
		{
			anchoMapa = 12;
			largoMapa = 32;
			mapa = new int[anchoMapa, largoMapa];
		}

		private string strCelda(int x, int y)
        {
			if (jugadorX == x && jugadorY == y)
			{
				Console.ForegroundColor = ConsoleColor.Magenta;
				return "*";
			}//Jugador
			if (mapa[x, y] == 0) return "0"; Console.ForegroundColor = ConsoleColor.Green; //Muro
			if (mapa[x, y] == 1) return "-"; Console.ForegroundColor = ConsoleColor.Green;//Camino
			Console.ForegroundColor = ConsoleColor.Green;
			return "x";						 //Error
        }
		// pinta el mapa
		private void pintaMapa()
		{
			for (int x = 0; x < anchoMapa; x++)
            {
				for (int y = 0; y < largoMapa; y++)
				{
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write(strCelda(x, y));
				}
				Console.WriteLine();
			}
		}
		// detecta si has llegado al límite de la matriz
		private bool inIndex(int x,int y)
        {
			if (x >= anchoMapa || x < 0 || y >= largoMapa || y < 0) return false;
			return true;
		}
		// detecta si has llegado al límite en y del mapa y entonces crea un nuevo mapa
		private bool inIndexY( int y, Jugador jugador)
		{
			if (y >= largoMapa || y < 0) { CrearMapa(jugador); return false; }
			
			return true;
		}
		// hace que el jugador se mueva por el mapa
		private bool mueveJugador(int dx, int dy, Jugador jugador)
        {
			int nX = jugadorX + dx;
			int nY = jugadorY + dy;
			if (!inIndexY(nY, jugador)) {  return false; }
			if (!inIndex(nX ,nY)) return false;
			if (mapa[nX, nY] == 0) return false;
			if (mapa[nX, nY] == 1)
            {
				jugadorX = nX;
				jugadorY = nY;
				return true;
            }
			return false;
		}
		// aquí sucede todo el juego, se detectan las teclas con las que se mueve y pinta el mapa cada vez que el personaje se mueve
		public void Juego(Enemigos[] misenemigos, ref Jugador jugador, Armas[] misarmas, Pociones[] mispotis)
		{

			Console.ForegroundColor = ConsoleColor.Green;
			int getdinero = jugador.getdinero;
			ConsoleKeyInfo movimiento;
			bool salir = false;
			int probabilidad = 60;
			Random rnd = new Random();
			int aleatorio;
			bool rePinta = true;
			do
			{
				aleatorio = rnd.Next(0, 150);
				// en caso de que el jugador muera el juego terminará
				if (jugador.getvida <= 0) { salir = true; } 
				else {
					if (rePinta)
					{
						Console.Clear();
						pintaMapa();
						rePinta = false;
					}
					else Console.SetCursorPosition(0, Console.CursorTop);

					// con esto el jugador se moverá usando w, a, s, d o escape en caso de que quiera terminar el juego
					movimiento = Console.ReadKey();
					if (jugador.getxp >= 150) { ++jugador.getnivel; jugador.getxp = 0;}
					if (movimiento.Key == ConsoleKey.W) rePinta = mueveJugador(-1, 0,jugador);
					if (movimiento.Key == ConsoleKey.S) rePinta = mueveJugador(+1, 0, jugador);
					if (movimiento.Key == ConsoleKey.A) rePinta = mueveJugador(0, -1, jugador);
					if (movimiento.Key == ConsoleKey.D) rePinta = mueveJugador(0, +1, jugador); 
					if (movimiento.Key == ConsoleKey.Escape )
					{
						rePinta = true;
						salir = true;
					}
					if (rePinta == false) continue;
					

						Console.WriteLine();

					if (probabilidad < 90) { probabilidad += 10; }
					// esto hace que con cada paso que das tengas un 10% de probabilidades de encontrar una tiena, un objeto o un monstruo, además con cada paso las probabilidades de que te aparezca un monstruo suben un 10%
					
					if (aleatorio >= 50 && aleatorio <= probabilidad)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Program.Combate(misenemigos, ref jugador, misarmas, mispotis); probabilidad = 60;
					}
					else if (aleatorio < 10 ) {
						Console.ForegroundColor = ConsoleColor.Cyan;
						Controlador.Lootear(jugador, misarmas, mispotis);
					}
					if (aleatorio < 20 && aleatorio >= 10 )
					{
						Console.ForegroundColor = ConsoleColor.White;
						Program.Crear_Tienda(getdinero, misarmas, mispotis, jugador);
					}

					Console.ForegroundColor = ConsoleColor.Green;
				}
			} while (salir != true);
		}
		public void CrearMapa(Jugador jugador)
		{
			// aquí se crea el mapa, que tendrá dos posibles caminos
			Random rnd = new Random();
			int iAux = 0;
			int jAux = 0;
			int aleatorio = rnd.Next(2, anchoMapa - 2);
			// rellena todo con 0
			for (int i = 0; i < anchoMapa; i++)
			{
				for (int j = 0; j < largoMapa; j++)
				{
					mapa[i, j] = 0;
				}
			}
			// crea un primer camino
			for (int i = 0; i < anchoMapa; i++)
			{
				for (int j = 0; j < largoMapa; j++)
				{

					int randomPatron = rnd.Next(0, 10);
					try
					{

						if (aleatorio == i && j == 0) { mapa[i, j] = 1; jugadorX = i; jugadorY = j; }
						else if (j == 1 && mapa[i, j - 1] == 1)
						{ mapa[i, j] = 1; mapa[i + 1, j] = 1; mapa[i - 1, j] = 1; jAux = j; iAux = i - 1; }
						else if (mapa[i, j] == 1)
						{
							if (randomPatron <= 1)
							{
								if (mapa[i + 1, j] != 1) mapa[i + 1, j] = 1;
							}
							if (randomPatron > 1) { mapa[i, j + 1] = 1; }
						}
					}
					catch (IndexOutOfRangeException)
					{
						try
						{
							mapa[i, j + 1] = 1;
						}
						catch (IndexOutOfRangeException) { }
					}

				}
			}
			// crea un segundo camino
			do
			{
				try
				{

					int randomPatron = rnd.Next(0, 8);
					if (randomPatron > 2) { mapa[iAux, jAux + 1] = 1; }
					mapa[iAux, jAux] = 1;
					if (randomPatron <= 2)
					{
						if (mapa[iAux - 1, jAux] != 1) mapa[iAux - 1, jAux] = 1;
						iAux--;
					}
					jAux++;

				}
				catch (IndexOutOfRangeException)
				{
					try
					{
						iAux++;
						mapa[iAux, jAux] = 1;
					}
					catch (IndexOutOfRangeException) { }
				}
			} while (jAux < largoMapa - 1);
			mapa[iAux, jAux] = 1;
		}
	}
}
