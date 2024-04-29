using System;


namespace ProyectoCIne
{
    public class CinemaFunction
    {
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            CinemaFunction other = (CinemaFunction)obj;

            return Date == other.Date && Movie.Equals(other.Movie);
        }

        public override int GetHashCode()
        {
           return Date.GetHashCode() ^ Movie.GetHashCode();
        }

        public DateTime Date { get; set; }
        public double Price { get; set; }
        public Movie Movie { get; set; }

        public string Director { get; set; }
        public CinemaFunction(DateTime aDate, double aPrice, Movie aMovie, string aDirector)
        {
            Date = aDate;
            Price = aPrice;
            Movie = aMovie;
            Director = aDirector;
        }



    }
}
