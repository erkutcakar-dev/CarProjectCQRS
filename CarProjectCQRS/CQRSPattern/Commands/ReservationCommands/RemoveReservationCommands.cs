namespace CarProjectCQRS.CQRSPattern.Commands.ReservationCommands
{
    public class RemoveReservationCommands
    {
        public int ReservationId { get; set; }

        public RemoveReservationCommands(int reservationId)
        {
            ReservationId = reservationId;
        }


    }
}
