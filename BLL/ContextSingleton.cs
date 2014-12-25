using DAL;

namespace BLL
{
    public class ContextSingleton
    {
        private readonly static GmStoreContext Context = new GmStoreContext();
        static ContextSingleton()
        {
            
        }

        private ContextSingleton()
        {
            
        }

        public static GmStoreContext Instance
        {
            get { return Context; }
        }
    }
}
