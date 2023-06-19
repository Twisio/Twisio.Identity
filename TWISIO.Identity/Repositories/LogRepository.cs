using TWISIO.Identity.API.Entities;
using TWISIO.Identity.API.Entities.Enums;
using TWISIO.Identity.API.Interfaces;
using TWISIO.Identity.API.Interfaces.Repositories;

namespace TWISIO.Identity.API.Repositories
{
    /// <inheritdoc/>
    public class LogRepository : ILogRepository
    {
        private readonly IDBContext _dbContext;

        public LogRepository(IDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(string message, LogType type)
        {
            var log = new Log
            {
                Id = Guid.NewGuid(),
                Message = message,
                Type = type,
                Date = DateTime.UtcNow
            };

            await _dbContext.Logs.AddAsync(log);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public async Task AddAsync(string message, string? innerMessage, LogType type)
        {
            var log = new Log
            {
                Id = Guid.NewGuid(),
                Message = message,
                InnerMessage = innerMessage,
                Type = type,
                Date = DateTime.UtcNow
            };

            await _dbContext.Logs.AddAsync(log);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public async Task AddAsync(User user, string message, LogType type)
        {
            var log = new Log
            {
                Id = Guid.NewGuid(),
                User = user,
                Message = message,
                Type = type,
                Date = DateTime.UtcNow
            };

            await _dbContext.Logs.AddAsync(log);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public async Task AddAsync(User user, string message, string innerMessage, LogType type)
        {
            var log = new Log
            {
                Id = Guid.NewGuid(),
                User = user,
                Message = message,
                InnerMessage = innerMessage,
                Type = type,
                Date = DateTime.UtcNow
            };

            await _dbContext.Logs.AddAsync(log);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }
    }
}
