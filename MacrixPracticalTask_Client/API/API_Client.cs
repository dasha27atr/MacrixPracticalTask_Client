using MacrixPracticalTask_Client.Models;
using MacrixPracticalTask_Client.Models.DTO;
using System.Text;

namespace MacrixPracticalTask_Client.API
{
    public class API_Client
    {
        private HttpClient client { get; set; }
        private Config.Config Config { get; set; }

        public API_Client()
        {
            Config = Utils.GetConfig();
            client = new();
        }

        public async Task<(bool, object)> GetAll(int pageNumber, int pageSize, string orderBy = "lastName")
        {
            string requestUri = Config.MainUrl + Config.MethodNames.GetAll.GetAllUrl + "?" + 
                Config.MethodNames.GetAll.PageNumber + "=" + pageNumber + "&" +
                Config.MethodNames.GetAll.PageSize + "=" + pageSize + "&" +
                Config.MethodNames.GetAll.OrderBy + "=" + orderBy;

            HttpResponseMessage response = await client.GetAsync(requestUri);
            string responseMessage = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var deserializedPerson = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PersonDTO>>(responseMessage);

                return (true, deserializedPerson);
            }
            else
            {
                return (false, "");
            }
        }

        public async Task<(bool, object)> GetPersonById(int personId)
        {
            string requestUri = Config.MainUrl + Config.MethodNames.GetPersonByIdUrl + "/" + personId;

            HttpResponseMessage response = await client.GetAsync(requestUri);
            string responseMessage = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var deserializedPerson = Newtonsoft.Json.JsonConvert.DeserializeObject<PersonDTO>(responseMessage);

                return (true, deserializedPerson);
            }
            else
            {
                return (false, new Error(response.ReasonPhrase ?? "", (int)response.StatusCode, responseMessage));
            }
        }

        public async Task<(bool, object)> CreatePerson(PersonForCreationDTO person)
        {
            string requestUri = Config.MainUrl + Config.MethodNames.CreatePersonUrl;

            var personJson = Newtonsoft.Json.JsonConvert.SerializeObject(person);
            HttpContent content = new StringContent(personJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(requestUri, content);
            string responseMessage = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var deserializedPerson = Newtonsoft.Json.JsonConvert.DeserializeObject<PersonDTO>(responseMessage);

                return (true, deserializedPerson);
            }
            else
            {
                return (false, new Error(response.ReasonPhrase ?? "", (int)response.StatusCode, responseMessage));
            }
        }

        public async Task<(bool, object)> UpdatePerson(int personId, PersonForCreationDTO person)
        {
            string requestUri = Config.MainUrl + Config.MethodNames.UpdatePersonUrl + "/" + personId;

            var personJson = Newtonsoft.Json.JsonConvert.SerializeObject(person);
            HttpContent content = new StringContent(personJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync(requestUri, content);
            string responseMessage = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return (true, "");
            }
            else
            {
                return (false, new Error(response.ReasonPhrase ?? "", (int)response.StatusCode, responseMessage));
            }
        }

        public async Task<(bool, object)> DeletePerson(int personId)
        {
            string requestUri = Config.MainUrl + Config.MethodNames.DeletePersonUrl + "/" + personId;

            HttpResponseMessage response = await client.DeleteAsync(requestUri);
            string responseMessage = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return (true, "");
            }
            else
            {
                return (false, new Error(response.ReasonPhrase ?? "", (int)response.StatusCode, responseMessage));
            }
        }
    }
}
