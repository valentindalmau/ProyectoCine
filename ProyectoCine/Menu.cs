using Newtonsoft.Json;
using ProyectoCine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProyectoCIne
{
    public class Menu
    {
        public static void ShowMenu()
        {
            Console.WriteLine("1. Crear función");
            Console.WriteLine("2. Modificar función");
            Console.WriteLine("3. Eliminar función");
            Console.WriteLine("4. Mostrar funciones");
            Console.WriteLine("5. Salir");
        }

        public static int ReadOption()
        {
            string opcion;
            do
            {
                Console.WriteLine("Elija una opción (1-5):");
                opcion = Console.ReadLine();

                if (Regex.IsMatch(opcion, "^[1-5]$"))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Por favor, ingrese un número válido (1-5).");
                }

            } while (true); 
            
            return int.Parse(opcion);
        }

        public static DateTime ReadDate()
        {
            int año, mes, dia;
            bool fechaValida = false;
            DateTime fecha = DateTime.MinValue;

            do
            {
                Console.Write("Ingrese el año (YYYY): ");
                if (!int.TryParse(Console.ReadLine(), out año))
                {
                    Console.WriteLine("Año ingresado no válido. Inténtelo de nuevo.");
                    continue;
                }

                Console.Write("Ingrese el mes (1-12): ");
                if (!int.TryParse(Console.ReadLine(), out mes) || mes < 1 || mes > 12)
                {
                    Console.WriteLine("Mes ingresado no válido. Inténtelo de nuevo.");
                    continue;
                }

                Console.Write("Ingrese el día: ");
                if (!int.TryParse(Console.ReadLine(), out dia) || dia < 1 || dia > DateTime.DaysInMonth(año, mes))
                {
                    Console.WriteLine("Día ingresado no válido para el mes y año especificados. Inténtelo de nuevo.");
                    continue;
                }

                fechaValida = true;
                fecha = new DateTime(año, mes, dia);

            } while (!fechaValida);
            return fecha;
        }
        public static Movie ReadMovie(List<Movie> peliculas)
        {
            int index = 0;
            int pos;
            foreach (Movie pelicula in peliculas)
            {
                Console.WriteLine($"{index+1}");
                Console.WriteLine(pelicula.Title);
                Console.WriteLine(pelicula.Director);
                if (pelicula.IsNational)
                {
                    Console.WriteLine("Nacionalidad: argentina");
                }
                else
                {
                    Console.WriteLine("Nacionalidad: extranjera");
                }
                Console.WriteLine("\n");
                index++;
            }
            Console.WriteLine("Selecciona una pelicula por su posicion: ");
            while(!int.TryParse(Console.ReadLine(), out pos) || pos < 1 || pos > peliculas.Count())
            {
                Console.WriteLine("Numero incorrecto, seleccione una pelicula");
            }
            return peliculas[pos-1];

        }

        public static CinemaFunction ReadFunction()
        {
            string peliculasJson = File.ReadAllText("C:\\Users\\valen\\Desktop\\proyecto react\\ProyectoCine\\ProyectoCine\\movies.json");
            List<Movie>? peliculas = JsonConvert.DeserializeObject<List<Movie>>(peliculasJson);
            int precio = 0;
            DateTime fecha = ReadDate();
            Movie pelicula = ReadMovie(peliculas);
            Console.WriteLine("Escriba el precio de la funcion: ");
            while(!int.TryParse(Console.ReadLine(), out precio) || precio <= 0)
            {
                Console.WriteLine("Precio ingresado no válido. Inténtelo de nuevo.");
            }


            return new CinemaFunction(fecha, precio, pelicula, pelicula.Director);
        }
        public static void CallFunction(int option, FunctionManager manager)
        {
            switch (option)
            {
                case 1:
                    Console.WriteLine("Le vamos a pedir los datos de la funcion a ingresar");
                    CinemaFunction function = ReadFunction();
                    manager.AddFunction(function);
                    break;
                case 2:
                    Console.WriteLine("Le vamos a pedir los datos de la funcion a modificar");
                    CinemaFunction currentFunction = ReadFunction();
                    Console.WriteLine("Le vamos a pedir los datos de la funcion a agregar");
                    CinemaFunction newFunction = ReadFunction();
                    manager.UpdateFunction(currentFunction, newFunction);
                    break;
                case 3:
                    Console.WriteLine("Le vamos a pedir los datos de la funcion a eliminar");
                    CinemaFunction deleteFunction = ReadFunction();
                    manager.DeleteFunction(deleteFunction);
                    break;
                case 4:
                    List<CinemaFunction> funciones = manager.Functions;
                    foreach (CinemaFunction funcion in funciones)
                    {
                        Console.WriteLine(funcion.Date);
                        Console.WriteLine(funcion.Price);
                        Console.WriteLine(funcion.Movie.Title);
                        Console.WriteLine(funcion.Movie.Director);
                        if (funcion.Movie.IsNational)
                        {
                            Console.WriteLine("Nacionalidad: argentina");
                        }
                        else
                        {
                            Console.WriteLine("Nacionalidad: extranjera");
                        }
                        Console.WriteLine("\n");
                    }
                    break;
                case 5:
                    Console.WriteLine("Saliendo del programa");
                    break;
                default:
                    Console.WriteLine("Opción no válida. Inténtelo de nuevo.");
                    break;
            }
        }
    }
}
