import { Component, OnInit, Input } from '@angular/core';
import { QuestionInfo } from 'src/app/shared/models/question-info.model';
import { QuestionActivityService } from 'src/app/shared/services/question-activity.service';
import { QuestionInfoService } from 'src/app/shared/services/question-info.service';
import { UserInfoService } from 'src/app/shared/services/user-info.service';
import { UserInfo } from 'src/app/shared/models/user-info.model';
import { LoginService } from 'src/app/shared/services/login.service';

@Component({
  selector: 'app-question-list',
  templateUrl: './question-list.component.html',
})
export class QuestionListComponent implements OnInit {
  likeLock:boolean = true;
  dislikeLock:boolean = true;
  @Input() routeUrl:string;
  @Input() questions:QuestionInfo[] = [];
  constructor(private questionActivity:QuestionActivityService ,private loginService:LoginService,private questionServices: QuestionInfoService,private userInfoServices: UserInfoService) { }

  ngOnInit() {
    this.loginService.getLoggedUserDetail().subscribe(res=>{
        this.loginService.loggedInUserDetail = res as UserInfo;
    });
  }

  like(question: QuestionInfo) {
    if(this.likeLock){
        this.likeLock = false;
        this.questionActivity.likeActivity(question.id).subscribe(res => {
            this.questionServices.getQuestion(question.id).subscribe(
                res => {
                    question.upvotes = (res as QuestionInfo).upvotes;
                    question.status = (res as QuestionInfo).status;
                    this.userInfoServices.getUserById(question.askedById).subscribe(res=>{
                        this.userInfoServices.selectedUser = (res as UserInfo);
                    });
                    this.likeLock = true;
                });
            });
        }
    }

    dislike(question: QuestionInfo) {
        if(this.dislikeLock){
            this.dislikeLock = false;
            this.questionActivity.dislikeActivity(question.id).subscribe(res => {
                this.questionServices.getQuestion(question.id).subscribe(
                    res => {
                        question.upvotes = (res as QuestionInfo).upvotes;
                        question.status = (res as QuestionInfo).status;
                        this.userInfoServices.getUserById(question.askedById).subscribe(res=>{
                            this.userInfoServices.selectedUser = (res as UserInfo);
                            this.dislikeLock = true;
                        });
                
                    });
                });
        }
    }

}
