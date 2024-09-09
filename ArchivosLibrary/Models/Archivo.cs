namespace ArchivosLibrary.Models
{
    public class Archivo
    {
        public int Id { get; set; }
        public string Nombre {  get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public bool Disponible { get; set; }
    }
}
