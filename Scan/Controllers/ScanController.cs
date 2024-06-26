using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scan.Scans.Commands.StartScan;
using Scan.Scans.Commands.StopScan;
using Scan.Scans.Queries.GetScans;
using System.Net;
using System.Net.NetworkInformation;

namespace Scan.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ScanController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ScanController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("start")]
		public async Task<IActionResult> StartScan([FromBody] StartScanCommand command)
		{
			var scanId = await _mediator.Send(command);
			return Ok(scanId);
		}

		[HttpPost("stop")]
		public async Task<IActionResult> StopScan([FromBody] StopScanCommand command)
		{
			await _mediator.Send(command);
			return NoContent();
		}

		[HttpGet]
		public async Task<IActionResult> GetScans()
		{
			var scans = await _mediator.Send(new GetScansQuery());
			return Ok(scans);
		}
	}
}
