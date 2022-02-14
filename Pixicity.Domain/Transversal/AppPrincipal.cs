namespace Pixicity.Domain.Transversal
{
    public class AppPrincipal : IAppPrincipal
    {
        public AppPrincipal(long id, string userName, string ip, bool isAdmin)
        {
            Id = id;
            UserName = userName;
            IP = ip;
            IsAdmin = isAdmin;
        }

        public long Id { get; set; }
        public string UserName { get; set; }
        public string IP { get; set; }
        public bool IsAdmin { get; set; }
    }
}
