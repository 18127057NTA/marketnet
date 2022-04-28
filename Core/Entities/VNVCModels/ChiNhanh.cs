namespace Core.Entities.VNVCModels
{
    public class ChiNhanh
    {
        public string Id {get; set;}
        public string DiaChi {get; set;}
        public string Ten {get; set;}
        public TinhThanh TinhThanh {get; set;}
        public int TinhThanhId {get; set;}
    }
}