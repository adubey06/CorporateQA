import { Injectable } from '@angular/core';
import { UserInfo } from '../models/user-info.model';
import { HttpClient, HttpParams } from '@angular/common/http'
@Injectable({
  providedIn: 'root'
})
export class UserInfoService {
  userList: UserInfo[]= [];
    selectedUser: UserInfo;
    readonly routeUrl: string = 'api/UserInfo'
    constructor(private http: HttpClient) {
    }
    getAllUsers() {
        return this.http.get(this.routeUrl);
    }


    getFilteredUser(searchKey: string) {
        let params = new HttpParams().set('searchKey', searchKey);
        return this.http.get(this.routeUrl, {params })
    }


    getUserById(id: number) {
        return this.http.get(this.routeUrl + '/' + id);
    }
}
