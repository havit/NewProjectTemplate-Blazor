import Button from '@material-ui/core/Button';
import Paper from '@material-ui/core/Paper';
import { createStyles, Theme, WithStyles, withStyles } from '@material-ui/core/styles';
import Typography from '@material-ui/core/Typography';
import * as React from 'react';
import { connect } from 'react-redux';

import { ApplicationState } from '../store';
import * as CounterStore from '../store/counter';

const styles = (theme: Theme) => createStyles({
    root: {
        ...theme.mixins.gutters(),
        paddingTop: theme.spacing.unit * 2,
        paddingBottom: theme.spacing.unit * 2
    }
});

type CounterProps =
    CounterStore.CounterState
    & ReturnType<typeof CounterStore.actionDispatchers>
    & WithStyles<typeof styles>

class Counter extends React.Component<CounterProps> {
    render() {
        const { classes } = this.props;
        return <Paper className={classes.root} elevation={1}>
            <Typography variant="headline" component="h3">
                Counter
            </Typography>
            <Typography component="p">
                This is a simple example of a React component connected to Redux store.
            </Typography>
            <Typography component="p">
                Current count: <strong>{this.props.count}</strong>
            </Typography>
            <Button color='primary' variant='contained' onClick={() => this.props.incrementAsync()}>Increment</Button>
            <Button color='secondary' variant='contained' onClick={() => this.props.decrementAsync()}>Decrement</Button>
        </Paper>
    }
}

// Wire up the React component to the Redux store
export default connect(
    (state: ApplicationState) => state.counter,
    dispatch => CounterStore.actionDispatchers(dispatch)
)(withStyles(styles)(Counter));