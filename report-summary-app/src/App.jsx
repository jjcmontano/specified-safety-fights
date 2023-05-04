import React from 'react';

import {
    Box,
    CssBaseline,
    GlobalStyles,
    StyledEngineProvider,
    ThemeProvider
} from '@mui/material';

import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';

import './App.css';
import ReportList from './components/ReportList';
import SummaryDialog from './components/SummaryDialog';
import TopNav from './components/TopNav';
import ReportsStore from './contexts/ReportsStore';
import theme from './theme';

function App() {

    return (
        <StyledEngineProvider injectFirst>
            <ThemeProvider theme={theme}>
                <CssBaseline />
                <GlobalStyles />
                <ReportsStore>
                    <>
                        <SummaryDialog />
                        <Box sx={{ flexGrow: 1 }}>
                            <TopNav />
                        </Box>
                        <Box p={1}>
                            <ReportList />
                        </Box>
                    </>
                </ReportsStore>
            </ThemeProvider>
        </StyledEngineProvider>
    );
}

export default App;
