import { call, spawn } from 'redux-saga/effects';

import { watchCounterAsync } from './counter';

export default function* rootSaga() {
    const sagas = [
        watchCounterAsync
    ];

    yield sagas.map(saga =>
        spawn(function* () {
            while (true) {
                try {
                    yield call(saga)
                } catch (e) {
                    console.log(e)
                }
            }
        })
    )
}