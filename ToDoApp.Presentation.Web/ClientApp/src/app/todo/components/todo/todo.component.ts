import { Component, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';

import { Observable } from 'rxjs';

import { ItemModel } from '../../todo.model';
import { getActiveItems } from '../../state';
import { LoadActiveItems, UpdateItem } from '../../state/todo.actions';

@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ToDoComponent implements OnInit {
  value: string;

  items$: Observable<ItemModel[]>;

  constructor(private readonly _store: Store<any>) { }

  ngOnInit(): void {
    this.items$ = this._store.select(getActiveItems);
    this._store.dispatch(new LoadActiveItems());
  }

  add(): void {
    this._store.dispatch(new UpdateItem({ id: null, name: this.value }));
    this.value = '';
  }

  check(item: ItemModel): void {
    this._store.dispatch(new UpdateItem(item));
  }
}
