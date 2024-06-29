using MediatR;

namespace Scan.Scans.Commands.StartScan
{
	public class StartScanCommand : IRequest<Guid>
	{
		public string StartIp { get; set; }
		public string EndIp { get; set; }
	}
}
