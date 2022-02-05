using MovieSystem.Domain.SeedWork;

namespace MovieSystem.Domain.AggregatesModel.UserAggregate
{
    public class UserFavouriteMovie : Entity
    {
        public string UpdatedUser { get; private set; }
        public DateTime UpdatedDate { get; private set; } = DateTime.Now;
        public int MovieId { get; private set; }
        public int UserId { get; private set; }
        public string Reason { get; private set; }
        public Rating Rating { get; private set; }
        
        public UserFavouriteMovie(int movieId, int userId, string reason, Rating rating, string user)
        {
            MovieId = movieId;
            UserId = userId;
            Reason = reason;
            Rating = rating;
            UpdatedUser = user;
        }

        public void Update(string reason, Rating rating, string user)
        {
            Reason = reason;
            Rating = rating;
            UpdatedUser = user;
        }

    }
}
