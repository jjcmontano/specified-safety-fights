import { Grid } from '@mui/material';
import React, { useContext } from 'react';
import { ReportsContext } from '../contexts/ReportsStore';
import ReportCard from './ReportCard';

function ReportList() {
    const [{reports}] = useContext(ReportsContext);
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