namespace Coding.Tracker
{
    public interface ICodeSessionService
    {
        void CreateDatabase();
        void Create(CodeSession session);
        List<CodeSession> ReadAllData();
        void Update(CodeSession session);
        void Delete(int id);
    }
}
