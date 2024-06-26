namespace Scan.Scans
{
    public class ScanInfo
    {
        public Guid ScanId { get; set; }
        public string NetworkRange { get; set; }
        public DateTime StartTime { get; set; }
        public bool IsRunning { get; set; }
    }
}
