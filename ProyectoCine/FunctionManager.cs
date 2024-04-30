using ProyectoCIne;
using System.Diagnostics;
using System.IO;

namespace ProyectoCine
{

    public class FunctionManager
    {
        private List<CinemaFunction> functions = new List<CinemaFunction>();

        public List<CinemaFunction> Functions { get { return functions; } set { functions = value; } }

        public void ShowFunctions() 
        {
            var index = 0;
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
            if (TooManyInternationals(functions, cinemaFunction.Date) || TooManyOfDirector(functions, cinemaFunction.Date, cinemaFunction.Movie.Director) || FunctionAlreadyExist(functions, cinemaFunction))
            {
                Console.WriteLine("La funcion no se puede agregar por incumplir alguna norma");
            }
            else
            {
                functions.Add(cinemaFunction);
            }
        }

        public void UpdateFunction()
        {

            ShowFunctions();
            var ban = true;
            while (ban)
            {
                Console.WriteLine("Seleccione que numero de funcion desea modificar por ID: ");
                if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= functions.Count)
                {
                    Console.WriteLine("Le vamos a pedir los datos para modificar la funcion");
                    CinemaFunction function = Menu.ReadFunction();
                    if (TooManyInternationals(functions, function.Date) || TooManyOfDirector(functions, function.Date, function.Movie.Director) || FunctionAlreadyExist(functions, function))
                    {
                        Console.WriteLine("La funcion no se pudo modificar por incumplir alguna norma");
                    }
                    else
                    {
                        functions[index - 1] = function;
                        Console.WriteLine("Función modificada correctamente.");
                    }
                    ban = false;

                }
                else
                {
                    Console.WriteLine($"Numero incorrecto, por favor seleccione un numero entre 1 y {functions.Count}");
                }
            }
        }

        public void DeleteFunction()
        {
            var ban = true;
            ShowFunctions();
            while(ban)
            {
                Console.WriteLine("Seleccione que numero de funcion desea eliminar por ID: ");
                if(int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= functions.Count)
                {
                    functions.RemoveAt(index - 1);
                    Console.WriteLine("Función eliminada correctamente.");
                    ban = false;
                }
                else
                {
                    Console.WriteLine($"Numero incorrecto, por favor seleccione un numero entre 1 y {functions.Count}");
                }
            }
            


        }

        public bool TooManyInternationals(List<CinemaFunction> aListOfFunctions, DateTime aDate)
        {
            var internationalsTodayCount = (from f in aListOfFunctions
                                            where f.Movie.IsNational == false
                                            where f.Date.Year == aDate.Year && f.Date.Month == aDate.Month && f.Date.Day == aDate.Day
                                            select f.Director).Count();

            return internationalsTodayCount >= 8;
        }

        public bool TooManyOfDirector(List<CinemaFunction> aListOfFunctions, DateTime aDate, string aDirector)
        {
            var moviesFromDirectorToday = (from f in aListOfFunctions
                                            where f.Movie.Director == aDirector
                                            where f.Date.Year == aDate.Year && f.Date.Month == aDate.Month && f.Date.Day == aDate.Day
                                            select f.Director).Count();
            return moviesFromDirectorToday >= 10;
        }
        public bool FunctionAlreadyExist(List<CinemaFunction> aListOfFunctions, CinemaFunction aFunction)
        {
            var equalFunctions = (from f in aListOfFunctions
                                  where f.Equals(aFunction)
                                  select f.Director).Count();
            return equalFunctions > 0;
        }
    }



}
