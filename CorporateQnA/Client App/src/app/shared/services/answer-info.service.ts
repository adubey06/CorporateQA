import { Injectable } from '@angular/core';
import { AnswerInfo } from '../models/answer-info.model';
import { HttpClient } from '@angular/common/http';
import { Answer } from '../models/answer.model';
@Injectable({
    providedIn: 'root'
})
export class AnswerInfoService {
    base = "api/AnswerInfo";
    answerList: AnswerInfo[] = [];
    constructor(private http: HttpClient) {
    }
    getAllAnswers(id: number) {
        return this.http.get(`${this.base}/${id}`);
    }

    getAnswer(id:number){
        return this.http.get(`${this.base}/Single/${id}`)
    }

    addAnswer(answer: Answer) {
        return this.http.post(this.base, answer);
    }
}