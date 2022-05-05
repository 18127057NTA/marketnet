import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, of, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IAddress } from '../shared/models/address';
import { IUser } from '../shared/models/user';
import { INgtiem } from 'src/app/shared/models/vnvc-models/ngtiem';
import { BasketService } from 'src/app/basket/basket.service';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<IUser>(1); // null -> ?
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router, private basketService: BasketService) {}

  loadCurrentUser(token: string) {
    if (token == null) {
      this.currentUserSource.next(null);
      return of(null);
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.get(this.baseUrl + 'account', { headers }).pipe(
      map((user: IUser) => {
        if (user) {
          //localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }

  login(values: any) {

    /*let maKhVip = {...values};
    maKhVip.maGioHang = this.basketService.getCurrentBasketValue().id;*/

    let maKHVip = {
      maVip : values.maVip,
      maGioHang : this.basketService.getCurrentBasketValue().id
    };

    return this.http.post(this.baseUrl + 'account/login', maKHVip);/*.pipe(
      map((user: IUser) => {
        //localStorage.setItem('token', user.token);
        this.currentUserSource.next(user);
      })
    );*/
  }

  register(values: any) {
    /*return this.http.post(this.baseUrl + 'account/register', values).pipe(
      map((user: IUser) => {
        localStorage.setItem('token', user.token);
        this.currentUserSource.next(user);
      })
    );*/
    //Lấy mã giỏ hàng
    let ttNgTiem = {...values};
    ttNgTiem.maGioHang = this.basketService.getCurrentBasketValue().id;
    return this.http.post(this.baseUrl + 'account/register', ttNgTiem);
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  checkEmailExist(email: string) {
    return this.http.get(this.baseUrl + 'account/emailexists?email=' + email);
  }

  getUserAddress() {
    return this.http.get<IAddress>(this.baseUrl + 'account/address');
  }

  updateUserAddress(address: IAddress) {
    return this.http.put<IAddress>(this.baseUrl + 'account/address', address);
  }
}
