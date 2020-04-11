import * as React from 'react';
import Layout from './components/Layout';
import Home from './components/Home';
import { MuiThemeProvider, createMuiTheme } from '@material-ui/core/styles';
import purple from '@material-ui/core/colors/purple';
import green from '@material-ui/core/colors/green';

const theme = createMuiTheme({
    palette: {
        primary: purple,
        secondary: green
    }
});

export const routes =
    <MuiThemeProvider theme={theme}>
        <Layout>
            <Home />
        </Layout>
    </MuiThemeProvider>