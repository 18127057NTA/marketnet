import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import {
  Basket,
  IBasket,
  IBasketItem,
  IBasketTotals,
} from '../shared/models/basket';
import { IDeliveryMethod } from '../shared/models/deliveryMethod';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root',
})
export class BasketService {
  baseUrl = environment.apiUrl;
  private basketSource = new BehaviorSubject<IBasket>(null);
  basket$ = this.basketSource.asObservable();

  private basketTotalSource = new BehaviorSubject<IBasketTotals>(null);
  basketTotal$ = this.basketTotalSource.asObservable();

  shipping = 0;

  constructor(private http: HttpClient) {}

  createPaymentIntent() {
    return this.http
      .post(this.baseUrl + 'payments/' + this.getCurrentBasketValue().id, {})
      .pipe(
        map((basket: IBasket) => {
          this.basketSource.next(basket);
          console.log(this.getCurrentBasketValue());
        })
      );
  }

  setShippingPrice(deliveryMethod: IDeliveryMethod) {
    this.shipping = deliveryMethod.price;
    const basket = this.getCurrentBasketValue();
    basket.deliveryMethodId = deliveryMethod.id;
    basket.shippingPrice = deliveryMethod.price;
    this.calculaTotals();
    this.setBasket(basket);
  }

  getBasket(id: string) {
    return this.http.get(this.baseUrl + 'basket?id=' + id).pipe(
      map((basket: IBasket) => {
        this.basketSource.next(basket);
        this.shipping = basket.shippingPrice;
        //console.log(this.getCurrentBasketValue());
        this.calculaTotals();
      })
    );
  }

  setBasket(basket: IBasket) {
    return this.http.post(this.baseUrl + 'basket', basket).subscribe(
      (response: IBasket) => {
        this.basketSource.next(response);
        //console.log(response);
        this.calculaTotals();
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getCurrentBasketValue() {
    return this.basketSource.value;
  }

  addItemToBasket(prodct: IProduct, quant = 1) {
    const itemToAdd: IBasketItem = this.mapProductToBasketItem(prodct, quant);
    const basket = this.getCurrentBasketValue() ?? this.createBasket();

    basket.items = this.addOrUpdateItem(basket.items, itemToAdd, quant);
    this.setBasket(basket);
  }
  /*
  //Tăng số lượng của một mặt hàng trong giỏ
  incrementItemQuantity(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();
    const foundItemIndex = basket.items.findIndex((x) => x.id === item.id);
    basket.items[foundItemIndex].quantity++;
    this.setBasket(basket);
  }
  //Giảm số lượng của một mặt hàng trong giỏ
  decrementItemQuantity(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();
    const foundItemIndex = basket.items.findIndex((x) => x.id === item.id);

    if (basket.items[foundItemIndex].quantity > 1) {
      basket.items[foundItemIndex].quantity--;
    } else {
      //Xóa mặt hàng khỏi giỏ
      this.removeItemFromBasket(item);
    }
  }*/
  //Xóa mặt hàng khỏi giỏ hàng
  removeItemFromBasket(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();
    if (basket.items.some((x) => x.id === item.id)) {
      basket.items = basket.items.filter((i) => i.id !== item.id);
      if (basket.items.length > 0) {
        this.setBasket(basket);
      } else {
        this.deleteBasket(basket);
      }
    }
  }

  //Xóa local basket
  deleteLocalBasket(id: string) {
    this.basketSource.next(null);
    this.basketTotalSource.next(null);
    localStorage.removeItem(`basket_${id}`); //localStorage.removeItem('basket_id');
  }

  //Hàm xóa giỏ hàng
  deleteBasket(basket: IBasket) {
    return this.http.delete(this.baseUrl + 'basket?id=' + basket.id).subscribe(
      () => {
        this.basketSource.next(null);
        this.basketTotalSource.next(null);
        localStorage.removeItem('basket_id');
      },
      (error) => {
        console.log(error);
      }
    );
  }

  private addOrUpdateItem(
    items: IBasketItem[],
    itemToAdd: IBasketItem,
    quant: number
  ): IBasketItem[] {
    console.log(items);

    const index = items.findIndex((i) => i.id === itemToAdd.id);
    if (index === -1) {
      //quantity = soLuongGoi
      itemToAdd.soLuongGoi = quant;
      items.push(itemToAdd);
    } else {
      items[index].soLuongGoi += quant;
    }
    return items;
  }

  private calculaTotals() {
    const basket = this.getCurrentBasketValue();
    //const shipping = 0;
    const shipping = this.shipping;
    const subtotal = basket.items.reduce(
      //gia = unitPrice, soLuongGoi = quantity
      (a, b) => b.gia * b.soLuongGoi + a,
      0
    );
    const total = subtotal + shipping;
    this.basketTotalSource.next({ shipping, total, subtotal });
  }

  private createBasket(): IBasket {
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id);

    return basket;
  }

  private mapProductToBasketItem(prodct: IProduct, quant: number): IBasketItem {
    /*return {
      id: prodct.id,
      productName: prodct.name,
      unitPrice: prodct.unitPrice,
      quantity: quant,
      pictureUrl: prodct.pictureUrl,
      storeName: prodct.store,
      storeAddress: prodct.storeAddress,
      typeName: prodct.productType,
      supplierName: prodct.supplierName,
    };*/

    return {
      id: prodct.id,
      ten: prodct.ten,
      gia: prodct.gia,
      soLuongGoi: quant,
      phongBenh: prodct.phongBenh,
      tongSoLieu: prodct.tongSoLieu,
      hinhAnh: prodct.hinhAnh,
    };
  }
}
