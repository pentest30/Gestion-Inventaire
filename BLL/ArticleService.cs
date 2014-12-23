using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL;
using GM.Entity.Models;
using Microsoft.Practices.Unity;

namespace BLL
{
   public class ArticleService : IService , IRepository<Article>
   {
       // static Repository<Article> _articleRepository;
       readonly static UnityContainer Container = new UnityContainer();
       private static EntityFactory<Article> _factory; 
       private  static GmStoreContext _db;
       public ArticleService(GmStoreContext db)
       {
           _db = db;
           Container.RegisterInstance(new EntityFactory<Article>(_db));
           _factory = Container.Resolve<EntityFactory<Article>>();
           Container.RegisterInstance(new Repository<Article>(_db));
           //_articleRepository = Container.Resolve<Repository<Article>>();
       }

      public  IEnumerable<Article> ListWithCount()
       {

          if (SelectAll().Any())
          {
              foreach (var article in SelectAll())
              {
                  var qnt = 0;
                  var ligne = article.BonEntreeLignes.FirstOrDefault(x => x.ArticleId == article.Id);
                  if (ligne != null)
                  {
                      qnt += Convert.ToInt32(ligne.Qnt);
                      article.QntMagsin = qnt;
                  }

                  yield return article;
              }
          }
          
       }

       public Article Create()
       {
           return _factory.Create();
       }
       public  IEnumerable<Article> SelectAll()
       {
           return _db.Articles;
       }

       public Article SelectById(object id)
       {
           return _db.Articles.FirstOrDefault(a=>a.Id== (int)id);
       }

       public void Insert(Article item)
       {
           _db.Articles.Add(item);
       }

       public void Update(Article item)
       {
           _db.Articles.Attach(item);
           _db.Entry(item).State = EntityState.Modified;
       }

       public void Delete(object id)
       {
           var item = _db.Articles.Find(id);
           _db.Articles.Attach(item);
           _db.Entry(item).State = EntityState.Deleted;

       }

       public void Save()
       {
           _db.SaveChanges();
       }

       public IEnumerable<Article> Find(Func<Article, bool> predicate)
       {
           return _db.Articles.Where(predicate);
       }

       public IQueryable<Article> GetAllLazyLoad(params Expression<Func<Article, object>>[] children)
       {
           children.ToList().ForEach(x => _db.Articles.Include(x).Load());
           return _db.Articles;
       }
   }
}