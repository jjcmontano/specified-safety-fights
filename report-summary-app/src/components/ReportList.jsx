import { Error } from '@mui/icons-material';
import { Grid, Skeleton, Typography } from '@mui/material';
import React, { useContext, useEffect } from 'react';
import ReportsApi from '../api/ReportsApi';
import { ReportsContext } from '../contexts/ReportsStore';
import ReportCard from './ReportCard';

function ReportList() {
    const [{reports, reportsLoading, reportsError}, dispatch] = useContext(ReportsContext);

    useEffect(() => {
        const getReports = async () => {
            try {
                dispatch({type: 'SET_REPORTS_LOADING'});
                const reportsData = await ReportsApi.getReports();
                dispatch({type: 'SET_REPORTS', payload: reportsData});
            } catch (error) {
                dispatch({type: 'SET_REPORTS_ERROR', payload: error?.message ?? error ?? 'Unknown'});
            } finally {
                dispatch({type: 'SET_REPORTS_DONE'})
            }
        }

        getReports();
    }, [])

    if (reportsLoading) {
        return (<Skeleton variant="rounded" width={400} height={200} />);
    }
    
    if (reportsError) {
        return (<Typography color="error.main" variant="body1"><Error sx={{mr: 1}} />Failed to fetch reports: {reportsError}</Typography>);
    }

    return (
        <Grid container spacing={1} alignItems="flex-end">
            {reports.map(report => (
                <Grid item key={report.id}>
                    <ReportCard key={report.id} report={report} />
                </Grid>
            ))}
        </Grid>
    );
    
}

// ReportList.propTypes = {
//     reports: PropTypes.arrayOf(
//         PropTypes.shape({
//             id: PropTypes.string,
//             name: PropTypes.string,
//             year: PropTypes.number,
//             reportSectorTitle: PropTypes.string,
//             reportCode: PropTypes.string,
//         })
//     ).isRequired,
// }

export default ReportList;