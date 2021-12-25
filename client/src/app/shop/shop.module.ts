import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { ProductItemComponent } from './product-item/product-item.component';
import { SharedModule } from '../shared/shared.module';
import { ProductDetailsComponent } from './product-details/product-details.component';
//import { RouterModule } from '@angular/router';// trước đó
import { ShopRoutingModule } from './shop-routing.module';

@NgModule({
  declarations: [ShopComponent, ProductItemComponent, ProductDetailsComponent],
  imports: [
    CommonModule, 
    SharedModule,
    //RouterModule, trước đó
    ShopRoutingModule
  ],
  //Xuất component
  //exports: [ShopComponent],
})
export class ShopModule {}
