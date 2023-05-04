import {
    Card, CardContent, CardHeader, Typography
} from '@mui/material';
import PropTypes from 'prop-types';
import React from 'react';

function ReportCard(props) {
    const { report } = props
    return (
        <Card sx={(theme) => ({boxShadow: theme.shadows[2]})}>
            <CardHeader
                title={report.name}
                subheader={report.year}
                titleTypographyProps={{ align: 'center' }}
                subheaderTypographyProps={{
                    align: 'center',
                }}
                sx={{
                    backgroundColor: (theme) =>
                        theme.palette.mode === 'light'
                            ? theme.palette.grey[200]
                            : theme.palette.grey[700],
                }}
            />
            <CardContent>
                <Typography variant="body1">{`${report.reportCode} ${report.reportSectorTitle}`}</Typography>
            </CardContent>
        </Card>
    );
}

ReportCard.propTypes = {
    report: PropTypes.shape({
        id: PropTypes.string,
        name: PropTypes.string,
        year: PropTypes.number,
        reportSectorTitle: PropTypes.string,
        reportCode: PropTypes.string,
    }).isRequired,
}

export default ReportCard;