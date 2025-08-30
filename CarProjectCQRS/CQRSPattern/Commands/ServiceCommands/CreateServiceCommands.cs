namespace CarProjectCQRS.CQRSPattern.Commands.ServiceCommands
{
    public class CreateServiceCommands
    {
       
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string IconTitle { get; set; }
        public string IconSubtitle { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
