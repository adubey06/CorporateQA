import { Component, OnInit } from '@angular/core';
import { LoginService } from './shared/services/login.service';
import { UserInfo } from './shared/models/user-info.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
    title = 'ClientApp';

    constructor(private login: LoginService) {

    }

    ngOnInit() {
        this.login.getLoggedUserDetail().subscribe(res => {
            this.login.loggedInUserDetail = (res as UserInfo);
        })
    }
}
