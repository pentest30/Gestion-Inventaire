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
            using (var db = new GmStoreContext())
            {
                var container = new UnityContainer();

                container.RegisterInstance(new ArticleService());
                container.RegisterInstance(new PieceService());
                container.RegisterInstance(new StockService());
                container.RegisterInstance(new EntityFactory<Departement>(db));
                container.RegisterInstance(new Repository<Departement>());
                container.RegisterInstance(new Repository<Service>());
                container.RegisterInstance(new Repository<Employe>());
                container.RegisterInstance(new Repository<Article>());
                container.RegisterInstance(new Repository<Magasin>());
                container.RegisterInstance(new Repository<Marque>());
                container.RegisterInstance(new Repository<PieceEmployee>());
                container.RegisterInstance(new Repository<Piece>());
                container.RegisterInstance(new Repository<Categorie>());
                container.RegisterInstance(new Repository<SousCategorie>());
                container.RegisterInstance(new Repository<BonEntree>());
                container.RegisterInstance(new Repository<BonEntreeLigne>());
                container.RegisterInstance(new Repository<BonSortie>());
                container.RegisterInstance(new Repository<BonSortieLigne>());
            }
            AutoMapper.Mapper.CreateMap<Piece, Piece>()
               .ForMember(x => x.Id, o => o.Ignore())
               .ForMember(x => x.NInventaire, o => o.Ignore());
            AutoMapper.Mapper.CreateMap<Piece, PieceMagasin>()
                .ForMember(x => x.PieceId, o => o.MapFrom(p => p.Id))
                .ForMember(x => x.MagasinId, o => o.MapFrom(p => p.MagasinId))
                .ForMember(x => x.Date, o => o.MapFrom(p => p.DateEntree))
                .ForMember(x => x.BonEntreeId, o => o.MapFrom(p => p.BonEntreeId));

        }
    }
}
