using MediatR;

namespace Scan.Scans.Queries.GetScans
{
	public class GetScansQueryHandler : IRequestHandler<GetScansQuery, List<ScanInfo>>
	{
		public Task<List<ScanInfo>> Handle(GetScansQuery request, CancellationToken cancellationToken)
		{
			var scans = ScansContainer.Scans
				.Where(s => s.IsRunning)
				.ToList();

			return Task.FromResult(scans);
		}
	}
}
