using ProyectoCine;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoCIne
{
    public class Movie
    {
        public string Title { get; set; }
        public string Director { get; set; }

        public bool IsNational { get; set; }

        public Movie(string aTitle, string aDirector, bool aIsNational)
        {
            Title = aTitle;
            Director = aDirector;
            IsNational = aIsNational;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Movie other = (Movie)obj;

            return Title == other.Title && Director == other.Director;
        }

        public override int GetHashCode()
        {
            return Title.GetHashCode() ^ Director.GetHashCode();
        }
    }
}
