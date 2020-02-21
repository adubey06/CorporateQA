import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { QuestionInfoService } from 'src/app/shared/services/question-info.service';
import { CategoryInfo } from '../../shared/models/category-info.model';
import { CategoryDetailService } from '../../shared/services/category-detail.service';
import { ShowOption } from '../../shared/models/show-options.model';
import { QuestionInfo } from '../../shared/models/question-info.model';
import { SortByOption } from '../../shared/models/sort-options.model';
import { LoginService } from '../../shared/services/login.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { Question } from '../../shared/models/question.model';

@Component({
    selector: 'app-options',
    templateUrl: './options.component.html'
})
export class OptionsComponent implements OnInit {

    config = {
        toolbar: ".toolbar"
    }

    categories: CategoryInfo[] = [];

    question: Question = new Question();

    questionForm: FormGroup = this.fb.group({
        title: ['', [Validators.required, this.isNotSpaceString]],
        description: ['', [Validators.required, this.isNotSpaceString]],
        categoryId: ['', [Validators.required, Validators.minLength(1)]]
    });

    
    showOptions: {
        key: string;
        value: ShowOption;
    }[] = [{
            key: "All Questions",
            value: ShowOption.All
        },
        {
            key: "My Question",
            value: ShowOption.MyQuestions
        },
        {
            key: "My Participation",
            value: ShowOption.MyParticipation
        },
        ];

    sortByOptions: {
        key: string;
        value: SortByOption; }[] = [{
            key: "Recent",
            value: SortByOption.Recent
        }];

    constructor(private fb: FormBuilder, private questionServices: QuestionInfoService, private categoryServices: CategoryDetailService, private login: LoginService, private router: Router) { }

    ngOnInit() {

        this.categoryServices.getCategoryList().subscribe(res => {
            this.categories = res as CategoryInfo[];
        });  
    }

    onSearchText() {
        this.router.navigateByUrl('/home');
        this.questionServices.getAllQuestions().subscribe(res => {
            this.questionServices.questionList = res as QuestionInfo[];
        })
    }

    onChangeCategory() {
        this.router.navigateByUrl('/home');
        this.questionServices.getAllQuestions().subscribe(res => {
            this.questionServices.questionList = res as QuestionInfo[];
        });
    }

    onChangeShow() {
        this.router.navigateByUrl('/home');
        this.questionServices.getAllQuestions().subscribe(res => {
            this.questionServices.questionList = res as QuestionInfo[];
        })
    }

    onChangeSortBy() {
        this.router.navigateByUrl('/home');
        this.questionServices.getAllQuestions().subscribe(res => {
            this.questionServices.questionList = res as QuestionInfo[];
        })
    }

    onReset() {
        this.router.navigateByUrl('/home');
        this.questionServices.searchText = "";
        this.questionServices.selectedCategory = 0;
        this.questionServices.selectedShowOption = ShowOption.All;
        this.questionServices.selectedSortByOption = SortByOption.Recent;
        this.questionServices.getAllQuestions().subscribe(res => {
            this.questionServices.questionList = (res as QuestionInfo[]);
        })
    }

    onSubmit() {
        this.question.title = this.questionForm.controls.title.value;
        this.question.description = this.questionForm.controls.description.value;
        this.question.categoryId = Number(this.questionForm.controls.categoryId.value);
        this.question.askedBy = this.login.loggedInUserDetail.id;
        this.question.askedOn = new Date();
        console.log(this.question.description);
        this.questionServices.addQuestion(this.question).subscribe(res => {
            this.questionServices.getAllQuestions().subscribe(res => {
                this.questionServices.questionList = (res as QuestionInfo[]);
            });
        });
       
    }

    onDismiss() {
        this.questionForm.reset();
        this.questionForm.controls.categoryId.setValue("");
    }

    isNotSpaceString(control: AbstractControl): { [key: string]: any } | null {

        let inputString: string = control.value;
        if (inputString != null) {
            inputString = inputString.replace(/<[^>]*>/g, '');
        }

        if (inputString == null || inputString.trim().length == 0)
            return { 'IsNotSpaceString': 'Invalid Answer' };
        else
            return null;

    }
 
}
