using System;
using System.Windows;
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
                    case "Magasins": ContentControl.Content = new MagasinView();
                        break;
                    case "Marques": ContentControl.Content = new MarqueView();
                        break;
                    case "Articles": ContentControl.Content = new ArticleView();
                        break;
                    case "Matériels entrants": ContentControl.Content = new MaterielInView();
                        break;
                    case "Bons d'entrées": ContentControl.Content = new BonEntreeMagasinView();
                        break;
                    case "Bons de sortie": ContentControl.Content = new BonSortieView();
                        break;
                    case "Stock": ContentControl.Content = new PieceView();
                        break;
                    case "Types": ContentControl.Content = new TypeView();
                        break;
                }
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }
        }
    }
}
