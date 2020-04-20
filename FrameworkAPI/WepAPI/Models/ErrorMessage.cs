namespace WepAPI.Models
{
    public class ErrorMessage
    {
        public string Message { get; set; }
        public object ModelState { get; set; }
        public ErrorMessage jsonConvert(string jsoninput)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorMessage>(jsoninput);
        }
    }
}