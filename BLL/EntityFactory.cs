using DAL;
using Microsoft.Practices.Unity;

namespace BLL
{
    public  class EntityFactory<T> where T: class
    {
        private static Repository<T> _repository;
        private readonly GmStoreContext _context;
        public EntityFactory(GmStoreContext context)
        {
            _context = context;
            //_repository = repository;
        }
        public  T Create()
        {
            var container = new UnityContainer();
            container.RegisterInstance(new Repository<T>(_context));
            _repository = container.Resolve<Repository<T>>();
            return _repository.Set.Create<T>();
        }
    }
}
