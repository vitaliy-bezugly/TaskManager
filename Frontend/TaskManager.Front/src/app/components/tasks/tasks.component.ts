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
}
