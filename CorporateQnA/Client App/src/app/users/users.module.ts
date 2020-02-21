import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UsersRoutingModule } from './users-routing.module';
import { UsersComponent } from './users.component';
import { UsersListComponent } from './users-list/users-list.component';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { UsersListContainerComponent } from './users-list/users-list-container/users-list-container.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { MyQuestionsComponent } from './user-detail/my-questions/my-questions.component';
import { MyAnswersComponent } from './user-detail/my-answers/my-answers.component';
import { QuestionListComponent } from './user-detail/my-questions/question-list/question-list.component';
import { QuestionDetailComponent } from './user-detail/my-questions/question-detail/question-detail.component';
import { DefaultTemplateComponent } from './user-detail/my-questions/default-template/default-template.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AnsweredQuestionDetailComponent } from './user-detail/my-answers/answered-question-detail/answered-question-detail.component';
import { AnsweredQuestionListComponent } from './user-detail/my-answers/answered-question-list/answered-question-list.component';
import { QuillModule } from 'ngx-quill';


@NgModule({
  declarations: [UsersComponent, UsersListComponent, UserDetailComponent, UsersListContainerComponent, NotFoundComponent, MyQuestionsComponent, MyAnswersComponent, QuestionListComponent, QuestionDetailComponent, DefaultTemplateComponent, AnsweredQuestionDetailComponent, AnsweredQuestionListComponent],
  imports: [
    CommonModule,
    UsersRoutingModule,
    FormsModule,
      ReactiveFormsModule,
      QuillModule
  ]
})
export class UsersModule { }
