namespace Lists.Processor
{
    public interface IReferenceDataProvider

    {
        T ValueOrDefault<T>(string key, T defaultValue);

        void Reload();
    }
}