using Newtonsoft.Json;
using ProyectoCine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            string option;
            do
            {
                Console.WriteLine("Elija una opción (1-5):");
                option = Console.ReadLine();

                if (Regex.IsMatch(option, "^[1-5]$"))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Por favor, ingrese un número válido (1-5).");
                }

            } while (true); 
            
            return int.Parse(option);
        }

        public static DateTime ReadDate()
        {
            int year, month, day, hours, minutes;
            bool validDate = false;
            DateTime date = DateTime.MinValue;

            do
            {
                Console.Write("Ingrese el año (entre 1900 y el año actual): ");
                if (!int.TryParse(Console.ReadLine(), out year) || year < 1900 || year > DateTime.Now.Year)
                {
                    Console.WriteLine($"Año inválido. Por favor ingrese un año entre 1900 y {DateTime.Now.Year}.");
                    continue;
                }

                Console.Write("Ingrese el mes (1-12): ");
                if (!int.TryParse(Console.ReadLine(), out month) || month < 1 || month > 12)
                {
                    Console.WriteLine("Mes ingresado no válido. Inténtelo de nuevo.");
                    continue;
                }

                Console.Write("Ingrese el día: ");
                if (!int.TryParse(Console.ReadLine(), out day) || day < 1 || day > DateTime.DaysInMonth(year, month))
                {
                    Console.WriteLine("Día ingresado no válido para el mes y año especificados. Inténtelo de nuevo.");
                    continue;
                }

                Console.Write("Ingrese la hora (0-23): ");
                if (!int.TryParse(Console.ReadLine(), out hours) || hours < 0 || hours > 23)
                {
                    Console.WriteLine("Hora ingresada no válida. Inténtelo de nuevo.");
                    continue;
                }

                Console.Write("Ingrese los minutos (0-59): ");
                if (!int.TryParse(Console.ReadLine(), out minutes) || minutes < 0 || minutes > 59)
                {
                    Console.WriteLine("Minutos ingresados no válidos. Inténtelo de nuevo.");
                    continue;
                }
                validDate = true;
                date = new DateTime(year, month, day, hours, minutes, 0);
                if (date > DateTime.Now)
                {
                    Console.WriteLine("La fecha ingresada no puede ser posterior a la fecha actual. Inténtelo de nuevo.");
                    continue;
                }

            } while (!validDate);
            return date;
        }
        public static Movie ReadMovie(List<Movie> movies)
        {
            int index = 0;
            int pos;
            foreach (Movie movie in movies)
            {
                Console.WriteLine($"ID: {index+1}");
                Console.WriteLine($"Titulo: {movie.Title}");
                Console.WriteLine($"Director: {movie.Director}");
                if (movie.IsNational)
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
            Console.WriteLine("Selecciona una pelicula por su ID: ");
            while(!int.TryParse(Console.ReadLine(), out pos) || pos < 1 || pos > movies.Count())
            {
                Console.WriteLine("Numero incorrecto, seleccione una pelicula");
            }
            return movies[pos-1];

        }

        public static CinemaFunction ReadFunction()
        {
            string moviesJson = File.ReadAllText("C:\\Users\\valen\\Desktop\\proyecto react\\ProyectoCine\\ProyectoCine\\movies.json");
            List<Movie>? movies = JsonConvert.DeserializeObject<List<Movie>>(moviesJson);
            int price = 0;
            DateTime date = ReadDate();
            Movie movie = ReadMovie(movies);
            Console.WriteLine("Escriba el precio de la funcion: ");
            while(!int.TryParse(Console.ReadLine(), out price) || price <= 0)
            {
                Console.WriteLine("Precio ingresado no válido. Inténtelo de nuevo.");
            }


            return new CinemaFunction(date, price, movie, movie.Director);
        }
        public static void CallFunction(int option, FunctionManager manager, FunctionRepository repository)
        {
            switch (option)
            {
                case 1:
                    Console.WriteLine("Le vamos a pedir los datos de la funcion a ingresar");
                    CinemaFunction function = ReadFunction();
                    manager.AddFunction(function);
                    repository.SaveFunctions(manager.Functions);
                    break;
                case 2:
                    manager.UpdateFunction();
                    repository.SaveFunctions(manager.Functions);
                    break;
                case 3:
                    manager.DeleteFunction();
                    repository.SaveFunctions(manager.Functions);
                    break;
                case 4:
                    manager.ShowFunctions();
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
