namespace CarProjectCQRS.CQRSPattern.Commands.CarCommands
{
    public class RemoveCarCommands
    {
        public int CarId { get; set; }

        public RemoveCarCommands(int carId)
        {
            CarId = carId;
        }
    }
}
