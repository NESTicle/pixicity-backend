namespace Pixicity.Domain.Transversal
{
    public interface IAppPrincipal
    {
        long Id { get; set; }
        string UserName { get; set; }
        string IP { get; set; }
        bool IsAdmin { get; set; }
        bool IsModerador { get; set; }
    }
}
