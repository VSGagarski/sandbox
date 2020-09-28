using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MIS.LIB.KIS.Api;
using MIS.LIB.KIS.Client;
using MIS.LIB.KIS.Model;

namespace Example
{
    public class Example
    {

        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }
        static async Task MainAsync(string[] args)
        {

            Configuration config = new Configuration();
            config.BasePath = "http://mis.emsoft.ru/Scud";
            // Configure API key authorization: Bearer
            config.ApiKey.Add("ApiKey", "autosyskey");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.ApiKeyPrefix.Add("ApiKey", "Bearer");

            var apiInstance = new AppointmentApi(config);
            var doctorId = Guid.Parse("6a572b1f-2656-4b2e-9065-c23216f4cb0b"); // Guid | 
            var startPoint = Convert.ToDateTime("2020-08-20T19:20:30+01:00");  // DateTime? |  (optional) 
            var endPoint = Convert.ToDateTime("2020-09-28T19:20:30+01:00");  // DateTime? |  (optional) 

            try
            {
                // Get AppointmentModel by doctorId

                List<AppointmentModel> result = await apiInstance.ApiAppointmentDoctorDoctorIdGetAsync(doctorId, startPoint, endPoint);
                result.ForEach(s => Debug.WriteLine(s.ToString()));
                // Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AppointmentApi.ApiAppointmentDoctorDoctorIdGet: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }

        }
    }
}