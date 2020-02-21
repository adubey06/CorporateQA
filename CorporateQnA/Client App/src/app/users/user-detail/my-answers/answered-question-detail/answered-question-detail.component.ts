import { Component, OnInit } from '@angular/core';
import { QuestionInfo } from 'src/app/shared/models/question-info.model';
import { AnswerInfo } from 'src/app/shared/models/answer-info.model';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Answer } from 'src/app/shared/models/answer.model';
import { AnswerActivityService } from 'src/app/shared/services/answer-activity.service';
import { UserInfoService } from 'src/app/shared/services/user-info.service';
import { QuestionActivityService } from 'src/app/shared/services/question-activity.service';
import { LoginService } from 'src/app/shared/services/login.service';
import { QuestionInfoService } from 'src/app/shared/services/question-info.service';
import { AnswerInfoService } from 'src/app/shared/services/answer-info.service';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { UserInfo } from 'src/app/shared/models/user-info.model';
import { AnsweredQuestionInfo } from 'src/app/shared/models/answered-question-info.model';

@Component({
  selector: 'app-answered-question-detail',
  templateUrl: './answered-question-detail.component.html'
})
export class AnsweredQuestionDetailComponent implements OnInit {
  question:QuestionInfo= new QuestionInfo();
  answers: AnswerInfo[] = [];
  highlightedAnswer: number;
  answerForm: FormGroup; 
  answer: Answer= new Answer();
  askedById:number;
  constructor(private formBuilder: FormBuilder,private router:Router,private answerActivity:AnswerActivityService,private userInfoServices:UserInfoService,private questionActivity: QuestionActivityService, private loginService: LoginService, private questionInfoService: QuestionInfoService, private answerInfoService: AnswerInfoService, private route: ActivatedRoute) { }
  ngOnInit() {
    this.loginService.getLoggedUserDetail().subscribe(res=>{
        this.loginService.loggedInUserDetail = res as UserInfo;
    });
      this.route.params.subscribe((params: Params) => {
          let id = +params['questionId'];
          this.highlightedAnswer = +params['answerId'];
          this.askedById = Number(this.router.url.match(/(\d+)/)[0]);
          this.questionActivity.viewActivity(id).subscribe(
              res => {
                this.questionInfoService.getQuestion(id).subscribe(res => {
                    this.question = res as QuestionInfo;
                    this.questionInfoService.getMyAnsweredQuestions(this.askedById).subscribe(res => {
                        this.questionInfoService.answeredQuestionList = (res as AnsweredQuestionInfo[]);
                        console.log(res);
                    });
                });
                this.answerInfoService.getAllAnswers(id).subscribe(res => {
                    this.answers = (res as AnswerInfo[]);
                });

              });


      });

      this.answerForm = this.formBuilder.group({
          description: ['', [Validators.required]],
      });
  }

  like(answer:AnswerInfo){
    this.answerActivity.likeActivity(answer.id).subscribe(res=>{
        this.answerInfoService.getAnswer(answer.id).subscribe(res=>{
            answer.likes = (res as AnswerInfo).likes;
            answer.dislikes = (res as AnswerInfo).dislikes;
            answer.status = (res as AnswerInfo).status;
        });
    });
}

dislike(answer:AnswerInfo){
    this.answerActivity.dislikeActivity(answer.id).subscribe(res=>{
        this.answerInfoService.getAnswer(answer.id).subscribe(res=>{
            answer.likes = (res as AnswerInfo).likes;
            answer.dislikes = (res as AnswerInfo).dislikes;
            answer.status = (res as AnswerInfo).status;
        });
    });
}

  onSubmit() {
      this.answer.description = this.answerForm.controls.description.value;
      this.answer.answeredBy = this.loginService.loggedInUserDetail.id;
      this.answer.answeredOn = new Date();
      this.answer.questionId = this.question.id;
      this.answerInfoService.addAnswer(this.answer).subscribe(res=>{
          this.answerForm.setValue({description:""});
          this.route.params.subscribe((params: Params) => {
              let id = +params['questionId'];
              this.questionInfoService.getQuestion(id).subscribe(res => {
                  this.question = res as QuestionInfo;
                  this.userInfoServices.getUserById(this.userInfoServices.selectedUser.id).subscribe(res => {
                      this.userInfoServices.selectedUser = (res as UserInfo);
                  });
              });
              this.answerInfoService.getAllAnswers(id).subscribe(res => {
                  this.answers = (res as AnswerInfo[]);
              });


          });

      });
  }

}
