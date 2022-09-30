import { Injectable } from '@angular/core';
import { TaskViewModel } from 'src/viewmodels/TaskViewModel';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  tasks : TaskViewModel[] = [
    {"id": '5f3844ac676b45acab7139126e899d75', "title": "Some title 1", "description": "Some description 1", "createdTime": new Date(), "expirationTime":  new Date()},
    {"id": 'c6b7cf774073424da15b70c16c9ec60d', "title": "Some title 2", "description": "Some description 2", "createdTime": new Date(), "expirationTime":  new Date()},
    {"id": '5bf9066e0429428d9f9a292825f1130d', "title": "Some title 3", "description": "Some description 3", "createdTime": new Date(), "expirationTime":  new Date()},
    {"id": '5c61435a1f7049d4a746ed09fd20e3a4', "title": "Some title 4", "description": "Some description 4", "createdTime": new Date(), "expirationTime":  new Date()},
    {"id": '7f246e96926d40348a2dd0b6d27afb91', "title": "Some title 5", "description": "Some description 5", "createdTime": new Date(), "expirationTime":  new Date()}
  ];
  constructor() { }

  public GetTasks() : TaskViewModel[] {
    return this.tasks;
  }
}
