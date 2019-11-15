import { ActionReducer, MetaReducer } from '@ngrx/store';

import { environment } from '../../environments/environment';

export function loggerReducer(reducer: ActionReducer<any>): ActionReducer<any> {
    return function (state: any, action: any): any {
        console.log(state, 'action', action);
        return reducer(state, action);
    };
}

export const metaReducers: MetaReducer<any>[] = environment.production
    ? []
    : [loggerReducer];
