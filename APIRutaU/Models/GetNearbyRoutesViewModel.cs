namespace APIRutaU.Models
{
    public class GetNearbyRoutesViewModel
    {
        public Guid? User_Id { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
    }
}
