import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IPagination } from './shared/models/pagination';
import { IProduct } from './shared/models/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
//Khi chạy trang chủ - OnInit
export class AppComponent implements OnInit {
  //Tên thương hiệu
  title = 'Marketnet';

  //products: any[]; // Trước đó
  products: IProduct[];

  //constructor(private http: HttpClient) {}//Trước đó
  constructor(){}

  //Lấy danh sách sản phẩm,pageSize tối đa là 20 mà để dôi ra 50, khi mới vô root
  ngOnInit(): void {
    /*this.http.get('https://localhost:5001/api/products?pageSize=50').subscribe(
      //(response: any) => { // trước đó
      (response: IPagination) => {
        this.products = response.data;
        //console.log(response); //trước đó
      },
      (error) => {
        console.log(error);
      }
    );*/ //Trước đó

  }
}
