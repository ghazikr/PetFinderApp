namespace PetFinderApp.Models
{
    public class Pet
    {
        public int id { get; set; }
        public string name { get; set; }
        public string breed { get; set; }
        public string gender { get; set; }
        public string ImageUrl { get; set; }
        public string health { get; set; }
        public string genderAndage
        {
            get { return gender + ", " + age; }
        }
        public int age { get; set; }
        public int height { get; set; }
        public int weight { get; set; }
        public int isAdopted { get; set; }
        public int isLost { get; set; }
        public int idUser { get; set; }
        public string OwnerEmail { get; set; }
    }
}
