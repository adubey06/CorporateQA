import { Component, OnInit } from '@angular/core';
import { CategoryDetailService } from '../shared/services/category-detail.service';
import { CategoryInfo } from '../shared/models/category-info.model';
import { Category } from '../shared/models/category.model';
import { FormGroup, Validators, AbstractControl, FormBuilder } from '@angular/forms';
import { LoginService } from '../shared/services/login.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
})
export class CategoriesComponent implements OnInit {
    show: boolean = false;
    searchText: string = "";  
    categoryList: CategoryInfo[];
    constructor(private fb: FormBuilder, private categoryDetailService: CategoryDetailService, private user: LoginService) { }
    added: boolean = true;
    showCategoryDetail : boolean = false;
    categoryDetail: Category = new Category();
    category: Category = new Category();
    categoryForm: FormGroup = this.fb.group({
        name: ['', [Validators.required, this.isNotSpaceString]],
        description: ['', [Validators.required, this.isNotSpaceString]],
    });; 

    fetchCategoryList() {
        this.categoryDetailService.getCategoryList().subscribe(res => {
            this.categoryList = res as CategoryInfo[];
        });
    }


    ngOnInit() {
        this.fetchCategoryList();
    }
   
    onClickCategory(val: Category) {
        this.categoryDetail = val;
        
    }
    

    onSubmit() {
        this.category.name = this.categoryForm.controls.name.value;
        this.category.description = this.categoryForm.controls.description.value;
        this.category.createdBy = this.user.loggedInUserDetail.id;
        this.category.createdOn = new Date();
        this.categoryDetailService.addCategory(this.category).subscribe(res => {
            this.fetchCategoryList();
        },
            err => {
                alert(err);
            }
        );   
    }

    onDismiss() {
        this.categoryForm.reset();
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
