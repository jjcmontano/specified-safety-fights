import {
    Card, CardActionArea, CardContent, CardHeader, Typography
} from '@mui/material';
import PropTypes from 'prop-types';
import React from 'react';

function ReportCard(props) {
    const { report } = props
    return (
        <Card sx={(theme) => ({boxShadow: theme.shadows[2]})}>
            <CardActionArea>
                <CardHeader
                    title={report.name}
                    subheader={`Year: ${report.year}`}
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
                    <Typography variant="body1">{`${report.reportCode} - ${report.reportSectorTitle} Report`}</Typography>
                </CardContent>
            </CardActionArea>
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