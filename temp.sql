INSERT INTO khachHang(Id, Cccd, DiaChi, Email, HoTen, Sdt)
	VALUES (1,'52200009715','302 LE VAN LUONG','VanA@gmail.com','Tran Van A','0775463089'),
			(2,'62300009820','129 NGUYEN THI MINH KHAI','LeB@gmail.com','Ngo Le B','0784561102'),
			(3,'41200000020','242 NGUYEN VAN CU','HuyC@gmail.com','Nguyen Huy C','0797586908'),
			(4,'12200007135','02 SONG HANH','QuangD@gmail.com','Truong Quang D','0376276462'),
			(5,'38200008246','253 DIEN BIEN PHU','HoangE@gmail.com','Luu Hoang E','0382455252'),
			(6,'83200005190','157 HOANG VAN THU','MinhF@gmail.com','Tran Minh F','0394568754')
-- SQLite
INSERT INTO VIP(Id,KhachHangId,KhachHangMaVip,NgayBD,NgayKT)
	VALUES (1,1,'KH001','01/01/2019','01/01/2023'),
			(2,3,'KH003','01/01/2020','12/12/2023')