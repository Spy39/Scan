using MediatR;

namespace Scan.Scans.Commands.StopScan
{
	public class StopScanCommandHandler : IRequestHandler<StopScanCommand>
	{
		public Task Handle(StopScanCommand request, CancellationToken cancellationToken)
		{
			var scan = ScansContainer.Scans.FirstOrDefault(s => s.ScanId == request.ScanId);
			if (scan != null) 
			{
				scan.IsRunning = false;
			}

			return Task.CompletedTask;
		}
	}
}
