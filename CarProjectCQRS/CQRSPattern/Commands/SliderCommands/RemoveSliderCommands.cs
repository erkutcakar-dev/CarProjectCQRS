namespace CarProjectCQRS.CQRSPattern.Commands.SliderCommands
{
    public class RemoveSliderCommands
    {
        public int SliderId { get; set; }
        
        public RemoveSliderCommands()
        {
        }
        
        public RemoveSliderCommands(int sliderId)
        {
            SliderId = sliderId;
        }
    }
}
