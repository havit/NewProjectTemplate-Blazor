require('core-js/shim'); // react recommended shims
require('raf').polyfill(); // react recommended shim for requestAnimationFrame()
import 'reset-css';

import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { AppContainer } from 'react-hot-loader';
import { Provider } from 'react-redux';
import { ConnectedRouter } from 'react-router-redux';
import { createBrowserHistory } from 'history';
import configureStore from './configureStore';
import { ApplicationState } from './store';
import * as moment from 'moment';

import * as RoutesModule from './routes';
let routes = RoutesModule.routes;

moment.locale("cs");

// Create browser history to use in the Redux store
const history = createBrowserHistory();

// Get the application-wide store instance, prepopulating with state from the server where available.
const initialState = window["initialReduxState"] as ApplicationState;
const store = configureStore(history, initialState);

const renderApp = () => {
    ReactDOM.render(
        <AppContainer>
            <Provider store={store}>
                <ConnectedRouter history={history} children={routes} />
            </Provider>
        </AppContainer>,
        document.getElementById('app')
    );
}

renderApp();

// Allow Hot Module Replacement
if (module.hot) {
    module.hot.accept('./routes', () => {
        routes = require<typeof RoutesModule>('./routes').routes;
        renderApp();
    });
}
