using ProyectoCIne;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoCine
{

    public class FunctionManager
    {
        private List<CinemaFunction> functions = new List<CinemaFunction>();

        public List<CinemaFunction> Functions { get { return functions; } set { functions = value; } }

        public void ShowFunctions() 
        {
            int index = 0;
            foreach (CinemaFunction function in functions)
            {
                Console.WriteLine($"ID: {index+1}");
                Console.WriteLine($"Fecha y hora: {function.Date}");
                Console.WriteLine($"Precio: {function.Price}");
                Console.WriteLine($"Titulo: {function.Movie.Title}");
                Console.WriteLine($"Director: {function.Movie.Director}");
                if (function.Movie.IsNational)
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
            if (functions.Count < 1) 
            {
                Console.WriteLine("No hay funciones");
            }
        }
        public void AddFunction(CinemaFunction cinemaFunction)
        {
            if (TooManyInternationals(functions, cinemaFunction.Date) && TooManyOfDirector(functions, cinemaFunction.Date, cinemaFunction.Movie.Director))
            {
                functions.Add(cinemaFunction);
            }
            else
            {
                Console.WriteLine("La funcion no se puede agregar por incumplir alguna norma");
            }
        }

        public void UpdateFunction()
        {

            ShowFunctions();
            while (true)
            {
                Console.WriteLine("Seleccione que numero de funcion desea modificar por ID: ");
                if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= functions.Count)
                {
                    Console.WriteLine("Le vamos a pedir los datos para modificar la funcion");
                    CinemaFunction function = Menu.ReadFunction();
                    functions[index-1] = function;
                    Console.WriteLine("Función modificada correctamente.");
                    break;
                }
                else
                {
                    Console.WriteLine($"Numero incorrecto, por favor seleccione un numero entre 1 y {functions.Count}");
                }
            }
        }

        public void DeleteFunction()
        {
            ShowFunctions();
            while(true)
            {
                Console.WriteLine("Seleccione que numero de funcion desea eliminar por ID: ");
                if(int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= functions.Count)
                {
                    functions.RemoveAt(index - 1);
                    Console.WriteLine("Función eliminada correctamente.");
                    break;
                }
                else
                {
                    Console.WriteLine($"Numero incorrecto, por favor seleccione un numero entre 1 y {functions.Count}");
                }
            }
            


        }

        public bool TooManyInternationals(List<CinemaFunction> aListOfFunctions, DateTime aDate)
        {
            int internationalMoviesCount = 0;

            foreach (var func in aListOfFunctions)
            {
                if (func.Date == aDate)
                {
                    if (!func.Movie.IsNational)
                    {
                        internationalMoviesCount++;

                        if (internationalMoviesCount >= 8)
                            return false;
                    }
                }
            }

            return true;
        }

        public bool TooManyOfDirector(List<CinemaFunction> aListOfFunctions, DateTime aDate, string aDirector)
        {
            int directorMoviesCount = 0;

            foreach (var func in aListOfFunctions)
            {
                if (func.Date == aDate)
                {
                    if (func.Director == aDirector)
                    {
                        directorMoviesCount++;

                        if (directorMoviesCount >= 10)
                            return false;
                    }
                }
            }

            return true;
        }
    }



}
