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
        FunctionRepository repository = new FunctionRepository();
        manager.Functions = repository.LoadFunctions();
        do
        {
            Menu.ShowMenu();
            int option = Menu.ReadOption();
            if(option == 5)
            {
                break;
            }
            Menu.CallFunction(option, manager, repository);
            
        }while (true);

        
        


    }
}