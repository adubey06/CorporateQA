import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { QuestionInfo } from '../../shared/models/question-info.model';
import { QuestionInfoService } from 'src/app/shared/services/question-info.service';
import { AnswerInfo } from 'src/app/shared/models/answer-info.model';
import { AnswerInfoService } from 'src/app/shared/services/answer-info.service';
import { ActivatedRoute, Params } from '@angular/router';
import { FormGroup, FormControl, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { Answer } from 'src/app/shared/models/answer.model';
import { LoginService } from 'src/app/shared/services/login.service';
import { QuestionActivityService } from '../../shared/services/question-activity.service';
import { AnswerActivityService } from 'src/app/shared/services/answer-activity.service';
import { UserInfo } from 'src/app/shared/models/user-info.model';


@Component({
    selector: 'app-question-detail',
    templateUrl: './question-detail.component.html'
})
export class QuestionDetailComponent implements OnInit {

    question: QuestionInfo=new QuestionInfo();
    answers: AnswerInfo[] = [];
    answerForm: FormGroup;
    userId ?: number;
    answer: Answer= new Answer();
    likeLock:boolean = true;
    dislikeLock: boolean = true;
    markBestLock: boolean = true;

    constructor(private formBuilder: FormBuilder,private answerActivity:AnswerActivityService, private questionActivity: QuestionActivityService, private loginService: LoginService, private questionInfoService: QuestionInfoService, private answerInfoService: AnswerInfoService, private route: ActivatedRoute) { }
    ngOnInit() {
        this.loginService.getLoggedUserDetail().subscribe(res=>{
            this.loginService.loggedInUserDetail = res as UserInfo;
            this.userId = (res as UserInfo).id;
        });

        this.route.params.subscribe((params: Params) => {
            let id = +params['id'];
            this.questionActivity.viewActivity(id).subscribe(
                res => {
                    this.questionInfoService.getAllQuestions().subscribe(res => {
                        this.questionInfoService.questionList = (res as QuestionInfo[]);
                    });
                });
            this.questionInfoService.getQuestion(id).subscribe(res => {
                this.question = res as QuestionInfo;
            });
            this.answerInfoService.getAllAnswers(id).subscribe(res => {
                this.answers = (res as AnswerInfo[]);
            });

        });

        this.answerForm = this.formBuilder.group({
            description: ['', [Validators.required, isNotSpaceString]],
        });

    }

    like(answer:AnswerInfo){
        if(this.likeLock){
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

    dislike(answer:AnswerInfo){
        if(this.dislikeLock){
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

    markBest(answer: AnswerInfo){
        if (this.markBestLock) {
            this.markBestLock = false;
            this.answerActivity.markBestActivity(answer.id).subscribe(res => {
                this.answerInfoService.getAllAnswers(this.question.id).subscribe(res => {
                    this.answers = (res as AnswerInfo[]);
                    this.markBestLock = true;
                    this.questionInfoService.getAllQuestions().subscribe(res => {
                        this.questionInfoService.questionList = (res as QuestionInfo[]);
                    });
                    this.questionInfoService.getQuestion(this.question.id).subscribe(res => {
                        this.question = res as QuestionInfo;
                    });
                });
            });  
        }
    }

    onSubmit() {
        this.answer.description = this.answerForm.controls.description.value;
        this.answer.answeredBy = this.loginService.loggedInUserDetail.id;
        this.answer.answeredOn = new Date();
        this.answer.questionId = this.question.id;
        
        this.answerInfoService.addAnswer(this.answer).subscribe(res => {
        this.answerForm.controls.description.setValue('');
        this.questionInfoService.getQuestion(this.question.id).subscribe(res => {
            this.question = res as QuestionInfo;
        });
        this.answerInfoService.getAllAnswers(this.question.id).subscribe(res => {
            this.answers = (res as AnswerInfo[]);
        });

        this.questionInfoService.getAllQuestions().subscribe(res => {
            this.questionInfoService.questionList = (res as QuestionInfo[]);
        });

        });
    }
}

function isNotSpaceString(control: AbstractControl): { [key: string]: any } | null {
    let inputString: string = control.value;
    if (inputString != null) {
        inputString = inputString.replace(/<[^>]*>/g, '');
    }

    if (inputString == null || inputString.trim().length == 0)
        return { 'IsNotSpaceString': 'Invalid Answer' };
    else
        return null;
}