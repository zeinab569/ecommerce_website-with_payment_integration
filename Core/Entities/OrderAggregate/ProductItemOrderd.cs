namespace Core.Entities.OrderAggregate
{
    public class ProductItemOrderd
    {
        public ProductItemOrderd()
        {
            
        }
        public ProductItemOrderd(int id,string name,string picurl)
        {
            ProductItemID = id;
            ProductName = name;
            PictureUrl = picurl;
        }
        public int ProductItemID { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}
