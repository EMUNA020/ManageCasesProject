import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CasesComponent } from './components/cases/cases.component';

const routes: Routes = [
  { path: 'cases', component: CasesComponent },
  { path: '', redirectTo: '/cases', pathMatch: 'full' },
  { path: '**', redirectTo: '/cases' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
