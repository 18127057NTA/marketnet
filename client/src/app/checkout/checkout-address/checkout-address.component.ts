import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/account/account.service';
import { BasketService } from 'src/app/basket/basket.service';
import { IAddress } from 'src/app/shared/models/address';
import { IBasket } from 'src/app/shared/models/basket';
import { IChiNhanh } from 'src/app/shared/models/vnvc-models/chinhanh';
import { ITinhThanhTiem } from 'src/app/shared/models/vnvc-models/tinhthanhtiem';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.scss']
})
export class CheckoutAddressComponent implements OnInit {
  @Input() checkoutForm: FormGroup;
  //Danh sách tỉnh thành
  tinhThanhList: ITinhThanhTiem[];
  chiNhanhList: IChiNhanh[];
  thongTinNguoiMua: IAddress;

  constructor(
    private accountService: AccountService,
    private toastr: ToastrService, 
    private basketService: BasketService) { }

  ngOnInit(): void {
    //Soạn danh sách tỉnh thành trong này
    this.tinhThanhList = [
      {
        id: 1,
        tenTinhThanh: "TP HCM"
      },
      {
        id: 2,
        tenTinhThanh: "HÀ NỘI"
      },
      {
        id: 3,
        tenTinhThanh: "ĐÀ NẴNG"
      },
      {
        id: 4,
        tenTinhThanh: "HẢI PHÒNG"
      },
      {
        id: 5,
        tenTinhThanh: "CẦN THƠ"
      }
    ];
    this.chiNhanhList = [
      {
        id: 'Q7001',
        tenChiNhanh: 'VNVC LE VAN LUONG QUAN 7',
        tenTinhThanh: 'TP HCM'
      },
      {
        id: 'Q1001',
        tenChiNhanh: 'VNVC DONG KHOI QUAN 1',
        tenTinhThanh: 'TP HCM'
      },
      {
        id: 'Q1002',
        tenChiNhanh: 'VNVC NGUYEN TRAI QUAN 1',
        tenTinhThanh: 'TP HCM'
      },
      {
        id: 'Q2001',
        tenChiNhanh: 'VNVC SONG HANH QUAN 2',
        tenTinhThanh: 'TP HCM'
      },
      {
        id: 'Q3001',
        tenChiNhanh: 'VNVC DIEN BIEN PHU QUAN 3',
        tenTinhThanh: 'TP HCM'
      },
      {
        id: 'QTB001',
        tenChiNhanh: 'VNVC CONG HOA QUAN TAN BINH',
        tenTinhThanh: 'TP HCM'
      },

    ];
  }

  saveUserAddress() {
    this.thongTinNguoiMua = this.checkoutForm.get('addressForm').value;
    this.thongTinNguoiMua.maGiohang = this.basketService.getCurrentBasketValue().id;
    //test
    console.log(this.thongTinNguoiMua);
    //product
    this.accountService.updateUserAddress(this.thongTinNguoiMua).subscribe(() => {
      this.toastr.success('Thông tin đã được lưu!');
    }, error => {
      this.toastr.error(error.message);
      console.log(error);
    })
  }

}