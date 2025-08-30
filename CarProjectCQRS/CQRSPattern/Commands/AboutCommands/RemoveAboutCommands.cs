namespace CarProjectCQRS.CQRSPattern.Commands.AboutCommands
{
    public class RemoveAboutCommands
    {
        public int AboutId { get; set; }

        public RemoveAboutCommands(int aboutId)
        {
            AboutId = aboutId;
        }
    }
}
