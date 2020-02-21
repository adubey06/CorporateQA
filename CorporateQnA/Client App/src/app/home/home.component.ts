import { Component, OnInit } from '@angular/core';
import { QuestionInfo } from '../shared/models/question-info.model';
import { QuestionInfoService } from '../shared/services/question-info.service';
import { Question } from '../shared/models/question.model';
import { LoginService } from '../shared/services/login.service';
import { ShowOption } from '../shared/models/show-options.model';
import { SortByOption } from '../shared/models/sort-options.model';
import { UserInfo } from '../shared/models/user-info.model';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {

    constructor(private questionInfoServices: QuestionInfoService) { }

    ngOnInit() {

        this.questionInfoServices.selectedCategory = 0;
        this.questionInfoServices.selectedShowOption = ShowOption.All;
        this.questionInfoServices.selectedSortByOption = SortByOption.Recent;
        this.questionInfoServices.getAllQuestions().subscribe(res => {
            this.questionInfoServices.questionList = (res as QuestionInfo[]);

        });
    }
}
