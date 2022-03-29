import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { BreadcrumbService } from 'xng-breadcrumb';
import { BasketService } from 'src/app/basket/basket.service';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;
  quantity = 1;

  constructor(
    private shopService: ShopService,
    private activatedRoute: ActivatedRoute,
    private bcService: BreadcrumbService,
    private basketService: BasketService,
    private sanitizer:DomSanitizer
  ) {
    this.bcService.set('@productDetails', ' ');
  }

  ngOnInit(): void {
    this.loadProduct();
  }
  //Thêm mặt hàng vào sản phẩm
  addItemToBasket() {
    this.basketService.addItemToBasket(this.product, this.quantity);
  }
  //Tăng số lượng sản phẩm
  incrementQuantity() {
    this.quantity++;
  }
  //Giảm số lượng sản phẩm
  decrementQuantity() {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }
  //Tải chi tiết sản phẩm lên
  loadProduct() {
    this.shopService
      .getProduct(this.activatedRoute.snapshot.paramMap.get('id')) // Trước đó: +this.activatedRoute.snapshot.paramMap.get('id')
      .subscribe(
        (product) => {
          this.product = product;
          //this.product.moTaThongTin = this.sanitizer.bypassSecurityTrustHtml(product.moTaThongTin);
          this.bcService.set('@productDetails', product.ten); //product.name
        },
        (error) => {
          console.log(error);
        }
      );
  }
}
