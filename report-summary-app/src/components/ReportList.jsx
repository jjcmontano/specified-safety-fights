import { Grid } from '@mui/material';
import PropTypes from 'prop-types';
import React from 'react';
import ReportCard from './ReportCard';

function ReportList(props) {
    const { reports } = props
    return (
        <Grid container spacing={1} alignItems="flex-end">
            {reports.map(report => (
                <Grid item>
                    <ReportCard key={report.id} report={report} />
                </Grid>
            ))}
        </Grid>
    );
}

ReportList.propTypes = {
    reports: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.string,
            name: PropTypes.string,
            year: PropTypes.number,
            reportSectorTitle: PropTypes.string,
            reportCode: PropTypes.string,
        })
    ).isRequired,
}

export default ReportList;