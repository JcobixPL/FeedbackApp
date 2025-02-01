namespace FeedbackApp.Models.StoredFunctionsAndProcedures
{
    public class UserWithProjectDto
    {
        public int Id_Uzytkownika { get; set; }  
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int Id_Projektu { get; set; }
        public string Nazwa_Projektu { get; set; }
    }
}
