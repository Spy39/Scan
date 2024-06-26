using MediatR;

namespace Scan.Scans.Queries.GetScans
{
	public class GetScansQuery : IRequest<List<ScanInfo>>
	{
	}
}
