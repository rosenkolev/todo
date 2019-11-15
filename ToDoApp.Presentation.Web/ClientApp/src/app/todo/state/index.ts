import { createFeatureSelector, createSelector } from '@ngrx/store';
import { ToDoState } from './todo.reducer';

export const NAME = 'todo';

const getState = createFeatureSelector<ToDoState>(NAME);

/** Selector Functions **/

export const getActiveItems = createSelector(getState, state => state.data);
