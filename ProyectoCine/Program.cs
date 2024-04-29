using Newtonsoft.Json;
using ProyectoCine;
using ProyectoCIne;
using System.IO;
internal class Program
{
    private static void Main(string[] args)
    {
        //string[] directorFileContent = File.ReadAllLines("C:\\Users\\valen\\Desktop\\proyecto react\\ProyectoCine\\ProyectoCine\\directores.txt");
        string peliculasJson = File.ReadAllText("C:\\Users\\valen\\Desktop\\proyecto react\\ProyectoCine\\ProyectoCine\\movies.json");
        List<Movie>? peliculas = JsonConvert.DeserializeObject<List<Movie>>(peliculasJson);
        FunctionManager manager = new FunctionManager();
        do
        {
            Menu.ShowMenu();
            int opcion = Menu.ReadOption();
            if(opcion == 5)
            {
                break;
            }
            Menu.CallFunction(opcion, manager);
            
        }while (true);

        
        


    }
}