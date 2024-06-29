using Lextm.SharpSnmpLib.Messaging;
using Lextm.SharpSnmpLib;
using MediatR;
using System.Net;


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
				StartIp = request.StartIp,
				EndIp = request.EndIp,
				StartTime = DateTime.UtcNow,
				IsRunning = true
			};

			ScansContainer.Scans.Add(scanInfo);

			//Task.Run(() => StartScanning(scanInfo));
			StartScanning(scanInfo);
			return Task.FromResult(scanId);
		}

		private IPAddress IncrementIpAddress(IPAddress address)
		{
			var bytes = address.GetAddressBytes();

			for (int i = bytes.Length - 1; i >= 0; i--)
			{
				if (bytes[i] < 255)
				{
					bytes[i]++;
					break;
				}

				bytes[i] = 0;
			}

			return new IPAddress(bytes);
		}

		private void StartScanning (ScanInfo scanInfo)
		{

			try
			{
				var startIp = IPAddress.Parse(scanInfo.StartIp);
				var endIp = IPAddress.Parse(scanInfo.EndIp);

				for (var ip = startIp; !ip.Equals(endIp); ip = IncrementIpAddress(ip))
				{
					var endpoint = new IPEndPoint(ip, 161);
					var community = new OctetString("public");

					try
					{
						var result = Messenger.Get(VersionCode.V2, endpoint, community,
							new List<Variable> { new Variable(new ObjectIdentifier("1.3.6.1.2.1.1.1.0")) },
							5000);

						foreach (var variable in result)
						{
							Console.WriteLine($"{variable.Id}: {variable.Data}");
						}
					}
					catch (Exception)
					{
                        Console.WriteLine($"Ip: {ip} - error" );
                    }



				}
			}
			catch (Exception e)
			{

			}



		}
	}
}
