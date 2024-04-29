using ProyectoCIne;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoCine
{

    public class FunctionManager
    {
        private List<CinemaFunction> functions = new List<CinemaFunction>();

        public List<CinemaFunction> Functions { get {  return functions; } }

        public bool AddFunction(CinemaFunction cinemaFunction)
        {
            if (TooManyInternationals(functions, cinemaFunction.Date) && TooManyOfDirector(functions, cinemaFunction.Date, cinemaFunction.Movie.Director))
            {
                functions.Add(cinemaFunction);
                return true;
            }
            else
            {
                Console.WriteLine("La funcion no se puede agregar");
                return false;
            }
        }

        public void UpdateFunction(CinemaFunction currentCinemaFunction, CinemaFunction newCinemaFunction)
        {

            bool exists = functions.Where(function => function.Date == currentCinemaFunction.Date && function.Movie.Equals(currentCinemaFunction.Movie)).ToList().Any();

            if (exists)
            {
                bool couldAdd = AddFunction(newCinemaFunction);
                if(couldAdd)
                {
                    DeleteFunction(currentCinemaFunction);
                }
                else
                {
                    Console.WriteLine("No se pudo realizar la modificacion porque incumple alguna norma");
                }
                
                
            }
            else
            {
                Console.WriteLine("No existe la funcion a modificar");
            }

      
        }

        public void DeleteFunction(CinemaFunction cinemaFunction)
        {
            bool exists = functions.Remove(cinemaFunction);
            if (!exists)
            {
                Console.WriteLine("La funcion no existe");
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
