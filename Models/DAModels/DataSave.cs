namespace theCoffeeroom.Models.DAModels
{
    public class DataSave
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public bool Type { get; set; }
        public string Remark { get; internal set; }
        //0 - error
        //1 - success
    }
}
