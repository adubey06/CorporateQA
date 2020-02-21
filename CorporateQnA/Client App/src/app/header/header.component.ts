import { Component, OnInit } from '@angular/core';
import { formatDate } from '@angular/common';
import { Router } from '@angular/router';
import { LoginService } from '../shared/services/login.service';
import { UserInfo } from '../shared/models/user-info.model';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html'
})
export class HeaderComponent implements OnInit {

    currentDate: string;

    constructor(private router: Router, private loginService: LoginService, private spinnerService: NgxSpinnerService) { 

  }

    ngOnInit() {
        //this.spinnerService.show();
        this.loginService.getLoggedUserDetail().subscribe(res=>{
            this.loginService.loggedInUserDetail = res as UserInfo;
            this.spinnerService.hide();
      });
  
      this.currentDate = formatDate(new Date(), "dd MMM yyyy", 'en-us');
    }

    refresh() {
        this.router.navigate([""]).then(val => window.location.reload());
    }

 }
