import { v4 as uuidv4 } from 'uuid';

export interface IBasket {
  id: string;
  items: IBasketItem[];
  clientSecret?: string;
  paymentIntentId?: string;
  deliveryMethodId?: number;
  shippingPrice?: number;
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

export interface IBasketItem {
  id: string;
  ten: string;
  gia: number;
  soLuongGoi: number;
  phongBenh: string;
  tongSoLieu: number;
  hinhAnh: string;
}

export class Basket implements IBasket {
  id = uuidv4();
  items: IBasketItem[] = [];
}

export interface IBasketTotals {
  shipping: number;
  subtotal: number;
  total: number;
}
