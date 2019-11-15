import { Action } from '@ngrx/store';
import { ItemModel } from '../todo.model';

export enum ToDoActionTypes {
  LoadActiveItems = '[ToDo] Load Active Items',
  LoadActiveItemsSuccess = '[ToDo] Load Active Items Success',
  LoadActiveItemsFail = '[ToDo] Load Active Items Fail',
  UpdateItem = '[ToDo] Update',
  UpdateItemFail = '[ToDo] Update Fail',
  AddItem = '[ToDo] Add Item',
  RemoveItem = '[ToDo] Remove Item',
}

export class LoadActiveItems implements Action {
  readonly type = ToDoActionTypes.LoadActiveItems;
}

export class LoadActiveItemsSuccess implements Action {
  readonly type = ToDoActionTypes.LoadActiveItemsSuccess;
  constructor(public readonly payload: ItemModel[]) { }
}

export class LoadActiveItemsFail implements Action {
  readonly type = ToDoActionTypes.LoadActiveItemsFail;
  constructor(public readonly message: string) { }
}

export class UpdateItem implements Action {
  readonly type = ToDoActionTypes.UpdateItem;
  constructor(public readonly payload: ItemModel) { }
}

export class AddItem implements Action {
  readonly type = ToDoActionTypes.AddItem;
  constructor(public readonly payload: ItemModel) { }
}

export class RemoveItem implements Action {
  readonly type = ToDoActionTypes.RemoveItem;
  constructor(public readonly payload: ItemModel) { }
}

export class UpdateItemFail implements Action {
  readonly type = ToDoActionTypes.UpdateItemFail;
  constructor(public readonly payload: ItemModel) { }
}

export type ToDoActions = LoadActiveItems
  | LoadActiveItemsSuccess
  | LoadActiveItemsFail
  | UpdateItem
  | UpdateItemFail
  | AddItem
  | RemoveItem;
