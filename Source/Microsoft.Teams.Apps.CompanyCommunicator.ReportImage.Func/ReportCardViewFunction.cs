using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Text;

namespace Microsoft.Teams.Apps.CompanyCommunicator.ReportImage.Func
{
    public static class ReportCardViewFunction
    {
        [FunctionName("ReportCardViewFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log, ExecutionContext context)
        {
            
            string notificationId = req.Query["notification"];

            log.LogInformation("Image hit:" + String.Join(",",req.Query) + " - " +  (String.IsNullOrEmpty(notificationId) ? "No Notification ID" : "Notification ID" + notificationId));

            var path = System.IO.Path.Combine(context.FunctionAppDirectory, "reportImage.jpg");
            var image = await File.ReadAllBytesAsync(path);
           
            
            var fileStreamResult = new FileContentResult(image, "image/jpeg");

            

            return fileStreamResult;
        }

        private static MemoryStream StringToMemoryStream(Encoding encoding, string source)
        {
            var content = encoding.GetBytes(source);
            return new MemoryStream(content);
        }
    }


}
