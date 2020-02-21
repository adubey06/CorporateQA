import { Injectable, OnInit } from '@angular/core';
import { QuestionActivity } from '../models/question-activity.model';
import { HttpClient } from '@angular/common/http';
import { LoginService } from './login.service';
import { Activity } from '../models/activity.model.ts';
import { UserInfo } from '../models/user-info.model';

@Injectable({
  providedIn: 'root'
})
export class QuestionActivityService {
  activity:QuestionActivity = new QuestionActivity();
  readonly base:string = "api/QuestionActivity";
  constructor(private http:HttpClient,private login:LoginService) { 
      this.activity.activityBy = this.login.loggedInUserDetail.id;
  }

  viewActivity(id:number){
    this.activity.activityPerformedOn=id;
    this.activity.activityOn = new Date();
    this.activity.type = Activity.View;
    return this.http.post(this.base,this.activity);
  }

  likeActivity(id:number){
    this.activity.activityPerformedOn=id;
    this.activity.activityOn = new Date();
    this.activity.type = Activity.Like;
    return this.http.post(this.base,this.activity);
  }

  dislikeActivity(id:number){
    this.activity.activityPerformedOn=id;
    this.activity.activityOn = new Date();
    this.activity.type = Activity.Dislike;
    return this.http.post(this.base,this.activity);
  }
}
