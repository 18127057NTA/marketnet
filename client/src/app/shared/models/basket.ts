import {v4 as uuidv4} from 'uuid';

export interface IBasket {
  id: string;
  items: IBasketItem[];
}

export interface IBasketItem {
  id: number;
  productName: string;
  unitPrice: number;
  quantity: number;
  pictureUrl: string;
  storeName: string;
  storeAddress: string;
  typeName: string;
  supplierName: string;
}

export class Basket implements IBasket{
    id = uuidv4();
    items: IBasketItem[] = [];
}

export interface IBasketTotals{
  shipping: number;
  subtotal: number;
  total: number;
}