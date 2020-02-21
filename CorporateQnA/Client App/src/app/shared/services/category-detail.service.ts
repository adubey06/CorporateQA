import { Injectable } from '@angular/core';
import { CategoryInfo } from '../models/category-info.model';
import { HttpClient } from '@angular/common/http';
import { Category } from '../models/category.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryDetailService {
    categoryList: CategoryInfo[] = [];
    readonly routeUrl: string = "api/CategoryInfo"
    constructor(private http: HttpClient) { 
    }

    getCategoryList() {
        return this.http.get(this.routeUrl);
    }

    addCategory(category: Category) {
        return this.http.post(this.routeUrl, category);
    }


}
