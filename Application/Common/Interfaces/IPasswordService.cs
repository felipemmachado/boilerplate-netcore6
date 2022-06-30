namespace Application.Common.Interfaces
{
    public interface IPasswordService
    {
        string Generate(string password);

        bool Check(string key, string password);

        string GetAlphanumericCode(int qtdPositions);
    }
}
