import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filter'
})
export class FilterPipe implements PipeTransform {

  transform(value: any, filterString : string) {
    if(value.length === 0 || filterString === '') {
      return value
    }

    filterString = filterString.toLowerCase()
    const tasks = []
    for (const task of value) {
      if(task['title'].toLowerCase().includes(filterString)) {
        tasks.push(task)
      }
    }
    return tasks
  }
}
