namespace GM.Entity.Models
{
    public class SousService
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public int DepartementId { get; set; }
        public int ServiceId { get; set; }
        public Departement Departement { get; set; }
        public Service Service { get; set; }


    }
}
