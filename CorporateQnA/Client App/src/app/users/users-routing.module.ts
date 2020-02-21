import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { UsersComponent } from './users.component';
import { UsersListComponent } from './users-list/users-list.component';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { MyQuestionsComponent } from './user-detail/my-questions/my-questions.component';
import { DefaultTemplateComponent } from './user-detail/my-questions/default-template/default-template.component';
import { QuestionDetailComponent } from './user-detail/my-questions/question-detail/question-detail.component';
import { MyAnswersComponent } from './user-detail/my-answers/my-answers.component';
import { AnsweredQuestionDetailComponent } from './user-detail/my-answers/answered-question-detail/answered-question-detail.component';

const routes: Routes = [{ path: '', component: UsersComponent ,
children : [{
path : '' , component: UsersListComponent
},
{
    path: 'not-found', component: NotFoundComponent
},
{
  path:':id' , component: UserDetailComponent,
  children:[
    // {
    //    path:'', redirectTo:'all-questions',pathMatch:'full'
    // },
  {
    path: 'all-questions' , component: MyQuestionsComponent,
    children:[{
      path :'' ,component:DefaultTemplateComponent
    },
    {
      path:':questionId' , component:QuestionDetailComponent
    }
  ]
  },
  {
    path:'all-answers' , component:MyAnswersComponent,
    children:[
      {
        path:'',component:DefaultTemplateComponent
      },
      {
        path:":questionId/:answerId",component :AnsweredQuestionDetailComponent
      }
    ]
  }
]
},

]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
