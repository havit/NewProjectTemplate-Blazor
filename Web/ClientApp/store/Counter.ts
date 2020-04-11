import { Dispatch, Reducer } from 'redux';
import { delay } from 'redux-saga';
import { put, takeEvery, select } from 'redux-saga/effects';

import { createGenericReducer, Omit } from '../helpers/reducer-helper';

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type ReducerKnownAction = IncrementCountAction | DecrementCountAction;

// State
type StateUnion = IncrementCountAction & DecrementCountAction;
export type CounterState = Omit<StateUnion, "type">;

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.
// Use @typeName and isActionType for type detection that works even after serialization/deserialization.

export enum CounterSagaActionTypes {
    IncrementCountAsync = 'INCREMENT_COUNT_ASYNC',
    DecrementCountAsync = 'DECREMENT_COUNT_ASYNC'
}

export enum CounterReducerActionTypes {
    IncrementCount = 'INCREMENT_COUNT',
    DecrementCount = 'DECREMENT_COUNT'
}

interface IncrementCountAction {
    type: CounterReducerActionTypes.IncrementCount;
    count: number;
}

interface DecrementCountAction {
    type: CounterReducerActionTypes.DecrementCount;
    count: number;
}

// Sagas
function* incrementAsync() {
    const state = yield select();   // just like getState()
    const counter: CounterState = state.counter;

    yield delay(1000)
    yield put<ReducerKnownAction>({ type: CounterReducerActionTypes.IncrementCount, count: counter.count + 1 })
}

function* decrementAsync() {
    const state = yield select();   // just like getState()
    const counter: CounterState = state.counter;

    yield delay(1000)
    yield put<ReducerKnownAction>({ type: CounterReducerActionTypes.DecrementCount, count: counter.count - 1 })
}

export function* watchCounterAsync() {
    yield takeEvery(CounterSagaActionTypes.IncrementCountAsync, incrementAsync)
    yield takeEvery(CounterSagaActionTypes.DecrementCountAsync, decrementAsync)
}

export const actionDispatchers = (dispatch: Dispatch<ReducerKnownAction>) => ({
    incrementAsync: () => { dispatch({ type: CounterSagaActionTypes.IncrementCountAsync }) },
    decrementAsync: () => { dispatch({ type: CounterSagaActionTypes.DecrementCountAsync }) }
})

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const initialState: CounterState = { count: 0 };

export const reducer: Reducer<CounterState> = createGenericReducer<CounterState, ReducerKnownAction>(CounterReducerActionTypes, initialState);