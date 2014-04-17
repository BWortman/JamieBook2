// IisExpressHelper.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace WebApi2Book.Web.Api.IntegrationTests
{


    // TODO: Troubleshoot...


    public class IisExpressHelper
    {
        private Process _iisProcess;
        private string _srcRootDirName;
        private string _targetApplication;
        private string _targetPort;

        public void StartServer()
        {
            _targetPort = ConfigurationManager.AppSettings.Get("TargetPort");
            _srcRootDirName = ConfigurationManager.AppSettings["IisExpressSrcRootDir"].ToLowerInvariant();

            var curDir = Directory.GetCurrentDirectory().ToLowerInvariant();
            var srcRootIndex = curDir.IndexOf(_srcRootDirName, StringComparison.Ordinal);

            if (srcRootIndex < 0)
            {
                // Testing is occurring from a different directory than source (TeamCity)
                // May need to revisit IisExpressSrcRootDir to be full path?
                return;
            }

            var srcRoot = Path.Combine(curDir.Substring(0, srcRootIndex), _srcRootDirName);
            _targetApplication = Path.Combine(srcRoot, "WebApi2Book.Web.Api");

            if (_iisProcess != null)
            {
                Trace.TraceInformation("Cleaning up IISExpress from previous run...");
                SafeCleanupIisProcess();
            }

            Trace.TraceInformation("Starting up IISExpress...");
            var thread = new Thread(StartIisExpress) {IsBackground = true};
            thread.Start();
        }

        public void StopServer()
        {
            SafeCleanupIisProcess();
        }

        private void StartIisExpress()
        {
            // Source: http://www.reimers.dk/jacob-reimers-blog/testing-your-web-application-with-iis-express-and-unit-tests

            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                ErrorDialog = true,
                LoadUserProfile = true,
                CreateNoWindow = true,
                UseShellExecute = false,
                Arguments = string.Format("/path:\"{0}\" /port:{1}", _targetApplication, _targetPort)
            };

            var programfiles = string.IsNullOrEmpty(startInfo.EnvironmentVariables["programfiles"])
                ? startInfo.EnvironmentVariables["programfiles(x86)"]
                : startInfo.EnvironmentVariables["programfiles"];

            startInfo.FileName = programfiles + "\\IIS Express\\iisexpress.exe";

            try
            {
                _iisProcess = new Process {StartInfo = startInfo};
                _iisProcess.Start();
                _iisProcess.WaitForExit();
            }
            catch (Exception e)
            {
                Trace.TraceError("Failure launching IISExpress: {0}", e);
                SafeCleanupIisProcess();
            }
        }

        private void SafeCleanupIisProcess()
        {
            try
            {
                _iisProcess.Close();
            }
            catch
            {
                // Eat it.
            }
            try
            {
                _iisProcess.Dispose();
            }
            catch
            {
                // Eat it.
            }
            try
            {
                _iisProcess = null;
            }
            catch
            {
                // Eat it.
            }
        }
    }
}