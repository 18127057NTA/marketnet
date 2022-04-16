import { Component, OnInit } from '@angular/core';
import {
  AsyncValidatorFn,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { Observable, of, timer } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { AccountService } from '../account.service';
import { BasketService } from 'src/app/basket/basket.service';
import { INgtiem } from 'src/app/shared/models/vnvc-models/ngtiem';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  errors: string[];

  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    private router: Router,
    //Lấy mã giỏ hàng đang có sẵn
    private basketService: BasketService
  ) {}

  ngOnInit(): void {
    this.createRegisterForm();
  }

  createRegisterForm() {
    /*this.registerForm = this.fb.group({
      displayName: [null, [Validators.required]],
      email: [null, 
        [Validators.required, Validators
        .pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')],
        [this.validateEmailNotTaken()]
      ],
      phoneNumber: [null, Validators.required],
      password: [null, Validators.required]
    });*/

    this.registerForm = this.fb.group({
      hoVaTen: [
        null, 
        [
          Validators.required,
          
        ]
      ],
      ngaySinh: [
        null, 
        [
          Validators.required,
          //Validators.pattern('^\d{1,2}\/\d{1,2}\/\d{4}$'),
        ]
      ],
      gioiTinh: [
        null, 
        [
          Validators.required,
          Validators.pattern('^(?:n|N|nam|Nam|nữ|Nữ)$'),
        ]
      ],
      soDienThoai: [
        null,
        [
          Validators.required, 
          //Validators.pattern('^[0-9]d{2}-d{3}-d{4}$'),
        ],
      ],
      email: [
        null,
        [
          Validators.required,
          Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$'),
        ],
        [this.validateEmailNotTaken()],
      ],
      diaChi: [null, [Validators.required]],
      //Lấy giỏ hàng hiện tại
      maGioHang: this.basketService.getCurrentBasketValue().id
    });
  }

  //Tải lại form mới để thêm người tiêm
  onSubmit() {
    //C1
    //Đẩy người tiêm lên giỏ hàng
    //C2
    this.accountService.register(this.registerForm.value).subscribe(
      () => {
        console.log(this.registerForm.value);
        //this.router.navigateByUrl('/shop');
        this.router.navigateByUrl('/account/register');
      },
      (error) => {
        console.log(error);
        this.errors = error.errors;
      }
    );
    //C1
    //Có thểm thao tác với người tiêm mới tạo trong kết quả trả về bên trên
  }

  validateEmailNotTaken(): AsyncValidatorFn {
    return (control) => {
      return timer(500).pipe(
        switchMap(() => {
          if (!control.value) {
            return of(null);
          }
          return this.accountService.checkEmailExist(control.value).pipe(
            map((res) => {
              return res ? { emailExists: true } : null;
            })
          );
        })
      );
    };
  }
}
