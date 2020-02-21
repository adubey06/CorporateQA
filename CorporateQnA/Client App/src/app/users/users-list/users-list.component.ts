import { Component, OnInit } from '@angular/core';
import { UserInfoService } from '../../shared/services/user-info.service';
import { UserInfo } from '../../shared/models/user-info.model';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html'
})
export class UsersListComponent implements OnInit {
    usersData: UserInfo[] = [];
    constructor(private userInfoService: UserInfoService) { }

    ngOnInit() {
        this.userInfoService.getAllUsers().subscribe(
            res => {
                this.userInfoService.userList = (res as UserInfo[]);
                this.usersData = this.userInfoService.userList;
            }
        );
  }

    search(event: any) {
        this.userInfoService.getFilteredUser(event.target.value).subscribe(res => {
            this.userInfoService.userList = (res as UserInfo[]);
            this.usersData = (res as UserInfo[]);
        });
    }
}
