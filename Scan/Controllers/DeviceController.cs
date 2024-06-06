using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.NetworkInformation;

namespace Scan.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DeviceController : ControllerBase
	{
		[HttpGet("scan")]
		public async Task<IEnumerable<DeviceInfo>> ScanNetwork()
		{
			var device = new List<DeviceInfo>();
			using (var ping = new Ping())
			{
				foreach (var host in GetHosts())
				{
					var reply = await ping.SendPingAsync(host, 1000);

					if(reply.Status == IPStatus.Success)
					{
						var deviceInfo = GetDeviceInfo(reply.Address);
						device.Add(deviceInfo);
					}
				}
			}
			return device;
		}

		private IEnumerable<string> GetHosts()
		{
			return new[] { "192.168.1.0/24" };
		}

		private DeviceInfo GetDeviceInfo(IPAddress address)
		{
			return new DeviceInfo
			{
				IPAddress = address.ToString(),
				MACAddress = GetMACAddress(address),
				Hostname = GetHostname(address),
				Manufacturer = "Unknown",
				Model = "Unknown",
				OperatingSystem = "Unknown",
			};
		}

		private string GetMACAddress(IPAddress address) 
		{
			return "00:00:00:00:00:00";
		}

		private string GetHostname(IPAddress address)
		{
			return address.ToString();
		}
	}
}
