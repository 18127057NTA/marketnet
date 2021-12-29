import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';
import { ISupplier } from '../shared/models/suppliers';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  //Tìm kiếm
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  //Danh sách sản phẩm
  products: IProduct[];
  //Danh sách nhà cung cấp
  suppliers: ISupplier[];
  //Loại sản phẩm
  types: IType[];

  //Paging, Sorting
  shopParams = new ShopParams();
  totalCount: number;

  sortOptions = [
    { name: 'Tự nhiên', value: 'name' },
    { name: 'Giá thấp tới cao', value: 'priceAsc' },
    { name: 'Giá cao tới thấp', value: 'priceDesc' },
  ];

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getProducts();
    this.getSuppliers();
    this.getTypes();
  }
  //Lấy danh sách sản phẩm
  getProducts() {
    this.shopService
      // .getProducts(this.supplierIdSelected, this.typeIdSelected, this.sortSelected) //trước đó 
      .getProducts(this.shopParams)
      .subscribe(
        (response) => {
          this.products = response.data;
          //Paging
          this.shopParams.pageNumber = response.pageIndex;
          this.shopParams.pageSize = response.pageSize;
          this.totalCount = response.count;
        },
        (error) => {
          console.log(error);
        }
      );
  }
  //Lấy danh sách nhà cung cấp
  getSuppliers() {
    this.shopService.getSuppliers().subscribe(
      (response) => {
        //this.suppliers = response.data;// Trước đó
        this.suppliers = [
          {
            id: 0,
            name: 'Tất cả',
            email: null,
            phone: null,
            password: null,
            createdDate: null,
            updatedDate: null,
          },
          ...response,
        ];
      },
      (error) => {
        console.log(error);
      }
    );
  }
  //Lấy danh sách nhà cung cấp
  getTypes() {
    this.shopService.getTypes().subscribe(
      (response) => {
        this.types = [
          { id: 0, name: 'Tất cả', createdDate: null, updatedDate: null },
          ...response,
        ];
      },
      (error) => {
        console.log(error);
      }
    );
  }

  //Chọn sản phẩm theo nhà cung cấp nào đó
  onSupplierSelected(supplierId: number) {
    this.shopParams.supplierId = supplierId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }
  //Chọn sản phẩm theo loại nào đó
  onTypeSelected(typeId: number) {
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }
  //Sắp xếp sản phẩm theo chế độ
  onSortSelected(sort: string){
    this.shopParams.sort = sort;
    this.getProducts();
  }
  
  onPageChanged(event: any){
    if(this.shopParams.pageNumber != event){
      this.shopParams.pageNumber = event;
      this.getProducts();
    }
  }

  //Tìm kiếm
  onSearch(){
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }
  //Đặt lại
  onReset(){
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}