import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';

import { Observable, empty } from 'rxjs';

import { ItemModel } from './todo.model';

@Injectable()
export class ToDoService {
  static readonly url = '/api/items';

  constructor(private readonly _http: HttpClient) { }

  getActiveItems(): Observable<ItemModel[]> {
    return this._http.get<ItemModel[]>(ToDoService.url);
  }

  update(item: ItemModel): Observable<HttpResponse<ItemModel>> {
    return this._http.post<ItemModel>(ToDoService.url, item, { observe: 'response' });
  }
}
