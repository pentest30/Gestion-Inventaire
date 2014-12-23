

using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;

namespace GM.UI
{
    public class EtatMateriel
    {
        
         List<EtatMateriel> GetValue()
        {
            var  etats= new List<EtatMateriel>
            {
                new EtatMateriel
                {
                    Id = 1,
                    Etat = "Neuf"
                },
                new EtatMateriel
                {
                    Id = 2,
                    Etat = "Difectueux"
                },
                new EtatMateriel
                {
                    Id = 3,
                    Etat = "Réformé"
                },
                new EtatMateriel
                {
                    Id = 4,
                    Etat = "Occasion"
                }
            };
            return etats;
        }

        public int Id { get; set; }
        public string Etat { get; set; }

        public List<EtatMateriel> EtatMateriels
        {
            get
            {
                return GetValue();
                
            }
        }


    }


}
