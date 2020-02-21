import { Injectable, OnInit } from '@angular/core';
import { UserInfo } from '../models/user-info.model';
import { HttpClient } from '@angular/common/http';


@Injectable({
    providedIn: 'root'
})
export class LoginService {
    loggedInUserDetail: UserInfo = new UserInfo();
    private routedUrl = "api/LoginInfo";

    constructor(private http: HttpClient) {

    }
   
    getLoggedUserDetail(){
        return this.http.get(this.routedUrl);
    }
}
