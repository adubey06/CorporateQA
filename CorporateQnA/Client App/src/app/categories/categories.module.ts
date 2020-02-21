import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoriesRoutingModule } from './categories-routing.module';
import { CategoriesComponent } from './categories.component';
import { FormsModule } from '@angular/forms';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { searchPipe } from '../shared/pipes/customSearchPipe.pipes';
import {  HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [CategoriesComponent, searchPipe],
  imports: [
      CommonModule,
      HttpClientModule,
      CategoriesRoutingModule,
      FormsModule,
      Ng2SearchPipeModule,
      ReactiveFormsModule
  ],
  exports: [
      CategoriesComponent
  ]

})
export class CategoriesModule { }
