import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params }   from '@angular/router';

import { AuthService } from '../../_shared/services/authentication/authentication.service';
import { AlertService } from '../../_shared/directives/alert-notification/alert.service';
import { AlertComponent } from '../../_shared/directives/alert-notification/alert.component';

import { ProjectService } from '../../secure/project/project.service';
import { Project } from '../../secure/project/project.model';

import { UsersInProjectsService } from '../../secure/user-project/user-project.service';

import { User } from '../../secure/user/user.model';

@Component({
    selector: 'body',
    templateUrl: 'secure.component.html',
    providers: [AuthService, ProjectService, AlertService]
})
export class SecureComponent implements OnInit{ 
    projects: Project[];    
    selectedProjectId: string;   
    selectedProject: Project;
    loggedUser: User;
    isAdmin: boolean = false;

    constructor(
        private authService : AuthService,
        private alertService: AlertService,
        private projectService: ProjectService,
        private userInProjectService: UsersInProjectsService,
        private router: Router
    ){}

    ngOnInit() {      
         this.selectedProjectId = "";
         this.loggedUser = JSON.parse(sessionStorage.getItem("loggedUser"));

         if(this.loggedUser.UserProfileId == 1)
            this.isAdmin = true;

         this.selectedProject = new Project();
         
         this.listProjects();
         
        if(sessionStorage.getItem("selectedProjectId") != null){
            this.selectedProjectId = sessionStorage.getItem("selectedProjectId");    
        }
    }

    logout(){
        this.authService.logout();
        this.router.navigate(['./login']);
    }

    hasAuthority(menu:string) : boolean{
        return this.authService.hasAuthority(menu);
    }

    listProjects(){
        this.userInProjectService.getByUser(this.loggedUser.UserId.toString())
            .subscribe(response =>{
                this.projects = JSON.parse(JSON.stringify(response.Data as any));  
                if(this.projects.length == 1)
                {
                    this.selectedProjectId = this.projects[0].ProjectId.toString(); 
                    this.projectService.getById(this.selectedProjectId)
                        .subscribe(response =>{
                            this.selectedProject = JSON.parse(JSON.stringify(response.Data as any));  
                            sessionStorage.setItem("selectedProjectId", this.selectedProjectId);
                            sessionStorage.setItem("selectedCustomerId", this.selectedProject.CustomerId.toString());
                        });
                }

            });
    }

    changeSelectedProject(selectedValue: string){
        
        this.selectedProjectId = selectedValue;

        this.projectService.getById(this.selectedProjectId)
            .subscribe(
                response =>{
                    this.selectedProject = JSON.parse(JSON.stringify(response.Data as any));        
                    sessionStorage.setItem("selectedProjectId", this.selectedProjectId);  
                    sessionStorage.setItem("selectedCustomerId", this.selectedProject.CustomerId.toString());

                    this.router.navigate(['./home']);
                },err=>{
                    this.alertService.error(err.message);
                });        
    }
}