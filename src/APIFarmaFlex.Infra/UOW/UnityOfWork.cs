using APIFarmaFlex.Infra.Interfaces;
using APIFarmaFlex.Infra.ORM;

namespace APIFarmaFlex.Infra.UOW
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly DataContext _context;

        public UnityOfWork(DataContext context)
        {
            _context = context;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
            //
        }
    }
}
