import { Injectable } from '@angular/core';
import { TaskViewModel } from 'src/viewmodels/TaskViewModel';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  tasks : TaskViewModel[] = [
    {"id": '5f3844ac676b45acab7139126e899d75', "title": "Some title 1", "description": "Some description 1", "createdTime": new Date('10/11/2022'), "expirationTime":  new Date('2022-10-27'), "isImportant": false},
    {"id": 'c6b7cf774073424da15b70c16c9ec60d', "title": "Make dinner", "description": "Some description 2", "createdTime": new Date('10/11/2022'), "expirationTime":  new Date('2022-10-27'), "isImportant": true},
    {"id": '5bf9066e0429428d9f9a292825f1130d', "title": "Go to the gym", "description": "", "createdTime": new Date(1478708162000), "expirationTime":  new Date('2022-10-27'), "isImportant": false},
    {"id": '5c61435a1f7049d4a746ed09fd20e3a4', "title": "Go to the shop", "description": "Buy: potato, onion, garlic222, tomato, 1kg of pork and spagetti", "createdTime": new Date('2022-10-27'), "expirationTime":  new Date(), "isImportant": true},
    {"id": '7f246e96926d40348a2dd0b6d27afb91', "title": "Some title 5", "description": "Some description 5", "createdTime": new Date('10/11/2022'), "expirationTime":  new Date('2022-10-27'), "isImportant": false}
  ];
  constructor() { }

  public GetTasks() : TaskViewModel[] {
    return this.tasks;
  }
}
