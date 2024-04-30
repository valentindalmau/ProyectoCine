using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCIne
{
    public class FunctionRepository
    {
        private const string FilePath = "C:\\Users\\valen\\Desktop\\proyecto react\\ProyectoCine\\ProyectoCine\\CinemaFunctions.json";

        public List<CinemaFunction> LoadFunctions()
        {
            List<CinemaFunction> functions = new List<CinemaFunction>();

            if (File.Exists(FilePath))
            {
                try
                {
                    string json = File.ReadAllText(FilePath);
                    functions = JsonConvert.DeserializeObject<List<CinemaFunction>>(json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al cargar las funciones: {ex.Message}");
                }
            }

            return functions;
        }

        public void SaveFunctions(List<CinemaFunction> functions)
        {
            bool fileExists = File.Exists(FilePath);

            if (!fileExists)
            {
                try
                {
                    File.Create(FilePath).Dispose();
                    Console.WriteLine("Archivo creado exitosamente");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al crear el archivo: {ex.Message}");
                }
            }


            string json = JsonConvert.SerializeObject(functions, Formatting.Indented);

            File.WriteAllText(FilePath, json);
            Console.WriteLine("Cambios guardados exitosamente");
        }
    }
}
