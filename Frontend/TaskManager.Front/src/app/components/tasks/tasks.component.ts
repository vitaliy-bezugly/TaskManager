import { DatePipe } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { TaskService } from 'src/app/services/task.service';
import { TaskViewModel } from 'src/models/taskViewModel';

import * as moment from 'moment';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {
moment(arg0: Date) {
throw new Error('Method not implemented.');
}
  public tasks : TaskViewModel[] = [];
  public task : TaskViewModel = new TaskViewModel()
  public alertMessage : string = ''
  public successOperation : boolean = false

  public filteredString : string = ''

  constructor(private taskService: TaskService, public datePipe: DatePipe) {

    taskService.getTasks().subscribe(data => {
      this.tasks = data
    }, (error : HttpErrorResponse) => {
      this.handleError(error, 'Can not get tasks from server')
    })

    let main = document.getElementById('main-tag')
    main?.classList.remove('bg-image')
  }

  ngOnInit(): void {
  }

  getTasks() : TaskViewModel[] {
    return this.tasks
  }

  addTask() : void {
    this.taskService.addTask(this.task).subscribe(data => {
      this.handleSuccess('Task was successfully added!')
      this.updateTasks()
    }, (error : HttpErrorResponse) => {
      this.handleError(error)
    })
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

  updateTasks() : void {
    this.taskService.getTasks().subscribe(data => {
      this.tasks = data
    }, (error : HttpErrorResponse) => {
      alert(error)
    })
  } 

  public GetMonthByMonthDate(number : number) : string {
    return 'October';
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

  public ReverseImportandValue(taskId: string) : void {
    this.tasks.find(x => x.id === taskId)!.isImportant = !this.tasks.find(x => x.id === taskId)!.isImportant
  }

  public ShowImportantTask() : void {
    this.tasks = this.tasks.filter(x => x.isImportant === true)
  }
  public ShowTodayTask() : void {
    this.ChangeHeaderText('Today')
    this.ShowDate()
    this.taskService.getTasks().subscribe(data => {
      this.tasks = data
    })
  }
  public ShowTomorrowTask() : void {
    this.ChangeHeaderText('Tomorrow')
    this.HideDate()
    this.tasks = []
  }
  public ShowUpcomingTask() : void {
    this.ChangeHeaderText('Upcoming')
    this.HideDate()
    this.tasks = []
  }

  public PrintDate(date: Date) : void {
    console.log(date)
  }

  public GetTodaysDate() : string {
    return (moment(new Date())).format('DD MMMM YYYY')
  }

  private ChangeHeaderText(text : string) : void {
    let headerText = document.getElementById('header-date-text')
    headerText!.innerText = text
  }
  private HideDate() : void {
    let spanDate = document.getElementById('header-date') as HTMLSpanElement
    spanDate.style.display = 'none'
  }
  private ShowDate() : void {
    let spanDate = document.getElementById('header-date') as HTMLSpanElement
    spanDate.style.display = 'inline'
  }

  public ParseDateToReadableFormat(date : Date) : string {
    return (moment(date)).format('DD MMMM YYYY HH:mm:ss')
  }

  public UpdateTask(task : TaskViewModel) : void {
    this.taskService.updateTask(task).subscribe(data => {
      this.updateTasks()
      this.handleSuccess('Successfully updated task!')
    }, (error : HttpErrorResponse) => {
      this.handleError(error, 'Can not update task')
    })
    this.UndoToData(task.id)
  }

  public DeleteTask(taskId : string) : void {
    this.taskService.deleteTask(taskId).subscribe(data => {
      this.updateTasks()
      this.handleSuccess('Successfully deleted task!')
    }, (error : HttpErrorResponse) => {
      this.handleError(error, 'Can not delete task')
    })
  }
}
