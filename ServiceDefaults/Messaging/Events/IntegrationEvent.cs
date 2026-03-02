namespace ServiceDefaults.Messaging.Events
{
    public record IntegrationEvent
    {
        public Guid EventId => Guid.NewGuid();
        public DateTime EventOccured => DateTime.Now;
        public string EventType => GetType().AssemblyQualifiedName;
    }
}
