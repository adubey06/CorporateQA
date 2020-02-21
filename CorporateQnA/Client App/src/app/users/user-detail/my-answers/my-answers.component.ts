import { Component, OnInit } from '@angular/core';
import { QuestionInfoService } from 'src/app/shared/services/question-info.service';
import { LoginService } from 'src/app/shared/services/login.service';
import { Router } from '@angular/router';
import { AnsweredQuestionInfo } from 'src/app/shared/models/answered-question-info.model';

@Component({
  selector: 'app-my-answers',
  templateUrl: './my-answers.component.html'
})
export class MyAnswersComponent implements OnInit {
  routeUrl:string;
  constructor(private questionInfoService:QuestionInfoService, private loginService:LoginService, private router:Router) { }

  ngOnInit() {
    this.routeUrl = this.router.url;
    let id = Number(this.routeUrl.match(/(\d+)/)[0]);
    this.questionInfoService.getMyAnsweredQuestions(id).subscribe(res=>{
        this.questionInfoService.answeredQuestionList = (res as AnsweredQuestionInfo[]);
    });
  }

}
