import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { BasketService } from 'src/app/basket/basket.service';
import { IDeliveryMethod } from 'src/app/shared/models/deliveryMethod';
import { IPTThanhToan } from 'src/app/shared/models/vnvc-models/ptthanhtoan';
import { CheckoutService } from '../checkout.service';

@Component({
  selector: 'app-checkout-delivery',
  templateUrl: './checkout-delivery.component.html',
  styleUrls: ['./checkout-delivery.component.scss']
})
export class CheckoutDeliveryComponent implements OnInit {
  @Input() checkoutForm: FormGroup;
  deliveryMethods: IDeliveryMethod[];
  //Danh sách phương thức thanh toán
  phuongThucTT: IPTThanhToan[];

  constructor(private checkoutService: CheckoutService, private basketService: BasketService) { }

  ngOnInit(): void {
    this.phuongThucTT = [
    {
      id: 1,
      ten: "Thanh toán bằng thẻ ATM"
    },
    {
      id: 2,
      ten: "Thanh toán bằng thẻ VISA/MASTER/JCB"
    },
    {
      id: 3,
      ten: "Thanh toán bằng thẻ thành viên"
    },
    {
      id: 4,
      ten: "Thanh toán qua chuyển khoản"
    },
    {
      id: 5,
      ten: "Thanh toán tại trung tâm"
    }
    ];
    /*this.checkoutService.getDeliveryMethods().subscribe((dm: IDeliveryMethod[]) => {
      this.deliveryMethods = dm;
    }, error => {
      console.log(error);
    })*/
  }

  setShippingPrice(pttt: IPTThanhToan) {
    this.basketService.setShippingPrice(pttt);
  }

}