namespace Pixicity.Domain.Transversal
{
    public interface IAppPrincipal
    {
        long Id { get; set; }
        string UserName { get; set; }
        bool IsAdmin { get; set; }
    }
}
