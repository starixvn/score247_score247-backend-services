using System.Threading.Tasks;
using Fanex.Data.Repository;
using Fanex.Logging;
using Soccer.Core.Models.Favorites;
using Soccer.Database.Favorites.Commands;

namespace Soccer.API.Favorites
{
    public interface IFavoriteCommandService
    {
        Task<int> AddFavorite(UserFavorite userFavorite);

        Task<int> RemoveFavorite(UserFavorite userFavorite);
    }

    public class FavoriteCommandService : IFavoriteCommandService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly ILogger logger;

        public FavoriteCommandService(IDynamicRepository dynamicRepository, ILogger logger)
        {
            this.dynamicRepository = dynamicRepository;
            this.logger = logger;
        }

        public Task<int> AddFavorite(UserFavorite userFavorite)
        {
            logger.Info($"AddFavorite user {userFavorite.UserId} - device {userFavorite.DeviceId} ");

            return dynamicRepository.ExecuteAsync(new InsertOrUpdateFavoriteCommand(userFavorite));
        }


        public Task<int> RemoveFavorite(UserFavorite userFavorite)
        {
            logger.Info($"RemoveFavorite user {userFavorite.UserId} - device {userFavorite.DeviceId} ");

            return dynamicRepository.ExecuteAsync(new RemoveFavoriteCommand(userFavorite));
        }
    }
}