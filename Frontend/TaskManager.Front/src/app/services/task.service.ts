import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AUTH_API_URL } from 'src/app-injection-token';
import { CreateTaskRequest } from 'src/models/createTaskRequest';
import { TaskViewModel } from 'src/models/taskViewModel';
import { UpdateTaskRequest } from 'src/models/updateTaskRequest';
import { ACCES_TOKEN_KEY } from './authorization.service';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  constructor(private httpClient : HttpClient, @Inject(AUTH_API_URL) private apiUrl : string) { 
  }

  public getTasks() : Observable<TaskViewModel[]> {
    const headers = 'Bearer ' + localStorage.getItem(ACCES_TOKEN_KEY)
    
    return this.httpClient.get<TaskViewModel[]>(this.apiUrl + 'Task', {
      headers: new HttpHeaders().set('Authorization', headers )
    })
  }

  public addTask(task : CreateTaskRequest) {
    const headers = 'Bearer ' + localStorage.getItem(ACCES_TOKEN_KEY)

    return this.httpClient.post<CreateTaskRequest>(this.apiUrl + 'Task', task, {
      headers: new HttpHeaders().set('Authorization', headers )
    })
  }

  public updateTask(id : string, task : UpdateTaskRequest) {
    const headers = 'Bearer ' + localStorage.getItem(ACCES_TOKEN_KEY)

    return this.httpClient.put<UpdateTaskRequest>(this.apiUrl + 'Task/' + id, task, {
      headers: new HttpHeaders().set('Authorization', headers )
    })
  }

  public deleteTask(taskId : string) {
    const headers = 'Bearer ' + localStorage.getItem(ACCES_TOKEN_KEY)

    return this.httpClient.delete<TaskViewModel[]>(this.apiUrl + 'Task/' + taskId, {
      headers: new HttpHeaders().set('Authorization', headers )
    })
  }
}
