namespace Core.Specifications
{
    public class ProductSpecPrams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 6;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public int? StoreId { get; set; }
        public int? TypeId { get; set; }
        public int? SupplierId { get; set; }
        public string Sort { get; set; }

        //Search
        private string _search;
        public string Search { 
            get => _search;
            set => _search = value.ToLower(); 
        }
    }
}