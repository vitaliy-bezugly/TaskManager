import { DatePipe } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { TaskService } from 'src/app/services/task.service';
import { TaskViewModel } from 'src/models/taskViewModel';

import * as moment from 'moment';
import { CreateTaskRequest } from 'src/models/createTaskRequest';
import { UpdateTaskRequest } from 'src/models/updateTaskRequest';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {
  public uncompletedTasks : TaskViewModel[] = []
  public completedTasks : TaskViewModel[] = []
  public tasksToDiplay : TaskViewModel[] = []

  public task : TaskViewModel = new TaskViewModel()
  public taskToAdd = new CreateTaskRequest()

  public alertMessage : string = ''
  public successOperation : boolean = false

  public filteredString : string = ''

  constructor(private taskService: TaskService, public datePipe: DatePipe) {
    let main = document.getElementById('main-tag')
    main?.classList.remove('bg-image')
  }

  ngOnInit(): void {
    this.taskService.getTasks().subscribe(data => {
      this.completedTasks = data.filter(x => x.isComplited)
      this.uncompletedTasks = data.filter(x => !x.isComplited)
      this.tasksToDiplay = this.uncompletedTasks;
    }, (error : HttpErrorResponse) => {
      this.handleError(error, 'Can not get tasks from server')
    })
  }

  getTasks() : TaskViewModel[] {
    return this.tasksToDiplay
  }

  handleSuccess(specificAlertMessage : string) : void {
    this.alertMessage = specificAlertMessage
    this.successOperation = true
  }

  handleError(error : HttpErrorResponse, specificAlertMessage : string = 'Something goes wrong') {
    console.log(error)
    if(error.status === 401) {
      this.alertMessage = 'You are unauthorized!'
    }
    else {
      this.alertMessage = specificAlertMessage
    }

    this.showErrorAlert()
  }

  showErrorAlert() : void {
    let alertError = document.getElementById('alertError')
    alertError?.removeAttribute('hidden')
  }

  public changeTasksToDiplayToCopletedTasks() : void {
    this.tasksToDiplay = this.completedTasks

    let complitedButton = document.getElementById('completedTaskButton')
    complitedButton?.classList.add('active')
    let uncomplitedButton = document.getElementById('uncompletedTaskButton')
    uncomplitedButton?.classList.remove('active')
  }
  public changeTasksToDiplayToUncopletedTasks() : void {
    this.tasksToDiplay = this.uncompletedTasks

    let complitedButton = document.getElementById('completedTaskButton')
    complitedButton?.classList.remove('active')
    let uncomplitedButton = document.getElementById('uncompletedTaskButton')
    uncomplitedButton?.classList.add('active')
  }

  updateTasks() : void {
    this.taskService.getTasks().subscribe(data => {
      this.tasksToDiplay = data.filter(x => !x.isComplited)
    }, (error : HttpErrorResponse) => {
      alert(error)
    })
  } 

  public ReplaceAddDivWithForm() : void {
    let div = document.getElementById('add-task-div') as HTMLDivElement
    let divform = document.getElementById('form-toadd') as HTMLDivElement

    div.classList.add('collapse')
    divform.classList.remove('collapse')
  }
  
  public UndoToDiv() : void {
    let div = document.getElementById('add-task-div') as HTMLDivElement
    let divForm = document.getElementById('form-toadd') as HTMLDivElement
    
    div.classList.remove('collapse')
    divForm.classList.add('collapse')
  }

  public ReplaceLiWithForm(taskId : string) : void {
    let data = document.getElementById('task-data-' + taskId) as HTMLDivElement
    let divform = document.getElementById('form-toedit-' + taskId) as HTMLDivElement

    data.style.display = 'none'
    divform.classList.remove('collapse')
  }
  
  public UndoToData(taskId : string) : void {
    let data = document.getElementById('task-data-' + taskId) as HTMLDivElement
    let divform = document.getElementById('form-toedit-' + taskId) as HTMLDivElement

    data.style.display = 'flex'
    divform.classList.add('collapse')
  }

  public ReverseImportandValue(task: TaskViewModel) : void {
    task!.isImportant = !task?.isImportant
    this.UpdateTask(task)
  }

  public CompliteTask(task : TaskViewModel) {
    let taskToSuccess = this.MapTaskVMToUpdateRequest(task)
    taskToSuccess.isComplited = true

    this.taskService.updateTask(task.id, taskToSuccess).subscribe(data => {
      this.updateTasks()
      this.completedTasks.push(task)
      this.handleSuccess('Successfully complited task!')
      this.tasksToDiplay = this.tasksToDiplay.filter(x => x.id !== data.title)
    }, (error : HttpErrorResponse) => {
      this.handleError(error, 'Can not complite task')
    })
  }

  public ShowImportantTask() : void {
    this.uncompletedTasks = this.uncompletedTasks.filter(x => x.isImportant === true)
  }

  public GetTodaysDate() : string {
    return (moment(new Date())).format('DD MMMM YYYY')
  }

  public ParseDateToReadableFormat(date : Date) : string {
    return (moment(date)).format('DD MMMM YYYY HH:mm:ss')
  }

  public AddTask() : void {
    this.taskService.addTask(this.taskToAdd).subscribe(data => {
      this.handleSuccess('Task was successfully added!')
      this.updateTasks()
    }, (error : HttpErrorResponse) => {
      this.handleError(error)
    })
  }

  public UpdateTask(task : TaskViewModel) : void {
    let taskToUpdate = this.MapTaskVMToUpdateRequest(task)

    this.taskService.updateTask(task.id, taskToUpdate).subscribe(data => {
      this.handleSuccess('Successfully updated task!')
      this.updateTasks()
    }, (error : HttpErrorResponse) => {
      let errorMessage = ''
      if(error.error.errors["Title"]) {
        for (let item of error.error.errors["Title"]) {
          errorMessage += item
        }
      }
      this.handleError(error, errorMessage)
    })
    this.UndoToData(task.id)
  }
  public DeleteTask(taskId : string) : void {
    this.taskService.deleteTask(taskId).subscribe(data => {
      this.handleSuccess('Successfully deleted task!')
      this.updateTasks()
    }, (error : HttpErrorResponse) => {
      this.handleError(error, 'Can not delete task')
    })
  }

  private MapTaskVMToUpdateRequest(task : TaskViewModel) : UpdateTaskRequest {
    let taskToUpdate = new UpdateTaskRequest()
    taskToUpdate.title = task.title
    taskToUpdate.description = task.description
    taskToUpdate.isImportant = task.isImportant
    taskToUpdate.expirationTime = task.expirationTime

    return taskToUpdate
  }
}
