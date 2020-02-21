import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { QuestionInfoService } from 'src/app/shared/services/question-info.service';
import { QuestionInfo } from 'src/app/shared/models/question-info.model';
import { LoginService } from 'src/app/shared/services/login.service';

@Component({
  selector: 'app-my-questions',
  templateUrl: './my-questions.component.html'
})
export class MyQuestionsComponent implements OnInit {
  routeUrl:string;

  constructor(private router:Router,private questionInfoServices : QuestionInfoService,private loginService:LoginService) { }

  ngOnInit() {
    this.routeUrl =this.router.url; 
    let id = Number(this.routeUrl.match(/(\d+)/)[0]);
    this.questionInfoServices.getMyQuestions(id).subscribe(res=>{
      this.questionInfoServices.questionList = res as QuestionInfo[];
    });
}

}
