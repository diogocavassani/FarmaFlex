namespace APIFarmaFlex.Infra.Interfaces
{
    public interface IUnityOfWork
    {
        void Commit();
        void Rollback();
    }
}
