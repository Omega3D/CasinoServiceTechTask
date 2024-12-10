namespace CasinoServices.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IPersonRepository Person { get; }
    }
}
