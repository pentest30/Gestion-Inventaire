using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using GM.UI.Views;
using ZonaTools.XPlorerBar;

namespace GM.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            //ContentControl.Content = new DepartementView();

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (((XPlorerItem)sender).ItemText)
                {
                    
                    case "Sous catégories": ContentControl.Content = new SousCategorieView();
                        break;
                    case "Départements": ContentControl.Content = new DepartementView();
                        break;
                    case "Catégories": ContentControl.Content = new CategorieView();
                        break;
                    case "Services": ContentControl.Content = new ServiceView();
                        break;
                    case "Sous services": ContentControl.Content = new SousServiceView();
                        break;
                    case "Magasins": ContentControl.Content = new MagasinView();
                        break;
                    case "Marques": ContentControl.Content = new MarqueView();
                        break;
                    case "Articles": ContentControl.Content = new ArticleView();
                        break;
                    case "Distribution du matériels": ContentControl.Content = new MaterielInView();
                        break;
                    case "Bons d'entrées": ContentControl.Content = new BonEntreeMagasinView();
                   
                        break;
                    case "Commandes Internes": ContentControl.Content = new CommandeView();
                        break;
                    case "Bons de sortie": ContentControl.Content = new BonSortieView();
                        break;
                    case "Nouveau stock": ContentControl.Content = new PieceView();
                        break;
                    case "Types": ContentControl.Content = new TypeView();
                        break;
                    case "Matériels en utilisation": ContentControl.Content = new PieceSrviceView();
                        break;
                    case "Matériels par location": ContentControl.Content = new ExplorerMatrielsView();
                        break;
                        
                   
                        
                }
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            List<Button> buttons = EventHelper.GetLogicalChildCollection<Button>(ContentControl);
            if (e.Key == (Key.A  ) &&Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                 var add = buttons.FirstOrDefault(x => x.Name == "AddButton");
                    if (add != null)
                        add.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            if (e.Key == (Key.E) && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                var add = buttons.FirstOrDefault(x => x.Name == "SaveButton");
                if (add != null)
                    add.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            if (e.Key == (Key.M) && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                var update = buttons.FirstOrDefault(x => x.Name == "UpdateButton");
                    if (update != null)
                        update.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            if (e.Key == (Key.S) && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                var delete = buttons.FirstOrDefault(c => c.Name.Equals("DeleteButton"));
                if (delete != null)
                    delete.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
            if (e.Key == (Key.B) && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                var back = buttons.FirstOrDefault(c => c.Name.Equals("BackButton"));
                if (back != null)
                    back.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
    }
}
