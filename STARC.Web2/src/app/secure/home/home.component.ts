import { Component, OnInit } from "@angular/core";

// Component Decorator
@Component({
//moduleId: module.id,
  selector: 'home',
  templateUrl: 'home.component.html'
})

export class HomeComponent {
  message: string;

  constructor(  
  ) { }

  ngOnInit(): void {
    console.log(sessionStorage.getItem("selectedProjectId"));
    if(sessionStorage.getItem("selectedProjectId") == null)
    {
      this.message = "Select a project to start...";
    }
  }
}