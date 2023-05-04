import {
    AppBar, Button, Toolbar, Typography,
} from '@mui/material';
import React from 'react';

function TopNav() {
    return (<AppBar position="static">
        <Toolbar>
            <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
              ReportSummary
            </Typography>
            <Button variant="contained" color="success">Add Report</Button>
        </Toolbar>
    </AppBar>);
}

export default TopNav;
