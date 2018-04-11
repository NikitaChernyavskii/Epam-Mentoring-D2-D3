namespace PowerStateManagement.Models
{
    public struct SystemPowerInformation
    {
        ulong MaxIdlenessAllowed;
        ulong Idleness;
        ulong TimeRemaining;
        string CoolingMode;
    }
}
