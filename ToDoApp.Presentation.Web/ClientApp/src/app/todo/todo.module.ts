import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';

import { SharedModule } from '../shared/shared.module';

import { ToDoEffects } from './state/todo.effects';
import { NAME } from './state/index';
import { reducer } from './state/todo.reducer';
import { ToDoComponent } from './components/todo/todo.component';
import { ToDoService } from './todo.service';

export const routes: Routes = [
  { path: '', component: ToDoComponent }
];

@NgModule({
  declarations: [
    ToDoComponent
  ],
  providers: [
    ToDoService
  ],
  imports: [
    CommonModule,
    FormsModule,
    SharedModule,
    RouterModule.forChild(routes),
    StoreModule.forFeature(NAME, reducer),
    EffectsModule.forFeature([ToDoEffects])
  ],
  exports: [RouterModule]
})
export class ToDoModule { }
