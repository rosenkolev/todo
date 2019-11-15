import { TestBed } from '@angular/core/testing';
import { provideMockActions } from '@ngrx/effects/testing';
import { of, throwError, Observable } from 'rxjs';

import { ToDoService } from '../todo.service';
import { ToDoEffects } from './todo.effects';
import { LoadActiveItems, LoadActiveItemsSuccess, LoadActiveItemsFail } from './todo.actions';

describe('BenchEffects', () => {
  let service: jasmine.SpyObj<ToDoService>;
  let actions$: Observable<any>;
  let effects: ToDoEffects;

  beforeEach(() => service = jasmine.createSpyObj('todoService', ['getActiveItems']));
  beforeEach(() =>
    TestBed.configureTestingModule({
      providers: [
        { provide: ToDoService, useValue: service },
        ToDoEffects,
        provideMockActions(() => actions$)
      ],
    }));
  beforeEach(() => effects = TestBed.get(ToDoEffects));

  it('.load$ should return a LoadSuccess action, on success', done => {
    const dummy = [{ id: '1', name: 'A' }];

    service.getActiveItems.and.returnValue(of(dummy));

    actions$ = of(new LoadActiveItems());

    effects.load$.subscribe((result: LoadActiveItemsSuccess) => {
      expect(result.payload).toEqual(dummy);
        done();
    });
  });

  it('.load$ should return a LoadFail action, on error', done => {
    const msg = 'Not Found';

    service.getActiveItems.and.returnValue(throwError(msg));

    actions$ = of(new LoadActiveItems());

    effects.load$.subscribe((result: LoadActiveItemsFail) => {
        expect(result.message).toEqual(msg);
        done();
    });
  });
});
