

namespace CodeMonkeys___HVMPR_Project

{
    using CodeMonkeys___HVMPR_Project.Models;
    using Firebase.Database;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Reactive.Linq;
    using System.Threading.Tasks;


    public class Fb
    {
        public static List<Appointment> myAppointment = new List<Appointment>();

      //  public static void Main(string[] args)
        //{
         //   Run().Wait();
        //}


        private static async Task Run()
        {
            //******************** Initialization ***************************//
            var client = new FirebaseClient("https://cpts323battle.firebaseio.com/");
            HttpClient httpclient = new HttpClient();
            string selectedkey = "", responseString, companyId;
            FormUrlEncodedContent content;
            HttpResponseMessage response;


            //******************** Get initial list of Prospect ***********************//
            var Appointments = await client
               .Child("appointments")//Prospect list
               .OnceAsync<Appointment>();
            foreach (var appointment in Appointments)
            {
                Console.WriteLine($"OA1:{appointment.Key}:->{appointment.Object.acepted}");


                //create a ilist os appointments
                //smart selection to improve your profit
                //selectedkey = appointment.Key;
            }



            //******************** Get Prospect list one by one real time ***************************/
            var child = client.Child("appointments");
            var observable = child.AsObservable<Appointment>();
            int length = myAppointment.Count;
            int tempi = 0;
            //get a new appoinment in the list
            var subscriptionFree = observable
                .Where(f => !string.IsNullOrEmpty(f.Key)) // you get empty Key when there are no data on the server for specified node
                .Where(f => f.Object?.acepted == false)
                .Subscribe(appointment =>
                {
                    //find 
                    
                    for (int i = 0; i < length; i++)
                    {
                        if (myAppointment[i].key == appointment.Key)
                        {
                            tempi = i;
                        }
                    }

                    if (tempi > 0)
                    {
                        myAppointment[tempi] = appointment.Object;
                    }
                    else
                    {
                        appointment.Object.key = appointment.Key;
                        myAppointment.Add(appointment.Object);
                        //i diction key

                    }
                });

            //******************* Cloud Function subcribe a Company ****************/
            var company = new Company
            {
                companyName = "WSU VANS",
                status = "active"
            };

            var dictionary = new Dictionary<string, string>
            {
                { "companyName",company.companyName  },
                { "status",company.status}
            };

            content = new FormUrlEncodedContent(dictionary);
            response = await httpclient.PostAsync("https://us-central1-cpts323battle.cloudfunctions.net/reportCompany", content);
            responseString = await response.Content.ReadAsStringAsync();
            Response data = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(responseString);
            //Response message
            Console.WriteLine(data.message);
            Console.WriteLine(data.companyId);
            companyId = data.companyId;

            //******************* Call Cloud Function select a Appointment By Id ****************/
            dictionary = new Dictionary<string, string>
                     {
                         { "key",selectedkey  },
                         { "carPlate","AXL234"  },
                         { "did","2"},
                         { "company","Company_name"},
                         { "companyId",companyId},
                         { "carStars","4.8"},
                         { "image","http:.."}
                     };
            content = new FormUrlEncodedContent(dictionary);
            response = await httpclient.PostAsync("https://us-central1-cpts323battle.cloudfunctions.net/selectAppointmentById", content);
            responseString = await response.Content.ReadAsStringAsync();
            data = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(responseString);
            Console.WriteLine(data.message);

            //*******************Direct firebase modification example, this is uses only to report the appointment status ****************/
            var child2 = client.Child("/appointmentsStatus/" + selectedkey + "/status/0");
            var status = new Status
            {
                code = "100"
            };
            await child2.PutAsync(status.code);

            var child3 = client.Child("/appointmentsStatus/" + selectedkey + "/status/1");
            status = new Status
            {
                code = "110"
            };
            await child3.PutAsync(status.code);

            var child4 = client.Child("/appointmentsStatus/" + selectedkey + "/status/2");
            status = new Status
            {
                code = "120"
            };
            await child4.PutAsync(status.code);

            //******************* Call Cloud Function updatePosition ****************/

            dictionary = new Dictionary<string, string>
            {
                 { "key",selectedkey  },
                 { "did","12"},
                 { "companyId",companyId},
                 { "lat","46.271135"},
                 { "lng","-119.278556"}
            };

            content = new FormUrlEncodedContent(dictionary);
            response = await httpclient.PostAsync("https://us-central1-cpts323battle.cloudfunctions.net/updatePosition", content);
            responseString = await response.Content.ReadAsStringAsync();
            data = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(responseString);
            Console.WriteLine(data.message);
            Console.WriteLine("Current index for the point " + data.index);



            //******************* Call Cloud Function for finishAppointment ****************/

            dictionary = new Dictionary<string, string>
            {
                { "key", selectedkey  },
                { "did", "12"},
                { "companyId", companyId},
                { "lat", "10.1991"},
                { "lng", "-75.8890"}
            };

            content = new FormUrlEncodedContent(dictionary);
            response = await httpclient.PostAsync("https://us-central1-cpts323battle.cloudfunctions.net/finishAppointment", content);
            responseString = await response.Content.ReadAsStringAsync();
            data = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(responseString);
            Console.WriteLine(data.message);


            var stop = Console.ReadLine();

            subscriptionFree.Dispose();
            //subscriptionSelected.Dispose();
        }
    }
}
