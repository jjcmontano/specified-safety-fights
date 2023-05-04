import React, { Suspense } from 'react';

import {
    Box,
    CssBaseline,
    GlobalStyles,
    Skeleton,
    StyledEngineProvider,
    ThemeProvider,
} from '@mui/material';

import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';

import './App.css';
import ReportList from './components/ReportList';
import TopNav from './components/TopNav';
import theme from './theme';

function App() {
    return (
        <StyledEngineProvider injectFirst>
            <ThemeProvider theme={theme}>
                <CssBaseline />
                <GlobalStyles />
                <Suspense fallback={<Skeleton />}>
                    <Box sx={{ flexGrow: 1 }}>
                        <TopNav />
                    </Box>
                    <Box p={1}>
                        <ReportList reports={[
                            {
                                id: 'foo',
                                name: 'foo',
                                year: 1,
                                reportSectorTitle: 'foo',
                                reportCode: 'foo',
                            },
                            {
                                id: 'bar',
                                name: 'foo',
                                year: 2,
                                reportSectorTitle: 'foo',
                                reportCode: 'foo',
                            },
                        ]} />
                    </Box>
                </Suspense>
            </ThemeProvider>
        </StyledEngineProvider>
    );
}

export default App;
