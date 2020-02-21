import { Component, OnInit } from '@angular/core';
import { QuestionInfo } from 'src/app/shared/models/question-info.model';
import { AnswerInfo } from 'src/app/shared/models/answer-info.model';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Answer } from 'src/app/shared/models/answer.model';
import { QuestionActivityService } from 'src/app/shared/services/question-activity.service';
import { LoginService } from 'src/app/shared/services/login.service';
import { QuestionInfoService } from 'src/app/shared/services/question-info.service';
import { AnswerInfoService } from 'src/app/shared/services/answer-info.service';
import { ActivatedRoute, Params } from '@angular/router';
import { UserInfoService } from 'src/app/shared/services/user-info.service';
import { UserInfo } from 'src/app/shared/models/user-info.model';
import { AnswerActivityService } from 'src/app/shared/services/answer-activity.service';


@Component({
  selector: 'app-question-detail',
  templateUrl: './question-detail.component.html'
})
export class QuestionDetailComponent implements OnInit {
    question: QuestionInfo=new QuestionInfo();
    answers: AnswerInfo[] = [];
    answerForm: FormGroup; 
    answer: Answer = new Answer();
    userId?: number;
    questionId: number;
    markBestLock: boolean = true;
    likeLock: boolean = true;
    dislikeLock: boolean = true;

  constructor(private formBuilder: FormBuilder,private answerActivity:AnswerActivityService,private userInfoServices:UserInfoService,private questionActivity: QuestionActivityService, private loginService: LoginService, private questionInfoService: QuestionInfoService, private answerInfoService: AnswerInfoService, private route: ActivatedRoute) { }
  ngOnInit() {
      this.loginService.getLoggedUserDetail().subscribe(res => {
          this.loginService.loggedInUserDetail = res as UserInfo;
          this.userId = (res as UserInfo).id;
      });
      this.route.params.subscribe((params: Params) => {
          this.questionId = +params['questionId'];
          this.questionActivity.viewActivity(this.questionId).subscribe(
              res => {
                  this.questionInfoService.getQuestion(this.questionId).subscribe(res => {
                    this.question = res as QuestionInfo;
                    this.questionInfoService.getMyQuestions(this.question.askedById).subscribe(res => {
                        this.questionInfoService.questionList = (res as QuestionInfo[]);
                    });
                });
                  this.answerInfoService.getAllAnswers(this.questionId).subscribe(res => {
                    this.answers = (res as AnswerInfo[]);
                });

              });


      });

      this.answerForm = this.formBuilder.group({
          description: ['', [Validators.required]],
      });
  }

    like(answer: AnswerInfo) {
        if (this.likeLock) {
            this.likeLock = false;
            this.answerActivity.likeActivity(answer.id).subscribe(res=>{
                this.answerInfoService.getAnswer(answer.id).subscribe(res=>{
                    answer.likes = (res as AnswerInfo).likes;
                    answer.dislikes = (res as AnswerInfo).dislikes;
                    answer.status = (res as AnswerInfo).status;
                    this.likeLock = true;
                });

            });
    }
}

    dislike(answer: AnswerInfo) {
        if (this.dislikeLock) {
            this.dislikeLock = false;
            this.answerActivity.dislikeActivity(answer.id).subscribe(res=>{
                this.answerInfoService.getAnswer(answer.id).subscribe(res=>{
                    answer.likes = (res as AnswerInfo).likes;
                    answer.dislikes = (res as AnswerInfo).dislikes;
                    answer.status = (res as AnswerInfo).status;
                    this.dislikeLock = true;
                });
            });
        }
    }

    markBest(answer: AnswerInfo) {
        if (this.markBestLock) {
            this.markBestLock = false;
            this.answerActivity.markBestActivity(answer.id).subscribe(res => {
                this.questionInfoService.getQuestion(this.questionId).subscribe(res => {
                    this.question = res as QuestionInfo;
                    this.questionInfoService.getMyQuestions(this.question.askedById).subscribe(res => {
                        this.questionInfoService.questionList = (res as QuestionInfo[]);
                    });
                });
                this.answerInfoService.getAllAnswers(this.questionId).subscribe(res => {
                    this.answers = (res as AnswerInfo[]);
                });
                this.markBestLock = true;
            });
        }
    }

  onSubmit() {
      this.answer.description = this.answerForm.controls.description.value;
      this.answer.answeredBy = this.loginService.loggedInUserDetail.id;
      this.answer.answeredOn = new Date();
      this.answer.questionId = this.question.id;
      this.answerInfoService.addAnswer(this.answer).subscribe(res=>{
          this.answerForm.setValue({description:""});
          
          this.questionInfoService.getQuestion(this.questionId).subscribe(res => {
                  this.question = res as QuestionInfo;
                  this.userInfoServices.getUserById(this.question.askedById).subscribe(res=>{
                      this.userInfoServices.selectedUser = (res as UserInfo);
                  });
              });
          this.answerInfoService.getAllAnswers(this.questionId).subscribe(res => {
                  this.answers = (res as AnswerInfo[]);
              });


          });
  }
}