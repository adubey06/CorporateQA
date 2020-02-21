import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home.component';
import { OptionsComponent } from './options/options.component';
import { QuestionListComponent } from './question-list/question-list.component';
import { QuestionDetailComponent } from './question-detail/question-detail.component';
import { DefaultTemplateComponent } from './default-template/default-template.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { QuillModule } from 'ngx-quill';
import { from } from 'rxjs';



@NgModule({
    declarations: [HomeComponent, OptionsComponent, QuestionListComponent, QuestionDetailComponent, DefaultTemplateComponent],
  imports: [
    CommonModule,
    HomeRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    QuillModule
  ],
  exports: [ 
  ]
})
export class HomeModule { }
