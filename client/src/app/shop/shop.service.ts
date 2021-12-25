import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '../shared/models/pagination';
import { IType } from '../shared/models/productType';
import { ISupplier } from '../shared/models/suppliers';
import { delay, map } from 'rxjs/operators';
import { ShopParams } from '../shared/models/shopParams';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) {}

  //Lấy danh sách sản phẩm
  //getProducts() {} // Trước đó
  //getProducts(supplierId?: number, typeId?: number, sort?: string) // trước đó
  getProducts(shopParams: ShopParams) {
    //Tham số trong đường link
    let params = new HttpParams();

    //Gắn thêm tham số vào đường link
    if (shopParams.supplierId !== 0) {
      params = params.append('supplierId', shopParams.supplierId.toString());
    }
    //Gắn thêm tham số vào đường link
    if (shopParams.typeId !== 0) {
      params = params.append('typeId', shopParams.typeId.toString());
    }
    //Gắn tham số sort vào đường link
    /*if(shopParams.sort){
      params = params.append('sort', shopParams.sort);
    }*/

    //Search param
    if (shopParams.search) {
      params = params.append('search', shopParams.search);
    }
    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber.toString());
    params = params.append('pageSize', shopParams.pageSize.toString()); //pageSize?

    //return this.http.get<IPagination>(this.baseUrl + 'products'); // Trước đó
    return this.http
      .get<IPagination>(this.baseUrl + 'products', {
        observe: 'response',
        params,
      })
      .pipe(
        map((response) => {
          return response.body;
        })
      );
  }
  //Lấy chi tiết sản phẩm
  getProduct(id: number) {
    return this.http.get<IProduct>(this.baseUrl + 'products/' + id);
  }

  //Lấy danh sách nhà cung cấp
  getSuppliers() {
    return this.http.get<ISupplier[]>(this.baseUrl + 'products/suppliers');
  }
  //Lấy danh sách của hàng
  //Lấy danh sách loại sản phẩm
  getTypes() {
    return this.http.get<IType[]>(this.baseUrl + 'products/types');
  }
}
