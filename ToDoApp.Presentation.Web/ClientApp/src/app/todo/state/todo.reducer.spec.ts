import { initialState, reducer } from './todo.reducer';
import { LoadActiveItems, LoadActiveItemsSuccess } from './todo.actions';

describe('ToDoReducer', () => {
  it('should return the default state', () => {
    const action = new LoadActiveItems();
    const state = reducer(undefined, action);
    expect(state.data).toBe(initialState.data);
    expect(state.error).toBe(initialState.error);
  });

  it('should update data', () => {
    const actionSuccess = new LoadActiveItemsSuccess([{ id: '1', name: 'A' }]);
    const state = reducer(undefined, actionSuccess);
    expect(state.data.length).toBe(1);
  });
});
