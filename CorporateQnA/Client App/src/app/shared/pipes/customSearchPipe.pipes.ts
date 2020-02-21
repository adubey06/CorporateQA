import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'search'})
export class searchPipe implements PipeTransform {
    transform(items: any, term: any): any {
        if (items == undefined) return;
    if (term === undefined) return items;
    return items.filter(function(item) {
        if(item['name'].toString().toLowerCase().includes(term.toLowerCase())){
          return true;
        }else{
          return false;
        }
    });
  }
}