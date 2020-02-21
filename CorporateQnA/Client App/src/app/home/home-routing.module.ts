import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home.component';
import { QuestionDetailComponent } from './question-detail/question-detail.component';
import { DefaultTemplateComponent } from './default-template/default-template.component';

const routes: Routes = [{ path: '', component: HomeComponent,
    children: [
        { path: "", component: DefaultTemplateComponent },
         {path:"question/:id" , component: QuestionDetailComponent}
] }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
