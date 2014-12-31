using System.Collections.Generic;
namespace GM.UI
{
    public class EtatMateriel
    {
        static List<EtatMateriel> GetValue()
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
                    Etat = "Bon"
                },
                new EtatMateriel
                {
                    Id = 3,
                    Etat = "mauvais"
                },
                new EtatMateriel
                {
                    Id = 4,
                    Etat = "Occasion"
                },
                 new EtatMateriel
                {
                    Id = 4,
                    Etat = "Défectueux"
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
