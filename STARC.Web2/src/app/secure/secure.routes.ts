import { RouterModule, Routes, provideRoutes } from '@angular/router';

import { AuthGuard } from '../_shared/services/authentication/authentication.guard';

import { HomeComponent } from './home/home.component';
import { CustomerListComponent } from './customer/customer-list.component';
import { CustomerDetailComponent } from './customer/customer-detail.component';
import { ProjectListComponent,  } from './project/project-list.component';
import { ProjectDetailComponent,  } from './project/project-detail.component';
import { UserListComponent,  } from './user/user-list.component';
import { UserDetailComponent,  } from './user/user-detail.component';
import { UsersInProjectsComponent  } from './user-project/user-project.component';
import { TestPlanListComponent,  } from './test-plan/test-plan-list.component';
import { TestPlanDetailComponent,  } from './test-plan/test-plan-detail.component';
import {  ManageTestsComponent } from './manage-tests/manage-tests.component';

export const SECURE_ROUTES: Routes = [
    { 
        path: 'home',      
        component: HomeComponent,
        canActivate: [ AuthGuard ]
    },    
    { 
        path: 'customer-detail',      
        component: CustomerDetailComponent,
        canActivate: [ AuthGuard ]
    },
    { 
        path: 'customer-detail/:id',      
        component: CustomerDetailComponent ,
        canActivate: [ AuthGuard ]
    },
    {
        path: 'customer-list',
        component: CustomerListComponent,
        canActivate: [ AuthGuard ]
    },
    { 
        path: 'project-detail',      
        component: ProjectDetailComponent,
        canActivate: [ AuthGuard ]
    },
    { 
        path: 'project-detail/:id',      
        component: ProjectDetailComponent ,
        canActivate: [ AuthGuard ]
    },
    {
        path: 'project-list',
        component: ProjectListComponent,
        canActivate: [ AuthGuard ]
    },
        { 
        path: 'user-detail',      
        component: UserDetailComponent,
        canActivate: [ AuthGuard ]
    },
    { 
        path: 'user-detail/:id',      
        component: UserDetailComponent ,
        canActivate: [ AuthGuard ]
    },
    {
        path: 'user-list',
        component: UserListComponent,
        canActivate: [ AuthGuard ]
    },
    {
        path: 'user-project',
        component: UsersInProjectsComponent,
        canActivate: [ AuthGuard ]
    },
        { 
        path: 'test-plan-detail',      
        component: TestPlanDetailComponent ,
        canActivate: [ AuthGuard ]
    },
    { 
        path: 'test-plan-detail/:id',      
        component: TestPlanDetailComponent ,
        canActivate: [ AuthGuard ]
    },
    {
        path: 'test-plan-list',
        component: TestPlanListComponent,
        canActivate: [ AuthGuard ]
    },
    {
        path: 'manage-tests',
        component: ManageTestsComponent,
        canActivate: [ AuthGuard ]
    },
    { 
        path: '',
        redirectTo: '/home',
        pathMatch: 'full'
    }    
];
