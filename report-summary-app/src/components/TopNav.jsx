import { FileUploadOutlined } from '@mui/icons-material';
import {
    AppBar, Button, Toolbar, Typography,
} from '@mui/material';
import React, { useContext, useState } from 'react';
import ReportsApi from '../api/ReportsApi';
import { ReportsContext } from '../contexts/ReportsStore';

function TopNav() {
    const [reportFile, setReportFile] = useState();
    const [{reportsLoading}, dispatch] = useContext(ReportsContext);

    const handleFileChange = (e) => {
        if (e.target.files) {
            setReportFile(e.target.files[0]);
        }
    }

    const handleClick = () => {
        if (!reportFile) return;

        const postReport = async () => {
            try {
                dispatch({type: 'SET_REPORTS_LOADING'});
                const reportsData = await ReportsApi.postReport(reportFile);
                dispatch({type: 'ADD_REPORT', payload: reportsData});
            } catch (error) {
                dispatch({type: 'SET_REPORTS_ERROR', payload: error?.message ?? error ?? 'Unknown'});
            } finally {
                dispatch({type: 'SET_REPORTS_DONE'})
            }
        }

        postReport();
    }

    return (<AppBar position="static">
        <Toolbar>
            <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>ReportSummary</Typography>
            <Button variant="contained" color="success" onClick={handleClick} disabled={reportsLoading} component="label">
                Add Report
                <FileUploadOutlined />
                <input hidden accept="application/json" type="file" onChange={handleFileChange} />
            </Button>
        </Toolbar>
    </AppBar>);
}

export default TopNav;
