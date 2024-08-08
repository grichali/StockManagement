import { RouterModule, Routes } from "@angular/router";
import { AppComponent } from "./app.component";
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";


const routes: Routes = [
  {
    path: '',
    component: AppComponent,
  },
  {
    path: 'auth',
    loadChildren: () => import("./auth/auth.module").then((m) => m.AuthModule),
  }
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports:[RouterModule]
})
export class AppRoutingModule { }
