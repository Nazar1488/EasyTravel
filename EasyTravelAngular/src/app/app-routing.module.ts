import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent, FindComponent, AuthComponent, LoginComponent, RegisterComponent, MonitorComponent, ProfileComponent } from './components';
import { AuthGuardService, AuthorizedGuardService } from './guards';

const routes: Routes = [
  {
    path: '', component: AuthComponent, canActivate: [AuthorizedGuardService], children: [
      { path: "", redirectTo: "/login", pathMatch: "full" },
      {
        path: 'login',
        component: LoginComponent
      },
      {
        path: 'register',
        component: RegisterComponent
      }
    ]
  },
  {
    path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuardService], children: [
      { path: "", redirectTo: "/dashboard/profile", pathMatch: "full" },
      {
        path: 'profile', 
        component: ProfileComponent
      },
      {
        path: 'find',
        component: FindComponent
      },
      {
        path: 'monitor',
        component: MonitorComponent
      }
    ]
  },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
