import { ToDoActions, ToDoActionTypes } from './todo.actions';
import { ItemModel } from '../todo.model';

export interface ToDoState {
  data: ItemModel[];
  error: string;
}

export const initialState: ToDoState = {
  data: [],
  error: null
};

export function reducer(state = initialState, action: ToDoActions): ToDoState {
  switch (action.type) {
    case ToDoActionTypes.LoadActiveItems:
      return { ...initialState };
    case ToDoActionTypes.LoadActiveItemsSuccess:
      return { ...state, data: action.payload };
    case ToDoActionTypes.LoadActiveItemsFail:
      return { ...state, error: action.message };
    case ToDoActionTypes.AddItem:
      return { ...state, data: state.data.concat(action.payload) };
    case ToDoActionTypes.RemoveItem:
      return { ...state, data: state.data.filter(it => it.id !== action.payload.id) };
    default:
      return state;
  }
}
