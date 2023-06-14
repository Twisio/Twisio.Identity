namespace TWISIO.Identity.Persistence
{
    public static class DbInitializer
    {
        public static void Initialize(DBContext dbContext)
        {
            dbContext.Database.EnsureCreated();
        }
    }
}
