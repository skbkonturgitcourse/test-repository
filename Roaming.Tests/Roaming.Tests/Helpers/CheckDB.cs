using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Roaming.Tests;

public class CheckDB
{
    private RequestInfo info;

    public RequestInfo GetInfo(string email)
    {
        var uid = GetRequestUid(email);
        info = RequestFullInfo(uid);
        return info;
    }
    private string GetRequestUid(string email)
    {
        var request = WebRequest.Create($"https://qa-course.kontur.host/api/roaming/list?fromEmail={email}&count=1");
        var response = request.GetResponse();
        using (var stream = response.GetResponseStream())
        {
            var reader = new StreamReader(stream);
            var responseFromServer = reader.ReadToEnd();
            var json = JObject.Parse(responseFromServer);
            return json["items"].First["RequestUid"].ToString();
        }
    }
    
    private RequestInfo RequestFullInfo (string requestUid)
    {
        var request = WebRequest.Create($"https://qa-course.kontur.host/api/roaming/req?uid={requestUid}");
        var response = request.GetResponse();
        using (var stream = response.GetResponseStream())
        {
            var reader = new StreamReader(stream);
            var responseFromServer = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<RequestInfo>(responseFromServer);
        }
    }
}

public class RequestInfo
{
    public string RoamingRequestId { get; set; }
    public string Organisation { get; set; }
    public string Inn { get; set; }
    public string Kpp { get; set; }
    public string Fio { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string ProofMail { get; set; }
    public string Contragents { get; set; }
    public string CreatedDate { get; set; }
    public int Step { get; set; }
    public string RequestUid { get; set; }
    public bool ContragentsAreValid { get; set; }
}