import { DatePipe } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { TaskService } from 'src/app/services/task.service';
import { TaskViewModel } from 'src/viewmodels/TaskViewModel';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {
  public tasks : TaskViewModel[] = [];

  constructor(private taskService: TaskService, public datePipe: DatePipe) {

    taskService.GetTasks().subscribe(data => {
      this.tasks = data
    }, (error : HttpErrorResponse) => {
      console.log('error while getting tasks: ', error)
    })

    let main = document.getElementById('main-tag')
    main?.classList.remove('bg-image')
  }

  ngOnInit(): void {
  }

  getTasks() : TaskViewModel[] {
    return this.tasks
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
  public UndoToDate(taskId : string) : void {
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
    this.taskService.GetTasks().subscribe(data => {
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
}
