using MediatR;

namespace Scan.Scans.Commands.StopScan
{
	public class StopScanCommand : IRequest
	{
		public Guid ScanId { get; set; }
	}
}
