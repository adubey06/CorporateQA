import { Component, OnInit, Input } from '@angular/core';
import { UserInfoService } from 'src/app/shared/services/user-info.service';
import { UserInfo } from '../../../shared/models/user-info.model';

@Component({
  selector: 'app-users-list-container',
  templateUrl: './users-list-container.component.html'
})
export class UsersListContainerComponent implements OnInit {
    @Input() usersdata: UserInfo[] = [];
  constructor() { }

  ngOnInit() {
  }

}
