import { Component, OnInit } from '@angular/core';
import { UserInfoService } from 'src/app/shared/services/user-info.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserInfo } from '../../shared/models/user-info.model';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html'
})
export class UserDetailComponent implements OnInit {
    constructor(private userInfoService: UserInfoService, private route: ActivatedRoute, private router: Router) { }

    ngOnInit() {
        this.route.params.subscribe(
            param => {
                let id = param['id'];
                this.userInfoService.getUserById(id).subscribe(
                    res => {
                        this.userInfoService.selectedUser = (res as UserInfo);
                    },
                    err => {
                        this.router.navigate(["/users/not-found"]);
                    }
                );

            }
        );
  }

}
