import { createStore, applyMiddleware, compose, combineReducers, GenericStoreEnhancer, Store } from 'redux';
import { routerReducer, routerMiddleware } from 'react-router-redux';
import * as StoreModule from './store';
import { History } from 'history';
import createSagaMiddleware from 'redux-saga'
import rootSaga from './store/root-saga';

const configureStore = (history: History, initialState?: StoreModule.ApplicationState) => {
    // Build middleware. These are functions that can process the actions before they reach the store.
    const windowIfDefined = typeof window === 'undefined' ? null : window as {};
    // If devTools is installed, connect to it
    const devToolsExtension = windowIfDefined && windowIfDefined["devToolsExtension"] as () => GenericStoreEnhancer;
    const sagaMiddleware = createSagaMiddleware();
    const createStoreWithMiddleware = compose(
        applyMiddleware(
            sagaMiddleware,
            routerMiddleware(history)),
        devToolsExtension ? devToolsExtension() : f => f
    )(createStore);    

    // Combine all reducers and instantiate the app-wide store instance
    const allReducers = buildRootReducer(StoreModule.reducers);
    const store = createStoreWithMiddleware(allReducers, initialState) as Store<StoreModule.ApplicationState>;

    sagaMiddleware.run(rootSaga);
    
    // Enable Webpack hot module replacement for reducers
    if (module.hot) {
        module.hot.accept('./store', () => {
            const nextRootReducer = require<typeof StoreModule>('./store');
            store.replaceReducer(buildRootReducer(nextRootReducer.reducers));
        });
    }

    return store;
}

const buildRootReducer = (allReducers) => {
    return combineReducers<StoreModule.ApplicationState>({ ...allReducers, routing: routerReducer });
}

export default configureStore