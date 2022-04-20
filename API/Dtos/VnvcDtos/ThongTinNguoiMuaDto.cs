using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.VnvcDtos
{
    public class ThongTinNguoiMuaDto
    {
        public string HoTen { get; set; }
        public string Sdt{ get; set; }
        public string Email { get; set; }
        public string Cccd { get; set; }
        public string DiaChi { get; set; }
        public int TinhThanhTiem { get; set; }
        public string ChiNhanhTiem { get; set; }
        public string MaGioHang {get; set;}
        
    }
}