import { Injectable } from '@angular/core';

import { Observable, of } from 'rxjs';

import { MenuItem } from './layout.model';

@Injectable()
export class LayoutService {
  getMenuItems(): Observable<MenuItem[]> {
    return of([{ name: 'ToDo', path: '/todo', relative: true }]);
  }
}
