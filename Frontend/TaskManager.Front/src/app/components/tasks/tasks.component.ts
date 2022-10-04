import { Component, OnInit } from '@angular/core';
import { TaskService } from 'src/app/services/task.service';
import { TaskViewModel } from 'src/viewmodels/TaskViewModel';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {
  tasks : TaskViewModel[] = [];
  constructor(private taskService: TaskService) {
    this.tasks = taskService.GetTasks();
  }

  ngOnInit(): void {
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
}
