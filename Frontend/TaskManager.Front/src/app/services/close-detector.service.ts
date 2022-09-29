import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CloseDetectorService {
  constructor() { }
  
  public DetectionHandler : any;

  public CloseSidebar() : void {
    this.DetectionHandler.Invoke();
  }
  
}
