export class ShopParams {
  //Mặt định chọn tất cả =0, biến filter
  supplierId = 0;
  typeId = 0;

  //Sort
  sort= 'name';
  //Paging
  pageNumber = 1;
  pageSize = 6;
  
  //Tìm kiếm
  search: string;
}
