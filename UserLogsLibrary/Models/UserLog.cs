namespace UserLogsLibrary.Models
{
    public class UserLog
    {
        public int Id { get; set; }
        public int Usuario_Id { get; set; }
        public int Archivo_Id { get; set; }
        public string Accion {  get; set; }
    }
}
