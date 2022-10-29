namespace SauaS.InterProcessNotification.ProducerProcess.Model
{
    public class EventInfo
    {
        public string EventId { get; set; }
        public string Computer { get; set; }
        public string Source { get; set; }


        public override string ToString()
        {
            return $"EventId :[{EventId}] Computer :[{Computer}] Source :[{Source}]";
        }
    }
}
