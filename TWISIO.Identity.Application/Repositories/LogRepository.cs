using TWISIO.Identity.Application.Interfaces;
using TWISIO.Identity.Application.Interfaces.Repositories;
using TWISIO.Identity.Domain;
using TWISIO.Identity.Domain.Enums;

namespace TWISIO.Identity.Application.Repositories
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
