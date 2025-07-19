namespace AMSWebApi.ViewModel
{
    public class AssetViewModel
    {
        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public int PurchaseOrderId { get; set; }
        public string PurchaseOrderStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AssetStatus { get; set; }
    }
}
