using MediatR;

namespace Scan.Scans.Commands.StartScan
{
	public class StartScanCommand : IRequest<Guid>
	{
		public string NetworkRange { get; set; }
	}
}
