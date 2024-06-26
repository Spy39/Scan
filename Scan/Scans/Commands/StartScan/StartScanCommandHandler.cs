using MediatR;

namespace Scan.Scans.Commands.StartScan
{
	public class StartScanCommandHandler : IRequestHandler<StartScanCommand, Guid>
	{
		public Task<Guid> Handle(StartScanCommand request, CancellationToken cancellationToken)
		{
			var scanId = Guid.NewGuid();
			var scanInfo = new ScanInfo 
			{
				ScanId = scanId,
				NetworkRange = request.NetworkRange,
				StartTime = DateTime.UtcNow,
				IsRunning = true
			};

			ScansContainer.Scans.Add(scanInfo);

			Task.Run(() => StartScanning(scanInfo));
			return Task.FromResult(scanId);
		}

		private async Task StartScanning (ScanInfo scanInfo)
		{
			Console.WriteLine("Сканирование завершено!");
		}
	}
}
