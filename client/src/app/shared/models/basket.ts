import { v4 as uuidv4 } from 'uuid';

export interface IBasket {
  id: string; //Mã giỏ hàng
  items: IBasketItem[]; // Danh sách mặt hàng trong giỏ hàng
  //clientSecret?: string;
  //paymentIntentId?: string;
  //deliveryMethodId?: number;
  //shippingPrice?: number;
}

/*export interface IBasketItem {
  id: number;
  productName: string;
  unitPrice: number;
  quantity: number;
  pictureUrl: string;
  storeName: string;
  storeAddress: string;
  typeName: string;
  supplierName: string;
}*/

//Thông tin của mặt hàng trong giỏ hàng
export interface IBasketItem {
  id: string;
  ten: string;
  gia: number;
  soLuongGoi: number;
  hinhAnh: string;
}


export class Basket implements IBasket {
  id = uuidv4();
  items: IBasketItem[] = [];
}

//Tổng kết giỏ hàng
export interface IBasketTotals {
  //shipping: number;
  subtotal: number;
  total: number;
}
