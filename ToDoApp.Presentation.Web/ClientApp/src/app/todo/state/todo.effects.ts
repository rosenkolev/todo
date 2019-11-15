import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';

import { of, Observable } from 'rxjs';
import { mergeMap, map, catchError } from 'rxjs/operators';

import {
  ToDoActionTypes,
  LoadActiveItems,
  LoadActiveItemsSuccess,
  LoadActiveItemsFail,
  UpdateItem,
  UpdateItemFail,
  AddItem,
  RemoveItem } from './todo.actions';
import { ToDoService } from '../todo.service';

@Injectable()
export class ToDoEffects {
  constructor(
    private _actions$: Actions,
    private _service: ToDoService) { }

  @Effect()
  load$ = this._actions$
    .pipe(
      ofType<LoadActiveItems>(ToDoActionTypes.LoadActiveItems),
      mergeMap(() =>
        this._service
          .getActiveItems()
          .pipe(
            map(data => new LoadActiveItemsSuccess(data)),
            catchError(error => of(new LoadActiveItemsFail(error))))));

  @Effect()
  update$ = this._actions$
    .pipe(
      ofType<UpdateItem>(ToDoActionTypes.UpdateItem),
      mergeMap(action =>
        this._service
          .update(action.payload)
          .pipe(
            map(res => res.status === 201 ? new AddItem(res.body) : new RemoveItem(res.body)),
            catchError(error => of(new UpdateItemFail(error))))));
}
