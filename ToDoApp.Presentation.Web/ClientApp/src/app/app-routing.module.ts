import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes, NoPreloading } from '@angular/router';

import { LayoutModule } from './shared/features/layout/layout.module';

import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { LayoutComponent } from './shared/features/layout/components/layout/layout.component';
import { SharedModule } from './shared/shared.module';

export const appRoutes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', pathMatch: 'full', redirectTo: 'todo' },
      { path: 'todo', loadChildren: './todo/todo.module#ToDoModule' },
    ]
  },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [
    CommonModule,
    LayoutModule,
    SharedModule,
    RouterModule.forRoot(appRoutes, {
      preloadingStrategy: NoPreloading,
      useHash: false,
      enableTracing: false // debugs the routes
    })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
