namespace Pixicity.Domain.Transversal
{
    public class AppPrincipal : IAppPrincipal
    {
        public AppPrincipal(long id, string userName, bool isAdmin)
        {
            Id = id;
            UserName = userName;
            IsAdmin = isAdmin;
        }

        public long Id { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
    }
}
