using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BLL;
using GM.Entity.Models;
using GM.UI.Common;
using Microsoft.Practices.Unity;

namespace GM.UI.ModelView
{
   public class TreeViewModel:ObjectBase
    {
       private static IRepository<Departement> _departementRepository;
       private readonly Repository<PieceEmployee> _pieceEmployeeRepository;
       private ObservableCollection<HorsStockView> _horsStockViews;
       private object _current;
       public ObservableCollection<HorsStockView> HorsStockViews
       {
           get { return _horsStockViews; }
           set
           {
               _horsStockViews = value;
               OnPropertyChanged("HorsStockViews");
           }
       }
       
       public DelegateCommand<object> DelegateCommand { get; set; }
       public static ICollection<Departement> Items
       {
           get
           {
               if (_departementRepository != null)
               {
                   return _departementRepository.GetAllLazyLoad(x => x.Services).ToList();

               }
               return Items;

           }
       }
      
      
       public TreeViewModel()
       {
           var container = new UnityContainer();
            _departementRepository = container.Resolve<Repository<Departement>>();
            _pieceEmployeeRepository = container.Resolve<Repository<PieceEmployee>>();
           DelegateCommand = new DelegateCommand<object>(LoadData);

       }

       public void UpdateDatagrid(long id)
       {
           if (_current.GetType() == typeof(Departement))
           {
               HorsStockViews =
                   new ObservableCollection<HorsStockView>(
                       AutoMapper.Mapper.Map<IList<HorsStockView>>(
                           _pieceEmployeeRepository.GetAllLazyLoad(x => x.Piece, x => x.Piece.Article)
                               .Where(x => x.DepartementId == ((Departement)_current).Id)));
           }
           else if (_current.GetType().Name.StartsWith("Service"))
           {
               HorsStockViews =
                   new ObservableCollection<HorsStockView>(
                       AutoMapper.Mapper.Map<IList<HorsStockView>>(
                           _pieceEmployeeRepository.GetAllLazyLoad(x => x.Piece, x => x.Piece.Article)
                               .Where(x => x.ServiceId == ((Service)_current).Id)));
           }
           else if (_current.GetType().Name.StartsWith("SousService"))
           {
               HorsStockViews =
                   new ObservableCollection<HorsStockView>(
                       AutoMapper.Mapper.Map<IList<HorsStockView>>(
                           _pieceEmployeeRepository.GetAllLazyLoad(x => x.Piece, x => x.Piece.Article)
                               .Where(x => x.SousServiceId == ((SousService)_current).Id)));
           }
       }

       private void LoadData(object o)
       {
           _current = o;
           if (o.GetType() == typeof (Departement))
           {
               HorsStockViews =
                   new ObservableCollection<HorsStockView>(
                       AutoMapper.Mapper.Map<IList<HorsStockView>>(
                           _pieceEmployeeRepository.GetAllLazyLoad(x => x.Piece, x => x.Piece.Article)
                               .Where(x => x.DepartementId == ((Departement) o).Id)));
           }
           else if (o.GetType().Name.StartsWith("Service"))
           {
               HorsStockViews =
                   new ObservableCollection<HorsStockView>(
                       AutoMapper.Mapper.Map<IList<HorsStockView>>(
                           _pieceEmployeeRepository.GetAllLazyLoad(x => x.Piece, x => x.Piece.Article)
                               .Where(x => x.ServiceId == ((Service) o).Id)));
           }
           else if (o.GetType().Name.StartsWith("SousService"))
           {
               HorsStockViews =
                   new ObservableCollection<HorsStockView>(
                       AutoMapper.Mapper.Map<IList<HorsStockView>>(
                           _pieceEmployeeRepository.GetAllLazyLoad(x => x.Piece, x => x.Piece.Article)
                               .Where(x => x.SousServiceId == ((SousService) o).Id)));
           }
       }
    }
}
