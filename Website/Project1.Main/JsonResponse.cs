
namespace Project1.Main {

    public class JsonResponse {

        public static readonly JsonResponse Success = new JsonResponse (true, "Success!");

        public bool SuccessFlag { get; private set; }
        public string ResponseText { get; private set; }

        public JsonResponse (bool successFlag, string responseText) {

            SuccessFlag = successFlag;
            ResponseText = responseText;
        }
    }
}
