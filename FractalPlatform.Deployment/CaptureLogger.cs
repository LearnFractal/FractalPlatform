using Microsoft.Build.Framework;
using System.Collections.Generic;

namespace FractalPlatform.Deployment
{
	internal class CaptureLogger : ILogger
	{
		public List<string> Errors { get; private set; } = new List<string>();
		public List<string> Warnings { get; private set; } = new List<string>();

		public LoggerVerbosity Verbosity { get; set; } = LoggerVerbosity.Normal;
		public string Parameters { get; set; }

		public void Initialize(IEventSource eventSource)
		{
			// Capture error messages
			eventSource.ErrorRaised += (sender, e) => Errors.Add(e.Message);

			// Capture warning messages
			eventSource.WarningRaised += (sender, e) => Warnings.Add(e.Message);
		}

		public void Shutdown() { }
	}
}