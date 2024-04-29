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
            List<CinemaFunction> functions;

            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                functions = JsonConvert.DeserializeObject<List<CinemaFunction>>(json);
            }
            else
            {
                functions = new List<CinemaFunction>();
            }

            return functions;
        }

        public void SaveFunctions(List<CinemaFunction> functions)
        {
            bool fileExists = File.Exists(FilePath);

            if (!fileExists)
            {
                File.Create(FilePath).Dispose();
                Console.WriteLine("Archivo creado exitosamente");
            }

            string json = JsonConvert.SerializeObject(functions, Formatting.Indented);

            File.WriteAllText(FilePath, json);
            Console.WriteLine("Cambios guardados exitosamente");
        }
    }
}
