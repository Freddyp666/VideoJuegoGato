using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoJuegoGato
{
    internal class Program
    {
        //Se crea un arreglo bidemensional para representar el tablero del juego del gato
        static int[,] tablero = new int[3, 3];

        //Creamos un arreglo para los simbolos de los jugadores, en este caso X y O
        static char[] simbolos = { ' ', 'X', 'O' };

        static void Main(string[] args)
        {
            //Variable para preguntar si el usuario quiere jugar de nuevo
            bool jugarDeNuevo;

            do
            {
                // Reiniciar tablero
                tablero = new int[3, 3];

                //Variable para verificar si el juego termino o no
                bool terminado = false;

                //Llamamos a la función para dibujar el tablero
                DibujarTablero();

                //Ciclo principal del juego, se ejecuta mientras el juego no haya terminado
                do
                {
                    //turno del jugador 1
                    JugadorPosicion(1);//envia el valor de uno

                    //Dibujamos el tablero después de que el jugador 1 haga su movimiento
                    DibujarTablero();

                    //Verificamos si el jugador 1 ha ganado
                    terminado = ComprobarGanador();
                    if (terminado == true)
                    {
                        Console.WriteLine("¡Jugador 1 gana!");

                    }
                    else
                    {

                        terminado = ComprobarEmpate();
                        if (terminado == true)
                        {
                            Console.WriteLine("¡Empate!");
                        }

                        //Si el jugador uno no gano, ni hubi empate, entonces es el turno del jugador 2
                        else
                        {

                            //Turno del jugador 2
                            JugadorPosicion(2);//envia el valor de dos

                            //Dibujamos el tablero después de que el jugador 2 haga su movimiento
                            DibujarTablero();

                            //Verificamos si el jugador 2 ha ganado
                            terminado = ComprobarGanador();

                            if (terminado == true)
                            {
                                Console.WriteLine("¡Jugador 2 gana!");
                            }

                        }
                    }



                } while (terminado == false);//Se ejecuta mientras el juego no haya terminado. Repite hasta 3 en linea o empate(Tablero lleno)

                // Preguntar si quiere volver a jugar
                Console.Write("\n¿Jugar otra vez? (s/n): ");
                jugarDeNuevo = Console.ReadLine().ToLower() == "s";
            } while (jugarDeNuevo);

        }//FIN MAIN

        static void DibujarTablero()
        {

            Console.Clear();

            ////Variable de conteo
            int fila = (Console.WindowHeight / 2) - 5;
            int columna = (Console.WindowWidth / 2) - 10;

            // Título
            Console.SetCursorPosition(columna, fila - 4);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("TIC TAC TOE");
            Console.ResetColor();

            Console.SetCursorPosition(columna + 2, fila - 1);
            Console.WriteLine("1   2   3");
            // Parte superior
            Console.SetCursorPosition(columna, fila);
            Console.WriteLine("┌───┬───┬───┐");

            // Filas del tablero
            for (int i = 0; i < 3; i++)
            {
                // número de fila a la izquierda
                Console.SetCursorPosition(columna - 2, fila + 1 + (i * 2));
                Console.Write(i + 1);

                // tablero
                Console.SetCursorPosition(columna, fila + 1 + (i * 2));
                Console.Write("│");

                for (int j = 0; j < 3; j++)
                {
                    char simbolo = simbolos[tablero[i, j]];

                    if (simbolo == 'X') Console.ForegroundColor = ConsoleColor.Cyan;
                    else if (simbolo == 'O') Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.Write($" {simbolo} ");
                    Console.ResetColor();
                    Console.Write("│");
                }

                if (i < 2)
                {
                    Console.SetCursorPosition(columna, fila + 2 + (i * 2));
                    Console.WriteLine("├───┼───┼───┤");
                }
            }

            // Parte inferior
            Console.SetCursorPosition(columna, fila + 6);
            Console.WriteLine("└───┴───┴───┘");

            Console.SetCursorPosition(columna - 10, fila + 8);
            Console.WriteLine("Jugador 1 = X   |   Jugador 2 = O");


        }

        //Preguntar al usuario si desea jugar de nuevo
        static void JugadorPosicion(int jugador)
        {
            int fila, columna;
            do
            {
                Console.WriteLine();
                Console.WriteLine($"Turno del Jugador: {jugador}", jugador);

                //Pedimos el numero de la fila
                // Pedir fila
                Console.Write("Fila (1-3): ");
                while (!int.TryParse(Console.ReadLine(), out fila) || fila < 1 || fila > 3)
                {
                    Console.Write("⚠ Fila inválida. Ingresa (1-3): ");
                }

                //Pedimos el numero de la columna
                Console.Write(" | Columna (1-3): ");
                while (!int.TryParse(Console.ReadLine(), out columna) || columna < 1 || columna > 3)
                {
                    Console.Write("⚠ Columna inválida. Ingresa (1-3): ");
                }

                //Verificamos que la posición seleccionada no esté ocupada
                if (tablero[fila - 1, columna - 1] != 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("⚠ Casilla ocupada, intenta otra.");
                    Console.ResetColor();
                }


            } while (tablero[fila - 1, columna - 1] != 0);

            //Todo es correcto se la asigan ala jugador correspodiente
            tablero[fila - 1, columna - 1] = jugador;

        }

        //Función para verificar si hay un ganador si hay tres en linea
        static bool ComprobarGanador()
        {

            int fila = 0, columna = 0;
            bool ticTacToe = false;
            //Un for para verificar las filas de las casillas son iguales y no esten vacias
            for (fila = 0; fila < 3; fila++)
            {

                if ((tablero[fila, 0] == tablero[fila, 1]) && (tablero[fila, 0] == tablero[fila, 2]) && (tablero[fila, 0] != 0))
                {
                    ticTacToe = true;
                }
            }

            //Si en alguna columna  todas las casillas son iguales y no estan vacias
            for (columna = 0; columna < 3; columna++)
            {

                if ((tablero[0, columna] == tablero[1, columna]) && (tablero[0, columna] == tablero[2, columna]) && (tablero[0, columna] != 0))
                {
                    ticTacToe = true;
                }

            }

            //Si en alguna diagonal todas las casillas son iguales y no estan vacias
            if ((tablero[0, 0] == tablero[1, 1]) && (tablero[0, 0] == tablero[2, 2]) && (tablero[0, 0] != 0))
            {
                ticTacToe = true;
            }

            //Si en alguna diagonal todas las casillas son iguales y no estan vacias
            if ((tablero[0, 2] == tablero[1, 1]) && (tablero[0, 2] == tablero[2, 0]) && (tablero[0, 2] != 0))
            {
                ticTacToe = true;
            }

            return ticTacToe;
        }

        //Verifica si hay empate, es decir, si todas las casillas estan ocupadas y no hay un ganador
        static bool ComprobarEmpate()
        {
            int fila, columna;
            bool hayEspacio = false;
            for (fila = 0; fila < 3; fila++)
            {
                for (columna = 0; columna < 3; columna++)
                {
                    //Indica que si hay una casilla vacia, es decir, con valor 0, entonces hay espacio para jugar
                    if (tablero[fila, columna] == 0)
                    {
                        hayEspacio = true;
                    }
                }
            }

            //Si el ciclo anterior nos regresa un true, indicamos que hay espacio, entonces se tiene que regresar un false, es decir,
            //no hay empate, pero si el ciclo anterior nos regresa un false, indicamos que no hay espacio, entonces se tiene que regresar
            //un true para indicar que hay un empate en la funcion de main()
            return !hayEspacio;

        }

    }//FIN CLASS

}//FIN NAMESPACE
