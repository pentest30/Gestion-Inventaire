namespace GM.Entity.Models
{
    public class Type
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public int? CategorieId { get; set; }
        public int? SousCategorieId { get; set; }
        public Categorie Categorie { get; set; }
        public SousCategorie SousCategorie { get; set; }

    }
}
