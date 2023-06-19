namespace TWISIO.Identity.API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DBContext dbContext)
        {
            dbContext.Database.EnsureCreated();
        }
    }
}
