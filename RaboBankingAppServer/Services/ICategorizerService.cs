using RaboBankingAppServer.Entities;

namespace RaboBankingAppServer.Services
{
    public interface ICategorizerService
    {
        Transaction Categorize(Transaction transaction);
        void CategorizeAll();
        string[] ConvertStringToArrayOfStrings(string stringList);
    }
}