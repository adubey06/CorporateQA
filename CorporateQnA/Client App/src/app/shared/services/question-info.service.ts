import { Injectable, OnInit } from '@angular/core';
import { QuestionInfo } from '../models/question-info.model';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Question } from '../models/question.model';
import { ShowOption } from '../models/show-options.model';
import { SortByOption } from '../models/sort-options.model';
import { AnsweredQuestionInfo } from '../models/answered-question-info.model';
@Injectable({
    providedIn: 'root'
})
export class QuestionInfoService {
    questionList: QuestionInfo[] = [];
    answeredQuestionList: AnsweredQuestionInfo[]=[];
    searchText: string = "";
    selectedCategory: number;
    selectedQuestion: QuestionInfo;
    selectedShowOption: ShowOption;
    selectedSortByOption: SortByOption;
    base = "api/QuestionInfo";
    constructor(private http: HttpClient) {
        this.selectedShowOption = ShowOption.All;
        this.selectedSortByOption = SortByOption.Recent;
    }

    getAllQuestions() {
        let params = new HttpParams().set('searchKey', this.searchText).
            set('categoryId', this.selectedCategory.toString()).
            set('show', this.selectedShowOption.toString()).
            set('sortBy', this.selectedSortByOption.toString());

        return this.http.get(`${this.base}`, { params: params });
    }

    getMyQuestions(id:number){
        return this.http.get(`${this.base}/MyQuestion/${id}`);
    }

    getMyAnsweredQuestions(id:number){
        return this.http.get(`${this.base}/MyAnswered/${id}`);
    }

    getQuestion(id: number) {
        return this.http.get(`${this.base}/${id}`);
    }

    addQuestion(question: Question) {
        return this.http.post(this.base, question);
    }
}