using BLL;
using DAL;
using GM.Entity.Models;
using Microsoft.Practices.Unity;

namespace GM.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            var container = new UnityContainer();
            var dbContext = new GMStoreContext();
            container.RegisterInstance(new Repository<Departement>(dbContext));
            container.RegisterInstance(new Repository<Service>(dbContext));
            container.RegisterInstance(new Repository<Employe>(dbContext));
            container.RegisterInstance(new Repository<Article>(dbContext)); 
            container.RegisterInstance(new Repository<Magasin>(dbContext));
            container.RegisterInstance(new Repository<Marque>(dbContext));
            container.RegisterInstance(new Repository<Mouvement>(dbContext));
            container.RegisterInstance(new Repository<Piece>(dbContext));
            container.RegisterInstance(new Repository<Categorie>(dbContext));
            container.RegisterInstance(new Repository<SousCategorie>(dbContext));
            container.RegisterInstance(new Repository<BonEntree>(dbContext));
           
        }
    }
}
