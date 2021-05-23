import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./Tabs/tabs/tabs.module').then(m => m.TabsPageModule)
  },  {
    path: 'employee-input',
    loadChildren: () => import('./pages/employee-input/employee-input.module').then( m => m.EmployeeInputPageModule)
  },
  {
    path: 'customer-input',
    loadChildren: () => import('./pages/customer-input/customer-input.module').then( m => m.CustomerInputPageModule)
  },
  {
    path: 'product-input',
    loadChildren: () => import('./pages/product-input/product-input.module').then( m => m.ProductInputPageModule)
  },
  {
    path: 'tarea-info',
    loadChildren: () => import('./pages/tarea-info/tarea-info.module').then( m => m.TareaInfoPageModule)
  },
  {
    path: 'help-info',
    loadChildren: () => import('./pages/help-info/help-info.module').then( m => m.HelpInfoPageModule)
  },
  {
    path: 'login',
    loadChildren: () => import('./pages/login/login.module').then( m => m.LoginPageModule)
  },
  {
    path: 'user-input',
    loadChildren: () => import('./pages/user-input/user-input.module').then( m => m.UserInputPageModule)
  },
  {
    path: 'team',
    loadChildren: () => import('./pages/team/team.module').then( m => m.TeamPageModule)
  },
  {
    path: 'member',
    loadChildren: () => import('./pages/member/member.module').then( m => m.MemberPageModule)
  },
  {
    path: 'repositorio',
    loadChildren: () => import('./pages/repositorio/repositorio.module').then( m => m.RepositorioPageModule)
  }

];
@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
