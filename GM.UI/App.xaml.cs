﻿using BLL;
using GM.Entity.Models;
using GM.UI.ModelView;
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
           
            container.RegisterInstance(new StockService());
            container.RegisterInstance(new ArticleService(container.Resolve<StockService>()));
            container.RegisterInstance(new PieceService());
            container.RegisterInstance(new EntityFactory<Departement>());
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
            container.RegisterInstance(new Repository<TypeArticle>());

            AutoMapper.Mapper.CreateMap<Piece, Piece>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.NInventaire, o => o.MapFrom(x => x.NInventaire));
            AutoMapper.Mapper.CreateMap<Piece, PieceMagasin>()
                // .ForMember(x => x.ArticleId, o => o.MapFrom(p => p.ArticleId))
                .ForMember(x => x.PieceId, o => o.MapFrom(p => p.Id))
                //  .ForMember(x => x.MagasinId, o => o.MapFrom(p => p.MagasinId))
                .ForMember(x => x.Date, o => o.MapFrom(p => p.DateEntree))
                .ReverseMap();
            //   .ForMember(x => x.BonEntreeId, o => o.MapFrom(p => p.BonEntreeId));
            AutoMapper.Mapper.CreateMap<PieceMagasin, StockModelView>()
                .ForMember(x => x.Etat, o => o.MapFrom(p => p.Piece.EtatPiece))
                .ForMember(x => x.NBon, o => o.MapFrom(p => p.BonEntree.NBon))
                .ForMember(x => x.Date, o => o.MapFrom(p => p.Date))
                .ForMember(x => x.Inventaire, o => o.MapFrom(p => p.Piece.NInventaire))
                .ForMember(x => x.Model, o => o.MapFrom(p => p.Article.Libelle))
                .ForMember(x => x.Categorie, o => o.MapFrom(p => p.Article.Categorie.Libelle))
                .ForMember(x => x.SousCategorie, o => o.MapFrom(p => p.Article.SousCategorie.Libelle))
                //.ForMember(x => x.Type, o => o.MapFrom(p => p.Article.TypeArticle.Libelle))
                .ForMember(x => x.Marque, o => o.MapFrom(p => p.Article.Marque.Libelle));
            AutoMapper.Mapper.CreateMap<PieceEmployee, HorsStockView>()
                .ForMember(x => x.NBon, o => o.MapFrom(p => p.BonSortie.NBon))
                .ForMember(x => x.Date, o => o.MapFrom(p => p.Date))
                .ForMember(x => x.Inventaire, o => o.MapFrom(p => p.Piece.NInventaire))
                .ForMember(x => x.Model, o => o.MapFrom(p => p.Piece.Article.Libelle))
                .ForMember(x => x.Categorie, o => o.MapFrom(p => p.Piece.Article.Categorie.Libelle))
                .ForMember(x => x.SousCategorie, o => o.MapFrom(p => p.Piece.Article.SousCategorie.Libelle))
                //.ForMember(x => x.Type, o => o.MapFrom(p => p.Article.TypeArticle.Libelle))
                .ForMember(x => x.Marque, o => o.MapFrom(p => p.Piece.Article.Marque.Libelle))
                .ForMember(x => x.Departement, o => o.MapFrom(p => p.Departement.Libelle))
                .ForMember(x => x.Service, o => o.MapFrom(p => p.Service.Libelle))
                .ForMember(x => x.SousService, o => o.MapFrom(p => p.SousService.Libelle));
               
         //   AutoMapper.Mapper.CreateMap<PieceEmployee, HorsStockView>();
            AutoMapper.Mapper.CreateMap<Article, ArticleViewModel>()
                .ForMember(x => x.Model, o => o.MapFrom(p => p.Libelle))
                .ForMember(x => x.QntTotal, o => o.MapFrom(p => p.QntTotal))
                .ForMember(x => x.Qnt, o => o.MapFrom(p => p.QntMagsin))
                .ForMember(x => x.SousCategorie, o => o.MapFrom(p => p.SousCategorie.Libelle))
                .ForMember(x => x.Marque, o => o.MapFrom(p => p.Marque.Libelle));
            AutoMapper.Mapper.CreateMap<BonSortieLigne, BonSortieReportView>()
                .ForMember(x => x.NBon, o => o.MapFrom(p => p.BonSortie.NBon))
                .ForMember(x => x.Magasin, o => o.MapFrom(p => p.BonSortie.Magasin.Libelle))
                .ForMember(x => x.Departement, o => o.MapFrom(p => p.Departement.Libelle))
                .ForMember(x => x.Service, o => o.MapFrom(p => p.Service.Libelle))
                .ForMember(x => x.CommandeInterne, o => o.MapFrom(p => p.BonSortie.CommndeInterne.Libelle))
                .ForMember(x => x.Article, o => o.MapFrom(p => p.Article.Libelle))
                .ForMember(x => x.Materiel, o => o.MapFrom(p => p.Article.SousCategorie.Libelle))
                .ForMember(x => x.DateSortie, o => o.MapFrom(p => p.BonSortie.DateSortie));
            AutoMapper.Mapper.CreateMap<Piece, Reformet>()
                .ForMember(x => x.PieceId, o => o.MapFrom(p => p.Id))
                .ForMember(x => x.PieceNInventaire, o => o.MapFrom(p => p.NInventaire));
            AutoMapper.Mapper.CreateMap<Location, TreeViewModel>();

        }
    }
}
