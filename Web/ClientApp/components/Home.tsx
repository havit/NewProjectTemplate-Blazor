import * as React from 'react'
import Counter from '../containers/Counter'
import Configuration from '../configuration'
import SimpleAppBar from './SimpleAppBar';
import { withStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Typography from '@material-ui/core/Typography';

const styles = theme => ({
    root: {
        ...theme.mixins.gutters(),
        paddingTop: theme.spacing.unit * 2,
        paddingBottom: theme.spacing.unit * 2,
    },
});

const Home = (props) => {
    const { classes } = props;

    return <>        
        <SimpleAppBar />
        <Paper className={classes.root} elevation={1}>
            <Counter />        
            <Typography variant="headline" component="h3">
                Web configuration
            </Typography>
            <Typography component="p">
                {JSON.stringify(Configuration)}
            </Typography>
        </Paper>
    </>;
}

export default withStyles(styles)(Home);