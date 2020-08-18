namespace SHPA.Blockchain.Server.ActionResult
{
    public class JsonResultModel<T>
    {
        public bool Success { get; set; }
        public string[] Errors { get; set; }
        public T Result { get; set; }
    }
}